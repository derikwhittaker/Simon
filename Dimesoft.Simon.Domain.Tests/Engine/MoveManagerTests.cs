using Dimesoft.Simon.Domain.Engine;
using Dimesoft.Simon.Domain.Model;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Dimesoft.Simon.Domain.Tests.Engine
{
    [TestClass]
    public class MoveManagerTests
    {

        [TestMethod]
        public void LastMoveIndex_WhenMoveMade_WhenValid_WillIncrementIndex()
        {
            var moveManager = new MoveManager(new MoveGenerator());

            var originalIndex = moveManager.LastMoveIndex;

            moveManager.AddMove(GameTile.TopLeft);

            moveManager.MakeMove(GameTile.TopLeft);

            var updatedIndex = moveManager.LastMoveIndex;

            Assert.AreNotEqual(originalIndex, updatedIndex);
        }

        [TestMethod]
        public void LastMoveIndex_WhenMoveMade_WhenInValid_WillNotIncrementIndex()
        {
            var moveManager = new MoveManager(new MoveGenerator());

            var originalIndex = moveManager.LastMoveIndex;

            moveManager.AddMove(GameTile.TopLeft);

            moveManager.MakeMove(GameTile.TopRight);

            var updatedIndex = moveManager.LastMoveIndex;

            Assert.AreEqual(originalIndex, updatedIndex);
        }

        [TestMethod]
        public void AddSequence_WillAddItemToList()
        {
            var moveManager = new MoveManager(new MoveGenerator());

            moveManager.AddMove(GameTile.BottomRight);

            Assert.AreEqual(1, moveManager.GameSequence.Count);
        }

        [TestMethod]
        public void MakeMove_WhenMoveIsValid_WillReturnCorrectResult()
        {
            var moveManager = new MoveManager(new MoveGenerator());

            moveManager.AddMove(GameTile.BottomRight);
            moveManager.AddMove(GameTile.BottomRight);
            moveManager.AddMove(GameTile.BottomLeft);

            var expectedResult = MoveResult.Valid;
            var result = moveManager.MakeMove(GameTile.BottomRight);

            Assert.AreEqual(expectedResult, result);
        }
        
        [TestMethod]
        public void MakeMove_WhenMoveIsNotValid_WillReturnCorrectResult()
        {
            var moveManager = new MoveManager(new MoveGenerator());

            moveManager.AddMove(GameTile.BottomRight);
            moveManager.AddMove(GameTile.BottomRight);
            moveManager.AddMove(GameTile.BottomLeft);

            var expectedResult = MoveResult.InValid;
            var result = moveManager.MakeMove(GameTile.BottomLeft);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void MakeMove_WhenMultipleMoves_WillAllBeValud()
        {
            var moveManager = new MoveManager(new MoveGenerator());

            moveManager.AddMove(GameTile.BottomRight);
            moveManager.AddMove(GameTile.BottomRight);
            moveManager.AddMove(GameTile.BottomLeft);

            var result1 = moveManager.MakeMove(GameTile.BottomRight);
            var result2 = moveManager.MakeMove(GameTile.BottomRight);
            var result3 = moveManager.MakeMove(GameTile.BottomLeft);

            Assert.AreEqual(MoveResult.Valid, result1);
            Assert.AreEqual(MoveResult.Valid, result2);
            Assert.AreEqual(MoveResult.Valid, result3);
        }

        [TestMethod]
        public void MakeMove_WhenMultipleMoves_MiddleWillBeInvalid()
        {
            var moveManager = new MoveManager(new MoveGenerator());

            moveManager.AddMove(GameTile.BottomRight);
            moveManager.AddMove(GameTile.BottomRight);
            moveManager.AddMove(GameTile.BottomLeft);

            var result1 = moveManager.MakeMove(GameTile.BottomRight);
            var result2 = moveManager.MakeMove(GameTile.TopLeft);
            var result3 = moveManager.MakeMove(GameTile.BottomLeft);

            Assert.AreEqual(MoveResult.Valid, result1);
            Assert.AreEqual(MoveResult.InValid, result2);
        }

        [TestMethod]
        public void GetSequence_WillAddNewItemToList()
        {
            var moveManager = new MoveManager(new MoveGenerator());

            var sequence = moveManager.GetSequence();

            Assert.AreEqual(1, sequence.Count);
        }


        [TestMethod]
        public void GetSequence_WhenCalledMultipleTimes_WillAddNewItemsToList()
        {
            var moveManager = new MoveManager(new MoveGenerator());

            var sequence1 = moveManager.GetSequence();
            Assert.AreEqual(1, sequence1.Count);

            var sequence2 = moveManager.GetSequence();
            Assert.AreEqual(2, sequence2.Count);

            var sequence3 = moveManager.GetSequence();
            Assert.AreEqual(3, sequence3.Count);

            var sequence4 = moveManager.GetSequence();
            Assert.AreEqual(4, sequence4.Count);
        }
    }
}