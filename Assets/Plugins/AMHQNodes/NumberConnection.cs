using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework {

	public class NumberConnectionType : ValueConnectionType
	{	
	public override string Identifier { get { return "Number"; } }
	public override Type Type { get { return typeof(Number); } }
	public override Color Color { get { return Color.magenta; } }
	}
}
	