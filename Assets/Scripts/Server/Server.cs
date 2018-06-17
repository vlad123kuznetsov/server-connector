using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public interface IServerHandler
{
	short MessageType { get; }
	void Handle(NetworkMessage message);
}

public interface IServerFeature
{
	IEnumerable<IServerHandler> Handlers();
}

public interface IServer
{
	void StartServer(int port);
	void Restart();
	void Shutdown();
	
	void Send(IEnumerable<int> connectionId, short msgType, MessageBase msg);
	void RegisterFeatureHandlers(IEnumerable<IServerFeature> handlers);

	event Action<bool> OnServerConnected;
	event Action<string> OnServerError;
	event Action<bool> OnServerDisconnect;
	
	IEnumerable<int> ActiveConnections { get; }
}