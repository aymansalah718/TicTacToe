using System;

namespace TicTacToe
{
    class Program
    {
        static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char currentPlayerSymbol = 'X';
        static string currentPlayer;
        static string player1;
        static string player2;
        static int player1Wins = 0;
        static int player2Wins = 0;
        static int ties = 0;

        static void Main(string[] args)
        {
            Console.Write("Enter the name of Player 1 (X): ");
            player1 = Console.ReadLine();
            Console.Write("Enter the name of Player 2 (O): ");
            player2 = Console.ReadLine();

            bool playAgain = true;

            while (playAgain)
            {
                PlayGame();

                Console.WriteLine("Do you want to play again? (y/n)");
                playAgain = Console.ReadLine().ToLower() == "y";
                ResetBoard();
            }

            Console.WriteLine("Final Score:");
            Console.WriteLine($"{player1} Wins: {player1Wins}, Losses: {player2Wins}");
            Console.WriteLine($"{player2} Wins: {player2Wins}, Losses: {player1Wins}");
            Console.WriteLine($"Ties: {ties}");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void PlayGame()
        {
            currentPlayer = player1;
            currentPlayerSymbol = 'X';

            int move;
            bool gameRunning = true;

            while (gameRunning)
            {
                Console.Clear();
                DisplayBoard();
                Console.WriteLine($"{currentPlayer} ({currentPlayerSymbol}), enter your move (1-9): ");
                while (!int.TryParse(Console.ReadLine(), out move) || move < 1 || move > 9 || board[move - 1] == 'X' || board[move - 1] == 'O')
                {
                    Console.WriteLine("Invalid move! Please enter a number between 1 and 9 that is not already taken: ");
                }

                board[move - 1] = currentPlayerSymbol;
                if (CheckWin())
                {
                    Console.Clear();
                    DisplayBoard();
                    Console.WriteLine($"{currentPlayer} wins!");
                    if (currentPlayer == player1) player1Wins++;
                    else player2Wins++;
                    gameRunning = false;
                }
                else if (CheckDraw())
                {
                    Console.Clear();
                    DisplayBoard();
                    Console.WriteLine("It's a draw!");
                    ties++;
                    gameRunning = false;
                }
                else
                {
                    currentPlayerSymbol = (currentPlayerSymbol == 'X') ? 'O' : 'X';
                    currentPlayer = (currentPlayer == player1) ? player2 : player1;
                }
            }
        }

        static void DisplayBoard()
        {
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {board[0]}  |  {board[1]}  |  {board[2]}  ");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {board[3]}  |  {board[4]}  |  {board[5]}  ");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {board[6]}  |  {board[7]}  |  {board[8]}  ");
            Console.WriteLine("     |     |      ");
        }

        static bool CheckWin()
        {
            int[,] winPatterns = new int[,]
            {
                {0, 1, 2},
                {3, 4, 5},
                {6, 7, 8},
                {0, 3, 6},
                {1, 4, 7},
                {2, 5, 8},
                {0, 4, 8},
                {2, 4, 6}
            };

            for (int i = 0; i < winPatterns.GetLength(0); i++)
            {
                if (board[winPatterns[i, 0]] == currentPlayerSymbol &&
                    board[winPatterns[i, 1]] == currentPlayerSymbol &&
                    board[winPatterns[i, 2]] == currentPlayerSymbol)
                {
                    return true;
                }
            }

            return false;
        }

        static bool CheckDraw()
        {
            foreach (char c in board)
            {
                if (c != 'X' && c != 'O')
                {
                    return false;
                }
            }
            return true;
        }

        static void ResetBoard()
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = (char)('1' + i);
            }
        }
    }
}
