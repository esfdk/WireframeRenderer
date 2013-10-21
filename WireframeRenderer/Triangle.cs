namespace WireframeRenderer
{
    /// <summary>
    /// Triangle used for rendering
    /// </summary>
    public class Triangle
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class. 
        /// </summary>
        public Triangle()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class. 
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

        #region Methods
        /// <summary>
        /// Updates the triangle for use with the camera.
        /// </summary>
        /// <param name="camera">The camera.</param>
        public void Update(Camera camera)
        {
            a.Update(camera);
            b.Update(camera);
            c.Update(camera);
        }
        #endregion
    }
}
