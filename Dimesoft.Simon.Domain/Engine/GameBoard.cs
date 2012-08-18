using System;
using System.Collections.Generic;
using Dimesoft.Simon.Domain.Model;
using Windows.UI.Xaml;

namespace Dimesoft.Simon.Domain.Engine
{
    public class GameBoard
    {
        private DispatcherTimer _gameClockTimer;
        private DifficultyLevel _currentDifficultyLevel = DifficultyLevel.Unknown;
        private IDictionary<Player, IMoveManager> _players = new Dictionary<Player, IMoveManager>();
        private GamePlayStatus _gamePlayStatus = GamePlayStatus.Notstarted;

        public event EventHandler<int> GameClockChanged = (sender, i) => { };

        public void Initialize()
        {
            Players = new Dictionary<Player, IMoveManager>();
            RunningClockInSeconds = 0;

            _gameClockTimer = new DispatcherTimer();
            _gameClockTimer.Interval = new TimeSpan(0, 0, 0, 1);
            _gameClockTimer.Tick += GameClockTicked;
        }

        private void GameClockTicked(object sender, object e)
        {
            RunningClockInSeconds++;
            GameClockChanged(this, RunningClockInSeconds);
        }

        public MoveResult HandleMove(Player player, GameTile gameTile)
        {
            if (player == null) { throw new ArgumentNullException("Players was null"); }
            if (gameTile == GameTile.Unknown) { throw new ArgumentOutOfRangeException("GameTile was out of range"); }

            var moveManager = GetMoveManager(player);

            var moveResult = moveManager.MakeMove(gameTile);

            return moveResult;
        }

        public IList<GameTile> GetMoveList(Player player)
        {
            if (player == null) { throw new ArgumentNullException("Players was null"); }

            var moveManager = GetMoveManager(player);

            return moveManager.GetSequence();
        }

        public void SetupBoard(IList<Player> players, DifficultyLevel difficultyLevel)
        {
            if (players == null) { throw new ArgumentNullException("Players was null"); }
            if (players.Count == 0) { throw new ArgumentOutOfRangeException("Players was out of range"); }
            if (difficultyLevel == DifficultyLevel.Unknown) { throw new ArgumentOutOfRangeException("DifficultyLevel was out of range"); }

            Initialize();

            foreach (var player in players)
            {
                Players.Add(player, new MoveManager(new MoveGenerator()));
            }

            CurrentDifficultyLevel = difficultyLevel;

            _gameClockTimer.Start();
            GamePlayStatus = GamePlayStatus.BeingPlayed;
        }

        public IDictionary<Player, IMoveManager> Players
        {
            get { return _players; }
            private set { _players = value; }
        }

        public DifficultyLevel CurrentDifficultyLevel
        {
            get { return _currentDifficultyLevel; }
            private set { _currentDifficultyLevel = value; }
        }

        public GamePlayStatus GamePlayStatus
        {
            get { return _gamePlayStatus; }
            private set { _gamePlayStatus = value; }
        }

        private IMoveManager GetMoveManager(Player player)
        {
            if (player == null) { throw new ArgumentNullException("Player was null"); }

            if (!Players.ContainsKey(player)) { throw new KeyNotFoundException(string.Format("Player {0} was not found in the list", player.Id)); }

            return Players[player];
        }

        private int RunningClockInSeconds { get; set; }
    }
}
