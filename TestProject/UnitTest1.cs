using PathFinder;
using Xunit;

namespace TestProject
{
    public class UnitTest1
    {
        private readonly int[,] matrix3 = { 
            { 0, 0, 0, 0, 0, 1, 0, 1, 0 }, 
            { 0, 0, 0, 0, 0, 0, 1, 0, 1 }, 
            { 0, 0, 0, 1, 0, 0, 0, 1, 0 },
            { 0, 0, 1, 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 0, 0, 0, 0, 0, 1, 0, 0 },
            { 0, 1, 0, 0, 0, 1, 0, 0, 0 },
            { 1, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 0, 1, 0, 0, 0, 0, 0 }
        };

        private readonly int[,] matrix4 = {
            //0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12,13,14,15
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0 },//0
            { 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },//1
            { 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0 },//2
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },//3
            { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0 },//4
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0 },//5
            { 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1 },//6
            { 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },//7
            { 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0 },//8
            { 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1 },//9
            { 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 },//10
            { 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },//11
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },//12
            { 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0 },//13
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0 },//14
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0 },//15
        };

        [Fact]
        public void Matrix3_Test()
        {
            TestMatrix(3, 3, matrix3);
        }

        [Fact]
        public void Matrix4_Test()
        {
            TestMatrix(4, 4, matrix4);
        }

        private void TestMatrix(int rows, int columns, int[,] expectedMatrix)
        {
            var generator = new MatrixGenerator();

            var matrix = generator.GetMatrix(rows, columns);

            Assert.Equal(expectedMatrix.GetLength(0), matrix.GetLength(0));
            Assert.Equal(expectedMatrix.GetLength(1), matrix.GetLength(1));

            for (var i = 0; i < matrix.GetLength(0); i++)
                for (var j = 0; j < matrix.GetLength(1); j++)
                    Assert.Equal(expectedMatrix[i, j], matrix[i, j]);
        }
    }
}
