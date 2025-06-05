using Draw.src.Model.Actions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    public class RotateAction : IAction
    {
        private Shape shape;
        private float oldAngle;
        private float newAngle;

        public RotateAction(Shape shape, float oldAngle, float newAngle)
        {
            this.shape = shape;
            this.oldAngle = oldAngle;
            this.newAngle = newAngle;
        }

        public void Undo()
        {
            shape.RotateDegree = oldAngle;
            UpdateMatrix();
        }

        public void Redo()
        {
            shape.RotateDegree = newAngle;
            UpdateMatrix();
        }

        private void UpdateMatrix()
        {
            PointF center = new PointF(
                shape.Location.X + shape.Width / 2,
                shape.Location.Y + shape.Height / 2
            );

            shape.Rotation = new Matrix();
            shape.Rotation.RotateAt(shape.RotateDegree, center);
        }
    }
}
