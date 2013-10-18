using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WireframeRenderer
{
    public partial class Renderer : Form
    {
        /// <summary>
        /// Painted graphics object.
        /// </summary>
        System.Drawing.Graphics graphicsObj;

        /// <summary>
        /// Pen used to draw
        /// </summary>
        Pen pen = new Pen(System.Drawing.Color.Red, 5);

        public List<Triangle> triangles = new List<Triangle>();

        /// <summary>
        /// Main renderer class.
        /// </summary>
        public Renderer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Paints the wireframe.
        /// </summary>
        /// <param name="sender">Who called the paint event</param>
        /// <param name="e">Arguments</param>
        private void Renderer_Paint(object sender, PaintEventArgs e)
        {
            graphicsObj = this.CreateGraphics();

            foreach (var t in triangles)
            {
                
            }
        }

        /// <summary>
        /// Draws the actual lines of a vertex
        /// </summary>
        /// <param name="v">The vertex to draw.</param>
        private void DrawTriangle(Triangle v)
        {
            graphicsObj.DrawLine(pen, v.a.screenCoordinate, v.b.screenCoordinate);
            graphicsObj.DrawLine(pen, v.a.screenCoordinate, v.c.screenCoordinate);
            graphicsObj.DrawLine(pen, v.b.screenCoordinate, v.c.screenCoordinate);
        }
    }
}
