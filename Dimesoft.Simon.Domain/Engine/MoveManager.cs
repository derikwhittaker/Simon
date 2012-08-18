using System.Collections.Generic;
using System.Diagnostics;
using Dimesoft.Simon.Domain.Model;

namespace Dimesoft.Simon.Domain.Engine
{
    public interface IMoveManager
    {
        AttemptResult MakeMove( GameTile move );
        IList<GameTile> GetSequence();
        int LastMoveIndex { get; }
        bool IsAtEndOfSequence { get; }
        void ResetSequenceCounter();
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

        public AttemptResult MakeMove( GameTile move )
        {
            var gameSequence = GameSequence[LastMoveIndex];
            
            if ( gameSequence == move )
            {
                LastMoveIndex++;

                return AttemptResult.Valid;
            }

            return AttemptResult.InValid;
        }

        public IList<GameTile> GetSequence()
        {
            var nextMove = _moveGenerator.Generate();

            AddMove(nextMove);

            Debug.WriteLine(nextMove.ToString());

            return GameSequence;
        }

        public bool IsAtEndOfSequence
        {
            get { return LastMoveIndex == GameSequence.Count; }
        }

        public void ResetSequenceCounter()
        {
            LastMoveIndex = 0;
        }

        public IList<GameTile> GameSequence { get; private set; }
        public int LastMoveIndex { get; private set; }

    }
}