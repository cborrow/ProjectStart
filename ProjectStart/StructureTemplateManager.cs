using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectStart
{
    public class StructureTemplateManager
    {
        List<StructureTemplate> structureTemplates;

        static string templatesPath;
        public static string TemplatesPath
        {
            get { return templatesPath; }
        }

        static StructureTemplateManager instance;
        public static StructureTemplateManager Manager
        {
            get { return instance; }
        }

        public StructureTemplateManager()
        {
            instance = this;
            templatesPath = Path.Combine(Environment.CurrentDirectory, "Data\\Templates\\Structure");
            structureTemplates = new List<StructureTemplate>();
        }

        public void Add(StructureTemplate template)
        {
            if (!structureTemplates.Contains(template))
                structureTemplates.Add(template);
        }

        public void Remove(StructureTemplate template)
        {
            if (structureTemplates.Contains(template))
                structureTemplates.Remove(template);
        }

        public StructureTemplate Find(string name)
        {
            foreach (StructureTemplate template in structureTemplates)
            {
                if (template.Name == name)
                    return template;
            }
            return null;
        }

        public string[] GetTemplateNames()
        {
            List<string> names = new List<string>();

            foreach (StructureTemplate template in structureTemplates)
            {
                names.Add(template.Name);
            }

            return names.ToArray();
        }
    }
}

