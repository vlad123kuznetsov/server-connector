using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

namespace Server
{
    public class UnityNetworkingServerServer : MonoBehaviour, IServer
    {
        public int portAdress;
	
        public event Action<bool> OnServerConnected = delegate(bool b) {  };
        public event Action<string> OnServerError = delegate(string s) {  };
        public event Action<bool> OnServerDisconnect= delegate(bool b) {  };

        public void Awake()
        {
            NetworkServer.RegisterHandler(MsgType.Connect, OnConnectToServer);
            NetworkServer.RegisterHandler(MsgType.Disconnect, OnDisconnectFromServer);
            NetworkServer.RegisterHandler(MsgType.Error, OnServerErrorMethod);
        }
        
        public void StartServer(int port)
        {
            NetworkServer.Listen(port);
            Debug.Log("Start listening server on port " + port);
        }

        public void Restart()
        {
            Shutdown();
            StartServer(portAdress);
        }

        public void Shutdown()
        {
            NetworkServer.Shutdown();
            Debug.Log("Stop server");
        }

        private void OnConnectToServer(NetworkMessage msg)
        {
            OnServerConnected(true);
        }

        private void OnDisconnectFromServer(NetworkMessage msg)
        {
            OnServerDisconnect(true);
        }

        private void OnServerErrorMethod(NetworkMessage msg)
        {
            var error = msg.ReadMessage<ErrorMessage>();
            OnServerError(error.ToString());
        }

        public void Send(IEnumerable<int> connectionId, short msgType, MessageBase msg)
        {
            NetworkServer.SendToAll(msgType, msg);
        }

        public void RegisterFeatureHandlers(IEnumerable<IServerFeature> handlers)
        {
            foreach (var handler in handlers)
            {
                foreach (var serverHandler in handler.Handlers())
                {
                    NetworkServer.RegisterHandler(serverHandler.MessageType, serverHandler.Handle);
                }
            }
        }

        public IEnumerable<int> ActiveConnections
        {
            get { 
                
                var connections =  NetworkServer.connections;
                var intConnection = connections.Select(p => p.connectionId);
                return intConnection;

            }
        }
    }
}