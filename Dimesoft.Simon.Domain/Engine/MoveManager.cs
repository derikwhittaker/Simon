using System.Collections.Generic;
using Dimesoft.Simon.Domain.Model;

namespace Dimesoft.Simon.Domain.Engine
{
    public class MoveManager
    {
        public MoveManager()
        {
            GameSequence = new List<GameTile>();
            LastMoveIndex = 0;
        }

        public void AddMove( GameTile move )
        {
            GameSequence.Add(move);
        }

        public MoveResult MakeMove( GameTile move )
        {
            var gameSequence = GameSequence[LastMoveIndex];

            LastMoveIndex++;

            if ( gameSequence == move )
            {
                return MoveResult.Valid;
            }

            return MoveResult.InValid;
        }

        public IList<GameTile> GameSequence { get; private set; }
        public int LastMoveIndex { get; private set; }

    }
}