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

		Dictionary<CharacterAttributeType, float> attributes = new Dictionary<CharacterAttributeType, float>();

		public override void Initialize(MonoBehaviour parent) {
			gameManager = parent as GameManager;

			attributes.Add(CharacterAttributeType.CHARISMA, startingCharisma);
			attributes.Add(CharacterAttributeType.ELOQUENCE, startingEloquence);
			attributes.Add(CharacterAttributeType.INNOVATION, startingInnovation);
			attributes.Add(CharacterAttributeType.KNOWLEDGE, startingKnowledge);
			attributes.Add(CharacterAttributeType.ORGANISATION, startingOrganisation);
			attributes.Add(CharacterAttributeType.STRESS, startingStress);
		}

		public void SetAttribute(CharacterAttributeType attribute, float value) {

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