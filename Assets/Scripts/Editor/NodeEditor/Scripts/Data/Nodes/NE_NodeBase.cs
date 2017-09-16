using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_NodeBase : ScriptableObject {

	//	PUBLIC VARIABLES
	public string nodeName;
	public Rect nodeRect;
	public NE_NodeGraph parentGraph;
	public NodeType nodeType;
	public bool isSelected = false;

	public List<NE_NodeInput> inputs;
	public List<NE_NodeOutput> outputs;
	public int numberOfInputs = 0;
	public int numberOfOutputs = 0;

	public System.Object nodeValue;

	//	PRIVATE VARIABLES
	protected GUISkin nodeSkin;


	//	SUBCLASSES

	[Serializable]
	public class NE_NodeOutput {
		public int index;
		public bool isOccupied = false;
	}

	//	MAIN FUNCTIONS
	public virtual void InitNode() {
		inputs = new List<NE_NodeInput>();
		outputs = new List<NE_NodeOutput>();

		//	Create inputs
		for(int i = 0; i < numberOfInputs; i++) {
			NE_NodeInput input = new NE_NodeInput();
			input.index = i;
			inputs.Add(input);
		}

		for (int i = 0; i < numberOfOutputs; i++) {
			NE_NodeOutput output = new NE_NodeOutput();
			output.index = i;
			outputs.Add(output);			
		}
	}

	//	EVALUATION

	public virtual void Evaluate() {
		throw new NotImplementedException("Node is trying to evaluate but has no method to do so");
	}

	public virtual float EvaluateAsFloat() {
		Evaluate();
		return Convert.ToSingle(nodeValue);
	}

	public virtual int EvaluateAsInt() {
		Evaluate();
		return Convert.ToInt32(nodeValue);
	}

	public virtual bool EvaluateAsBool() {
		Evaluate();
		return Convert.ToBoolean(nodeValue);
	}

	public virtual string EvaluateAsString() {
		Evaluate();
		return Convert.ToString(nodeValue);
	}

	//	GUI STUFF

	protected void DrawConnectors() {
		GetEditorSkin();
		
		foreach(NE_NodeInput input in inputs) {

			if(GUI.Button(new Rect(nodeRect.x - 24f, (nodeRect.y + ((nodeRect.height * 0.1f) * 2) - 8f) * input.index, 24f, 24f), "", nodeSkin.GetStyle("node_input"))) {
				if (parentGraph != null) {
					input.parentNode = parentGraph.connectionNode;
					input.isOccupied = input.parentNode != null ? true : false;

					parentGraph.wantsConnection = false;
					parentGraph.connectionNode = null;
				}
			}

				foreach(NE_NodeOutput output in outputs) {
			if(GUI.Button(new Rect(nodeRect.x + nodeRect.width, nodeRect.y + (nodeRect.height * 0.5f) - 12f, 24f, 24f), "", nodeSkin.GetStyle("node_output"))) {
				if (parentGraph != null) {
					parentGraph.wantsConnection = true;
					parentGraph.connectionNode = this;

				}
			}
		}

		input.DrawConnector();
		}
	}

    protected void DrawInputLines() {
		
		foreach(NE_NodeInput input in inputs) {
			if(input.parentNode && input.isOccupied) {
				//input.DrawConnections();
			} else {
				input.isOccupied = false;
			}
		}
    }

	#if UNITY_EDITOR
	public virtual void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin) {
		ProcessEvents(e, viewRect);

		if(isSelected){
			GUI.Box(nodeRect, nodeName, viewSkin.GetStyle("node_selected"));
		} else {
			GUI.Box(nodeRect, nodeName, viewSkin.GetStyle("node_default"));
		}
		EditorUtility.SetDirty(this);
	}

	public virtual void DrawNodeProperties() {
		
	}
	#endif

	void ProcessEvents(Event e, Rect viewRect) {
		if(isSelected) {

			if(e.type == EventType.mouseDrag) {

				if (nodeRect.Contains(e.mousePosition)) {
					nodeRect.x += e.delta.x;
					nodeRect.y += e.delta.y;
				}
			}
		}
	}

	protected void GetEditorSkin() {
			nodeSkin = (GUISkin)Resources.Load("GUISkins/Editor/NodeEditorSkin");
	}
}
