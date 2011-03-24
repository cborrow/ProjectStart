using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectStart
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            ContentTemplateManager ctm = new ContentTemplateManager();
            StructureTemplateManager stm = new StructureTemplateManager();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageContentTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContentManagerForm contentManagerForm = new ContentManagerForm();
            contentManagerForm.Show();
        }
    }
}
