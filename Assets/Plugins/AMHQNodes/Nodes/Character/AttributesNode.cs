﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Character/Get Attribute Value")]
	public class AttributesNode : Node 
	{
		public const string ID = "AttributesNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Get Attribute Value"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (140, 210); } }

		[ValueConnectionKnob("Stress", Direction.Out, "Number")]
		public ValueConnectionKnob stressKnob;

		[ValueConnectionKnob("Charisma", Direction.Out, "Number")]
		public ValueConnectionKnob charismaKnob;

		[ValueConnectionKnob("Innovation", Direction.Out, "Number")]
		public ValueConnectionKnob innovationKnob;

		[ValueConnectionKnob("Organisation", Direction.Out, "Number")]
		public ValueConnectionKnob organisationKnob;

		[ValueConnectionKnob("Knowledge", Direction.Out, "Number")]
		public ValueConnectionKnob knowledgeKnob;

		[ValueConnectionKnob("Eloquence", Direction.Out, "Number")]
		public ValueConnectionKnob eloquenceKnob;

		[ValueConnectionKnob("Lowest Attribute", Direction.Out, "Number")]
		public ValueConnectionKnob lowestAttributeKnob;

		[ValueConnectionKnob("Highest Attribute", Direction.Out, "Number")]
		public ValueConnectionKnob highestAttributeKnob;

		public Number stress = new Number();
		public Number charisma = new Number();
		public Number innovation = new Number();
		public Number organisation = new Number();
		public Number knowledge = new Number();
		public Number eloquence = new Number();
		public Number highestAttribute = new Number();
		public Number lowestAttribute = new Number();

		public List<Number> attributes = new List<Number>();

		protected string label = "";
		
		public override void NodeGUI () 
		{
			base.NodeGUI();

			if (GUI.changed)
				NodeEditor.curNodeCanvas.OnNodeChange(this);
		}
		
		public override bool Calculate () 
		{
			stress = 0f;
			stressKnob.SetValue<Number>(stress);
			charismaKnob.SetValue<Number>(charisma);
			innovationKnob.SetValue<Number>(innovation);
			organisationKnob.SetValue<Number>(organisation);
			knowledgeKnob.SetValue<Number>(knowledge);
			eloquenceKnob.SetValue<Number>(eloquence);

			return true;
		}

		protected Number GetLowestAttribute() {

			attributes.Clear();
			attributes.Add(stress);
			attributes.Add(charisma);
			attributes.Add(innovation);
			attributes.Add(organisation);
			attributes.Add(knowledge);
			attributes.Add(eloquence);

			return lowestAttribute;
		
		}
	}
}