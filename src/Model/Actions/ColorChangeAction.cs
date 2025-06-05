using Draw.src.Model.Actions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    public class ColorChangeAction : IAction
    {
        private Shape shape;
        private Color oldColor;
        private Color newColor;

        public ColorChangeAction(Shape shape, Color oldColor, Color newColor)
        {
            this.shape = shape;
            this.oldColor = oldColor;
            this.newColor = newColor;
        }

        public void Undo()
        {
            shape.FillColor = oldColor;
        }

        public void Redo()
        {
            shape.FillColor = newColor;
        }
    }

}
