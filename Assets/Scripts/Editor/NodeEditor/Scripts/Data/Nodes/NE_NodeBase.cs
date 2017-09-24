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
	public Vector2 position = new Vector2(0.0f, 0.0f);
	public Vector2 size = new Vector2(24f, 24f);

	public NE_NodeGraph parentGraph;
	public NodeType nodeType;
	public bool isSelected = false;

	public List<NE_NodeConnectorBase> connectors;
	public List<NE_NodeInput> inputs;
	public List<NE_NodeOutput> outputs;
	public int numberOfInputs;
	public int numberOfOutputs;

	public System.Object nodeValue;

	public GUISkin nodeSkin;

	//	MAIN FUNCTIONS
	public virtual void InitNode() {
		if (connectors == null) {
			inputs = new List<NE_NodeInput>();
			outputs = new List<NE_NodeOutput>();
			connectors = new List<NE_NodeConnectorBase>();
		}

		GetEditorSkin();

		//	This line is problematic, since it resets the lists everytime. This should be moved up once I've got serialization figured out.
		CreateConnectors();
	}

	void OnEnable() {
		InitNode();
	}

	void OnDisable() {
		Debug.Log("Saving assets!!");
		AssetDatabase.SaveAssets();
	}

	public virtual void OnClicked() {
		Evaluate();
		NE_NodeUtils.SaveGraph();
	}

	//	EVALUATION

	public virtual void Evaluate() {
		throw new NotImplementedException("Node is trying to evaluate but has no method to do so");
	}

	public virtual float EvaluateAsFloat() {
		Evaluate();
		Debug.Log("Evaluating to " + Convert.ToSingle(nodeValue));
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
		connectors.Clear();

		//	Create inputs
		for(int i = 0; i < numberOfInputs; i++) {
			NE_NodeInput input =  (NE_NodeInput)ScriptableObject.CreateInstance(typeof(NE_NodeInput));
			input.index = i;
			input.parentNode = this;
			inputs.Add(input);
			connectors.Add(input);
		}

		for (int i = 0; i < numberOfOutputs; i++) {
			NE_NodeOutput output =  (NE_NodeOutput)ScriptableObject.CreateInstance(typeof(NE_NodeOutput));
			output.index = i;
			output.parentNode = this;
			outputs.Add(output);	
			connectors.Add(output);
		}
	}

	//	GUI STUFF

	public void DrawConnectors() {
		if (connectors != null) {
			foreach (NE_NodeConnectorBase connector in connectors) {
				connector.GetConnectionPosition();
				connector.DrawGUI();
			}
		} else {
			InitNode();
		}
	}

	#if UNITY_EDITOR
	public virtual void UpdateNodeGUI(Event e, GUISkin viewSkin) {
		ProcessEvents(e);

		if(isSelected){
			GUI.Box(nodeRect, nodeName, viewSkin.GetStyle("node_selected"));
		} else {
			GUI.Box(nodeRect, nodeName, viewSkin.GetStyle("node_default"));
		}

		DrawConnectors();

		if (connectors != null) {
			foreach (NE_NodeConnectorBase connector in connectors) {

				connector.DrawConnections();

				if (connector.wantsConnection) {
					NE_NodeUtils.DrawLineToMouse(connector, e.mousePosition, "Right");
				}
			}
		}

		EditorUtility.SetDirty(this);
	}

	public virtual void DrawNodeProperties() {
		
	}
	#endif

	void ProcessEvents(Event e) {
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
