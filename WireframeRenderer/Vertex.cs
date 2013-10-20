namespace WireframeRenderer
{
    using System.Drawing;

    /// <summary>
    /// An 3D position.
    /// </summary>
    public class Vertex
    {
        #region Properties
        /// <summary>
        /// x-value of the vertex.
        /// </summary>
        public double x { get; set; }
        
        /// <summary>
        /// y-value of the vertex.
        /// </summary>
        public double y { get; set; }
        
        /// <summary>
        /// z-value of the vertex.
        /// </summary>
        public double z { get; set; }
        
        /// <summary>
        /// w-value of the vertex.
        /// </summary>
        public double w { get; set; }

        /// <summary>
        /// Screen coordinate of the vertex.
        /// </summary>
        public Point ScreenCoordinate { get; set; }

        #endregion
        #region Constructors
        /// <summary>
        /// Creates a new instance of Vertex.
        /// </summary>
        public Vertex(){}

        /// <summary>
        /// Creates a new instance of Vertex.
        /// For use when the dummy value (z) should be left as 0.
        /// </summary>
        /// <param name="x">The x-coordinate of the vertex.</param>
        /// <param name="y">The y-coordinate of the vertex.</param>
        /// <param name="z">The z-coordinate of the vertex.</param>
        public Vertex(double x, double y, double z) : this(x, y, z, 1){}

        /// <summary>
        /// Creates a new instance of Vertex.
        /// </summary>
        /// <param name="x">The x-coordinate of the vertex.</param>
        /// <param name="y">The y-coordinate of the vertex.</param>
        /// <param name="z">The z-coordinate of the vertex.</param>
        /// <param name="w">The w-coordinate of the vertex.</param>
        public Vertex(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates the vertex for use with drawing.
        /// </summary>
        public void Update(Camera camera)
        {
            // apply the view and perspective transforms
            var vertexViewSpace = MatrixToVertex(Matrix.NaiveMultiplication(camera.AllTransforms, VertexToMatrix(this)));

            // normalize by dividing through by the homogeneous coordinate w
            vertexViewSpace.x = vertexViewSpace.x / vertexViewSpace.w;
            vertexViewSpace.y = vertexViewSpace.y / vertexViewSpace.w;
            vertexViewSpace.z = vertexViewSpace.z / vertexViewSpace.w;

            // now map [-1, 1] into the screen coordinates (0, width) and (0, height)
            // where (0,0) is the top-left corner of the screen
            ScreenCoordinate = new Point
            {
                X = (int)(vertexViewSpace.x * (camera.Width / 2) + (camera.Width / 2)),
                Y = (int)(-vertexViewSpace.y * (camera.Height / 2) + (camera.Height / 2))
            };
        }
        #endregion

        #region Static Methods

        /// <summary>
        /// Converts the first column of a matrix into a vertex.
        /// </summary>
        /// <param name="m">The matrix to convert.</param>
        /// <returns>The resulting vertex.</returns>
        public static Vertex MatrixToVertex(Matrix m)
        {
            return new Vertex(m[0, 0], m[1, 0], m[2, 0], m[3,0]);
        }

        /// <summary>
        /// Converts a vertex into a 1-width matrix.
        /// </summary>
        /// <param name="v">The vertex to convert.</param>
        /// <returns>The resulting matrix.</returns>
        public static Matrix VertexToMatrix(Vertex v)
        {
            var result =  new Matrix(4,1);

            result[0, 0] = v.x;
            result[1, 0] = v.y;
            result[2, 0] = v.z;
            result[3, 0] = v.w;

            return result;
        }
        #endregion 
    }
}
