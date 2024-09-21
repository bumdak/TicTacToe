using System;

class TicTacToe
{
    static char[] board = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
    static char human = 'X';
    static char ai = 'O';

    static void PrintBoard()
    {
        Console.WriteLine("-------------");
        for (int i = 0; i < 9; i += 3)
        {
            Console.WriteLine("| {0} | {1} | {2} |", board[i], board[i + 1], board[i + 2]);
            Console.WriteLine("-------------");
        }
    }

    static bool CheckWin(char player)
    {
        int[,] winConditions = {
            { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },  // 가로 승리 조건
            { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },  // 세로 승리 조건
            { 0, 4, 8 }, { 2, 4, 6 }               // 대각선 승리 조건
        };

        for (int i = 0; i < winConditions.GetLength(0); i++)
        {
            if (board[winConditions[i, 0]] == player &&
                board[winConditions[i, 1]] == player &&
                board[winConditions[i, 2]] == player)
            {
                return true;
            }
        }

        return false;
    }

    static bool IsDraw()
    {
        foreach (char cell in board)
        {
            if (cell == ' ')
                return false;
        }
        return true;
    }

    static void PlayerMove()
    {
        int move;
        while (true)
        {
            Console.WriteLine("Enter your move (1-9): ");
            move = int.Parse(Console.ReadLine()) - 1;

            if (move >= 0 && move < 9 && board[move] == ' ')
            {
                board[move] = human;
                break;
            }
            else
            {
                Console.WriteLine("Invalid move, try again.");
            }
        }
    }

    static int Minimax(char[] newBoard, bool isMaximizing)
    {
        if (CheckWin(ai)) return 10;    // AI 승리 시
        if (CheckWin(human)) return -10; // 인간 승리 시
        if (IsDraw()) return 0;         // 무승부 시

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < 9; i++)
            {
                if (newBoard[i] == ' ')
                {
                    newBoard[i] = ai;
                    int score = Minimax(newBoard, false); // 인간의 차례로 넘김
                    newBoard[i] = ' ';
                    bestScore = Math.Max(score, bestScore); // 가장 높은 점수 선택
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int i = 0; i < 9; i++)
            {
                if (newBoard[i] == ' ')
                {
                    newBoard[i] = human;
                    int score = Minimax(newBoard, true); // AI의 차례로 넘김
                    newBoard[i] = ' ';
                    bestScore = Math.Min(score, bestScore); // 가장 낮은 점수 선택
                }
            }
            return bestScore;
        }
    }

    static int FindBestMove()
    {
        int bestMove = -1;
        int bestScore = int.MinValue;

        for (int i = 0; i < 9; i++)
        {
            if (board[i] == ' ')
            {
                board[i] = ai;  // AI가 임시로 수를 둠
                int score = Minimax(board, false);  // 상대 차례로 넘김
                board[i] = ' ';  // 탐색 후 보드 초기화

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = i; // 가장 높은 점수를 반환하는 위치 저장
                }
            }
        }
        return bestMove; // 최적의 수 반환
    }



    static int ValueMove()
    {

        return 0;
    }

    static void AIMove()
    {
        int bestMove = FindBestMove();
        if (bestMove != -1)
        {
            board[bestMove] = ai;
        }
    }

    static void Main()
    {
        while (true)
        {
            PrintBoard();

            // 플레이어의 차례
            PlayerMove();
            if (CheckWin(human))
            {
                PrintBoard();
                Console.WriteLine("You win!");
                break;
            }
            if (IsDraw())
            {
                PrintBoard();
                Console.WriteLine("It's a draw!");
                break;
            }

            // AI의 차례
            AIMove();
            if (CheckWin(ai))
            {
                PrintBoard();
                Console.WriteLine("AI wins!");
                break;
            }
            if (IsDraw())
            {
                PrintBoard();
                Console.WriteLine("It's a draw!");
                break;
            }
        }
    }
}