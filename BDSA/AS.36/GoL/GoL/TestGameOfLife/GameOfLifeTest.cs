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
            int size = 5;
            GameOfLife target = new GameOfLife(size);
            Assert.AreEqual(size, target.GetGrid().GetLength(0), "Dimension 0 is not correct, got " + target.GetGrid().GetLength(0));
            Assert.AreEqual(size, target.GetGrid().GetLength(1), "Dimension 1 is not correct, got " + target.GetGrid().GetLength(1));
            Assert.AreEqual(size * size, target.GetGrid().Length, "Cell count is not correct, got " + target.GetGrid().Length); // No extra dimensions, please!
        }

        /// <summary>
        ///A test for ChangeCellStates
        ///</summary>
        [TestMethod()]
        public void ChangeCellStatesTest()
        {
            int size = 5;
            GameOfLife target = new GameOfLife(size);
            CellUpdate[] cellUpdates = new CellUpdate[size * size];
            int c = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (c < 5)
                    {
                        cellUpdates[c] = new CellUpdate(i, j, 1);
                    }
                    else if (c < 15)
                    {
                        cellUpdates[c] = new CellUpdate(i, j, 0);
                    }
                    else
                    {
                        cellUpdates[c] = new CellUpdate(i, j, null);
                    }
                    c++;
                }
            }
            target.ChangeCellStates(cellUpdates);

            int?[,] Grid = target.GetGrid();
            c = 0; // Reset count
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (c < 5)
                        Assert.AreEqual(1, Grid[i, j], "Failed at " + i + "," + j);
                    else if (c < 15)
                        Assert.AreEqual(0, Grid[i, j], "Failed at " + i + "," + j);
                    else
                        Assert.AreEqual(null, Grid[i, j], "Failed at " + i + "," + j);
                    c++;
                }
            }
        }

        /// <summary>
        ///A test for Game Logic All Alive
        ///</summary>
        [TestMethod()]
        public void GameLogicAllAliveTest()
        {
            int size = 5;
            GameOfLife target = new GameOfLife(size);
            CellUpdate[] cellUpdates = new CellUpdate[size * size];
            int c = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cellUpdates[c] = new CellUpdate(i, j, 1);
                    c++;
                }
            }
            target.ChangeCellStates(cellUpdates);

            int?[,] Grid = target.GetGrid();
            c = 0; // Reset count
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Assert.AreEqual(1, Grid[i, j], "Failed at " + i + "," + j);
                    c++;
                }
            }
        }

        /// <summary>
        ///A test for Game Logic All Dead
        ///</summary>
        [TestMethod()]
        public void GameLogicAllDeadTest()
        {
            int size = 5;
            GameOfLife target = new GameOfLife(size);
            CellUpdate[] cellUpdates = new CellUpdate[size * size];
            int c = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cellUpdates[c] = new CellUpdate(i, j, 0);
                    c++;
                }
            }
            target.ChangeCellStates(cellUpdates);

            int?[,] Grid = target.GetGrid();
            c = 0; // Reset count
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Assert.AreEqual(0, Grid[i, j], "Failed at " + i + "," + j);
                    c++;
                }
            }
        }

        /// <summary>
        ///A test for Game Logic All Zombie
        ///</summary>
        [TestMethod()]
        public void GameLogicAllZombieTest()
        {
            int size = 5;
            GameOfLife target = new GameOfLife(size);
            CellUpdate[] cellUpdates = new CellUpdate[size * size];
            int c = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cellUpdates[c] = new CellUpdate(i, j, null);
                    c++;
                }
            }
            target.ChangeCellStates(cellUpdates);

            int?[,] Grid = target.GetGrid();
            c = 0; // Reset count
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Assert.AreEqual(null, Grid[i, j], "Failed at " + i + "," + j);
                    c++;
                }
            }
        }

        /// <summary>
        ///A test for Game Logic
        ///</summary>
        [TestMethod()]
        public void GameLogicTest()
        {
            int size = 5;
            GameOfLife target = new GameOfLife(size);
            CellUpdate[] cellUpdates = new CellUpdate[size * size];
            int?[] states = {1, 1, 0, 1, 0, null, 0, 1, 1, 1, null, 1, null, 0, null, 1, 1, null, 1, 1, null, null, 0, null, 1};
            int c = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cellUpdates[c] = new CellUpdate(i, j, states[c]);
                    c++;
                }
            }
            target.ChangeCellStates(cellUpdates);
            target.NextDay();

            int?[,] Grid = target.GetGrid();
            // 2: Alive or zombie
            // 3: Dead or zombie
            int?[] nextDayStates = { 3, 3, 0, 1, 1, null, 0, 3, 2, 2, null, 2, null, 0, null, 2, 2, null, 2, 2, null, null, 0, null, 2 };
            c = 0; // Reset count
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (nextDayStates[c] == 3)
                    {
                        Assert.IsTrue(Grid[i, j] == 0 || Grid[i, j] == null, "Failed at " + i + "," + j);
                    }
                    else if (nextDayStates[c] == 2)
                    {
                        Assert.IsTrue(Grid[i, j] == 1 || Grid[i, j] == null, "Failed at " + i + "," + j);
                    }
                    else
                    {
                        Assert.AreEqual(nextDayStates[c], Grid[i, j], "Failed at " + i + "," + j);
                    }
                    c++;
                }
            }
        }
    }
}
