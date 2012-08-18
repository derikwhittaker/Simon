using System;
using System.Collections.Generic;
using System.Diagnostics;
using Dimesoft.Simon.Domain.Model;

namespace Dimesoft.Simon.Domain.Engine
{
    public interface IMoveGenerator
    {
        GameTile Generate();
    }

    public class MoveGenerator : IMoveGenerator
    {
        private Random _random = new Random(3);
        public const int MaxMovesToGenerate = 400;
        private IList<GameTile> _possibleMoves;
        
        public MoveGenerator()
        {
            SetupGenerator();
        }

        private void SetupGenerator()
        {
            
            var seedList = new GameTile[4] { GameTile.BottomLeft, GameTile.TopRight, GameTile.BottomRight, GameTile.TopLeft };
            PossibleMoves = new List<GameTile>();

            for (var i = 0; i < MaxMovesToGenerate; i++)
            {
                var index = _random.Next(4);
                var tileType = seedList[index];

                PossibleMoves.Add(tileType);
            }
        }

        public GameTile Generate()
        {
            var index = _random.Next(PossibleMoves.Count);
            var gameTile = PossibleMoves[index];

            Debug.WriteLine("Index {0} Tile {1}", index, gameTile);

            return gameTile;
        }

        public IList<GameTile> PossibleMoves
        {
            get { return _possibleMoves; }
            private set { _possibleMoves = value; }
        }
    }
}