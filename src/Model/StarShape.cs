using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw.src.Model
{
    public class StarShape : Shape
    {
        #region Constructor

        public StarShape(RectangleF rect) : base(rect)
        {
        }

        #endregion

        public override bool Contains(PointF point)
        {
            PointF[] starPoints = GetStarPoints(Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            int intersections = 0;
            int numPoints = starPoints.Length;

            for (int i = 0; i < numPoints; i++)
            {
                PointF p1 = starPoints[i];
                PointF p2 = starPoints[(i + 1) % numPoints];

                // Проверяваме дали лъчът пресича ръба на звездата
                if ((p1.Y > point.Y) != (p2.Y > point.Y))
                {
                    float intersectX = p1.X + (point.Y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y);
                    if (intersectX > point.X)
                    {
                        intersections++;
                    }
                }
            }

            return (intersections % 2) == 1; // Ако броят на пресичанията е нечетен, точката е вътре
        }

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            ChangeSize(Scale);

            PointF[] starPoints = GetStarPoints(Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

            Pen pen = new Pen(StrokeColor, Stroke);
            Color color = Color.FromArgb(255 - Transparency, FillColor);
            Brush brush;

            if (Color1Gradient != Color.Empty && Color2Gradient != Color.Empty)
            {
                PathGradientBrush gradientBrush = new PathGradientBrush(starPoints);
                gradientBrush.CenterColor = Color1Gradient; // Централен цвят
                gradientBrush.SurroundColors = new Color[] { Color2Gradient }; // Външен цвят
                brush = gradientBrush;
            }
            else
            {
                brush = new SolidBrush(color);
            }

            Matrix oldTransform = grfx.Transform;
            // Прилагаме ротация само на тази фигура
            grfx.Transform = Rotation;

            grfx.FillPolygon(brush, starPoints);
            grfx.DrawPolygon(pen, starPoints);

            PointF center = new PointF(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height / 2);
            for (int i = 0; i < starPoints.Length; i++)
            {
                grfx.DrawLine(pen, center, starPoints[i]);
            }

            // Връщаме оригиналната трансформация, за да не влияе на другите фигури
            grfx.Transform = oldTransform;
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

        public override void ChangeSize(float scale)
        {
            Width = OriginalWidth + scale;
            Height = OriginalHeight + scale;
        }
    }
}
