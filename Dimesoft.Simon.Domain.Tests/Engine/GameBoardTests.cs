using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dimesoft.Simon.Domain.Engine;
using Dimesoft.Simon.Domain.Model;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Dimesoft.Simon.Domain.Tests.Engine
{
    [TestClass]
    public class GameBoardTests
    {
        [TestMethod]
        public void Initialize_WillResetGameBoardAsExpected()
        {
            var gameBoard = new GameBoard();

            gameBoard.Initialize();

            Assert.AreEqual(0, gameBoard.Players.Count);
        }

        [TestMethod]
        public void HandleMove_WhenPlayerNotInList_WillThrowExeption()
        {
            var player = new Player {Id = 1};
            var badPlayer = new Player { Id = 2 };
            var gameBoard = new GameBoard();
            gameBoard.SetupBoard(new List<Player>{player}, DifficultyLevel.Medium);

            try
            {
                gameBoard.HandleMove(badPlayer, GameTile.BottomRight);
            }
            catch (Exception e)
            {
                // all good
                return;
            }

            Assert.Fail("Should have had an exception thrown");
        }

        [TestMethod]
        public void HandleMove_WhenMoveValid_WillReturnCorrectResult()
        {
            var player = new Player { Id = 1 };
            var gameBoard = new GameBoard();

            gameBoard.SetupBoard(new List<Player> { player }, DifficultyLevel.Medium);
            var moveList = gameBoard.GetMoveList(player);

            var moveResult = gameBoard.HandleMove(player, moveList.Last());


            Assert.AreEqual(MoveResult.Valid, moveResult);
        }


        [TestMethod]
        public void SetupBoard_WhenOnePlayer_WillSetupBoardCorrectly()
        {
            var gameBoard = new GameBoard();
            gameBoard.SetupBoard(new List<Player>{new Player{Id = 1}}, DifficultyLevel.Easy);

            Assert.AreEqual(1, gameBoard.Players.Count);
        }

        [TestMethod]
        public void SetupBoard_WhenMultiPlayer_WillSetupBoardCorrectly()
        {
            var gameBoard = new GameBoard();
            gameBoard.SetupBoard(new List<Player>
                                     {
                                         new Player { Id = 1 },
                                         new Player { Id = 2 }
                                     }, DifficultyLevel.Easy);

            Assert.AreEqual(2, gameBoard.Players.Count);
        }

        [TestMethod]
        public void SetupBoard_WillSetThe_DifficultyLevelCorrectly()
        {
            var gameBoard = new GameBoard();
            gameBoard.SetupBoard(new List<Player>
                                     {
                                         new Player { Id = 1 },
                                         new Player { Id = 2 }
                                     }, DifficultyLevel.Easy);

            Assert.AreEqual(DifficultyLevel.Easy, gameBoard.CurrentDifficultyLevel);
        }
    }
}
