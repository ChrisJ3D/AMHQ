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
	public int numberOfInputs;
	public int numberOfOutputs;

	public System.Object nodeValue;

	public Vector2 size = new Vector2(24f, 24f);
	public Vector2 position = new Vector2(0.0f, 0.0f);

	//	PRIVATE VARIABLES
	protected GUISkin nodeSkin;

	//	MAIN FUNCTIONS
	public virtual void InitNode() {
		inputs = new List<NE_NodeInput>();
		outputs = new List<NE_NodeOutput>();

		GetEditorSkin();
		CreateConnectors();
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

	protected void CreateConnectors() {
		inputs.Clear();
		outputs.Clear();

		//	Create inputs
		for(int i = 0; i < numberOfInputs; i++) {
			NE_NodeInput input =  (NE_NodeInput)ScriptableObject.CreateInstance<NE_NodeInput>();
			input.index = i;
			input.parentNode = this;
			inputs.Add(input);
		}

		for (int i = 0; i < numberOfOutputs; i++) {
			NE_NodeOutput output =  (NE_NodeOutput)ScriptableObject.CreateInstance<NE_NodeOutput>();
			output.index = i;
			output.parentNode = this;
			outputs.Add(output);			
		}
	}

	//	GUI STUFF

	public void DrawConnectors() {
		if (inputs != null || outputs != null) {

			foreach(NE_NodeInput input in inputs) {

				input.GetConnectionPosition();

				if(GUI.Button(new Rect(input.position.x, input.position.y, input.size.x, input.size.y), "", nodeSkin.GetStyle("node_input"))) {
					if (parentGraph != null) {
						Debug.Log("input " + input.index + " clicked, with a Y position of " + input.position.y);

						parentGraph.wantsConnection = false;
						parentGraph.connectionNode = null;
					}
				}
			}

			foreach(NE_NodeOutput output in outputs) {
				if(GUI.Button(new Rect(nodeRect.x + nodeRect.width, nodeRect.y + (nodeRect.height * 0.5f) - 12f, 24f, 24f), "", nodeSkin.GetStyle("node_output"))) {
					if (parentGraph != null) {
						parentGraph.wantsConnection = true;
					}
				}
			}
		} else {
			InitNode();
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

		if(!nodeSkin) {
			InitNode();
		}

		if(isSelected){
			GUI.Box(nodeRect, nodeName, viewSkin.GetStyle("node_selected"));
		} else {
			GUI.Box(nodeRect, nodeName, viewSkin.GetStyle("node_default"));
		}

		DrawConnectors();

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
					position.x += e.delta.x;
					nodeRect.y += e.delta.y;
					position.y += e.delta.y;
				}
			}
		}
	}

	protected void GetEditorSkin() {
			nodeSkin = (GUISkin)Resources.Load("GUISkins/Editor/NodeEditorSkin");
	}
}
