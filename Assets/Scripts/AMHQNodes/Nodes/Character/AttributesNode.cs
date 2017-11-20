using System;
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

	Dictionary<CharacterAttributeType, Number> attributes = new Dictionary<CharacterAttributeType, Number>();

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
		attributes = gameManager.GetPlayerAttributes();

		if (attributes.TryGetValue(CharacterAttributeType.Stress, out stress)) {
			stressKnob.SetValue<Number>(stress);
		}

		if (attributes.TryGetValue(CharacterAttributeType.Charisma, out charisma)) {
			charismaKnob.SetValue<Number>(charisma);
		}

		if (attributes.TryGetValue(CharacterAttributeType.Innovation, out innovation)) {
			innovationKnob.SetValue<Number>(innovation);
		}

		if (attributes.TryGetValue(CharacterAttributeType.Organisation, out organisation)) {
			organisationKnob.SetValue<Number>(organisation);
		}

		if (attributes.TryGetValue(CharacterAttributeType.Knowledge, out knowledge)) {
			knowledgeKnob.SetValue<Number>(knowledge);
		}

		if (attributes.TryGetValue(CharacterAttributeType.Eloquence, out eloquence)) {
			eloquenceKnob.SetValue<Number>(eloquence);
		}

		lowestAttributeKnob.SetValue<Number>(GetLowestAttribute());
		highestAttributeKnob.SetValue<Number>(GetHighestAttribute());

		return true;
	}

	protected Number GetLowestAttribute() {
		if (attributes.Count <= 0) {
			Calculate();
			return null;
		}

		Number lowestAttribute = 9999;

		foreach (Number attribute in attributes.Values) {
			if (lowestAttribute > attribute) {
				lowestAttribute = attribute;
			}
		}

		return lowestAttribute;
	}

	protected Number GetHighestAttribute() {
		if (attributes.Count <= 0) {
			Calculate();
			return null;
		}

		Number highestAttribute = 0;

		foreach (Number attribute in attributes.Values) {
			if (highestAttribute < attribute) {
				highestAttribute = attribute;
			}
		}

		return highestAttribute;
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