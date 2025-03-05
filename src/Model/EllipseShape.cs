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

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

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

            grfx.FillEllipse(brush, ellipse);
            grfx.DrawEllipse(pen, ellipse);

        }
    }
}
