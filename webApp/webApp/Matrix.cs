namespace webApp
{
    record MatrixJson(int[] matrixMas, int numRows, int numColumns);

    public class Matrix
    {
        private int[] matrixMas;
        private int numRows;
        private int numColumns;
        private int[,] matrix;
        private double[,] scoreMatrix;

        public int[] MatrixMas
        {
            set { matrixMas = value; }
            get { return matrixMas; }
        }
        public int NumRows
        {
            set { numRows = value; }
            get { return numRows; }
        }
        public int NumColumns
        {
            set { numColumns = value; }
            get { return numColumns; }
        }
        public int[,] GetMatrix
        {
            set { }
            get { return matrix; }
        }
        public double[,] ScoreMatrix
        {
            set { }
            get { return scoreMatrix; }
        }

        public void calculate()
        {
            matrix = new int[numRows, numColumns];

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    matrix[i, j] = matrixMas[i * NumColumns + j];
                }
            }

            scoreMatrix = new double[numRows, numColumns];

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    scoreMatrix[i, j] = 100;
                }
            }
        }

        public double[] getScoreMatrixMas()
        {
            double[] scoreMatrixMas = new double[numRows * numColumns];

            for(int i = 0; i < numRows; i++)
            {
                for(int j = 0; j < numColumns; j++)
                {
                    scoreMatrixMas[i * numColumns + j] = scoreMatrix[i, j];
                }
            }

            return scoreMatrixMas;
        }

        public bool hasFinish(int posX, int posY)
        {
            return matrix[posY - 1, posX - 1] == 2;
        }
        public bool hasObstackle(int posX, int posY)
        {
            return matrix[posY - 1, posX - 1] == 1;
        }
        public void increaseScoreCell(int posX, int posY)
        {
            if(scoreMatrix[posY - 1, posX - 1] <= 1000000)
            {
                scoreMatrix[posY - 1, posX - 1] =
                    Math.Round(scoreMatrix[posY - 1, posX - 1] * 1.1);
            }
        }
        public void decreaseScoreCell(int posX, int posY)
        {
            scoreMatrix[posY - 1, posX - 1] =
                Math.Round(scoreMatrix[posY - 1, posX - 1] * 0.9);
        }

        public void logMatrix()
        {
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("-----------------------------");
        }

        public void logScoreMatrix()
        {
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    Console.Write($"{scoreMatrix[i, j]} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("-----------------------------");
        }
    }
}
