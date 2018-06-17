using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Client;
using Server;
using UnityEngine;

public class LaunchMode : MonoBehaviour
{
	private GameObject activeMode;
	
	[SerializeField]
	private bool launchAsServer;
	
	private bool IsServer()
	{
		#if UNITY_EDITOR
			return launchAsServer;
		#endif
		
		var consoleArgs = Environment.GetCommandLineArgs();
		
		if (consoleArgs.Length == 0 || consoleArgs.Contains("client"))
		{
			return false;
		}
		else if(consoleArgs.Contains("server"))
		{
			return true;
		}

		return true;
	}
	
	private void Start()
	{
		var isServer = IsServer();
		if (isServer)
		{
			Debug.Log("launched as server");
			LaunchServer();
		}
		else
		{
			Debug.Log("launched as client");
			LaunchClient();
		}
	}

	
	
	private void LaunchClient()
	{
		var activeModePrefab = Resources.Load<GameObject>("modes/client");
		activeMode = Instantiate<GameObject>(activeModePrefab);

	}

	private void LaunchServer()
	{
		var activeModePrefab = Resources.Load<GameObject>("modes/server");
		activeMode = Instantiate<GameObject>(activeModePrefab);
	}
}
