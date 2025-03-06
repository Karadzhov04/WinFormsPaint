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

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            Pen pen = new Pen(StrokeColor, Stroke);
            grfx.DrawLine(pen, Rectangle.Location,
                new PointF(Rectangle.Right, Rectangle.Bottom));
        }
    }
}
