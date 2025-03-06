using System;
using System.Collections.Generic;
using System.Drawing;
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

            //Pen pen = new Pen(StrokeColor, Stroke);
            Color color = Color.FromArgb(255 - Transparency, FillColor);

            grfx.FillEllipse(new SolidBrush(color), Rectangle);
        }
    }
}
