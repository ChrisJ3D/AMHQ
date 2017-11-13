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

	public AMHQCanvas nodeCanvas = null;

	public override void Initialize(MonoBehaviour parent) {
		gameManager = parent as GameManager;
		GetCanvasFromScene();
	}

	public void StartDialogue() {
		nodeCanvas.GetSceneLoadedNode();

		_dialogueBox = GameObject.Instantiate(UI_DialogueBoxPrefab).GetComponent<UI_DialogueBox>();
		_dialogueBox.Construct(this);
		_dialogueBox.uiManager = gameManager.uiManager;
		_dialogueBox.transform.SetParent(_canvasObject, false);
		_dialogueBox.SetData(nodeCanvas.startNode);
	}

	public void okButton() {
		nodeCanvas.TraverseNodes((int)EnumDialogInputValue.Next);
		if (_dialogueBox) {
			_dialogueBox.SetData(nodeCanvas.currentNode);
		}
	}

	public void backButton() {
		nodeCanvas.TraverseNodes((int)EnumDialogInputValue.Back);
		_dialogueBox.SetData(nodeCanvas.currentNode);
	}

	public void GetCanvasFromScene() {
		nodeCanvas = null;

		GameObject gameObject = GameObject.Find("NodeEditor_SceneSaveHolder");
		if (gameObject) {
			NodeCanvasSceneSave saveComponent = gameObject.GetComponent<NodeCanvasSceneSave>();

			if (saveComponent) {
			nodeCanvas = (AMHQCanvas)saveComponent.savedNodeCanvas;
			} else {
				Debug.Log("SceneSaveHolder was missing component, loading from file");
				GetCanvasFromFile();
			}
		} else {
			Debug.Log("No canvas in scene found, loading from file");
			GetCanvasFromFile();
		}
	}

	public void GetCanvasFromFile() {
		if(!gameManager) {
			Debug.Log("GameManager not set!!");
		}
		nodeCanvas = Resources.Load("Saves/" + gameManager.currentScene + "/LevelGraph", typeof(AMHQCanvas)) as AMHQCanvas;
	}
}

public enum EnumDialogInputValue
	{
		Next = -2,
		Back = -1,
	}