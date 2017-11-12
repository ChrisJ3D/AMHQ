using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

public class NodeManager : Singleton<NodeManager> {

	public GameManager gameManager;
	
	[SerializeField]
	private GameObject UI_DialogueBoxPrefab;
	private UI_DialogueBox _dialogueBox;

	[SerializeField]
	private RectTransform _canvasObject;

	public AMHQCanvas nodeCanvas;


	public override void Awake() {
		base.Awake();

		//	Traverse the node canvas and add all nodes to our dictionary.
		//	First we check if there's a canvas stored in our scene

		AMHQCanvas canvas = GetCanvasFromScene();
		if (canvas) {
			nodeCanvas = canvas;
		} else {
			//	If there was no canvas stored in the scene, we look in Resources/Saves
			foreach (AMHQCanvas savedCanvas in Resources.LoadAll<AMHQCanvas>("Saves/")) {
				nodeCanvas = savedCanvas;
			}
		}
	}

	public void ShowDialogueByID() {
		nodeCanvas.GetSceneLoadedNode();

		_dialogueBox = GameObject.Instantiate(UI_DialogueBoxPrefab).GetComponent<UI_DialogueBox>();
		_dialogueBox.Construct(this);
		_dialogueBox.uiManager = gameManager.uiManager;
		_dialogueBox.transform.SetParent(_canvasObject, false);
		_dialogueBox.SetData(nodeCanvas.startNode);
	}

	private BaseConversationNode GetNodeByTag(string tag) {
		return null;
	}

	public void okButton() {
		nodeCanvas.TraverseNodes((int)EnumDialogInputValue.Next);
		if (_dialogueBox) {
			_dialogueBox.SetData(nodeCanvas.currentNode);
		} else {Debug.Log("dialogueBox not set");}

	}

	public void backButton() {
		nodeCanvas.TraverseNodes((int)EnumDialogInputValue.Back);
		_dialogueBox.SetData(nodeCanvas.currentNode);
	}

	public AMHQCanvas GetCanvasFromScene() {
		AMHQCanvas canvas = null;

		GameObject gameObject = GameObject.Find("NodeEditor_SceneSaveHolder");
		if (gameObject) {
			NodeCanvasSceneSave saveComponent = gameObject.GetComponent<NodeCanvasSceneSave>();

			if (saveComponent) {
			canvas = (AMHQCanvas)saveComponent.savedNodeCanvas;
			}
		} else {
			Debug.Log("No canvas in scene found, returning null");
		}
		return canvas;
	}
}

public enum EnumDialogInputValue
	{
		Next = -2,
		Back = -1,
	}