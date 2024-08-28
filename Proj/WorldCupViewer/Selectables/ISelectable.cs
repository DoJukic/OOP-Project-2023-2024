using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupViewer.Selectables
{
    public interface ISelectable
    {
        public void NotifyHandlerSelfIsSelected(Control container)
        {
            SelectablesHandler.NotifySelected(container, this);
        }
        public void NotifyHandlerSelfIsDeselected(Control? container)
        {
            SelectablesHandler.NotifyDeselected(container, this);
        }
        public void NotifySelectableIsDeselected();
        public bool GetIsSelected();
        public void Selected();
        public void Deselected();
    }

}
