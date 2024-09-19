using UnityEngine;
using TMPro;

public class ChatMessage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MessageText;

    public void SetText(string text)
    {
        MessageText.text = text;
    }
}
