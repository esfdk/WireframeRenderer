using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WireframeRenderer
{
    /// <summary>
    /// Matrix used for renderer calculations.
    /// Source: http://dev.bratched.com/en/fun-with-matrix-multiplication-and-unsafe-code/
    /// </summary>
    class Matrix
    {
        /// <summary>
        /// The matrix.
        /// </summary>
        private readonly double[,] matrix;
        
        /// <summary>
        /// Creates a matrix of x * y dimensions.
        /// </summary>
        /// <param name="dim1">x dimension.</param>
        /// <param name="dim2">y dimension.</param>
        public Matrix(int dim1, int dim2)
        {
            matrix = new double[dim1, dim2];
        }

        public int Height { get { return matrix.GetLength(0); } }
        public int Width { get { return matrix.GetLength(1); } }

        /// <summary>
        /// Sets the value of element in the matrix.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <returns></returns>
        public double this[int x, int y]
        {
            get { return matrix[x, y]; }
            set { matrix[x, y] = value; }
        }
    }
}
