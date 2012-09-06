using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoL
{
    class GameOfLife
    {
        private int?[,] Grid;
        private Random random = new Random(); // Avoid instantiating a LOT of times

        // Constants for different states
        private static readonly int? Zombie = null;
        private static readonly int? Dead = 0;
        private static readonly int? Alive = 1;

        /// <summary>
        /// Create a new Game of Life with a size
        /// </summary>
        /// <param name="size"></param>
        public GameOfLife(int size)
        {
            if (size < 0) size *= -1;
            Grid = new int?[size, size];
            GenerateState();
        }

        /// <summary>
        /// Generate a random state
        /// </summary>
        private void GenerateState()
        {
            int size = Grid.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int? temp = random.Next(3);
                    Grid[i, j] = (temp == 2 ? Zombie : temp);
                }
            }
        }

        /// <summary>
        /// Run the game
        /// </summary>
        public void Run()
        {
            PrintGrid();

            ConsoleKeyInfo cki;
            while (true)
            {
                cki = Console.ReadKey();
                Console.WriteLine();
                if (cki.Key == ConsoleKey.Q)
                {
                    break;
                }
                else
                {
                    NextDay();
                    PrintGrid();
                }
            }
        }

        public void NextDay()
        {
            List<CellUpdate> cellUpdateList = new List<CellUpdate>();

            int size = Grid.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cellUpdateList.Add(GetNewCellState(i, j));
                }
            }

            CellUpdate[] cellUpdates = cellUpdateList.ToArray();
            ChangeCellStates(cellUpdates);
        }

        /// <summary>
        /// Find new state of a cell based on neighbors
        /// </summary>
        /// <param name="x">X coordinate of cell</param>
        /// <param name="y">Y coordinate of cell</param>
        /// <returns>CellUpdate struct with new state</returns>
        private CellUpdate GetNewCellState(int x, int y)
        {
            int size = Grid.GetLength(0);

            int zombieN = 0;
            int deadN = 0;
            int liveN = 0;
            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i < 0 || i >= size) continue; // If out of bounds, break
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (j < 0 || j >= size || (i == x && j == y)) continue; // If out of bounds or self, break
                    if (Grid[i, j] == Zombie) ++zombieN;
                    if (Grid[i, j] == Dead) ++deadN;
                    if (Grid[i, j] == Alive) ++liveN;
                }
            }

            Console.WriteLine(x + ", " + y + ": " + Grid[x, y]);
            Console.WriteLine("Zombies: " + zombieN);
            Console.WriteLine("Dead: " + deadN);
            Console.WriteLine("Alive: " + liveN);

            int? state = Grid[x, y];
            if (Grid[x, y] == Alive && liveN <= 1) state = Dead;                                // 1. A live cell with 1 or less neighbors dies
            if (Grid[x, y] == Alive && liveN >= 2 && liveN <= 3) state = Alive;                 // 2. A live cell with 2 or 3 neighbors survives
            if (Grid[x, y] == Alive && liveN >= 4) state = Dead;                                // 3. A live cell with 4 or more neighbors dies
            if (Grid[x, y] == Alive && zombieN >= 1 && random.Next(2) == 1) state = Zombie;     // 4. A live cell with 1 or more zombie neighbors has a 50% chance of surviving or becoming zombie
            if (Grid[x, y] == Dead && liveN == 3) state = Alive;                                // 5. A dead cell with 3 neighbors is come to life

            return new CellUpdate(x, y, state);
        }
        
        /// <summary>
        /// Change cell states according to each struct
        /// </summary>
        /// <param name="cellUpdates">Array of CellUpdate structs</param>
        public void ChangeCellStates(params CellUpdate[] cellUpdates)
        {
            int length = cellUpdates.Length;
            for (int i = 0; i < length; i++)
            {
                ChangeCellState(cellUpdates[i]);
            }
        }

        /// <summary>
        /// Change cell state according to struct
        /// </summary>
        /// <param name="cellUpdate">Cellupdate struct</param>
        private void ChangeCellState(CellUpdate cellUpdate)
        {
            int size = Grid.GetLength(0);
            if (cellUpdate.x >= size || cellUpdate.y >= size)
            {
                Console.WriteLine("Out of bounds");
            }
            else if (cellUpdate.state > 1)
            {
                Console.WriteLine("Unknown state");
            }
            else
            {
                Grid[cellUpdate.x, cellUpdate.y] = cellUpdate.state;
            }
        }

        /// <summary>
        /// Return current cell grid
        /// </summary>
        /// <returns>Current grid</returns>
        public int?[,] GetGrid()
        {
            return Grid;
        }

        /// <summary>
        /// Print current cell grid
        /// </summary>
        public void PrintGrid()
        {
            int size = Grid.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (Grid[i, j] == null)
                    {
                        Console.Write("z ");
                    }
                    else
                    {
                        Console.Write(Grid[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            GameOfLife gol = new GameOfLife(5);
            gol.Run();
        }
    }

    struct CellUpdate
    {
        public readonly int x;
        public readonly int y;
        public readonly int? state;

        public CellUpdate(int x, int y, int? state)
        {
            this.x = x;
            this.y = y;
            this.state = state;
        }
    }
}
