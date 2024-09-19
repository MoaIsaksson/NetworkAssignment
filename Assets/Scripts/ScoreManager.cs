using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ScoreManager : NetworkBehaviour
{
    //public NetworkVariable<int> playerScore = new NetworkVariable<int>();
    public Dictionary<ulong, int> PlayerScore = new Dictionary<ulong, int>();

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: 0";
        }
    }

    public void AddScore(int amount, ulong ClientId)
    {
        if (IsServer)
        {
            if (!PlayerScore.ContainsKey(ClientId))
            {
                PlayerScore[ClientId] = 0;
            }

            PlayerScore[ClientId] += amount;
            UpdateScoreTextClientRpc(PlayerScore[ClientId], ClientId);
           
        }
    }

    //[Rpc(SendTo.Everyone)]
    [ClientRpc]
    private void UpdateScoreTextClientRpc(int newScore, ulong ClientId)
    {
        if (NetworkManager.Singleton.LocalClientId == ClientId && scoreText != null)
        {
            scoreText.text = "Score: " + newScore.ToString();
        }
    }
}
