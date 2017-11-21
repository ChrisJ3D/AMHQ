using System;
using NodeEditorFramework;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// basic dialog node class, all other dialog nodes are derived from this
/// </summary>
namespace AMHQ {
	[Node(true, "Base/Conversation", new Type[]{typeof(AMHQCanvas)})]
	public abstract class BaseConversationNode : Node
	{
		public override bool AllowRecursion { get { return true; } }
		public abstract Type GetObjectType { get; }

		public override Vector2 MinSize { get { return new Vector2(350, 200); } }
		public override bool AutoLayout { get { return true;}}  //resizable renamed to autolayout?

		[FormerlySerializedAs("SayingCharacterName")]
		public string CharacterName;
		[FormerlySerializedAs("SayingCharacterPotrait")]
		public Sprite CharacterPotrait;
		[FormerlySerializedAs("WhatTheCharacterSays")]
		public string DialogLine;

		public AudioClip SoundDialog;

		[SerializeField]
		public int speakerIndex;

		public abstract BaseConversationNode GetDownstreamNode(int inputValue);
		public abstract bool IsBackAvailable();
		public abstract bool IsNextAvailable();

		public virtual BaseConversationNode PassAhead(int inputValue)
		{
			//	This method is required for non-dialogue nodes
			return this;
		}

		///check if the first connection of the specified port points to something
		protected bool IsAvailable(ConnectionPort port)
		{
			return port != null
				&& port.connections != null && port.connections.Count > 0
				&& port.connections[0].body != null
				&& port.connections[0].body != default(Node);
		}

		///return the dialog node pointed to by the first connection in the specified port
		protected BaseConversationNode getTargetNode(ConnectionPort port) {
			if (IsAvailable (port))
				return port.connections [0].body as BaseConversationNode;
			return null;
		}

		public virtual void Compile() {}
	}

	public class DialogueBackType : ConnectionKnobStyle //: IConnectionTypeDeclaration
	{
		public override string Identifier { get { return "DialogueBack"; } }
		public override Color Color { get { return Color.yellow; } }
	}

	public class DialogueForwardType : ValueConnectionType // : IConnectionTypeDeclaration
	{
		public override string Identifier { get { return "DialogueForward"; } }
		public override Type Type { get { return typeof(float); } }
		public override Color Color { get { return Color.cyan; } }
	}
}