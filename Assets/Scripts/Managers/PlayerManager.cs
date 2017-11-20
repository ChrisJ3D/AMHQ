using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AMHQ {
	public class PlayerManager : Singleton<PlayerManager> {

		public GameManager gameManager;

		public string playerName = "PlayerName";
		public string bloodType;
		public string zodiac;
		public string favoriteFood;
		public string favoriteSeason;
		public string birthDay;

		public float startingCharisma;
		public float startingEloquence;
		public float startingInnovation;
		public float startingKnowledge;
		public float startingOrganisation;
		public float startingStress;

		public Dictionary<CharacterAttributeType, Number> attributes {get { return _attributes; } }

		private Dictionary<CharacterAttributeType, Number> _attributes = new Dictionary<CharacterAttributeType, Number>();

		public override void Initialize(MonoBehaviour parent) {
			gameManager = parent as GameManager;

			_attributes.Add(CharacterAttributeType.Charisma, startingCharisma);
			_attributes.Add(CharacterAttributeType.Eloquence, startingEloquence);
			_attributes.Add(CharacterAttributeType.Innovation, startingInnovation);
			_attributes.Add(CharacterAttributeType.Knowledge, startingKnowledge);
			_attributes.Add(CharacterAttributeType.Organisation, startingOrganisation);
			_attributes.Add(CharacterAttributeType.Stress, startingStress);
		}

		public void SetAttribute(CharacterAttributeType attribute, float value) {
			_attributes.Add(attribute, (Number)value);
		}

		public Dictionary<CharacterAttributeType, Number> GetAllAttributes() {
			return _attributes;
		}

		public void AdjustAttribute(CharacterAttributeType attribute, Number value) {
			Number oldValue;
			if (_attributes.TryGetValue(attribute, out oldValue)) {
				_attributes.Add(attribute, oldValue + value);
			} else {
				Debug.Log("Unable to adjust value??");
			}
		}

		public float GetAttribute(CharacterAttributeType attribute) {
			return 0f;
		}

		public float GetLowestAttribute() {
			return 0f;
		}

		public float GetHighestAttribute() {
			return 0f;
		}
	}
}