using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "amhq/Attribute")]
[System.Serializable]
public class Attribute : ScriptableObject {

	public enum AttributeType {
		STRESS,
		ORGANISATION,
		CHARISMA,
		KNOWLEDGE,
		INNOVATION,
		ELOQUENCE
	};

	public int value;
	public AttributeType type;

	[SerializeField, HideInInspector]
	int _myprivatevar;

	public Attribute(AttributeType Type) {
		this.type = Type;
		this.value = 0;
		Vector3 blah = new Vector3(1,1,1);
	}

	public Attribute(AttributeType Type, int Value) {
		this.type = Type;
		this.value = Value;
	}

	public void Adjust(int amount) {
		value += amount;
	}
}
