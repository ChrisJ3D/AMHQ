using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager> {

	public List<Character> charactersList;

	public float GetAffectionValue(Character currentCharacter) {
		return currentCharacter.currentAffection;
	}

	public float GetAffectionValue(int index) {
		return FindCharacterByIndex(index).currentAffection;
	}

	public void SetAffectionValue(Character currentCharacter, float value) {
		currentCharacter.currentAffection = value; 
	}

	protected Character FindCharacterByIndex(int index) {
		
		Character currentCharacter = null;

		foreach(Character character in charactersList) {
			if (character.index == index) {
				currentCharacter = character;
			}
		}

		return currentCharacter;
	}
}
