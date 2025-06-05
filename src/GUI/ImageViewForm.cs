using System;
using System.Windows.Forms;
using System.Drawing;
using Draw;
using Draw.src.Model;
using System.Collections.Generic;
using System.Drawing.Drawing2D; // ако името на пространството с DialogProcessor е различно, смени го

namespace Draw.src.GUI
{
    public partial class ImageViewForm : Form
    {
        private DialogProcessor sharedProcessor;
        private Panel viewPort;

        //public ImageViewForm()
        //{
        //    InitializeComponent();
        //}
        public ImageViewForm(DialogProcessor processor)
        {
            sharedProcessor = processor;
            this.DoubleBuffered = true;

            // Настройки на формата
            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Text = "Допълнителен изглед";

            // Създаваме и конфигурираме viewPort панела
            viewPort = new Panel();
            viewPort.Dock = DockStyle.Fill;
            viewPort.BackColor = Color.White; // Или друг цвят за тестване
            viewPort.Paint += ViewPortPaint;
            viewPort.MouseDown += ViewPortMouseDown;
            viewPort.MouseMove += ViewPortMouseMove;
            viewPort.MouseUp += ViewPortMouseUp;

            this.Controls.Add(viewPort);

            // За реакция при промяна в модела
            sharedProcessor.ModelChanged += (s, e) => viewPort.Invalidate();
        }


        void ViewPortPaint(object sender, PaintEventArgs e)
        {

            sharedProcessor.ReDraw(sender, e);
        }

        void ViewPortMouseDown(object sender, MouseEventArgs e)
        {
            //if (!pickUpSpeedButton.Checked) return;

            Shape clickedShape = sharedProcessor.FindDeepestShapeAt(e.Location);

            if (clickedShape != null)
            {
                // Клик върху фигура
                if (Control.ModifierKeys == Keys.Control)
                {
                    if (sharedProcessor.Selection.Contains(clickedShape))
                        sharedProcessor.Selection.Remove(clickedShape);
                    else
                        sharedProcessor.Selection.Add(clickedShape);
                }
                else
                {
                    sharedProcessor.Selection.Clear();
                    sharedProcessor.Selection.Add(clickedShape);
                }

                sharedProcessor.IsDragging = true;
                sharedProcessor.LastLocation = e.Location;
            }
            else
            {
                // Клик на празно място
                if (sharedProcessor.Selection.Count > 0)
                {
                    // Проверка дали е вътре в общия обхващащ правоъгълник на селекцията
                    RectangleF selectionBounds = GetSelectionBounds(sharedProcessor.Selection);

                    if (selectionBounds.Contains(e.Location))
                    {
                        sharedProcessor.IsDragging = true;
                        sharedProcessor.LastLocation = e.Location;
                        return;
                    }
                }

                // Няма фигура и не е вътре в селекцията → започваме нова селекция
                sharedProcessor.Selection.Clear();
                sharedProcessor.StartSelectionPoint = e.Location;
                sharedProcessor.IsMarqueeSelecting = true;
            }

            viewPort.Invalidate();

            //if (sharedProcessor.Selection.Count > 0)
            //{
            //    statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
            //    LoadShapeProperties(sharedProcessor.Selection[0]);
            //}
        }
        private RectangleF GetSelectionBounds(List<Shape> selection)
        {
            if (selection == null || selection.Count == 0)
                return RectangleF.Empty;

            float minX = float.MaxValue, minY = float.MaxValue;
            float maxX = float.MinValue, maxY = float.MinValue;

            foreach (var shape in selection)
            {
                RectangleF r = shape.Rectangle;
                if (r.Left < minX) minX = r.Left;
                if (r.Top < minY) minY = r.Top;
                if (r.Right > maxX) maxX = r.Right;
                if (r.Bottom > maxY) maxY = r.Bottom;
            }

            return new RectangleF(minX + 5, minY + 5, maxX - minX + 5, maxY - minY + 5);
        }

        void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (sharedProcessor.IsDragging && sharedProcessor.Selection.Count > 0)
            {
                //statusBar.Items[0].Text = "Последно действие: Влачене";
                if (sharedProcessor.Selection.Count == 1 && sharedProcessor.Selection[0] is GroupShape group)
                {
                    sharedProcessor.TranslateGroupTo(group, e.Location);
                }
                else
                {
                    sharedProcessor.TranslateTo(e.Location);
                }
                viewPort.Invalidate();
            }

            if (sharedProcessor.IsMarqueeSelecting)
            {
                Point currentPoint = e.Location;
                int x = Math.Min(sharedProcessor.StartSelectionPoint.X, currentPoint.X);
                int y = Math.Min(sharedProcessor.StartSelectionPoint.Y, currentPoint.Y);
                int width = Math.Abs(sharedProcessor.StartSelectionPoint.X - currentPoint.X);
                int height = Math.Abs(sharedProcessor.StartSelectionPoint.Y - currentPoint.Y);

                sharedProcessor.SelectionRectangle = new Rectangle(x, y, width, height);
                viewPort.Invalidate(); // презарежда рисуването
            }

        }
        /// <summary>
        /// Прихващане на отпускането на бутона на мишката.
        /// Излизаме от режим "влачене".
        /// </summary>
        void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (sharedProcessor.IsMarqueeSelecting)
            {
                sharedProcessor.Selection.Clear();

                foreach (var shape in sharedProcessor.ShapeList)
                {
                    if (sharedProcessor.SelectionRectangle.Contains(Rectangle.Round(shape.Rectangle)))
                    {
                        sharedProcessor.Selection.Add(shape);
                    }
                }

                sharedProcessor.IsMarqueeSelecting = false;
                viewPort.Invalidate();
            }

            sharedProcessor.IsDragging = false;
        }
    }
}
