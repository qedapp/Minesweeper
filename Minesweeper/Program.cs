using System;
using Minesweeper.Domain;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Minesweeper <rows> <columns> <bombs>");
                return;
            }

            var rows = Convert.ToInt32(args[0]);
            var cols = Convert.ToInt32(args[1]);
            var bombs = Convert.ToInt32(args[2]);

            var view = new MinesweeperConsoleView(
                new Game(rows, cols, bombs),
                Console.Out, Console.In);

            view.Show();

            Console.ReadLine();
        }
    }
}
