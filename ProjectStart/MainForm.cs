using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ProjectStart
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            ContentTemplateManager ctm = new ContentTemplateManager();
            StructureTemplateManager stm = new StructureTemplateManager();

            LoadContentTemplates();
        }

        protected void LoadContentTemplates()
        {
            DirectoryInfo di = new DirectoryInfo(ContentTemplateManager.TemplatesPath);

            foreach (FileInfo fi in di.GetFiles("*.ctpl"))
            {
                ContentTemplate template = new ContentTemplate();

                using (StreamReader sr = new StreamReader(fi.OpenRead()))
                {
                    if (sr.ReadLine() != "ContentTemplate")
                    {
                        sr.Close();
                        return;
                    }

                    template.Name = sr.ReadLine();
                    template.Shortcut = sr.ReadLine();
                    template.Mode = (ContentTemplateMode)Enum.Parse(typeof(ContentTemplateMode), sr.ReadLine());
                    template.Content = sr.ReadToEnd();
                    ContentTemplateManager.Manager.Add(template);

                    sr.Close();
                }
            }
        }

        protected void LoadStructureTemplates()
        {

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                textBox1.Text = path;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DirectoryBuilder directoryBuilder = new DirectoryBuilder();
            directoryBuilder.BasePath = textBox1.Text;

            foreach (string line in textBox2.Lines)
            {
                ContentTemplate template = null;
                string pathName = "";
                string templateName = "";

                if (line.Contains(":"))
                {
                    string[] parts = line.Split(':');
                    pathName = parts[0];
                    templateName = parts[1];
                }
                else
                {
                    pathName = line;
                    templateName = "";
                }

                if (templateName != null)
                    template = ContentTemplateManager.Manager.FindByShortcut(templateName);

                if (!pathName.StartsWith("\\") && !pathName.EndsWith("\\"))
                    directoryBuilder.AddFile(pathName, template);
                if (pathName.EndsWith("\\"))
                    directoryBuilder.AddDirectory(pathName, template);
                if (pathName.StartsWith("\\"))
                    directoryBuilder.AddFile(pathName, template);
            }
        }
    }
}
