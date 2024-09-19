using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class ChatManager : NetworkBehaviour
{
    public static ChatManager Singleton;

    [SerializeField] ChatMessage ChatMessagePrefab;
    [SerializeField] CanvasGroup ChatContent;
    [SerializeField] TMP_InputField ChatInput;

    public String PlayerName;

    private void Awake()
    {
        ChatManager.Singleton = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage(ChatInput.text, PlayerName);
            ChatInput.text = "";
        }
    }

    public void SendChatMessage(string Message, string FromWho = null)
    {
        if (string.IsNullOrWhiteSpace(Message)) return;

        string S = FromWho + " > " + Message;
        SendChatMessageServerRpc(S);
    }

    void AddMessage(string Msg)
    {
        ChatMessage CM = Instantiate(ChatMessagePrefab, ChatContent.transform);
        CM.SetText(Msg);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SendChatMessageServerRpc(string message)
    {
        RecieveChatMessageClientRpc(message);
    }

    [ClientRpc]
    private void RecieveChatMessageClientRpc(string message)
    {
       ChatManager.Singleton.AddMessage(message);
    }
}
