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

		[SerializeField]
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
			_attributes[attribute] = (Number)value;
			Debug.Log(_attributes + " set to " + _attributes[attribute].value);
		}

		public Dictionary<CharacterAttributeType, Number> GetAllAttributes() {
			return _attributes;
		}

		/// <summary>
		/// Adjusts a given player value incrementally. If you want to set an attribute to a specific value, use SetAttribute()
		/// </summary>
		/// <param name="attribute">The attribute to adjust</param>
		/// <param name="value">The adjustment to be made. Use positive numbers to add, and negative numbers to subtract</param>
		public void AdjustAttribute(CharacterAttributeType attribute, Number value) {
			Number oldValue;
			if (_attributes.TryGetValue(attribute, out oldValue)) {
				_attributes[attribute] = oldValue + value;
				//_attributes.Add(attribute, oldValue + value);
			} else {
				Debug.Log("Unable to adjust value??");
			}
		}

		public float GetAttribute(CharacterAttributeType attribute) {
			return _attributes[attribute];
		}
		
		/// <summary>
		/// Returns the currently lowest player attribute value
		/// </summary>
		/// <returns></returns>
		public float GetLowestAttributeValue() {
			float lowest = 9999;
			foreach (KeyValuePair<CharacterAttributeType, Number> attribute in attributes) {
				if (lowest > attribute.Value) {
					lowest = attribute.Value;
				}
			}
			return lowest;
		}

		/// <summary>
		/// Returns the currently highest player attribute value
		/// </summary>
		/// <returns></returns>
		public float GetHighestAttributeValue() {
			float highest = 0;
			foreach (KeyValuePair<CharacterAttributeType, Number> attribute in attributes) {
				if (highest < attribute.Value) {
					highest = attribute.Value;
				}
			}
			return highest;
		}
	}
}