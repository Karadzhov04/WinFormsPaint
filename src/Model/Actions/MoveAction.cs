using Draw.src.Model.Actions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    public class MoveAction : IAction
    {
        public Shape Shape { get; }
        public PointF OldPosition { get; }
        public PointF NewPosition { get; }

        public MoveAction(Shape shape, PointF oldPos, PointF newPos)
        {
            Shape = shape;
            OldPosition = oldPos;
            NewPosition = newPos;
        }

        public void Undo()
        {
            Shape.Location = OldPosition;
        }

        public void Redo()
        {
            Shape.Location = NewPosition;
        }
    }
}
