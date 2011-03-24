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
    public partial class ContentManagerForm : Form
    {
        Timer autoSaveTimer;

        ContentTemplate selectedTemplate;
        private ContentTemplate SelectedTemplate
        {
            get { return selectedTemplate; }
            set
            {
                selectedTemplate = value;
                UpdateFormData();
            }
        }

        public ContentManagerForm()
        {
            InitializeComponent();

            autoSaveTimer = new Timer();
            autoSaveTimer.Interval = 1000;
            autoSaveTimer.Tick += new EventHandler(autoSaveTimer_Tick);
        }

        protected void UpdateFormData()
        {
            if (selectedTemplate == null)
                return;
            textBox1.Text = selectedTemplate.Name;
            textBox2.Text = selectedTemplate.Shortcut;
            textBox3.Text = selectedTemplate.Content;
            comboBox1.Text = selectedTemplate.Mode.ToString();
        }

        protected void UpdateTemplateData()
        {
            if (selectedTemplate == null)
                return;
            selectedTemplate.Name = textBox1.Text;
            selectedTemplate.Shortcut = textBox2.Text;
            selectedTemplate.Content = textBox3.Text;
            selectedTemplate.Mode = (ContentTemplateMode)Enum.Parse(typeof(ContentTemplateMode), comboBox1.Text);
        }

        protected void SaveTemplate(ContentTemplate template, string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine("ContentTemplate");
            sw.WriteLine(template.Name);
            sw.WriteLine(template.Shortcut);
            sw.WriteLine(template.Mode.ToString());
            sw.Write(template.Content);
            sw.Close();
        }

        protected ContentTemplate LoadTemplate(string path)
        {
            if (!File.Exists(path))
                return null;

            ContentTemplate template = new ContentTemplate();

            StreamReader sr = new StreamReader(path);

            if (sr.ReadLine() == "ContentTemplate")
            {
                template.Name = sr.ReadLine();
                template.Shortcut = sr.ReadLine();
                template.Mode = (ContentTemplateMode)Enum.Parse(typeof(ContentTemplateMode), sr.ReadLine());
                template.Content = sr.ReadToEnd();
            }

            sr.Close();

            ContentTemplateManager.Manager.Add(template);
            listBox1.Items.Add(template.Name);
            return template;
        }

        protected void SetText(Control c, string text)
        {
            c.TextChanged -= Control_TextChanged;
            c.Text = text;
            c.TextChanged += Control_TextChanged;
        }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            autoSaveTimer.Stop();

            int index = listBox1.Items.IndexOf(this.SelectedTemplate.Name);
            listBox1.Items[index] = textBox1.Text;
            UpdateTemplateData();

            autoSaveTimer.Tag = 0;
            autoSaveTimer.Start();
        }

        void autoSaveTimer_Tick(object sender, EventArgs e)
        {
            if ((int)autoSaveTimer.Tag < 1)
            {
                autoSaveTimer.Tag = (int)autoSaveTimer.Tag + 1;
                return;
            }

            autoSaveTimer.Stop();
            
            string path = Path.Combine(ContentTemplateManager.TemplatesPath, this.SelectedTemplate.Name + ".ctpl");
            SaveTemplate(this.SelectedTemplate, path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ContentTemplate template = new ContentTemplate();
            template.Name = "Untitled";

            ContentTemplateManager.Manager.Add(template);
            listBox1.Items.Add(template.Name);
            selectedTemplate = template;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;

            if(index >= 0 && index < listBox1.Items.Count)
                this.SelectedTemplate = ContentTemplateManager.Manager.Find((string)listBox1.Items[index]);
        }
    }
}
