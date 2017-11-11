using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterManager : Singleton<CharacterManager> {

	public GameManager gameManager;

	public List<Character> characterList;

	void Start() {
		GenerateCharacterObjects();

		if (characterList == null) {
			GenerateCharacterObjects();
		}
	}

	public void ShowCharacter(int index, int pose) {
		characterList[index].Show(pose);
	}

	public void ShowCharacter(int index) {
		Debug.Log("CharacterManager: Showing character at index " + index);
		characterList[index].Show();
	}

	public void HideCharacter(int index) {
		characterList[index].Hide();
	}

	protected List<string> GetCharacterAssets() {
		List<string> characterAssets = new List<string>();

		string itemFolderPath = "/Prefabs/Characters";
		string dataPath = Application.dataPath;
	
		string[] itemPaths = Directory.GetFiles(dataPath + itemFolderPath, searchOption: SearchOption.AllDirectories,
		searchPattern: "*.prefab");

			
		for(int i = 0; i < itemPaths.Length; i++) {
				string assetName = itemPaths[i].Substring(dataPath.Length+itemFolderPath.Length+1);
				assetName = assetName.Substring(0,assetName.Length-7);
				characterAssets.Add(assetName);
			}

		return characterAssets;
	}

	void GenerateCharacterObjects() {
		List<string> characterAssets = GetCharacterAssets();

		for (int i = 0; i < characterAssets.Count; i++) {
			GameObject currentCharacterObject = GameObject.Find(characterAssets[i]);

			Character currentCharacterComponent = (Character)currentCharacterObject.GetComponent("Character");

			currentCharacterComponent.index = i;

			characterList.Add(currentCharacterComponent);
		}
	}

	public float GetAffectionValue(Character currentCharacter) {
		return currentCharacter.currentAffection;
	}

	public float GetAffectionValue(int index) {
		return FindCharacterByIndex(index).currentAffection;
	}

	public void SetAffectionValue(Character currentCharacter, float value) {
		currentCharacter.currentAffection = value; 
	}

	public Character FindCharacterByIndex(int index) {
		
		Character currentCharacter = null;

		foreach(Character character in characterList) {
			if (character.index == index) {
				currentCharacter = character;
			}
		}

		if (currentCharacter)
			return currentCharacter;
		
		Debug.LogError("CharacterManager: Could not find character by index: " + index);
		return null;
	}

	public Character FindCharacterByName(string name) {
		foreach (Character character in characterList) {
			if (character.firstName == name || character.lastName == name) {
				return character;
			}
		}

		Debug.LogWarning("CharacterManager: Could not find character by name: " + name);
		return null;
	}
}
