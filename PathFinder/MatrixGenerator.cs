using System;

namespace PathFinder
{
    public class MatrixGenerator : IMatrixGenerator
    {
        private readonly int[,] steps = { { -1, -2 }, { -1, 2 }, { 1, -2 }, { 1, 2 }, { -2, -1 }, { -2, 1 }, { 2, -1 }, { 2, 1 } }; 
        public int[,] GetMatrix(int rows, int columns)
        {
            var matrixSize = rows * columns;
            var innerMatrix = new int[matrixSize, matrixSize];

            for (var x = 0; x < rows; x++)
            {
                for (var y = 0; y < columns; y++)
                {
                    for (var counter = 0; counter < steps.GetLength(0); counter++)
                    {
                        var xCoordinate = x + steps[counter, 0];
                        var yCoordinate = y + steps[counter, 1];
                        if (xCoordinate < 0 || xCoordinate > (rows - 1) || yCoordinate < 0 || yCoordinate > (columns - 1))
                            continue;

                        var innerX = x * columns + y;
                        var innerY = xCoordinate * rows + yCoordinate;

                        innerMatrix[innerX, innerY] = 1;
                    }
                }
            }
            return innerMatrix;
        }
    }
}
