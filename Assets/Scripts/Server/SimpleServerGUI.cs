using System.Collections;
using System.Collections.Generic;
using Server;
using UnityEngine;
using UnityEngine.Networking;

public class SimpleServerGUI : MonoBehaviour
{
	[SerializeField]
	private UnityNetworkingServerServer server;

	private int serverPort = 45555;

	private string serverStatus = "disconnected";
	private List<string> serverErrors = new List<string>();

	private void Awake()
	{
		server.OnServerConnected += OnServerConnected;
		server.OnServerDisconnect += OnServerDisconnected;
		server.OnServerError += OnServerError;
	}
	
	private void OnServerConnected(bool success)
	{
		serverStatus = "connected";
	}

	private void OnServerDisconnected(bool succes)
	{
		serverStatus = "disconnected";
	}

	private void OnServerError(string errorMsg)
	{
		serverErrors.Add(errorMsg);
	}
	
	private void OnGUI()
	{
		GUILayout.Space(300);
		
		var probablyServerPort = 0;
		var input = GUILayout.TextField(serverPort.ToString());
		if (int.TryParse(input, out probablyServerPort))
		{
			serverPort = probablyServerPort;
		}

		if (!NetworkServer.active)
		{
			if (GUILayout.Button("Start"))
			{
				server.StartServer(serverPort);
			}
		}
		else
		{
			if (GUILayout.Button("Restart"))
			{
				server.Restart();
			}

			if (GUILayout.Button("Stop"))
			{
				server.Shutdown();
			}
		}

		if (!NetworkServer.active)
			return;
		
		
		GUILayout.Space(20);
		
		GUILayout.Label("Server status:" + serverStatus);
		
		GUILayout.Label("Server Errors");

		foreach (var serverError in serverErrors)
		{
			GUILayout.Label(serverError);
			Debug.LogError(serverError);
		}
	}

}