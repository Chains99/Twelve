using System;
using TwelveLogic;
namespace Console
{
    class Program
    {

        static void PrintTwelve(int[,] board)
        {
            System.Console.Clear();
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y] == 0)
                        System.Console.Write(".");
                    else
                        System.Console.Write(board[x, y]);

                    System.Console.Write(" ");
                }
                System.Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Twelve gameController = new Twelve(4, 5,false);
            PrintTwelve(gameController.board);
            while (true)
            {
                int x = int.Parse(System.Console.ReadLine());
                int y = int.Parse(System.Console.ReadLine());
                gameController.Click(x, y);
                PrintTwelve(gameController.board);
            }
        }
    }
}
