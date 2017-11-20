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
		Stress = 0,
		Organisation = 1,
		Charisma = 2,
		Knowledge = 3,
		Innovation = 4,
		Eloquence = 5
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