using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ProjectStart
{
    public class ContentTemplateManager
    {
        List<ContentTemplate> contentTemplates;

        static string templatesPath;
        public static string TemplatesPath
        {
            get { return templatesPath; }
        }

        static ContentTemplateManager instance;
        public static ContentTemplateManager Manager
        {
            get { return instance; }
        }

        public ContentTemplateManager()
        {
            instance = this;
            templatesPath = Path.Combine(Environment.CurrentDirectory, "Data\\Templates\\Content");
            contentTemplates = new List<ContentTemplate>();
        }

        public void Add(ContentTemplate template)
        {
            if (!contentTemplates.Contains(template))
                contentTemplates.Add(template);
        }

        public void Remove(ContentTemplate template)
        {
            if (contentTemplates.Contains(template))
                contentTemplates.Remove(template);
        }

        public ContentTemplate Find(string name)
        {
            foreach (ContentTemplate template in contentTemplates)
            {
                if (template.Name == name)
                    return template;
            }
            return null;
        }

        public ContentTemplate FindByShortcut(string shortcut)
        {
            foreach (ContentTemplate template in contentTemplates)
            {
                if (template.Shortcut == shortcut)
                    return template;
            }
            return null;
        }

        public string[] GetTemplateNames()
        {
            List<string> names = new List<string>();

            foreach (ContentTemplate template in contentTemplates)
            {
                names.Add(template.Name);
            }

            return names.ToArray();
        }
    }
}

