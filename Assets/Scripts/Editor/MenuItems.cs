using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuItems : MonoBehaviour {

	[MenuItem("AMHQ/Level Flow Editor", false, 0)]
	public static void InitNodeEditor() {
		NE_Window.InitEditorWindow();
	}

	[MenuItem("AMHQ/Flag Editor", false, 1)]
	private static void OpenFlagEditorMenuOptions() {

	}

	[MenuItem("AMHQ/Create New Scene", false, 21)]
	private static void CreateNewSceneMenuOption() {
		
	}

	[MenuItem("AMHQ/Create New Item", false, 22)]
	private static void CreatNewItemMenuOption() {
		
	}

	[MenuItem("AMHQ/Create New Character", false, 23)]
	private static void CreateNewCharacterMenuOption() {
		
	}

	[MenuItem("AMHQ/Create New Hub", false, 24)]
	private static void CreateNewHubMenuOption() {
		
	}
}
