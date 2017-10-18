using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestManager))]
public class TestPropertyEditor : Editor {

	public TestManager targetManager;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable() {
		targetManager = (TestManager)target;
	}

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
