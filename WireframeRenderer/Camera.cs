namespace WireframeRenderer
{
    using System;

    /// <summary>
    /// The camera to view the 3D world.
    /// </summary>
    public class Camera
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class. 
        /// </summary>
        public Camera()
        {
            Postion = new Vector(15, 20, 5);
            LookPoint = new Vector(400, 0, 400);
            UpVector = new Vector(0, 1, 0);

            Far = -5000;
            Near = -300;

            FieldOfView = 90;
            AspectRatio = 16.0 / 9.0;

            // Convert FoV to radians for use with Math.Tan
            var fovradians = Math.PI / 180 * FieldOfView;

            Width = (-2) * Near * Math.Tan(fovradians / 2.0);
            Height = Width / AspectRatio;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the position of the camera.
        /// </summary>
        public Vector Postion { get; set; }
        
        /// <summary>
        /// Gets or sets the position the camera is looking at.
        /// </summary>
        public Vector LookPoint { get; set; }
        
        /// <summary>
        /// Gets or sets the up direction.
        /// </summary>
        public Vector UpVector { get; set; }

        /// <summary>
        /// Gets or sets the width of the camera view.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the camera view.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the limit on how far the camera can see.
        /// </summary>
        public double Far { get; set; }

        /// <summary>
        /// Gets or sets the distance from the camera where elements can be seen.
        /// </summary>
        public double Near { get; set; }

        /// <summary>
        /// Gets or sets the field of view of the camera (in degrees).
        /// </summary>
        public double FieldOfView { get; set; }
        
        /// <summary>
        /// Gets or sets the aspect ratio of the camera.
        /// </summary>
        public double AspectRatio { get; set; }

        /// <summary>
        /// Gets or sets the combination of all the transforms.
        /// </summary>
        public Matrix AllTransforms { get; set; }
        #endregion

        #region Transforms
        /// <summary>
        /// Calculates the transformation matrix for the camera's position.
        /// </summary>
        /// <returns>The resulting transformation matrix.</returns>
        public Matrix LocationTransform()
        {
            var locationTransform = new Matrix(4, 4);
            locationTransform[0, 0] = 1;
            locationTransform[1, 1] = 1;
            locationTransform[2, 2] = 1;
            locationTransform[3, 3] = 1;
            
            locationTransform[0, 3] = -Postion.X;
            locationTransform[1, 3] = -Postion.Y;
            locationTransform[2, 3] = -Postion.Z;

            return locationTransform;
        }

        /// <summary>
        /// Calculates the transformation matrix based on where the camera is looking.
        /// </summary>
        /// <returns>The resulting transform matrix.</returns>
        public Matrix LookTransform()
        {
            // Direction
            var x = LookPoint.X - Postion.X;
            var y = LookPoint.Y - Postion.Y;
            var z = LookPoint.Z - Postion.Z;
            var direction = new Vector(x, y, z);

            // n, u, v - vectors
            var nVector = direction.Normalise();
            var uVector = Vector.CrossProduct(UpVector, nVector).Normalise();
            var vVector = Vector.CrossProduct(nVector, uVector).Normalise();

            // Resultmatrix
            var resultMatrix = new Matrix(4, 4);
            resultMatrix[0, 0] = uVector.X;
            resultMatrix[0, 1] = uVector.Y;
            resultMatrix[0, 2] = uVector.Z;

            resultMatrix[1, 0] = vVector.X;
            resultMatrix[1, 1] = vVector.Y;
            resultMatrix[1, 2] = vVector.Z;

            resultMatrix[2, 0] = nVector.X;
            resultMatrix[2, 1] = nVector.Y;
            resultMatrix[2, 2] = nVector.Z;

            resultMatrix[3, 3] = 1;

            return resultMatrix;
        }

        /// <summary>
        /// Calculates the transformation matrix based on the camera perspective.
        /// </summary>
        /// <returns>The resulting transform matrix.</returns>
        public Matrix PerspectiveTransform()
        {
            var resultMatrix = new Matrix(4,4);

            resultMatrix[0, 0] = (2.0 * Near) / Width;
            resultMatrix[1, 1] = (2.0 * Near) / Height;
            resultMatrix[2, 2] = (-(Far + Near)) / (Far - Near);
            resultMatrix[2, 3] = (-2.0 * Far * Near) / (Far - Near);
            resultMatrix[3, 2] = -1;

            return resultMatrix;
        }

        /// <summary>
        /// Multiplies perspective transform, look transform and location transforms.
        /// </summary>
        /// <returns>The resulting matrix.</returns>
        public Matrix CalculateTransforms()
        {
            var m = Matrix.NaiveMultiplication(PerspectiveTransform(), LookTransform());
            m = Matrix.NaiveMultiplication(m, LocationTransform());

            AllTransforms = m;

            return m;
        }
        #endregion

        #region Rotation and move
        /// <summary>
        /// Moves the position of the camera according to input values.
        /// </summary>
        /// <param name="x">Amount to move on X-axis.</param>
        /// <param name="y">Amount to move on Y-axis.</param>
        /// <param name="z">Amount to move on Z-axis.</param>
        public void Move(double x, double y, double z)
        {
            Postion.X += x;
            Postion.Y += y;
            Postion.Z += z;
        }

        /// <summary>
        /// Moves the point the camera is looking at according to the input values.
        /// </summary>
        /// <param name="x">Amount to move on the X-axis.</param>
        /// <param name="y">Amount to move on the Y-axis.</param>
        /// <param name="z">Amount to move on the Z-axis.</param>
        public void LookMove(double x, double y, double z)
        {
            LookPoint.X += x;
            LookPoint.Y += y;
            LookPoint.Z += z;
        }
        #endregion
    }
}
