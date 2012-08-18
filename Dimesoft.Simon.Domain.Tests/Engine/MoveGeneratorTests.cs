using Dimesoft.Simon.Domain.Engine;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Dimesoft.Simon.Domain.Tests.Engine
{
    [TestClass]
    public class MoveGeneratorTests
    {
        [TestMethod]
        public void Ctor_WillSetupPossibleMoves()
        {
            var moveGenerator = new MoveGenerator();

            Assert.AreEqual(MoveGenerator.MaxMovesToGenerate, moveGenerator.PossibleMoves.Count);
        }
        
    }
}