using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;
using NodeEditorFramework.Standard;
using UnityEngine;
using System.IO;
using AMHQ;

[NodeCanvasType("AMHQ Canvas")]
public class AMHQCanvas : NodeCanvas
{
	public override string canvasName { get { return "AMHQ Node Canvas"; } }
	public BaseConversationNode currentNode {get { return _currentNode; } }
	public SceneLoadedNode startNode {get { return _startNode; } }
	public GameManager gameManager;

	private SceneLoadedNode _startNode = null;
	private BaseConversationNode _currentNode = null;

	public void Start() {
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}

	public void GetSceneLoadedNode() {
		_startNode = (SceneLoadedNode)this.nodes.FirstOrDefault (x => x is SceneLoadedNode);
		if (_startNode == null) {
			Debug.LogWarning("No SceneLoaded node found!!");
		}
	}

	public void TraverseNodes(int steps) {
		BaseConversationNode targetNode;

		if (_currentNode == null) {
			_currentNode = _startNode;
		}

		targetNode = (BaseConversationNode)_currentNode.GetDownstreamNode(steps);
		if (targetNode) {
			_currentNode = targetNode.PassAhead(steps);
		} else {
			_currentNode = null;
			Debug.LogError("targetNode returned null, traversal might have gone too far");
		}
	}

	public Dictionary<CharacterAttributeType, Number> GetCharacterStats() {
		return gameManager.playerManager.GetAllAttributes();
	}

	public List<string> GetItemsInAssetFolder() {

		List<string> itemsFolderEntries = new List<string>();

		string itemFolderPath = "/Prefabs/Items";
		string dataPath = Application.dataPath;
	
		string[] itemPaths = Directory.GetFiles(dataPath + itemFolderPath, searchOption: SearchOption.AllDirectories,
		searchPattern: "*.prefab");
			
		for(int i = 0; i < itemPaths.Length; i++) {
				string assetName = itemPaths[i].Substring(dataPath.Length+itemFolderPath.Length+1);
				assetName = assetName.Substring(0,assetName.Length-7);
				itemsFolderEntries.Add(assetName);
			}

		return itemsFolderEntries;
	}

	public List<string> GetCharactersInAssetFolder() {

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

	public List<string> GetFlagsInAssetsFolder() {

		List<string> flagAssets = new List<string>();

		string itemFolderPath = "/Prefabs/Flags";
		string dataPath = Application.dataPath;
	
		string[] itemPaths = Directory.GetFiles(dataPath + itemFolderPath, searchOption: SearchOption.AllDirectories,
		searchPattern: "*.prefab");
			
		for(int i = 0; i < itemPaths.Length; i++) {
				string assetName = itemPaths[i].Substring(dataPath.Length+itemFolderPath.Length+1);
				assetName = assetName.Substring(0,assetName.Length-7);
				flagAssets.Add(assetName);
			}

		return flagAssets;
	}
}
