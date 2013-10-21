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
        /// Initializes a new instance of the <see cref="Vector"/> class. 
        /// </summary>
        /// <param name="x">The X-coordinate of the vector.</param>
        /// <param name="y">The Y-coordinate of the vector.</param>
        /// <param name="z">The Z-coordinate of the vector.</param>
        public Vector(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the X value.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y value.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the Z value.
        /// </summary>
        public double Z { get; set; }
        #endregion

        #region Static Methods
        /// <summary>
        /// Cross product of two vectors.
        /// </summary>
        /// <param name="first">First vector.</param>
        /// <param name="second">Second vector.</param>
        /// <returns>The vector contained the cross product.</returns>
        public static Vector CrossProduct(Vector first, Vector second)
        {
            var a = (first.Y * second.Z) - (first.Z * second.Y);
            var b = (first.Z * second.X) - (first.X * second.Z);
            var c = (first.X * second.Y) - (first.Y * second.X);

            return new Vector(a, b, c);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Calculates the length of the vector.
        /// </summary>
        /// <returns>The length of the vector.</returns>
        public double Length()
        {
            return Math.Sqrt(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2) + Math.Pow(this.Z, 2));
        }

        /// <summary>
        /// Normalizes the vector.
        /// </summary>
        /// <returns>The vector normalized.</returns>
        public Vector Normalise()
        {
            var length = Length();
            var vector = new Vector(this.X / length, this.Y / length, this.Z / length);
            
            return vector;
        }
        #endregion
    }
}