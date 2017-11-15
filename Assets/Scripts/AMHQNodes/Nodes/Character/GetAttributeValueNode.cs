using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Character/Set Attribute Value")]
	public class SetAttributeValueNode : Node 
	{
		public const string ID = "SetAttributeValueNode";
		public override string GetID { get { return ID; } }
		public override string Title { get { return "Set Attribute"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (120, 90); } }
		public override string description { get { return "The Branch node reroutes the level flow based on the incoming condition. The condition should be in a boolean format (true/false)."; } }

		[ConnectionKnob("In", Direction.In, "Flow", NodeSide.Left, 10)]
		public ConnectionKnob flowIn;

		[ConnectionKnob("Out", Direction.Out, "Flow", NodeSide.Right, 10)]
		public ConnectionKnob flowOut;

		[ConnectionKnob("Attribute", Direction.In, "Number", NodeSide.Left, 20)]
		public ConnectionKnob attributeKnob;

		[ConnectionKnob("Value", Direction.In, "Number", NodeSide.Left, 30)]
		public ConnectionKnob valueKnob;

		public object speaker;
		public string content = "";
		
		public override void NodeGUI () 
		{
			base.NodeGUI();

			if (GUI.changed)
				NodeEditor.curNodeCanvas.OnNodeChange(this);
		}
		
		public override bool Calculate () 
		{
			return true;
		}
	}
}