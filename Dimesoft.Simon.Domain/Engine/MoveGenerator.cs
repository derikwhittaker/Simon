using System;
using System.Collections.Generic;
using System.Diagnostics;
using Dimesoft.Simon.Domain.Model;

namespace Dimesoft.Simon.Domain.Engine
{
    public class MoveGenerator
    {
        public const int MaxMovesToGenerate = 400;
        private IList<GameTile> _possibleMoves;
        
        public MoveGenerator()
        {
            SetupGenerator();
        }

        private void SetupGenerator()
        {
            var random = new Random(3);
            var seedList = new GameTile[4] { GameTile.BottomLeft, GameTile.TopRight, GameTile.BottomRight, GameTile.TopLeft };
            PossibleMoves = new List<GameTile>();

            for (var i = 0; i < MaxMovesToGenerate; i++)
            {
                var index = random.Next(4);
                var tileType = seedList[index];

                PossibleMoves.Add(tileType);
                Debug.WriteLine(tileType);
            }
        }

        public GameTile Generate()
        {
            var random = new Random(100);
            var index = random.Next(0, PossibleMoves.Count);
            var gameTile = PossibleMoves[index];

            return gameTile;
        }

        public IList<GameTile> PossibleMoves
        {
            get { return _possibleMoves; }
            private set { _possibleMoves = value; }
        }
    }
}