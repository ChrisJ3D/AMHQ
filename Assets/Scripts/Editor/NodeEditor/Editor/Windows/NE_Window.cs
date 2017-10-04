using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NE_Window : EditorWindow {

	public static NE_Window currentWindow;

	public NE_NodePropertyView propertyView;
	public NE_NodeWorkView workView;

	public NE_NodeGraph nodeGraph = null;

	public float viewPercentage = 0.75f;

	public static void InitEditorWindow() {
		currentWindow = (NE_Window)EditorWindow.GetWindow<NE_Window>();
		currentWindow.titleContent = new GUIContent("Level Flow");

		CreateViews();
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

		workView.UpdateView(position, new Rect(0f,0f,viewPercentage,1f), e, nodeGraph);
		propertyView.UpdateView(new Rect(position.width, position.y, position.width, position.height),
								new Rect(viewPercentage, 0f, 1f - viewPercentage, 1f), e, nodeGraph);

		Repaint();
	}

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
}
