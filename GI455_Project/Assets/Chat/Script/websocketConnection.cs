using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using TMPro;
using System;

namespace Chat
{
    public class websocketConnection : MonoBehaviour
    {
        public TMP_Text ChatDisplay;
        public GameObject NameInsertField, PortInsertField, IPInsertField, MessageInsertField;
        private WebSocket websocket;
        string inputName, inputPort, inputIP, inputMessage, msgFromServer;
        bool updateCheck = false;

        // Update is called once per frame
        void Update()
        {
            if(updateCheck == true)
            {
                UpdateChat();
                updateCheck = false;
            }
        }

        public void ConnectToChat()
        {
            inputName = NameInsertField.GetComponent<Text>().text;
            inputPort = PortInsertField.GetComponent<Text>().text;
            inputIP = IPInsertField.GetComponent<Text>().text;

            if (inputName == null)
            {
                inputName = "Anonymous";
            }

            if (inputIP != null && inputPort != null)
            {
                websocket = new WebSocket("ws://" + inputIP + ":" + inputPort + "/");
                websocket.Connect();
                websocket.OnMessage += OnMessage;
                websocket.Send(inputName);

                ChatDisplay.GetComponent<TMP_Text>().text += "\n<align=center>Chat Connected.\n";
            }
        }

        public void SendChatMessage()
        {
            //websocket.OnMessage += OnMessage;
            inputMessage = MessageInsertField.GetComponent<Text>().text;
            websocket.Send(inputMessage);
        }

        public void OnDestroy()
        {
            if(websocket != null)
            {
                ChatDisplay.GetComponent<TMP_Text>().text += "\n<align=center>Chat disconnected.";
                websocket.Close();
            }
        }

        public void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Debug.Log("Message from server: " + messageEventArgs.Data);
            msgFromServer = messageEventArgs.Data;
            updateCheck = true;
        }

        public void UpdateChat()
        {
            if (msgFromServer.Contains("-=-0-=[]p[]p=p=0-=0"))
            {
                msgFromServer = msgFromServer.Replace("-=-0-=[]p[]p=p=0-=0", "");
                ChatDisplay.GetComponent<TMP_Text>().text += ("<align=right>" + msgFromServer + "\n");
                
                msgFromServer = null;
            }
            else
            {
                ChatDisplay.GetComponent<TMP_Text>().text += ("<align=left>" + msgFromServer + "\n");
                msgFromServer = null;
            }
        }

    }

}

