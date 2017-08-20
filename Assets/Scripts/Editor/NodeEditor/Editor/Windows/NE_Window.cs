using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NE_Window : EditorWindow {
	#region Public Variables
	public static NE_Window currentWindow;

	public NE_NodePropertyView propertyView;
	public NE_NodeWorkView workView;

	public NE_NodeGraph nodeGraph;

	public float viewPercentage = 0.75f;
	#endregion

	#region Protected Variables
	#endregion

	#region Constructor
	#endregion

	#region Main Methods
	public static void InitEditorWindow() {
		currentWindow = (NE_Window)EditorWindow.GetWindow<NE_Window>();
		currentWindow.titleContent = new GUIContent("Level Flow");

		CreateViews();
	}

	void OnEnable() {
		//Debug.Log("Window Enabled");

	}

	void OnDestroy() {
		//Debug.Log("Window Destroyed");
	}

	void Update() {

	}

	void OnGUI() {
		if(propertyView == null || workView == null) {
			CreateViews();
			return;
		}


		//	Get current event
		Event e = Event.current;
		ProcessEvents(e);

		//	Update views
		EditorGUILayout.LabelField("This is our node editor");
		workView.UpdateView(position, new Rect(0f,0f,viewPercentage,1f), e, nodeGraph);
		propertyView.UpdateView(new Rect(position.width, position.y, position.width, position.height),
								new Rect(viewPercentage, 0f, 1f - viewPercentage, 1f), e, nodeGraph);

		Repaint();
	}
	#endregion

	#region Utility Methods
	static void CreateViews() {
		if(currentWindow != null) {
			currentWindow.propertyView = new NE_NodePropertyView();
			currentWindow.workView = new NE_NodeWorkView();
		} else {
			currentWindow = (NE_Window)EditorWindow.GetWindow<NE_Window>();
		}
	}

	void ProcessEvents(Event e) {
		if(e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftArrow) {
			viewPercentage -= 0.01f;
		}

		if(e.type == EventType.KeyDown && e.keyCode == KeyCode.RightArrow) {
			viewPercentage += 0.01f;
		}

	}
	#endregion
}
