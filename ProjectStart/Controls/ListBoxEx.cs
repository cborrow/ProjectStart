using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectStart.Controls
{
    public class ListBoxEx : ListBox
    {
        ListBoxItemRenderer renderer;
        public ListBoxItemRenderer Renderer
        {
            get { return renderer; }
            set { renderer = value; }
        }

        public ListBoxEx()
            : base()
        {
            this.ItemHeight = 30;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (renderer == null)
                throw new Exception("A renderer must be set to draw listbox items.");
            if (this.Items.Count <= 0)
                return;

            e.DrawBackground();
            e.DrawFocusRectangle();

            object item = this.Items[e.Index];

            renderer.DrawItem(e.Graphics, e.Bounds, item);

            base.OnDrawItem(e);
        }
    }
}
