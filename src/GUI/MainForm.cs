using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Security.Cryptography;


namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            this.MouseClick += Form1_MouseClick;
            foreach (Control c in this.Controls)
            {
                c.MouseClick += Form1_MouseClick;
            }



            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        /// <summary>
        /// Изход от програмата. Затваря главната форма, а с това и програмата.
        /// </summary>
        void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		
		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}

        /// <summary>
        /// Бутон, който поставя на произволно място правоъгълник със зададените размери.
        /// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
        /// </summary>
        void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();
			
			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";
			
			viewPort.Invalidate();
		}

        /// <summary>
        /// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
        /// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
        /// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
        /// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
        /// </summary>
        void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (pickUpSpeedButton.Checked)
            {
                Shape temp = dialogProcessor.ContainsPoint(e.Location);
                if (temp != null)
                {
                    if (Control.ModifierKeys == Keys.Control)
                    {
                        // Ако е натиснат Ctrl, добавяме или махаме обект от селекцията
                        if (dialogProcessor.Selection.Contains(temp))
                        {
                            dialogProcessor.Selection.Remove(temp);
                        }
                        else
                        {
                            dialogProcessor.Selection.Add(temp);
                        }
                    }
                    else
                    {
                        // Ако не е натиснат Ctrl, изчистваме старата селекция и избираме нов обект
                        dialogProcessor.Selection.Clear();
                        dialogProcessor.Selection.Add(temp);
                    }
                }
                if (dialogProcessor.Selection.Count > 0)
                {
                    statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
                    dialogProcessor.IsDragging = true;
                    dialogProcessor.LastLocation = e.Location;
                    viewPort.Invalidate();
                }
            }
        }

        /// <summary>
        /// Прихващане на преместването на мишката.
        /// Ако сме в режм на "влачене", то избрания елемент се транслира.
        /// </summary>
        void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dialogProcessor.IsDragging && dialogProcessor.Selection.Count > 0)
            {
                statusBar.Items[0].Text = "Последно действие: Влачене";

                // Транслираме всички селектирани обекти на новата позиция
                dialogProcessor.TranslateTo(e.Location);

                viewPort.Invalidate();
            }
        }


        /// <summary>
        /// Прихващане на отпускането на бутона на мишката.
        /// Излизаме от режим "влачене".
        /// </summary>
        void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}

        private void trackBarStroke_ValueChanged(object sender, EventArgs e)
        {
			if (dialogProcessor.Selection.Count > 0) 
			{

                foreach (Shape shape in dialogProcessor.Selection)
                {
                    shape.Stroke = trackBarStroke.Value;           
                }
                viewPort.Invalidate();
            }

        }

        private void ColorPickerButtonClicked(object sender, EventArgs e)
        {
			if (colorDialog1.ShowDialog() == DialogResult.OK && dialogProcessor.Selection.Count > 0)
			{
                foreach (Shape shape in dialogProcessor.Selection)
                {
                    shape.FillColor = colorDialog1.Color;
                }
				viewPort.Invalidate();
			}
        }

        private void TransparencyButtonClicked(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection.Count > 0)
			{
                foreach (Shape shape in dialogProcessor.Selection)
                {
                    shape.Transparency = trackBarTransparency.Value;
                }
				viewPort.Invalidate();
			}
        }

        private void StrokeColorPickerClicked(object sender, EventArgs e)
        {
			if (colorDialog1.ShowDialog() == DialogResult.OK && dialogProcessor.Selection.Count > 0)
			{
                foreach (Shape shape in dialogProcessor.Selection)
                {
                   shape.StrokeColor = colorDialog1.Color;
                }
				viewPort.Invalidate();
			}
        }

        private void drawElipseSpeedButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomEllipse();

            statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

            viewPort.Invalidate();
        }

        private void drawStarSpeedButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomStar();

            statusBar.Items[0].Text = "Последно действие: Рисуване на звездата";

            viewPort.Invalidate();
        }

        private void GradientColor1PickerClicked(object sender, EventArgs e)
        {
            if (colorDialog3.ShowDialog() == DialogResult.OK && dialogProcessor.Selection.Count > 0)
            {
                foreach (Shape shape in dialogProcessor.Selection)
                {
                    shape.Color1Gradient = colorDialog3.Color;
                }
                viewPort.Invalidate();
            }
        }

        private void GradientColor2PickerClicked(object sender, EventArgs e)
        {
            if (colorDialog3.ShowDialog() == DialogResult.OK && dialogProcessor.Selection.Count > 0)
            {
                foreach (Shape shape in dialogProcessor.Selection)
                    shape.Color2Gradient = colorDialog3.Color;
                viewPort.Invalidate();
            }
        }

        private void drawLineSpeedButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomLine();

            statusBar.Items[0].Text = "Последно действие: Рисуване на линия";

            viewPort.Invalidate();
        }

        private void drawPointSpeedButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomPoint();

            statusBar.Items[0].Text = "Последно действие: Рисуване на точка";

            viewPort.Invalidate();
        }

        private bool isWaitingForClick = false;

        private void CheckPointInPolygonClick(object sender, EventArgs e)
        {
            isWaitingForClick = true;
            MessageBox.Show("Цъкнете някъде в прозореца, за да проверите дали точката е във фигура.");
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isWaitingForClick)
            {
                isWaitingForClick = false; // Спираме режима на изчакване

                PointF mousePoint = e.Location; // Взимаме координатите на кликнатата точка

                //if (dialogProcessor.Selection == null)
                //{
                //    MessageBox.Show("Selection е null преди проверката!");
                //}
                //else
                //{
                //    MessageBox.Show($"Selection НЕ Е null: {dialogProcessor.Selection.ToString()}");
                //}
                // Проверяваме дали точката попада в някоя фигура
                bool insidePoint = false;
                if (dialogProcessor.Selection.Count == 0)
                {
                    MessageBox.Show("Няма избрана фигура!");
                    return;
                }
                foreach (Shape shape in dialogProcessor.Selection)
                {
                   if (shape.Contains(mousePoint))
                    {
                        MessageBox.Show("Точката е във фигура!");
                        insidePoint = true;
                    }
                }

                if(!insidePoint)
                MessageBox.Show("Точката не е в нито една фигура.");

            }
        }
        private void rotatePrimitiveSpeedButton_Click(object sender, EventArgs e)
        {
            float angle = float.Parse(Microsoft.VisualBasic.Interaction.InputBox("Въведете стойност:"));

            if (dialogProcessor.Selection.Count > 0)
            {
                foreach (Shape shape in dialogProcessor.Selection)
                {
                    PointF center = new PointF(
                        shape.Location.X + shape.Width / 2,
                        shape.Location.Y + shape.Height / 2
                    );

                    // Завъртаме фигурата спрямо центъра ѝ
                    shape.Rotation.RotateAt(angle, center);

                    // Актуализираме позицията след ротацията
                    shape.Location = RotatePoint(shape.Location, center, angle);
                }
                viewPort.Invalidate();
            }
        }

        // Метод за ротация на точка около център
        private PointF RotatePoint(PointF point, PointF center, float angle)
        {
            double radians = angle * (Math.PI / 180); // Конвертираме градуси в радиани
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);

            float dx = point.X - center.X;
            float dy = point.Y - center.Y;

            return new PointF(
                center.X + (dx * cos - dy * sin),
                center.Y + (dx * sin + dy * cos)
            );
        }

        private void numericUpDownScaleChange(object sender, EventArgs e)
        {
            float scale = (float)numericUpDownScale.Value;

            if (dialogProcessor.Selection.Count > 0) 
            {
                foreach (Shape shape in dialogProcessor.Selection)
                {
                    shape.Scale = scale;
                }            
            }

            viewPort.Invalidate();
        }

        private void RemoveGradientsButton_Click(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection.Count > 0)
            {
                foreach (Shape shape in dialogProcessor.Selection)
                {
                    shape.Color1Gradient = Color.Empty;
                    shape.Color2Gradient = Color.Empty;
                }
                viewPort.Invalidate();
            }
        }
    }
}
