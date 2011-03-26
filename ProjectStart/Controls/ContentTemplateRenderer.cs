using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectStart.Controls
{
    public class ContentTemplateRenderer : ListBoxItemRenderer
    {
        Font normalTextFont;
        Font subTextFont;

        public ContentTemplateRenderer()
        {
            normalTextFont = new Font(new FontFamily("Segoe UI"), 8.75f, FontStyle.Regular);
            subTextFont = new Font(new FontFamily("Segoe UI"), 7.75f, FontStyle.Regular);
        }

        public override void DrawItem(Graphics g, Rectangle bounds, object item)
        {
            ContentTemplate template = (ContentTemplate)item;

            g.DrawString(template.Name, normalTextFont, Brushes.Black, new PointF(bounds.Left + 3, bounds.Top + 2));
            g.DrawString(template.Shortcut, subTextFont, Brushes.LightGray, new PointF(bounds.Left + 5, bounds.Top + 15));

            base.DrawItem(g, bounds, item);
        }
    }
}
