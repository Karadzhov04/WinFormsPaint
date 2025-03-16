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

            ChangeSize(Scale);
            //Pen pen = new Pen(StrokeColor, Stroke);
            Color color = Color.FromArgb(255 - Transparency, FillColor);

            grfx.FillEllipse(new SolidBrush(color), Rectangle);
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
    }
}

