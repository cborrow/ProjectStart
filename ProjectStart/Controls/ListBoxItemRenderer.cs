using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectStart.Controls
{
    public abstract class ListBoxItemRenderer
    {
        public virtual void DrawItem(Graphics g, Rectangle bounds, object item)
        {

        }
    }
}
