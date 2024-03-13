namespace BiggerSudoku
{
    public class SolveBoard
    {
        private static int[,] _startBoard = new int[21, 21] {
            { 7, 8, 0, 0, 0, 5, 0, 0, 1, -1, -1, -1, 0, 0, 7, 5, 0, 3, 0, 0, 1 },
            { 0, 0, 1, 0, 8, 0, 7, 0, 4, -1, -1, -1, 1, 0, 0, 0, 2, 0, 6, 0, 9 },
            { 2, 4, 0, 0, 7, 0, 5, 0, 0, -1, -1, -1, 0, 9, 0, 6, 1, 0, 0, 2, 0 },
            { 0, 3, 8, 9, 0, 0, 4, 0, 0, -1, -1, -1, 7, 5, 0, 9, 0, 0, 0, 0, 6 },
            { 0, 0, 5, 8, 0, 0, 1, 7, 0, -1, -1, -1, 6, 0, 1, 7, 0, 0, 0, 0, 2 },
            { 4, 0, 7, 0, 1, 0, 0, 5, 0, -1, -1, -1, 0, 2, 9, 0, 6, 0, 7, 0, 0 },
            { 9, 0, 0, 5, 0, 4, 0, 1, 0, 0, 5, 2, 0, 0, 6, 0, 0, 1, 0, 9, 0 },
            { 0, 1, 0, 7, 9, 0, 0, 2, 0, 4, 0, 7, 0, 1, 0, 0, 7, 0, 2, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 9, 0, 7, 0, 0, 0, 5, 0, 0, 0, 0, 9, 0, 6, 0 },
            { -1, -1, -1, -1, -1, -1, 4, 7, 0, 2, 0, 0, 8, 0, 0, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, 0, 8, 2, 7, 0, 0, 0, 5, 0, -1, -1, -1, -1, -1, -1 },
            { -1, -1, -1, -1, -1, -1, 5, 0, 6, 0, 0, 0, 0, 2, 0, -1, -1, -1, -1, -1, -1 },
            { 6, 0, 0, 9, 4, 0, 0, 0, 8, 5, 7, 0, 0, 4, 0, 0, 7, 0, 2, 0, 3 },
            { 9, 0, 0, 8, 0, 3, 1, 0, 0, 8, 2, 0, 7, 9, 0, 6, 0, 0, 0, 0, 8 },
            { 0, 8, 0, 0, 0, 6, 7, 5, 0, 0, 0, 4, 0, 0, 8, 5, 0, 0, 9, 6, 0 },
            { 4, 3, 0, 0, 0, 0, 0, 8, 6, -1, -1, -1, 9, 0, 7, 0, 6, 0, 0, 2, 0 },
            { 0, 0, 6, 0, 9, 8, 0, 0, 2, -1, -1, -1, 0, 8, 4, 2, 0, 0, 7, 0, 0 },
            { 8, 0, 9, 0, 3, 0, 0, 0, 5, -1, -1, -1, 5, 0, 6, 0, 0, 7, 0, 0, 0 },
            { 1, 0, 8, 7, 0, 0, 0, 0, 3, -1, -1, -1, 0, 6, 0, 0, 0, 8, 0, 7, 0 },
            { 0, 4, 3, 2, 0, 0, 8, 0, 0, -1, -1, -1, 8, 0, 0, 0, 9, 0, 0, 0, 2 },
            { 0, 6, 0, 0, 8, 0, 0, 4, 0, -1, -1, -1, 1, 0, 0, 0, 0, 2, 6, 0, 0 },
        };

        private readonly int _RowCount = _startBoard.GetLength(0);
        private readonly int _ColumnCount = _startBoard.GetLength(1);
        private readonly int _MaxNumber = 9;

        public void Solve()
        {
            if (IsBoardSolved(_startBoard))
            {
                WriteBoard(_startBoard);
                Console.WriteLine("***********************");

                Console.WriteLine($"Solved!");
            }
            else
            {
                Console.WriteLine("Can't solve. Many apologies");
            }
        }

        public bool IsBoardSolved(int[,] newBoard)
        {
            for (var row = 0; row < _RowCount; row++)
            {
                for (var column = 0; column < _ColumnCount; column++)
                {
                    if (newBoard[row, column] == 0)
                    {
                        for (var number = 1; number <= _MaxNumber; number++)
                        {
                            if (IsValidPlacement(newBoard, number, row, column))
                            {
                                newBoard[row, column] = number;

                                if (IsBoardSolved(newBoard))
                                {
                                    return true;
                                }

                                newBoard[row, column] = 0;
                            }
                        }

                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsValidPlacement(int[,] board, int number, int row, int column)
        {
            return !IsNumberInRow(board, number, row, column) &&
                   !IsNumberInColumn(board, number, row, column) &&
                   !IsNumberInBox(board, number, row, column);
        }

        public bool IsNumberInRow(int[,] board, int number, int row, int column)
        {
            var startColumn = column - column % 6;
            if (startColumn > 12)
                startColumn = 12;

            for (int a = 0; a < 9; a++)
            {
                if (board[row, startColumn + a] == number)
                    return true;
            }

            return false;
        }

        public bool IsNumberInColumn(int[,] board, int number, int row, int column)
        {
            var startRow = row - row % 6;
            if (startRow > 12)
                startRow = 12;

            for (int a = 0; a < 9; a++)
            {
                if (board[startRow + a, column] == number)
                    return true;
            }

            return false;
        }

        public bool IsNumberInBox(int[,] board, int number, int row, int column)
        {
            int boxRow = row - row % 3;
            int boxColumn = column - column % 3;

            for (int r = boxRow; r < boxRow + 3; r++)
            {
                for (int c = boxColumn; c < boxColumn + 3; c++)
                {
                    if (board[r, c] == number)
                        return true;
                }
            }

            return false;
        }

        public void WriteBoard(int[,] board)
        {
            for (int row = 0; row < _RowCount; row++)
            {
                if (row % 3 == 0 && row > 0)
                    Console.WriteLine("-----------------------");

                for (int column = 0; column < _ColumnCount; column++)
                {
                    if (column % 3 == 0 && column > 0)
                        Console.Write("|");
                    Console.Write(board[row, column].ToString().PadLeft(3));
                }

                Console.WriteLine();
            }
        }
    }
}
