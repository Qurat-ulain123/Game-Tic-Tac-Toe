
using System;

class TicTacToeGame
{
    static char[,] grid = new char[3, 3];
    static string playerOne = "";
    static string playerTwo = "";

    static void Main()
    {
        Console.Title = "Tic Tac Toe Challenge";

  
        Console.WriteLine("    TIC TAC TOE CHALLENGE");

        Console.Write("Enter Player 1 name: ");
        playerOne = Console.ReadLine() ?? "Player 1";

        Console.Write("Enter Player 2 name: ");
        playerTwo = Console.ReadLine() ?? "Player 2";

        bool continuePlaying = true;

        while (continuePlaying)
        {
            StartGame();

            Console.Write("\nPlay another round? (Y/N): ");
            string answer = (Console.ReadLine() ?? "").Trim();

            continuePlaying = answer.Equals(
                "Y", StringComparison.OrdinalIgnoreCase);
        }

        Console.WriteLine("\nThanks for playing!");
    }

    static void ResetGrid()
    {
        char number = '1';

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                grid[row, col] = number++;
            }
        }
    }

    static void ShowGrid()
    {
        Console.WriteLine("\n    TIC TAC TOE BOARD\n");

        for (int row = 0; row < 3; row++)
        {
            Console.Write(" ");

            for (int col = 0; col < 3; col++)
            {
                Console.Write($" {grid[row, col]} ");

                if (col < 2)
                    Console.Write("|");
            }

            Console.WriteLine();

            if (row < 2)
                Console.WriteLine(" ----+----+----");
        }

        Console.WriteLine();
    }

    static int GetPosition(string name)
    {
        while (true)
        {
            Console.Write($"{name}, select a box (1-9): ");
            string input = Console.ReadLine() ?? "";

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Please enter a number.");
                continue;
            }

            if (choice < 1 || choice > 9)
            {
                Console.WriteLine("Choose a number from 1 to 9.");
                continue;
            }

            int row = (choice - 1) / 3;
            int col = (choice - 1) % 3;

            if (grid[row, col] == 'X' ||
                grid[row, col] == 'O')
            {
                Console.WriteLine("Box already occupied!");
                continue;
            }

            return choice;
        }
    }

    static bool HasWon(char symbol)
    {
        for (int i = 0; i < 3; i++)
        {
            if (grid[i, 0] == symbol &&
                grid[i, 1] == symbol &&
                grid[i, 2] == symbol)
                return true;

            if (grid[0, i] == symbol &&
                grid[1, i] == symbol &&
                grid[2, i] == symbol)
                return true;
        }

        bool diagonalOne =
            grid[0, 0] == symbol &&
            grid[1, 1] == symbol &&
            grid[2, 2] == symbol;

        bool diagonalTwo =
            grid[0, 2] == symbol &&
            grid[1, 1] == symbol &&
            grid[2, 0] == symbol;

        return diagonalOne || diagonalTwo;
    }

    static void StartGame()
    {
        ResetGrid();

        char currentSymbol = 'X';
        int turnCount = 0;

        while (turnCount < 9)
        {
            Console.Clear();
            ShowGrid();

            string currentName = currentSymbol == 'X'
                ? playerOne : playerTwo;

            Console.WriteLine(
                $"Current Turn: {currentName} ({currentSymbol})");

            int position = GetPosition(currentName);

            int row = (position - 1) / 3;
            int col = (position - 1) % 3;

            grid[row, col] = currentSymbol;
            turnCount++;

            if (HasWon(currentSymbol))
            {
                Console.Clear();
                ShowGrid();

                Console.WriteLine(
                    $"Congratulations {currentName}! You won!");
                return;
            }

            if (turnCount == 9)
            {
                Console.Clear();
                ShowGrid();

                Console.WriteLine(
                    "Game finished! It is a draw.");
                return;
            }

            currentSymbol = currentSymbol == 'X'
                ? 'O' : 'X';
        }
    }
}
