namespace WireframeRenderer
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

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
            KeyPress += Renderer_KeyPress;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Paints the wireframe.
        /// </summary>
        /// <param name="sender">Who called the paint event</param>
        /// <param name="e">Arguments</param>
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
        private void Renderer_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Escape makes the program stop
            if (e.KeyChar == 27)
            {
                Application.Exit();
            }

            #region Move
            // Left on x-axis.
            if (e.KeyChar == 'a') camera.Move(-10, 0, 0);
            // Right on x-axis.
            if (e.KeyChar == 'd') camera.Move(10, 0, 0);
            // Down on y-axis.
            if (e.KeyChar == 's') camera.Move(0, -10, 0);
            // Up on y-axis. 
            if (e.KeyChar == 'w') camera.Move(0, 10, 0);
            // "Backwards" on the z-axis.
            if (e.KeyChar == 'q') camera.Move(0, 0, -10);
            // "Forwards" on the z-axis.
            if (e.KeyChar == 'e') camera.Move(0, 0, 10);
            #endregion

            #region LookChange
            // Left on the x-axis.
            if (e.KeyChar == 'j') camera.LookMove(-10, 0, 0);
            // Right on x-axis.
            if (e.KeyChar == 'l') camera.LookMove(10, 0, 0);
            // Down on y-axis.
            if (e.KeyChar == 'k') camera.LookMove(0, -10, 0);
            // Up on y-axis. 
            if (e.KeyChar == 'i') camera.LookMove(0, 10, 0);
            // "Backwards" on the z-axis.
            if (e.KeyChar == 'u') camera.LookMove(0, 0, -10);
            // "Forwards" on the z-axis.
            if (e.KeyChar == 'o') camera.LookMove(0, 0, 10);
            #endregion

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
