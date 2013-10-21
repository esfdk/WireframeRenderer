namespace WireframeRenderer
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// The wireframe renderer.
    /// </summary>
    public partial class Renderer : Form
    {
        #region Fields
        /// <summary>
        /// Painted graphics object.
        /// </summary>
        private readonly Graphics graphicsObj;

        /// <summary>
        /// The camera to paint.
        /// </summary>
        private readonly Camera camera;

        /// <summary>
        /// Pen used to draw.
        /// </summary>
        private readonly Pen pen = new Pen(Color.Red, 5);

        /// <summary>
        /// The list of triangles 
        /// </summary>
        private readonly List<Triangle> triangles = new List<Triangle>();
        #endregion 

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Renderer"/> class. 
        /// Main renderer class.
        /// </summary>
        public Renderer()
        {
            InitializeComponent();
            
            camera = new Camera();

            Width = (int)camera.Width;
            Height = (int)camera.Height;

            graphicsObj = CreateGraphics();

            triangles = Loader.Pyramid();

            KeyPreview = true;
            KeyPress += this.RendererKeyPress;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Paints the wireframe.
        /// </summary>
        /// <param name="sender">Who called the paint event.</param>
        /// <param name="e">The arguments.</param>
        private void Renderer_Paint(object sender, PaintEventArgs e)
        {
            camera.CalculateTransforms();

            foreach (var t in triangles)
            {
                t.Update(camera);
                DrawTriangle(t);
            }
        }

        /// <summary>
        /// Handles keyboard input.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The arguments.</param>
        private void RendererKeyPress(object sender, KeyPressEventArgs e)
        {
            // Escape makes the program stop
            if (e.KeyChar == 27)
            {
                Application.Exit();
            }

            switch (e.KeyChar)
            {
                // Movement of camera.
                case 'a': // Left on X-axis.
                    camera.Move(-10, 0, 0);
                    break;
                case 'd': // Right on X-axis.
                    camera.Move(10, 0, 0);
                    break;
                case 's': // Down on Y-axis.
                    camera.Move(0, -10, 0);
                    break;
                case 'w': // Up on Y-axis. 
                    camera.Move(0, 10, 0);
                    break;
                case 'q': // Left on the Z-axis.
                    camera.Move(0, 0, -10);
                    break;
                case 'e': // Right on the Z-axis.
                    camera.Move(0, 0, 10);
                    break;

                // Movement of the lookpoint
                case 'j': // Left on the X-axis.
                    camera.LookMove(-10, 0, 0);
                    break;
                case 'l': // Right on X-axis.
                    camera.LookMove(10, 0, 0);
                    break;
                case 'k': // Down on Y-axis.
                    camera.LookMove(0, -10, 0);
                    break;
                case 'i': // Up on Y-axis. 
                    camera.LookMove(0, 10, 0);
                    break;
                case 'u': // Left on the Z-axis.
                    camera.LookMove(0, 0, -10);
                    break;
                case 'o': // Right on the Z-axis.
                    camera.LookMove(0, 0, 10);
                    break;
            }

            Invalidate();
        }

        /// <summary>
        /// Draws the actual lines of a triangle
        /// </summary>
        /// <param name="t">The triangle to draw.</param>
        private void DrawTriangle(Triangle t)
        {
            graphicsObj.DrawLine(pen, t.a.ScreenCoordinate, t.b.ScreenCoordinate);
            graphicsObj.DrawLine(pen, t.a.ScreenCoordinate, t.c.ScreenCoordinate);
            graphicsObj.DrawLine(pen, t.b.ScreenCoordinate, t.c.ScreenCoordinate);
        }
        #endregion 
    }
}
