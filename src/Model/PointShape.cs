using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    internal class PointShape : Shape
    {
        #region Constructor

        public PointShape(RectangleF rect) : base(rect) { }


        #endregion

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            ChangeSize(Scale);
            //Pen pen = new Pen(StrokeColor, Stroke);
            Color color = Color.FromArgb(255 - Transparency, FillColor);


			Matrix oldTransform = grfx.Transform;

			// Прилагаме ротация само на тази фигура
			grfx.Transform = Rotation;

			grfx.FillEllipse(new SolidBrush(color), Rectangle);
			// Връщаме оригиналната трансформация, за да не влияе на другите фигури
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
	}
}

