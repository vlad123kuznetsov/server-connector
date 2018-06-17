using System.Collections;
using System.Collections.Generic;
using Client;
using Server;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

/*

public class ChatModule : IServerFeature
{
	public IEnumerable<IServerHandler> Handlers()
	{
		return new List<IServerHandler>()
		{
			new ChatPrintHandler(),
			new ChatMessageHandler()
		}
	}
}

public class ChatPrintHandler : IServerHandler
{
	public short MessageType { get; }
	public void Handle(NetworkMessage message)
	{
		throw new System.NotImplementedException();
	}
}

public class ChatMessageHandler : IServerHandler
{
	public short MessageType { get; }
	public void Handle(NetworkMessage message)
	{
		throw new System.NotImplementedException();
	}
}
*/

public class ServerEchoTest : MonoBehaviour, IServerFeature, IServerHandler
{
	private UnityNetworkingServerServer serverConnector;

	private void Awake()
	{
		serverConnector = GetComponent<UnityNetworkingServerServer>();
		serverConnector.RegisterFeatureHandlers(new List<IServerFeature>() {this});
	}
	
	public IEnumerable<IServerHandler> Handlers()
	{
		return new List<IServerHandler>()
		{
			this
		};
	}

	public short MessageType
	{
		get { return MsgId.echoMsgId; }
	}
	
	public void Handle(NetworkMessage message)
	{
		var echoMsg = message.ReadMessage<StringMessage>().value;
		echoMsg += " from server";

		var responseMsg = new StringMessage(echoMsg);
		serverConnector.Send(serverConnector.ActiveConnections, MsgId.echoServerResponse, responseMsg);
	}
}
