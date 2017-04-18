using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;

namespace Peržiūros_Programa
{
    public class CanvasResizer
    {
        private PictureBox canvas;

        private int imageWidth;
        private int imageHeight;

        private int viewportWidth;
        private int viewportHeight;

        private Matrix<float> window;
        private Matrix<float> projection;
        private Matrix<float> windowProjection;
        private Matrix<float> inverseWindowProjection;

        public CanvasResizer(PictureBox canvas,
                             int imageWidth, int imageHeight,
                             int viewportWidth, int viewportHeight)
        {
            this.canvas = canvas;

            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;

            this.viewportWidth = viewportWidth;
            this.viewportHeight = viewportHeight;

            window = Matrix<float>.Build.DenseOfRowArrays(new float[][]{
                new[] {(float) viewportWidth / 2.0f, 0.0f, (float) viewportWidth / 2.0f},
                new[] {0.0f, (float) viewportHeight / -2.0f, (float) viewportHeight / 2.0f},
                new[] {0.0f, 0.0f, 1.0f}
            });

            projection = Matrix<float>.Build.DenseOfColumnArrays(new float[][]{
                new[] {((float) viewportHeight / (float) viewportWidth), 0.0f, 0.0f},
                new[] {0.0f, 1.0f, 0.0f},
                new[] {0.0f, 0.0f, 1.0f}
            });

            windowProjection = projection * window;
            inverseWindowProjection = windowProjection.Inverse();
        }

        /// <summary>
        /// Multiplies the current scale by the given <paramref name="scale"/>
        /// parameter at the origin point.
        /// </summary>
        /// <param name="scale"> A scaling factor. </param>
        /// <param name="originX"> Scaling origin X coordinate in form space. </param>
        /// <param name="originY"> Scaling origin Y coordinate in form space. </param>
        public void Resize(float scale, int originX, int originY)
        {
            Vector<float> origin = Unproject(originX, originY);

            Matrix<float> scalingMatrix
                = Matrix<float>.Build.DenseOfRowArrays(new float[][]{
                    new[] {scale, 0.0f, 0.0f},
                    new[] {0.0f, scale, 0.0f},
                    new[] {0.0f, 0.0f, 1.0f}});

            Matrix<float> translationMatrix
                = Matrix<float>.Build.DenseOfRowArrays(new float[][]{
                    new[] {1.0f, 0.0f, origin.At(0)},
                    new[] {0.0f, 1.0f, origin.At(1)},
                    new[] {0.0f, 0.0f, 1.0f}});
            Matrix<float> inverseTranslationMatrix = translationMatrix.Inverse();
            Matrix<float> transformationMatrix = windowProjection * translationMatrix 
                * scalingMatrix * inverseTranslationMatrix;

            Vector<float> topLeft = Unproject(canvas.Left, canvas.Top);
            Vector<float> bottomRight = Unproject(canvas.Right, canvas.Bottom);

            topLeft = transformationMatrix * topLeft;
            bottomRight = transformationMatrix * bottomRight;

            canvas.Left = (int)topLeft.At(0);
            canvas.Top = (int)topLeft.At(1);
            canvas.Width = (int)bottomRight.At(0) - canvas.Left;
            canvas.Height = (int)bottomRight.At(1) - canvas.Top;
        }

        private Vector<float> Unproject(int x, int y)
        {
            return inverseWindowProjection.Multiply(
                Vector<float>.Build.Dense(new float[] { x, y, 1.0f }));
        }
    }
}
