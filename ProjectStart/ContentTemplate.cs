using System;

namespace ProjectStart
{
	public class ContentTemplate
	{
		string name;
		public string Name {
			get { return name; }
			set { name = value; }
		}

		string shortcut;
		public string Shortcut {
			get { return shortcut; }
			set { shortcut = value; }
		}

		ContentTemplateMode mode;
		public ContentTemplateMode Mode {
			get { return mode; }
			set { mode = value; }
		}

		string content;
		public string Content {
			get { return content; }
			set { content = value; }
		}

		public ContentTemplate ()
		{
			name = string.Empty;
			shortcut = string.Empty;
			content = string.Empty;
			mode = ContentTemplateMode.CustomText;
		}

		public ContentTemplate (string name, string shortcut, string content) : 
			this(name, shortcut, content, ContentTemplateMode.CustomText)
		{
			
		}

		public ContentTemplate (string name, string shortcut, string content, ContentTemplateMode mode)
		{
			this.name = name;
			this.shortcut = shortcut;
			this.content = content;
			this.mode = mode;
		}
	}
}

