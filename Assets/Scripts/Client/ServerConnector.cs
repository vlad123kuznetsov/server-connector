using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Client
{
    public class ServerConnector : MonoBehaviour, IServerConnector
    {
        [SerializeField] private string url;
        [SerializeField] private int port;

        private NetworkClient client;
    
        public void Connect()
        {
            client = new NetworkClient();
            client.Connect(url, port);
        
            OnServerConnectedResult(true);
        }
    
        public void DisconectFromServer()
        {
            if (client != null)
            {
                client.Disconnect();
                OnDisonnectedFromServer(true);
            }
            else
            {
                Debug.LogError("You should connect to server first");
            }
        }

        public event Action<bool> OnServerConnectedResult = delegate(bool b) {  };
        public event Action<bool> OnDisonnectedFromServer = delegate(bool b) {  };
    
        public void Send(short msgId, MessageBase msg)
        {
            if (client != null)
            {
                client.Send(msgId, msg);
            }
            else
            {
                Debug.LogError("You should connect to server first");
            }
        }

        public void RegisterHandlers(IEnumerable<IServerMessageHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                client.RegisterHandler(handler.MessageType, handler.Handle);
            }
        }
    }
}