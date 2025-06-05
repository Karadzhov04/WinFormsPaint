using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    [Serializable]
    public class PointShape : Shape
    {
        #region Constructor
        public PointShape() { }
        public PointShape(RectangleF rect) : base(rect) { }
        public PointShape(PointShape rect) : base(rect) { }


        #endregion
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            Matrix oldTransform = grfx.Transform;

            grfx.TranslateTransform(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height / 2);
            grfx.RotateTransform(RotateDegree);
            ChangeSize(Scale);
            //grfx.ScaleTransform(Scale, Scale);
            grfx.TranslateTransform(-2.5f, -2.5f); // малко кръгче 5x5

            Color color = Color.FromArgb(255 - Transparency, FillColor);
            grfx.FillEllipse(new SolidBrush(color), 0, 0, 5, 5);

            grfx.Transform = oldTransform;
        }

        public override void ChangeSize(float scale)
        {
            float newSize = OriginalWidth + scale; // Нов размер (широчина = височина)

            // Запазваме центъра на точката
            float centerX = Rectangle.X + Rectangle.Width / 2;
            float centerY = Rectangle.Y + Rectangle.Height / 2;

            // Преизчисляваме X и Y така, че новият правоъгълник да остане центриран
            Rectangle = new RectangleF(
                centerX - newSize / 2,  // Нов X
                centerY - newSize / 2,  // Нов Y
                newSize, newSize        // Нова широчина и височина
            );
        }

		public override bool Contains(PointF point)
		{
			float radius = 4; // визуален радиус на точката, например 4 пиксела

			// Създаваме матрица на трансформация: ротация + позиция
			Matrix transform = this.Rotation.Clone(); // Rotation идва от базовия клас
			transform.Translate(this.Location.X, this.Location.Y, MatrixOrder.Append);

			// Инвертираме трансформацията, за да върнем точката в локални координати
			transform.Invert();

			PointF[] pts = new PointF[] { point };
			transform.TransformPoints(pts); // трансформираме кликнатата точка

			// Проверяваме дали тя попада в кръгче около (0,0) с даден радиус
			float dx = pts[0].X;
			float dy = pts[0].Y;
			return dx * dx + dy * dy <= radius * radius;
		}

        public override Shape Clone()
        {
            return new PointShape(this);
        }
    }
}

