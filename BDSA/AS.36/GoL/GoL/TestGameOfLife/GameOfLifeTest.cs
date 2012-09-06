using GoL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestGameOfLife
{
    
    
    /// <summary>
    ///This is a test class for GameOfLifeTest and is intended
    ///to contain all GameOfLifeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GameOfLifeTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GameOfLife Constructor
        ///</summary>
        [TestMethod()]
        public void GameOfLifeConstructorTest()
        {
            int size = 0; // TODO: Initialize to an appropriate value
            GameOfLife target = new GameOfLife(size);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for ChangeCellState
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GoL.exe")]
        public void ChangeCellStateTest()
        {
            // Private Accessor for ChangeCellState is not found. Please rebuild the containing project or run the Publicize.exe manually.
            Assert.Inconclusive("Private Accessor for ChangeCellState is not found. Please rebuild the containing " +
                    "project or run the Publicize.exe manually.");
        }

        /// <summary>
        ///A test for ChangeCellStates
        ///</summary>
        [TestMethod()]
        public void ChangeCellStatesTest()
        {
            int size = 0; // TODO: Initialize to an appropriate value
            GameOfLife target = new GameOfLife(size); // TODO: Initialize to an appropriate value
            CellUpdate[] cellUpdates = null; // TODO: Initialize to an appropriate value
            target.ChangeCellStates(cellUpdates);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GenerateState
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GoL.exe")]
        public void GenerateStateTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GameOfLife_Accessor target = new GameOfLife_Accessor(param0); // TODO: Initialize to an appropriate value
            target.GenerateState();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetNewCellState
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GoL.exe")]
        public void GetNewCellStateTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GameOfLife_Accessor target = new GameOfLife_Accessor(param0); // TODO: Initialize to an appropriate value
            int x = 0; // TODO: Initialize to an appropriate value
            int y = 0; // TODO: Initialize to an appropriate value
            CellUpdate_Accessor expected = null; // TODO: Initialize to an appropriate value
            CellUpdate_Accessor actual;
            actual = target.GetNewCellState(x, y);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Main
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GoL.exe")]
        public void MainTest()
        {
            string[] args = null; // TODO: Initialize to an appropriate value
            GameOfLife_Accessor.Main(args);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for NextDay
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GoL.exe")]
        public void NextDayTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GameOfLife_Accessor target = new GameOfLife_Accessor(param0); // TODO: Initialize to an appropriate value
            target.NextDay();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PrintGrid
        ///</summary>
        [TestMethod()]
        public void PrintGridTest()
        {
            int size = 0; // TODO: Initialize to an appropriate value
            GameOfLife target = new GameOfLife(size); // TODO: Initialize to an appropriate value
            target.PrintGrid();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Run
        ///</summary>
        [TestMethod()]
        public void RunTest()
        {
            int size = 0; // TODO: Initialize to an appropriate value
            GameOfLife target = new GameOfLife(size); // TODO: Initialize to an appropriate value
            target.Run();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
