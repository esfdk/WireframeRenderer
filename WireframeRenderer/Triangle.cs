using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WireframeRenderer
{
    /// <summary>
    /// Triangle used for rendering
    /// </summary>
    public class Triangle
    {
        public Vertex a { get; set; }

        public Vertex b { get; set; }

        public Vertex c { get; set; }
    }
}
