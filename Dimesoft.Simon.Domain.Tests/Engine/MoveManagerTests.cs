using Dimesoft.Simon.Domain.Engine;
using Dimesoft.Simon.Domain.Model;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Dimesoft.Simon.Domain.Tests.Engine
{
    [TestClass]
    public class MoveManagerTests
    {

        [TestMethod]
        public void LastMoveIndex_WhenMoveMade_WillIncrementIndex()
        {
            var moveManager = new MoveManager();

            var originalIndex = moveManager.LastMoveIndex;

            moveManager.AddMove(GameTile.TopLeft);

            moveManager.MakeMove(GameTile.TopRight);

            var updatedIndex = moveManager.LastMoveIndex;

            Assert.AreNotEqual(originalIndex, updatedIndex);
        }

        [TestMethod]
        public void AddSequence_WillAddItemToList()
        {
            var moveManager = new MoveManager();

            moveManager.AddMove(GameTile.BottomRight);

            Assert.AreEqual(1, moveManager.GameSequence.Count);
        }

        [TestMethod]
        public void MakeMove_WhenMoveIsValid_WillReturnCorrectResult()
        {
            var moveManager = new MoveManager();

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
            var moveManager = new MoveManager();

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
            var moveManager = new MoveManager();

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
            var moveManager = new MoveManager();

            moveManager.AddMove(GameTile.BottomRight);
            moveManager.AddMove(GameTile.BottomRight);
            moveManager.AddMove(GameTile.BottomLeft);

            var result1 = moveManager.MakeMove(GameTile.BottomRight);
            var result2 = moveManager.MakeMove(GameTile.TopLeft);
            var result3 = moveManager.MakeMove(GameTile.BottomLeft);

            Assert.AreEqual(MoveResult.Valid, result1);
            Assert.AreEqual(MoveResult.InValid, result2);
            Assert.AreEqual(MoveResult.Valid, result3);
        }
    }
}