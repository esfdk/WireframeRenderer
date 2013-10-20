namespace WireframeRenderer
{
    using System.Collections.Generic;

    /// <summary>
    /// Loads the triangles for the camera to use.
    /// </summary>
    class Loader
    {
        #region Static Methods
        /// <summary>
        /// Loads a standard box shape.
        /// </summary>
        /// <returns>A list of triangles making up a box.</returns>
        public static List<Triangle> Box()
        {
            // TODO: Actually implement
            return new List<Triangle>();
        }

        /// <summary>
        /// Loads a standard pyramid shape.
        /// </summary>
        /// <returns>A list of triangles making up a pyramid.</returns>
        public static List<Triangle> Pyramid()
        {
            var pyramidTriangles = new List<Triangle>();

            const int near = 50;
            const int mid = 100;
            const int far = 150;
            const int height = 75;
            
            var v1 = new Vertex(near, 0, near);
            var v2 = new Vertex(far, 0, near);
            var v3 = new Vertex(near, 0, far);
            var v4 = new Vertex(far, 0, far);
            var v5 = new Vertex(mid, height, mid);

            var t1 = new Triangle(v1, v2, v5);
            var t2 = new Triangle(v1, v3, v5);
            var t3 = new Triangle(v2, v4, v5);
            var t4 = new Triangle(v3, v4, v5);
        
            pyramidTriangles.Add(t1);
            pyramidTriangles.Add(t2);
            pyramidTriangles.Add(t3);
            pyramidTriangles.Add(t4);

            return pyramidTriangles;
        }

        /// <summary>
        /// Reads a file to load in triangles.
        /// </summary>
        /// <param name="filePath">The path to the file that needs to be loaded.</param>
        /// <returns>A list of triangles loaded from the specified file.</returns>
        public static List<Triangle> LoadFromFile(string filePath)
        {
            // TODO: Actually implement
            return new List<Triangle>();
        }
        #endregion 
    }
}
