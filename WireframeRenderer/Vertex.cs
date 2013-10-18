using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WireframeRenderer
{
    public class Vertex
    {
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
        public Point screenCoordinate { get; set; }
    }
}
