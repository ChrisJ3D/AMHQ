using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "amhq/CharacterAttribute")]
[System.Serializable]
public class CharacterAttribute : ScriptableObject {

	public enum CharacterAttributeType {
		STRESS,
		ORGANISATION,
		CHARISMA,
		KNOWLEDGE,
		INNOVATION,
		ELOQUENCE
	};

	public int value;
	public CharacterAttributeType type;

	[SerializeField, HideInInspector]
	int _myprivatevar;

	public CharacterAttribute(CharacterAttributeType Type) {
		this.type = Type;
		this.value = 0;
		Vector3 blah = new Vector3(1,1,1);
	}

	public CharacterAttribute(CharacterAttributeType Type, int Value) {
		this.type = Type;
		this.value = Value;
	}

	public void Adjust(int amount) {
		value += amount;
	}
}
