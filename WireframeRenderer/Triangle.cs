namespace WireframeRenderer
{
    /// <summary>
    /// Triangle used for rendering
    /// </summary>
    public class Triangle
    {
        #region Properties
        /// <summary>
        /// Gets or sets the A vertex.
        /// </summary>
        public Vertex a { get; set; }

        /// <summary>
        /// Gets or sets the B vertex.
        /// </summary>
        public Vertex b { get; set; }

        /// <summary>
        /// Gets or sets the C vertex.
        /// </summary>
        public Vertex c { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of Triangle.
        /// </summary>
        public Triangle(){}

        /// <summary>
        /// Creates a new instance of Triangle.
        /// </summary>
        /// <param name="a">The first vertex of the triangle.</param>
        /// <param name="b">The second vertex of the triangle.</param>
        /// <param name="c">The third vertex of the triangle.</param>
        public Triangle(Vertex a, Vertex b, Vertex c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates the triangle for use with the camera.
        /// </summary>
        public void Update(Camera camera)
        {
            a.Update(camera);
            b.Update(camera);
            c.Update(camera);
        }
        #endregion
    }
}
