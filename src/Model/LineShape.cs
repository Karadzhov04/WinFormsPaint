using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    internal class LineShape : Shape
    {
        #region Constructor

        public LineShape(RectangleF rect) : base(rect) { }

        #endregion

        public override bool Contains(PointF point)
        {
            if (base.Contains(point))
                // Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
                // В случая на правоъгълник - директно връщаме true
                return true;
            else
                // Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
                return false;
        }
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            ChangeSize(Scale);

            Matrix oldTransform = grfx.Transform;
            // Прилагаме ротация само на тази фигура
            grfx.Transform = Rotation;

            Pen pen = new Pen(StrokeColor, Stroke);
            grfx.DrawLine(pen, Rectangle.Location,
                new PointF(Rectangle.Right, Rectangle.Top));

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
