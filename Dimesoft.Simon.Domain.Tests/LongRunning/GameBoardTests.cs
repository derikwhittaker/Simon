using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimesoft.Simon.Domain.Engine;
using Dimesoft.Simon.Domain.Model;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Dimesoft.Simon.Domain.Tests.LongRunning
{
    [TestClass]
    public class GameBoardTests
    {
        [TestMethod]
        public void FullSinglerPlayerGame_FailingOnThirdLevel()
        {
            var gameBoard = new GameBoard();
            var player = new Player {Id = 1, Name = "Derik"};

            gameBoard.SetupBoard(new List<Player>{player}, DifficultyLevel.Easy);

            // setup the board for the first move
            var moveList = gameBoard.GetMoveList(player);

            var moveResult = gameBoard.HandleMove(player, moveList.Last());
            Assert.AreEqual(MoveResult.Valid, moveResult);

            // setup for next move
            moveList = gameBoard.GetMoveList(player);

            moveResult = gameBoard.HandleMove(player, moveList.Last());
            Assert.AreEqual(MoveResult.Valid, moveResult);

            // setup for next move
            moveList = gameBoard.GetMoveList(player);

            moveResult = gameBoard.HandleMove(player, moveList.Last());
            Assert.AreEqual(MoveResult.Valid, moveResult);

        }
    }
}
