namespace WireframeRenderer
{
    /// <summary>
    /// Matrix used for renderer calculations.
    /// Source: http://dev.bratched.com/en/fun-with-matrix-multiplication-and-unsafe-code/
    /// </summary>
    public class Matrix
    {
        #region Fields
        /// <summary>
        /// The matrix.
        /// </summary>
        private readonly double[,] matrix;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class. 
        /// Creates a matrix of height * width dimensions.
        /// </summary>
        /// <param name="dim1">Height dimension.</param>
        /// <param name="dim2">Width dimension.</param>
        public Matrix(int dim1, int dim2)
        {
            matrix = new double[dim1, dim2];
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the height of the matrix.
        /// </summary>
        public int Height
        {
            get
            {
                return matrix.GetLength(0);
            }
        }

        /// <summary>
        /// Gets the width of the matrix.
        /// </summary>
        public int Width
        {
            get
            {
                return matrix.GetLength(1);
            }
        }

        /// <summary>
        /// Sets the value of element in the matrix.
        /// </summary>
        /// <param name="x">The height position.</param>
        /// <param name="y">The width position.</param>
        /// <returns>The value at the specified location in the matrix.</returns>
        public double this[int x, int y]
        {
            get { return matrix[x, y]; }
            set { matrix[x, y] = value; }
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Multiplies a matrix with another.
        /// Source: http://dev.bratched.com/en/fun-with-matrix-multiplication-and-unsafe-code/
        /// </summary>
        /// <param name="m1">First matrix.</param>
        /// <param name="m2">Second matrix.</param>
        /// <returns>The resulting matrix.</returns>
        public static Matrix NaiveMultiplication(Matrix m1, Matrix m2)
        {
            var resultMatrix = new Matrix(m1.Height, m2.Width);
            for (var i = 0; i < resultMatrix.Height; i++)
            {
                for (var j = 0; j < resultMatrix.Width; j++)
                {
                    resultMatrix[i, j] = 0;
                    for (var k = 0; k < m1.Width; k++)
                    {
                        resultMatrix[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return resultMatrix;
        }
        #endregion
    }
}
