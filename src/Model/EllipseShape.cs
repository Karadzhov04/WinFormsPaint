using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    internal class EllipseShape : Shape
    {
        #region Constructor

        public EllipseShape(RectangleF rect) : base(rect)
        {
        }

        #endregion

        public override bool Contains(PointF point)
        {
            //TODO
            float a = Width / 2;
            float b = Height / 2;

            float xc = Location.X + a;
            float yc = Location.Y + b;

            PointF[] pointsToConvert = new PointF[] { point };
            Rotation.Invert();
            Rotation.TransformPoints(pointsToConvert);
            Rotation.Invert();
            PointF localPoint = pointsToConvert[0];

            return Math.Pow((localPoint.X - xc) / a, 2) + Math.Pow((localPoint.Y - yc) / b, 2) - 1 <= 0;
        }
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            ChangeSize(Scale);

            Pen pen = new Pen(StrokeColor, Stroke);
            Color color = Color.FromArgb(255 - Transparency, FillColor);
            RectangleF ellipse = new RectangleF(Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

            //Rectangle rect = new Rectangle(50, 50, 100, 100);
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(ellipse);
            //path.AddLine(0, 0, 200, 200);
            //path.AddRectangle(rect);

            // 2. Създаване на PathGradientBrush на базата на този път
            PathGradientBrush brush = new PathGradientBrush(path);

            // 3. Определяне на цветовете на градиента
            brush.CenterColor = Color.Yellow;  // Централен цвят
            brush.SurroundColors = new Color[] { Color.Red }; // Цветове по краищата

            Matrix oldTransform = grfx.Transform;

            // Прилагаме ротация само на тази фигура
            grfx.Transform = Rotation;

            grfx.FillEllipse(brush, ellipse);
            grfx.DrawEllipse(pen, ellipse);

            // Връщаме оригиналната трансформация, за да не влияе на другите фигури
            grfx.Transform = oldTransform;

        }
        public override void ChangeSize(float scale)
        {
            Width = OriginalWidth + scale;
            Height = OriginalHeight + scale;
        }
    }
}
