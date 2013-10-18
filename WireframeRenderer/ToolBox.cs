using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WireframeRenderer
{
    /// <summary>
    /// Contains methods helpful to the wireframe renderer calculations.
    /// </summary>
    class ToolBox
    {
        /// <summary>
        /// Multiplies a matrix with another.
        /// Source: http://dev.bratched.com/en/fun-with-matrix-multiplication-and-unsafe-code/
        /// </summary>
        /// <param name="m1">First matrix.</param>
        /// <param name="m2">Second matrix.</param>
        /// <returns>The resulting matrix.</returns>
        public static Matrix NaiveMultiplication(Matrix m1, Matrix m2)
        {
            var resultMatrix = new Matrix(m1.Height, m2.Width);
            for (var i = 0; i < resultMatrix.Height; i++)
            {
                for (var j = 0; j < resultMatrix.Width; j++)
                {
                    resultMatrix[i, j] = 0;
                    for (var k = 0; k < m1.Width; k++)
                    {
                        resultMatrix[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return resultMatrix;
        }

        /// <summary>
        /// Calculations the camera location transform matrix.
        /// </summary>
        /// <returns>The resulting matrix.</returns>
        public static Matrix CameraLocationTransform()
        {
            var cameraLocation = WireframeRenderer.cameraPosition;
            var cameraLocationTransform = new Matrix(4, 4);
            cameraLocationTransform[0, 0] = 1;
            cameraLocationTransform[1, 1] = 1;
            cameraLocationTransform[2, 2] = 1;
            cameraLocationTransform[3, 3] = 1;
            cameraLocationTransform[3, 0] = -cameraLocation[0];
            cameraLocationTransform[3, 1] = -cameraLocation[1];
            cameraLocationTransform[3, 2] = -cameraLocation[2];

            return cameraLocationTransform;
        }

        public static Matrix CameraLookTransform()
        {
            var cameraLocation = WireframeRenderer.cameraPosition;
            var cameraLookPoint = WireframeRenderer.cameraLookPoint;
            
            // Direction
            var direction = new double[3];
            direction[0] = cameraLookPoint[0] - cameraLocation[0];
            direction[1] = cameraLookPoint[1] - cameraLocation[1];
            direction[2] = cameraLookPoint[2] - cameraLocation[2];

            // Normalise for z-axis
            var normalVector = NormaliseVector(direction, VectorLength(direction));

            // x-axis
            var up = WireframeRenderer.up;
            var cp = CrossProduct(up, normalVector);
            var uVector = NormaliseVector(cp, VectorLength(cp));

            // y-axis
            var vVector = CrossProduct(normalVector, uVector);

            var resultMatrix = new Matrix(4, 4);
            resultMatrix[0, 0] = uVector[0];
            resultMatrix[0, 1] = uVector[1];
            resultMatrix[0, 2] = uVector[2];
            resultMatrix[1, 0] = vVector[0];
            resultMatrix[1, 1] = vVector[1];
            resultMatrix[1, 2] = vVector[2];
            resultMatrix[2, 0] = normalVector[0];
            resultMatrix[2, 1] = normalVector[1];
            resultMatrix[2, 2] = normalVector[2];
            resultMatrix[3, 3] = 1;

            return resultMatrix;
        }

        public static Matrix perspectiveTransform()
        {
            var near = WireframeRenderer.nearView;
            var far = WireframeRenderer.farView;
            var resultMatrix = new Matrix(4, 4);

            var width = -2*near*Math.Tan(WireframeRenderer.fieldOfView/2);
            var height = width/WireframeRenderer.aspectRatio;

            resultMatrix[0, 0] = (2*near) / width;
            resultMatrix[1, 1] = (2*near) / height;
            resultMatrix[2, 2] = -(far+near) / (far-near);
            resultMatrix[2, 3] = (-2 * far * near) / (far-near);
            resultMatrix[3, 3] = -1;

            return resultMatrix;
        }

        public static double VectorLength(double[] vector)
        {
            return Math.Sqrt(Math.Pow(vector[0], 2) + Math.Pow(vector[1], 2) + Math.Pow(vector[2], 2));
        }

        public static double[] NormaliseVector(double[] vector, double length)
        {
            var returnVector = new double[3];
            returnVector[0] = vector[0] / length;
            returnVector[1] = vector[1] / length;
            returnVector[2] = vector[2] / length;

            return returnVector;
        }
            
        public static double[] CrossProduct(double[] aVector, double[] bVector)
        {
            var result = new double[3];
            result[0] = (aVector[1] * bVector[2]) - (aVector[2] * bVector[1]);
            result[1] = (aVector[2] * bVector[0]) - (aVector[0] * bVector[2]);
            result[2] = (aVector[0] * bVector[1]) - (aVector[1] * bVector[0]);
            return result;
        }
    }
}
