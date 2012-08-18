using System.Collections.Generic;
using Dimesoft.Simon.Domain.Model;

namespace Dimesoft.Simon.Domain.Engine
{
    public interface IMoveManager
    {
        MoveResult MakeMove( GameTile move );
        IList<GameTile> GetSequence();
        int LastMoveIndex { get; }
    }

    public class MoveManager : IMoveManager
    {
        private readonly IMoveGenerator _moveGenerator;

        public MoveManager( IMoveGenerator moveGenerator )
        {
            _moveGenerator = moveGenerator;
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

        public IList<GameTile> GetSequence()
        {
            var nextMove = _moveGenerator.Generate();

            AddMove(nextMove);

            return GameSequence;
        }

        public IList<GameTile> GameSequence { get; private set; }
        public int LastMoveIndex { get; private set; }

    }
}