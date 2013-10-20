namespace WireframeRenderer
{
    using System;

    /// <summary>
    /// The camera to view the 3D world.
    /// </summary>
    public class Camera
    {
        #region Properties
        /// <summary>
        /// The position of the camera.
        /// </summary>
        public Vector Postion { get; set; }
        
        /// <summary>
        /// The position the camera is looking at.
        /// </summary>
        public Vector LookPoint { get; set; }
        
        /// <summary>
        /// Determines the up direction.
        /// </summary>
        public Vector UpVector { get; set; }

        /// <summary>
        /// The width of the camera view.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// The height of the camera view.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Limit on how far the camera can see.
        /// </summary>
        public double Far { get; set; }

        /// <summary>
        /// Distance from the camera where elements can be seen.
        /// </summary>
        public double Near { get; set; }

        /// <summary>
        /// The field of view of the camera (in degrees).
        /// </summary>
        public double FieldOfView { get; set; }
        
        /// <summary>
        /// The aspect ratio of the camera.
        /// </summary>
        public double AspectRatio { get; set; }

        /// <summary>
        /// The combination of all the transforms.
        /// </summary>
        public Matrix AllTransforms { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of the Camera class.
        /// </summary>
        public Camera()
        {
            Postion = new Vector(0, 50, 0);
            LookPoint = new Vector(400, 0, 400);
            UpVector = new Vector(0, 1, 0);

            Far = -5000;
            Near = -300;
            
            FieldOfView = 90;
            AspectRatio = 16.0/9.0;

            //Convert FoV to radians for use with Math.Tan
            var fovradians = Math.PI/180*(FieldOfView);

            Width = (-2)*Near*Math.Tan(fovradians/2.0);
            Height = Width / AspectRatio;
        }
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
            
            locationTransform[0, 3] = -Postion.x;
            locationTransform[1, 3] = -Postion.y;
            locationTransform[2, 3] = -Postion.z;

            return locationTransform;
        }

        /// <summary>
        /// Calculates the transformation matrix based on where the camera is looking.
        /// </summary>
        /// <returns>The resulting transform matrix.</returns>
        public Matrix LookTransform()
        {
            // Direction
            var x = LookPoint.x - Postion.x;
            var y = LookPoint.y - Postion.y;
            var z = LookPoint.z - Postion.z;
            var direction = new Vector(x, y, z);

            // n, u, v - vectors
            var nVector = direction.Normalise();
            var uVector = Vector.CrossProduct(UpVector, nVector).Normalise();
            var vVector = Vector.CrossProduct(nVector, uVector).Normalise();

            // Resultmatrix
            var resultMatrix = new Matrix(4, 4);
            resultMatrix[0, 0] = uVector.x;
            resultMatrix[0, 1] = uVector.y;
            resultMatrix[0, 2] = uVector.z;

            resultMatrix[1, 0] = vVector.x;
            resultMatrix[1, 1] = vVector.y;
            resultMatrix[1, 2] = vVector.z;

            resultMatrix[2, 0] = nVector.x;
            resultMatrix[2, 1] = nVector.y;
            resultMatrix[2, 2] = nVector.z;

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
            
            resultMatrix[0, 0] = (2.0*Near)/Width;
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
        /// <param name="x">Amount to move on x-axis.</param>
        /// <param name="y">Amount to move on y-axis.</param>
        /// <param name="z">Amount to move on z-axis.</param>
        public void Move(double x, double y, double z)
        {
            Postion.x += x;
            Postion.y += y;
            Postion.z += z;
        }

        /// <summary>
        /// Moves the point the camera is looking at according to the input values.
        /// </summary>
        /// <param name="x">Amount to move on the x-axis.</param>
        /// <param name="y">Amount to move on the y-axis.</param>
        /// <param name="z">Amount to move on the z-axis.</param>
        public void LookMove(double x, double y, double z)
        {
            LookPoint.x += x;
            LookPoint.y += y;
            LookPoint.z += z;
        }
        #endregion
    }
}
