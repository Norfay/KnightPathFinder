using System;
using System.Collections.Generic;

namespace ChessApp1
{
    class PathFinder
    {
        private int _columns;
        private int _rows;
        private int _matrixSize;
        private readonly string chessXCoordinatesChars = "abcdefgh"; //chess x chars

        //possible knight moves
        private readonly int[] xCoordinateMoves = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
        private readonly int[] yCoordinateMoves = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };


        public PathFinder(int columns = 8, int rows = 8)
        {
            _columns = columns;
            _rows = rows;
            _matrixSize = rows * columns;
        }

        public List<int> RestorePath(int startPoint, int finishPoint, int[] stepMap, bool[,] matrix)
        {
            List<int> backwardSteps = new List<int>() { finishPoint };

            var stepCounter = stepMap[finishPoint];
            var previousStep = finishPoint;
            while (stepCounter > 0)
            {
                stepCounter--;
                for (int i = 0; i < stepMap.Length; i++)
                {
                    if (stepMap[i] == stepCounter && matrix[i, previousStep])
                    {
                        backwardSteps.Insert(0, i);
                        previousStep = i;
                    }
                }
            }
            return backwardSteps;
        }

        //transforming chess coords to matrix coords method
        public int ChessToMatrixCoordinate(string chessCoordinate)
        {
            if(!CoordinateValidation(chessCoordinate))
                throw new Exception("Заданы неверные координаты");
            int x = chessXCoordinatesChars.IndexOf(chessCoordinate[0]);
            var intChessCoord = int.Parse(chessCoordinate[1].ToString());
            int y = intChessCoord - 1;
            var result = x * _columns + y;
            return result;
        }

        //transforming matrix coords to chess coords method
        public string MatrixToChessCoordinate(int matrixCoordinate)
        {            
            int x = matrixCoordinate / _rows;
            int y = matrixCoordinate % _rows + 1;
            string result = chessXCoordinatesChars[x] + y.ToString();
            return result;
        }

        //creating adjacency matrix
        public bool[,] CreateMatrix()
        {
            bool[,] innerMatrix = new bool[_matrixSize, _matrixSize];

            for (var x = 0; x < _rows; x++)
            {
                for (var y = 0; y < _columns; y++)
                {
                    for (var counter = 0; counter < xCoordinateMoves.Length; counter++)
                    {

                        var xCoordinate = x + xCoordinateMoves[counter];
                        var yCoordinate = y + yCoordinateMoves[counter];
                        if (xCoordinate < 0 || xCoordinate > (_rows - 1) || yCoordinate < 0 || yCoordinate > (_columns - 1))
                            continue;
                        var innerX = x * _columns + y;
                        var innerY = xCoordinate * _rows + yCoordinate;

                        innerMatrix[innerX, innerY] = true;
                    }
                }
            }
            return innerMatrix;
        }

        public int[] Bfs(int startPoint, int finishpoint, bool[,] matrix)
        {
            var currentStep = new List<int>();
            var nextStep = new List<int>();
            Queue<int> matrixQueue = new Queue<int>();
            Queue<int> oldQueue = new Queue<int>();
            int[] stepMap = new int[_matrixSize];

            var innerCounter = 0;
            nextStep.Add(startPoint);

            while (nextStep.Count != 0)
            {
                currentStep = nextStep;
                nextStep = new List<int>();
                foreach (var index in currentStep)
                {
                    if (!oldQueue.Contains(index))
                    {
                        oldQueue.Enqueue(index);
                        stepMap[index] = innerCounter;

                        for (var i = 0; i < _matrixSize; i++)
                        {
                            if (matrix[index, i])
                            {
                                if (!oldQueue.Contains(i) && !oldQueue.Contains(finishpoint))
                                {
                                    nextStep.Add(i);
                                }
                            }
                        }
                    }
                }
                innerCounter++;
            }
            return stepMap;
        }

        public bool CoordinateValidation(string coordinate)
        {
            if (coordinate.Length != 2) return false;
            if (!chessXCoordinatesChars.Contains(coordinate[0].ToString())) return false;
            if (!int.TryParse(coordinate[1].ToString(), out int index)) return false;
            if (index < 0 || index > _rows) return false;
            return true;
        }
    }
}
