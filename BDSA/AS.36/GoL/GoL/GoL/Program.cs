using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoL
{
    class GameOfLife
    {
        private int?[,] Grid;

        public GameOfLife(int size)
        {
            Grid = new int?[size, size];
            GenerateState();

            ConsoleKeyInfo cki;
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Q)
                {
                    break;
                }
                else
                {
                    Console.WriteLine();
                    NextDay();
                }
            }
        }

        private void GenerateState()
        {
            int size = Grid.GetLength(0);
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int? temp = random.Next(3);
                    Grid[i, j] = (temp == 2 ? null : temp);
                }
            }
        }

        private void NextDay()
        {
            PrintGrid();
        }

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
        }
    }
}
