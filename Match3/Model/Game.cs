using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Match3.Model
{
    public sealed class Game : INotifyPropertyChanged
    {
        private List<Tile> _lastMatches = new List<Tile>();

        private readonly Tile[,] _board = new Tile[16, 8];

        private readonly Color[] _colors = { Colors.Red, Colors.Green, Colors.Blue, Colors.LightYellow, Colors.RosyBrown };

        private int _pointsCount;
        public int PointsCount
        {
            get => _pointsCount;

            set
            {
                _pointsCount = value;

                OnPropertyChanged();
            }
        }

        private int _countdownValue;
        public int CountdownValue
        {
            get => _countdownValue;

            set
            {
                _countdownValue = value;

                OnPropertyChanged();
            }
        }

        public Game(Action<Tile> registerTile, Action<Tile> unregisterTile, Action<Tile> dropAnimation, int gameTime)
        {
            FillBoard(registerTile);
            DeleteAndDropTiles(dropAnimation, registerTile, unregisterTile);

            _countdownValue = gameTime * 60;

            DispatcherTimer gameTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal,
                delegate
                {
                    CountdownValue -= 1;
                    if (CountdownValue == 0)
                    {
                        Switcher.Switch(new GameOver(PointsCount));
                    }
                }, Application.Current.Dispatcher);
        }

        public void RemoveMatches(Action<Tile> deleteAnimation)
        {
            _lastMatches = CheckMatches();
            PointsCount += _lastMatches.Count;

            foreach (Tile match in _lastMatches)
            {
                deleteAnimation(match);
            }
        }

        public void FillBoard(Action<Tile> registerTileCallback)
        {
            Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (_board[i, j] != null)
                    {
                        continue;
                    }

                    _board[i, j] = new Tile(i - 8, j, _colors[random.Next(_colors.Length)]);
                    registerTileCallback(_board[i, j]);
                }
            }
        }

        public void TrySwapTiles(Tile firstTile, Tile secondTile, Action<Tile, Tile> successAnimCallback, Action<Tile, Tile> failAnimCallback)
        {
            if (Math.Abs(firstTile.Top - secondTile.Top) + Math.Abs(firstTile.Left - secondTile.Left) > 1)
            {
                return;
            }

            Utility.SwapTiles(
                ref _board[firstTile.Top + 8, firstTile.Left],
                ref _board[secondTile.Top + 8, secondTile.Left]);

            _lastMatches = CheckMatches();

            if (_lastMatches.Count > 0)
            {
                firstTile.SwapCoordinates(ref secondTile);
                successAnimCallback(firstTile, secondTile);
            }
            else
            {
                Utility.SwapTiles(
                    ref _board[firstTile.Top + 8, firstTile.Left],
                    ref _board[secondTile.Top + 8, secondTile.Left]);

                failAnimCallback(firstTile, secondTile);
            }
        }

        private void DeleteMatches(Action<Tile> unregisterTile)
        {
            foreach (Tile match in _lastMatches)
            {
                unregisterTile(_board[match.Top + 8, match.Left]);
                _board[match.Top + 8, match.Left] = null;
            }
        }

        public void DeleteAndDropTiles(Action<Tile> tileDropAnimation, Action<Tile> registerTile, Action<Tile> unregisterTile)
        {
            DeleteMatches(unregisterTile);

            int[] dropLengths = new int[8];

            for (int i = 16 - 1; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (_board[i, j] == null)
                    {
                        dropLengths[j]++;
                    }
                    else if (dropLengths[j] != 0)
                    {
                        if (_board[i + dropLengths[j], j] != null)
                        {
                            throw new InvalidOperationException("Упавшая плитка не может быть равна Null!");
                        }

                        Utility.SwapTiles(ref _board[i, j], ref _board[i + dropLengths[j], j]);
                        _board[i + dropLengths[j], j].Top = i + dropLengths[j] - 8;
                        tileDropAnimation(_board[i + dropLengths[j], j]);
                    }
                }
            }

            FillBoard(registerTile);
        }

        private List<Tile> CheckMatches()
        {
            bool[,] delete = new bool[16, 8];
            for (int i = 8; i < 16; i++)
            {
                int matches = 1;
                Color color = _board[i, 0].Color;
                for (int j = 1; j < 8; j++)
                {
                    if (_board[i, j].Color == color)
                    {
                        ++matches;
                    }
                    else
                    {
                        if (matches >= 3)
                        {
                            for (int k = 1; k < matches + 1; k++)
                            {
                                delete[i, j - k] = true;
                            }
                        }

                        color = _board[i, j].Color;
                        matches = 1;
                    }
                }

                if (matches < 3)
                {
                    continue;
                }

                for (int k = 1; k < matches + 1; k++)
                {
                    delete[i, 8 - k] = true;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                int matches = 1;
                Color color = _board[8, i].Color;
                for (int j = 9; j < 16; j++)
                {
                    if (_board[j, i].Color == color)
                    {
                        ++matches;
                    }
                    else
                    {
                        if (matches >= 3)
                        {
                            for (int k = 1; k < matches + 1; k++)
                            {
                                delete[j - k, i] = true;
                            }
                        }

                        color = _board[j, i].Color;
                        matches = 1;
                    }
                }

                if (matches < 3)
                {
                    continue;
                }

                for (int k = 1; k < matches + 1; k++)
                {
                    delete[16 - k, i] = true;
                }
            }

            List<Tile> result = new List<Tile>();
            for (int i = 8; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (delete[i, j])
                    {
                        result.Add(_board[i, j]);
                    }
                }
            }

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
