using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    public class StarShape : Shape
    {
        #region Constructor

        public StarShape(RectangleF rect) : base(rect)
        {
        }

        #endregion
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            PointF[] starPoints = GetStarPoints(Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            grfx.FillPolygon(new SolidBrush(FillColor), starPoints);
            grfx.DrawPolygon(Pens.Black, starPoints);

            PointF center = new PointF(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height / 2);
            for (int i = 0; i < starPoints.Length; i++)
            {
                grfx.DrawLine(Pens.Black, center, starPoints[i]);
            }

        }
        private PointF[] GetStarPoints(float x, float y, float width, float height)
        {
            List<PointF> points = new List<PointF>();

            int numPoints = 10;

            // Център на четириъгълника
            float cx = x + width / 2;
            float cy = y + height / 2;

            double angle = -Math.PI / 2; // Започваме от горния връх
            double angleStep = (2 * Math.PI) / numPoints; // 2 * pi / (2 * numPoints) 

            float outerRadius = width / 2;
            float innerRadius = outerRadius / 2.5f;

            for (int i = 0; i < numPoints; i++)
            {
                float radius = (i % 2 == 0) ? outerRadius : innerRadius;
                float currentX = cx + (float)(Math.Cos(angle) * radius);// защото почваме от външната горна точка
                float currentY = cy + (float)(Math.Sin(angle) * radius);// защото почваме от външната горна точка
                PointF currPoint = new PointF(currentX, currentY);
                points.Add(currPoint);
                angle += angleStep;
            }

            return points.ToArray();
        }
    }
}
