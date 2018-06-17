using System.Collections;
using System.Collections.Generic;
using Client;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;


public class MsgId
{
	public const short echoMsgId = 1233;
	public const short echoServerResponse = 1444;
}

public class TestEchoClient : MonoBehaviour, IServerMessageHandler
{
	private string echoMsg = "";
	private ServerConnector connector;
	private Vector2 scrollOffset = Vector3.zero;
	
	private List<string> serverResponses = new List<string>();

	private void Start()
	{
		connector = GetComponent<ServerConnector>();
	}

	private void OnGUI()
	{
		if (GUILayout.Button("connect to server"))
		{
			connector.Connect();
			connector.RegisterHandlers(new List<IServerMessageHandler>() {this});
		}
		
		echoMsg = GUILayout.TextField(echoMsg);

		if (GUILayout.Button("Send echo"))
		{
			var stringMsg = new StringMessage(echoMsg);
			connector.Send(MsgId.echoMsgId, stringMsg);
		}

		scrollOffset = GUILayout.BeginScrollView(scrollOffset);
		
		foreach (var serverMessages in serverResponses)
		{
			GUILayout.Label(serverMessages);
		}
		
		GUILayout.EndScrollView();
		
	}

	public short MessageType
	{
		get { return MsgId.echoServerResponse; }
	}

	public void Handle(NetworkMessage msg)
	{
		serverResponses.Add(msg.ReadMessage<StringMessage>().value);
	}
}
