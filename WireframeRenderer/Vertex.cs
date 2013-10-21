namespace WireframeRenderer
{
    using System.Drawing;

    /// <summary>
    /// An 3D position.
    /// </summary>
    public class Vertex
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> class. 
        /// </summary>
        public Vertex()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> class. 
        /// For use when the dummy value (Z) should be left as 0.
        /// </summary>
        /// <param name="x">The X-coordinate of the vertex.</param>
        /// <param name="y">The Y-coordinate of the vertex.</param>
        /// <param name="z">The Z-coordinate of the vertex.</param>
        public Vertex(double x, double y, double z)
            : this(x, y, z, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> class. 
        /// </summary>
        /// <param name="x">The X-coordinate of the vertex.</param>
        /// <param name="y">The Y-coordinate of the vertex.</param>
        /// <param name="z">The Z-coordinate of the vertex.</param>
        /// <param name="w">The W-coordinate of the vertex.</param>
        public Vertex(double x, double y, double z, double w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the X-value of the vertex.
        /// </summary>
        public double X { get; set; }
        
        /// <summary>
        /// Gets or sets the Y-value of the vertex.
        /// </summary>
        public double Y { get; set; }
        
        /// <summary>
        /// Gets or sets the Z-value of the vertex.
        /// </summary>
        public double Z { get; set; }
        
        /// <summary>
        /// Gets or sets the W-value of the vertex.
        /// </summary>
        public double W { get; set; }

        /// <summary>
        /// Gets or sets the screen coordinate of the vertex.
        /// </summary>
        public Point ScreenCoordinate { get; set; }

        #endregion

        #region Static Methods
        /// <summary>
        /// Converts the first column of a matrix into a vertex.
        /// </summary>
        /// <param name="m">The matrix to convert.</param>
        /// <returns>The resulting vertex.</returns>
        public static Vertex MatrixToVertex(Matrix m)
        {
            return new Vertex(m[0, 0], m[1, 0], m[2, 0], m[3, 0]);
        }

        /// <summary>
        /// Converts a vertex into a 1-width matrix.
        /// </summary>
        /// <param name="v">The vertex to convert.</param>
        /// <returns>The resulting matrix.</returns>
        public static Matrix VertexToMatrix(Vertex v)
        {
            var result = new Matrix(4, 1);

            result[0, 0] = v.X;
            result[1, 0] = v.Y;
            result[2, 0] = v.Z;
            result[3, 0] = v.W;

            return result;
        }
        #endregion 

        #region Methods
        /// <summary>
        /// Updates the vertex for use with drawing.
        /// </summary>
        /// <param name="camera">The camera.</param>
        public void Update(Camera camera)
        {
            // apply the view and perspective transforms
            var vertexViewSpace = MatrixToVertex(Matrix.NaiveMultiplication(camera.AllTransforms, VertexToMatrix(this)));

            // normalize by dividing through by the homogeneous coordinate W
            vertexViewSpace.X = vertexViewSpace.X / vertexViewSpace.W;
            vertexViewSpace.Y = vertexViewSpace.Y / vertexViewSpace.W;
            vertexViewSpace.Z = vertexViewSpace.Z / vertexViewSpace.W;

            // now map [-1, 1] into the screen coordinates (0, width) and (0, height)
            // where (0,0) is the top-left corner of the screen
            ScreenCoordinate = new Point
            {
                X = (int)(vertexViewSpace.X * (camera.Width / 2) + (camera.Width / 2)),
                Y = (int)(-vertexViewSpace.Y * (camera.Height / 2) + (camera.Height / 2))
            };
        }
        #endregion
    }
}
