﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using AMHQ;

[Node (false, "Character/Get Attribute Value", new Type[] { typeof(AMHQCanvas) })]
public class AttributesNode : BaseConversationNode 
{
	public override string Title {get { return "Get Attribute Value"; } }
	public override Vector2 MinSize { get { return new Vector2(150, 200); } }
	public override bool AutoLayout { get { return true; } }

	private const string Id = "getAttributeNode";
	public override string GetID { get { return Id; } }
	public override Type GetObjectType { get { return typeof(AttributesNode); } }
	public override string description { get { return ""; } }

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
		var gameManager = FindObjectOfType<GameManager>();
		Dictionary<CharacterAttributeType, Number> attributes = gameManager.GetPlayerAttributes();

		if (attributes.TryGetValue(CharacterAttributeType.STRESS, out stress)) {
			stressKnob.SetValue<Number>(stress);
		}

		if (attributes.TryGetValue(CharacterAttributeType.CHARISMA, out charisma)) {
			charismaKnob.SetValue<Number>(charisma);
		}

		if (attributes.TryGetValue(CharacterAttributeType.INNOVATION, out innovation)) {
			innovationKnob.SetValue<Number>(innovation);
		}

		if (attributes.TryGetValue(CharacterAttributeType.ORGANISATION, out organisation)) {
			organisationKnob.SetValue<Number>(organisation);
		}

		if (attributes.TryGetValue(CharacterAttributeType.KNOWLEDGE, out knowledge)) {
			knowledgeKnob.SetValue<Number>(knowledge);
		}

		if (attributes.TryGetValue(CharacterAttributeType.ELOQUENCE, out eloquence)) {
			eloquenceKnob.SetValue<Number>(eloquence);
		}

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

	public override BaseConversationNode GetDownstreamNode(int inputValue)
	{
		return null;
	}

	public override bool IsBackAvailable() {
		return false;
	}

	public override bool IsNextAvailable() {
		return false;
	}

}