using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimesoft.Simon.Domain.Engine;
using Dimesoft.Simon.Domain.Managers;
using Dimesoft.Simon.Domain.Model;
using GalaSoft.MvvmLight.Command;

namespace Dimesoft.Simon.Client.ViewModel
{
    public class GameBoardViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private GameBoard _gameBoardEngine;
        private AudioManager _audioManager = new AudioManager();

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
            get { return _bottomRightButtonPressedCommand ?? (_bottomRightButtonPressedCommand = new RelayCommand(BottomRightButtonPressed)); }
        }

        private async void BottomRightButtonPressed()
        {
            await _audioManager.Play("BottomRightButton.mp3");
        }

        public RelayCommand BottomLeftButtonPressedCommand
        {
            get { return _bottomLeftButtonPressedCommand ?? (_bottomLeftButtonPressedCommand = new RelayCommand(BottomLeftButtonPressed)) ; }
        }

        private async void BottomLeftButtonPressed()
        {
            await _audioManager.Play("BottomLeftButton.mp3");
        }

        public RelayCommand TopLeftButtonPressedCommand
        {
            get { return _topLeftButtonPressedCommand ?? (_topLeftButtonPressedCommand = new RelayCommand(TopLeftButtonPressed)); }
        }

        private async void TopLeftButtonPressed()
        {
            await _audioManager.Play("TopLeftButton.mp3");
        }

        public RelayCommand TopRightButtonPressedCommand
        {
            get { return _topRightButtonPressedCommand ?? (_topRightButtonPressedCommand = new RelayCommand(TopRightButtonPressed)); }
        }

        private async void TopRightButtonPressed()
        {
            await _audioManager.Play("TopRightButton.mp3");
        }

        public RelayCommand StartNewGameCommand
        {
            get { return _startNewGameCommand ?? (_startNewGameCommand = new RelayCommand(StartNewGame)); }
        }

        private void StartNewGame()
        {
            // hard coded to one player
            Player = new Player { Id = 1, Name = "Captain Simon" };

            _gameBoardEngine.SetupBoard(new List<Player> { Player }, DifficultyLevel.Easy);

            var moveList = _gameBoardEngine.GetMoveList(Player);
        }

        #endregion

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
