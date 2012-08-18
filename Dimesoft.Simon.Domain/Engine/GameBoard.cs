﻿using System;
using System.Collections.Generic;
using Dimesoft.Simon.Domain.Model;

namespace Dimesoft.Simon.Domain.Engine
{
    public class GameBoard
    {
        private GameLevel _currentGameLevel = new GameLevel() ;
        private DifficultyLevel _currentDifficultyLevel = DifficultyLevel.Unknown;
        private IDictionary<Player, IMoveManager> _players = new Dictionary<Player, IMoveManager>();

        public GameBoard()
        {
            
        }

        public void Initialize()
        {
            CurrentGameLevel.Reset();
            Players = new Dictionary<Player, IMoveManager>();
        }

        public MoveResult HandleMove(Player player, GameTile gameTile )
        {
            if (player == null) { throw new ArgumentNullException("Players was null"); }
            if (gameTile == GameTile.Unknown) { throw new ArgumentOutOfRangeException("GameTile was out of range"); }

            var moveManager = GetMoveManager(player);

            return moveManager.MakeMove(gameTile);
        }

        public IList<GameTile> GetMoveList(Player player)
        {
            if (player == null) { throw new ArgumentNullException("Players was null"); }

            var moveManager = GetMoveManager(player);

            return null;
        }

        public void SetupBoard(IList<Player> players, DifficultyLevel difficultyLevel )
        {
            if (players == null) { throw new ArgumentNullException("Players was null"); }
            if (players.Count == 0) { throw new ArgumentOutOfRangeException("Players was out of range"); }
            if (difficultyLevel == DifficultyLevel.Unknown) { throw new ArgumentOutOfRangeException("DifficultyLevel was out of range"); }

            Initialize();

            foreach (var player in players)
            {
                Players.Add(player, new MoveManager( new MoveGenerator() ));
            }

            CurrentDifficultyLevel = difficultyLevel;
        }

        public IDictionary<Player, IMoveManager> Players
        {
            get { return _players; }
            private set { _players = value; }
        }

        public GameLevel CurrentGameLevel
        {
            get { return _currentGameLevel; }
            private set { _currentGameLevel = value; }
        }

        public DifficultyLevel CurrentDifficultyLevel
        {
            get { return _currentDifficultyLevel; }
            private set { _currentDifficultyLevel = value; }
        }

        private IMoveManager GetMoveManager(Player player)
        {
            if (player == null) { throw new ArgumentNullException("Player was null"); }

            if (!Players.ContainsKey(player)) { throw new KeyNotFoundException(string.Format("Player {0} was not found in the list", player.Id)); }

            return Players[player];
        }
    }
}
