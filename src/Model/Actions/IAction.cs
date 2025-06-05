using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model.Actions
{
    public interface IAction
    {
        void Undo();
        void Redo();
    }

}
