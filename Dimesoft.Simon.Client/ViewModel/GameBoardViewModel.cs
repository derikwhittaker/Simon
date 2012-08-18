using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Dimesoft.Simon.Client.View;
using Dimesoft.Simon.Domain.Managers;
using Dimesoft.Simon.Domain.Model;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml.Controls.Primitives;
using GameBoard = Dimesoft.Simon.Domain.Engine.GameBoard;

namespace Dimesoft.Simon.Client.ViewModel
{
    public class GameBoardViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private GameBoard _gameBoardEngine;
        private AudioManager _audioManager = new AudioManager();
        private Popup _userMessagePopup;

        private RelayCommand _startNewGameCommand;
        private string _gameDuration;
        private RelayCommand _topLeftButtonPressedCommand;
        private RelayCommand _topRightButtonPressedCommand;
        private RelayCommand _bottomRightButtonPressedCommand;
        private bool _topLeftIsLit;
        private bool _topRightIsLit;
        private bool _bottomRightIsLit;
        private bool _bottomLeftIsLit;
        private RelayCommand _bottomLeftButtonPressedCommand;

        public Player Player { get; set; }

        public GameBoardViewModel()
        {
            _gameBoardEngine = new GameBoard();
            _gameBoardEngine.GameClockChanged += (s, i) => { GameDuration = new TimeSpan(0, 0, 0, i).ToString("g"); };
        }

        #region Commands

        public RelayCommand BottomRightButtonPressedCommand
        {
            get { return _bottomRightButtonPressedCommand ?? (_bottomRightButtonPressedCommand = new RelayCommand(BottomRightButtonPressed, CanBottomRightButtonPressed)); }
        }

        private bool CanBottomRightButtonPressed()
        {
            return _gameBoardEngine.GamePlayStatus == GamePlayStatus.BeingPlayed;
        }

        private async void BottomRightButtonPressed()
        {
            await HandleButtonPressedAsync(Player, GameTile.BottomRight, "BottomRightButton.mp3");
        }
        
        public RelayCommand BottomLeftButtonPressedCommand
        {
            get { return _bottomLeftButtonPressedCommand ?? (_bottomLeftButtonPressedCommand = new RelayCommand(BottomLeftButtonPressed, CanBottomLeftButtonPressed)); }
        }

        private bool CanBottomLeftButtonPressed()
        {
            return _gameBoardEngine.GamePlayStatus == GamePlayStatus.BeingPlayed;
        }

        private async void BottomLeftButtonPressed()
        {
            await HandleButtonPressedAsync(Player, GameTile.BottomLeft, "BottomLeftButton.mp3");
        }

        public RelayCommand TopLeftButtonPressedCommand
        {
            get { return _topLeftButtonPressedCommand ?? (_topLeftButtonPressedCommand = new RelayCommand(TopLeftButtonPressed, CanTopLeftButtonPressed)); }
        }

        private bool CanTopLeftButtonPressed()
        {
            return _gameBoardEngine.GamePlayStatus == GamePlayStatus.BeingPlayed;
        }

        private async void TopLeftButtonPressed()
        {
            await HandleButtonPressedAsync(Player, GameTile.TopLeft, "TopLeftButton.mp3");
        }

        public RelayCommand TopRightButtonPressedCommand
        {
            get { return _topRightButtonPressedCommand ?? (_topRightButtonPressedCommand = new RelayCommand(TopRightButtonPressed, CanTopRightButtonPressed)); }
        }

        private bool CanTopRightButtonPressed()
        {
            return _gameBoardEngine.GamePlayStatus == GamePlayStatus.BeingPlayed;
        }

        private async void TopRightButtonPressed()
        {
            await HandleButtonPressedAsync(Player, GameTile.TopRight, "TopRightButton.mp3");
        }

        public RelayCommand StartNewGameCommand
        {
            get { return _startNewGameCommand ?? (_startNewGameCommand = new RelayCommand(StartNewGame)); }
        }

        private async void StartNewGame()
        {
            // hard coded to one player
            Player = new Player { Id = 1, Name = "Captain Simon" };

            _gameBoardEngine.SetupBoard(new List<Player> { Player }, DifficultyLevel.Easy);

            await ShowPlayerSequence();

            //
            TopLeftButtonPressedCommand.RaiseCanExecuteChanged();
            TopRightButtonPressedCommand.RaiseCanExecuteChanged();
            BottomLeftButtonPressedCommand.RaiseCanExecuteChanged();
            BottomRightButtonPressedCommand.RaiseCanExecuteChanged();

        }

        private async Task ShowPlayerSequence()
        {
            var moveList = _gameBoardEngine.GetMoveList(Player);

            foreach (var move in moveList)
            {
                BottomLeftIsLit = false;
                BottomRightIsLit = false;
                TopLeftIsLit = false;
                TopRightIsLit = false;

                switch (move)
                {
                    case GameTile.BottomRight:
                        BottomRightIsLit = true;
                        break;

                    case GameTile.BottomLeft:
                        BottomLeftIsLit = true;
                        break;

                    case GameTile.TopRight:
                        TopRightIsLit = true;
                        break;

                    case GameTile.TopLeft:
                        TopLeftIsLit = true;
                        break;
                }

                Debug.WriteLine("Ticks {0}", DateTime.Now.Ticks);
                Task.Delay(200);
            }
        }

        #endregion

        private async Task HandleButtonPressedAsync(Player player, GameTile gameTilePressed, string successAudioFileName)
        {
            var result = _gameBoardEngine.HandleMove(player, gameTilePressed);

            if (result.AttemptResult == AttemptResult.Valid)
            {
                await _audioManager.Play(successAudioFileName);

                if ( result.IsAtEndOfSequence )
                {
                    _gameBoardEngine.ResetSequenceCounter(Player);
                    ShowPlayerSequence();
                }
            }
            else
            {
                await _audioManager.Play("Buzzer.mp3");
                
            }
        }


        public string GameDuration
        {
            get { return _gameDuration; }
            set
            {
                _gameDuration = value;
                 RaisePropertyChanged(() => GameDuration);
            }
        }

        public bool TopLeftIsLit
        {
            get { return _topLeftIsLit; }
            set
            {
                _topLeftIsLit = value;
                RaisePropertyChanged(() => TopLeftIsLit);
            }
        }

        public bool TopRightIsLit
        {
            get { return _topRightIsLit; }
            set
            {
                _topRightIsLit = value;
                RaisePropertyChanged(() => TopRightIsLit);
            }
        }

        public bool BottomRightIsLit
        {
            get { return _bottomRightIsLit; }
            set
            {
                _bottomRightIsLit = value;
                RaisePropertyChanged(() => BottomRightIsLit);
            }
        }

        public bool BottomLeftIsLit
        {
            get { return _bottomLeftIsLit; }
            set
            {
                _bottomLeftIsLit = value;
                RaisePropertyChanged(() => BottomLeftIsLit);
            }
        }
    }

}
