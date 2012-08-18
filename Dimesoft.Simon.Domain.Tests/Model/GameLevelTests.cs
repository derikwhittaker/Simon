using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimesoft.Simon.Domain.Model;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Dimesoft.Simon.Domain.Tests.Model
{
    [TestClass]
    public class GameLevelTests
    {
        [TestMethod] 
        public void Reset_WillSetCorrectDefaultValues()
        {
            var gameLevel = new GameLevel();

            gameLevel.Moves = 10;
            gameLevel.Level = 10;

            gameLevel.Reset();

            Assert.AreEqual(0, gameLevel.Moves);
            Assert.AreEqual(0, gameLevel.Level);
        }

        [TestMethod]
        public void IsReset_WhenCorrectValues_WillReturnTrue()
        {
            var gameLevel = new GameLevel();

            gameLevel.Moves = 0;
            gameLevel.Level = 0;

            Assert.IsTrue(gameLevel.IsReset);
        }

        [TestMethod]
        public void IsReset_WhenLevelIsNotCorrect_WillReturnFalse()
        {
            var gameLevel = new GameLevel();

            gameLevel.Moves = 0;
            gameLevel.Level = 10;

            Assert.IsFalse(gameLevel.IsReset);
        }


        [TestMethod]
        public void IsReset_WhenMovesIsNotCorrect_WillReturnFalse()
        {
            var gameLevel = new GameLevel();

            gameLevel.Moves = 10;
            gameLevel.Level = 0;

            Assert.IsFalse(gameLevel.IsReset);
        }
    }
}
