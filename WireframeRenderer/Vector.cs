namespace WireframeRenderer
{
    using System;

    /// <summary>
    /// A class that represents a 3D vector.
    /// </summary>
    public class Vector
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of Vector.
        /// </summary>
        /// <param name="x">The x-coordinate of the vector.</param>
        /// <param name="y">The y-coordinate of the vector.</param>
        /// <param name="z">The z-coordinate of the vector.</param>
        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the X value.
        /// </summary>
        public double x { get; set; }

        /// <summary>
        /// Gets or sets the Y value.
        /// </summary>
        public double y { get; set; }

        /// <summary>
        /// Gets or sets the Z value.
        /// </summary>
        public double z { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Calculates the length of the vector.
        /// </summary>
        /// <returns>The length of the vector.</returns>
        public double Length()
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        }

        /// <summary>
        /// Normalises the vector.
        /// </summary>
        /// <returns>The vector normalised.</returns>
        public Vector Normalise()
        {
            var length = Length();
            var vector = new Vector(x / length, y / length, z / length);
            
            return vector;
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Crossproduct of two vectors.
        /// </summary>
        /// <param name="aVector">First vector.</param>
        /// <param name="bVector">Second vector.</param>
        /// <returns>The vector contained the cross product.</returns>
        public static Vector CrossProduct(Vector aVector, Vector bVector)
        {
            var a = (aVector.y * bVector.z) - (aVector.z * bVector.y);
            var b = (aVector.z * bVector.x) - (aVector.x * bVector.z);
            var c = (aVector.x * bVector.y) - (aVector.y * bVector.x);
            
            return new Vector(a, b, c);
        }
        #endregion
    }
}