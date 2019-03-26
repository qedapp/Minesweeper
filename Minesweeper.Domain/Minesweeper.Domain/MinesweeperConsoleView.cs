using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Domain
{
    public class MinesweeperConsoleView
    {

        private Game game;
        private TextWriter writer;
        private TextReader reader;

        private bool showBombs = false;

        public MinesweeperConsoleView(Game game, TextWriter writer, TextReader reader)
        {
            this.game = game;
            this.writer = writer;
            this.reader = reader;
        }

        public void Show()
        {
            while (game.IsOver == false)
            {
                DrawUI();

                writer.WriteLine(" ");

                string showBombsStr = "Show Bombs (o)";
                if (showBombs)
                {
                    showBombsStr = "Hide Bombs (p)";
                }
                writer.WriteLine("Left (a), Right (s), Up (w), Down (z), " + showBombsStr + ", Quit (q)");
                writer.WriteLine(" ");
                var currentRow = game.Player.GetcurrentLocation().Y;
                var currentCol = game.Player.GetcurrentLocation().X;
                char c1 = (char)(currentCol + 64);

                var livesLeft = game.Player.GetLivesLeft();
                var numMoves = game.Player.GetNumMoves();
                var numBombsHit = game.Player.GetNumBombsHit();

                writer.WriteLine("Current Location : " + c1.ToString() + "," + currentRow.ToString() + "  | Lives Left : " + livesLeft.ToString() + "  | Number of Moves : " + numMoves.ToString() + " | Number of Bombs hit : " + numBombsHit.ToString());

                var input = reader.ReadLine().ToLower();
                switch (input)
                {
                    case "a":
                        CheckMove(input);
                        break;

                    case "s":
                        CheckMove(input);
                        break;

                    case "w":
                        CheckMove(input);
                        break;

                    case "z":
                        CheckMove(input);
                        break;

                    case "o":
                        ShowHideBombs(true);
                        break;

                    case "p":
                        ShowHideBombs(false);
                        break;

                    case "q":
                        GameOver();
                        return;

                    default:
                        InvalidCommand();
                        break;
                }
            }
            DrawUI();

            GameOver();
        }

        private void CheckMove(string direction)
        {

            var currentPos = game.Player.GetcurrentLocation();

            var newPos = currentPos;

            switch (direction)
            {
                case "a":
                    if (currentPos.X > 1)
                    {
                        newPos.X--;
                    }
                    break;

                case "s":
                    if (currentPos.X < game.Columns)
                    {
                        newPos.X++;
                    }
                    break;

                case "w":
                    if (currentPos.Y > 1)
                    {
                        newPos.Y--;
                    }
                    break;

                case "z":
                    if (currentPos.Y < game.Rows)
                    {
                        newPos.Y++;
                    }
                    break;

                default:
                    break;

            }

            if (!(newPos.X == currentPos.X && newPos.Y == currentPos.Y))
            {
                // We have moved

                game.Player.SetcurrentLocation(newPos);
                game.Player.IncNumMoves();
                game.GameGrid.RemovePlayer(currentPos.Y, currentPos.X);
                game.GameGrid.AddPlayer(newPos.Y, newPos.X);

                bool explosion = game.GameGrid.CheckForExplosion(newPos.Y, newPos.X);

                if (explosion)
                {
                    game.Player.IncNumBombsHit();

                    int livesLeft = game.Player.DecNumLives();
                    if (livesLeft == 0)
                    {
                        game.IsOver = true;
                        GameOver();
                    }
                }
            
                // Check to see if they have reached the other side

                if (newPos.X == game.Rows)
                {
                    game.isComplete = true;
                    game.IsOver = true;
                    GameOver();

                }
            }
 

        }

            private void ShowHideBombs(bool show)
        {
            showBombs = show;
        }

        private void DrawUI()
        {
            Console.Clear();
            Render();
        }

        public void Render()
        {
            DrawXHeader();

            for (var y = 1; y <= game.Rows; y++)
            {
                DrawRow(y);
            }
        }

        private void DrawXHeader()
        {
            writer.Write("     ");
            for (int x = 1; x <= game.Rows; x++)
            {
                char c1 = (char)(x + 64);
                writer.Write(" ");
                writer.Write(c1);
                writer.Write("  ");
            }
            writer.WriteLine();

        }

        private void DrawRow(int y)
        {
            var count = game.Rows.ToString().Length;
            writer.Write(y.ToString().PadLeft(count + 1, ' '));
            writer.Write(" |");

            for (var x = 1; x <= game.Columns; x++)
            {
                DrawCellAt(y, x);

            }
            writer.WriteLine();
        }

        private void DrawCellAt(int y, int x)
        {

            var cell = game.GetCellAtXY(x, y);

            writer.Write(" ");

            ShowCell(cell);

            writer.Write(" |");
        }

        private void ShowCell(GameCell cell)
        { 
            if (cell.IsPlayerHere && cell.GetIsBomb()) 
            {
                WriteWithColor("@", ConsoleColor.Red);
            }
            else if (cell.IsPlayerHere)
            {
                WriteWithColor("@", ConsoleColor.Yellow);
            }
            else if (cell.GetIsBomb() && cell.IsExplodedBomb)
            {
                WriteWithColor("*", ConsoleColor.Red);
            }
            else if (cell.GetIsBomb() && showBombs)
            {
                WriteWithColor("*", ConsoleColor.Gray);
            }
            else if (cell.GetHasBeenVisited())
            {
                WriteWithColor(" ", ConsoleColor.Green);
            }
            else
            {
                WriteWithColor(".", ConsoleColor.Green);
            }


        }


        private void WriteWithColor(string s, ConsoleColor color)
        {
            var foreground = Console.ForegroundColor;
            Console.ForegroundColor = color;
            writer.Write(s);
            Console.ForegroundColor = foreground;
        }

        private void InvalidCommand()
        {
            writer.WriteLine("Invalid command, please try again.");
        }

        private void GameOver()
        {

            var currentRow = game.Player.GetcurrentLocation().Y;
            var currentCol = game.Player.GetcurrentLocation().X;
            char c1 = (char)(currentCol + 64);

            var livesLeft = game.Player.GetLivesLeft();
            var numMoves = game.Player.GetNumMoves();
            var numBombsHit = game.Player.GetNumBombsHit();

            writer.WriteLine("Current Location : " + c1.ToString() + "," + currentRow.ToString() + "  | Lives Left : " + livesLeft.ToString() + "  | Number of Moves : " + numMoves.ToString() + " | Number of Bombs hit : " + numBombsHit.ToString());

            if (game.isComplete)
            {
                writer.WriteLine("Game Over! Congratulations you completed successfully");

            }
            else
            {
                writer.WriteLine("Game Over! Please try again");

            }

        }

    }
}
