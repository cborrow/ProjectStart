using System;

namespace ProjectStart
{
	public class StructureTemplate
	{
		string name;
		public string Name {
			get { return name; }
			set { name = value; }
		}

		string structure;
		public string Structure {
			get { return structure; }
			set { structure = value; }
		}

		public StructureTemplate ()
		{
			name = string.Empty;
			structure = string.Empty;
		}

		public StructureTemplate (string name, string structure)
		{
			this.name = name;
			this.structure = structure;
		}
	}
}

