using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AMHQ {

	public enum CharacterPosition {
		Left = 0,
		Middle = 1,
		Right = 2
	};

	public enum CharacterAttributeType {
		STRESS,
		ORGANISATION,
		CHARISMA,
		KNOWLEDGE,
		INNOVATION,
		ELOQUENCE
	};

	public enum AffectionLevel {
		Hostile, 
		Disintrested,
		Neutral,
		Friendly,
		Interested,
		Affectionate
	};

	public enum EnumDialogInputValue {
		Next = -2,
		Back = -1,
	};
}