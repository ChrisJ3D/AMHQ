using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Logic/Branch")]
	public class BranchNode : Node 
	{
		public const string ID = "BranchNode";
		public override string GetID { get { return ID; } }
		public override string Title { get { return "Branch"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (120, 60); } }
		public override string description { get { return "The Branch node reroutes the level flow based on the incoming condition. The condition should be in a boolean format (true/false)."; } }

		[ConnectionKnob("In", Direction.In, "Flow", NodeSide.Left, 10)]
		public ConnectionKnob flowIn;

		[ConnectionKnob("Condition", Direction.In, "Number", NodeSide.Left, 20)]
		public ConnectionKnob conditionKnob;

		[ConnectionKnob("True", Direction.Out, "Flow", NodeSide.Right, 10)]
		public ConnectionKnob trueKnob;

		[ConnectionKnob("False", Direction.Out, "Flow", NodeSide.Right, 20)]
		public ConnectionKnob falseKnob;		

		public object speaker;
		public string content = "";
		
		public override void NodeGUI () 
		{
			base.NodeGUI();
		}
		
		public override bool Calculate () 
		{
			return true;
		}
	}
}