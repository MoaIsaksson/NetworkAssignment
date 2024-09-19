using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUIManager : MonoBehaviour
{
    [SerializeField] Button hostbutton;
    [SerializeField] Button clientbutton;
    [SerializeField] Button quitbutton;
 
    [SerializeField] GameObject canvas1;

    private void Start()
    {
        hostbutton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            canvas1.SetActive(false);
        });
        clientbutton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            canvas1.SetActive(false);
        });


        quitbutton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            canvas1.SetActive(true);
        }
    }

   /* private void OnHostButtonClicked()
    {
        NetworkManager.Singleton.StartHost();
        canvas1.SetActive(false);
        Debug.Log("Host Started");
    }

    private void OnClientButtonClicked()
    {
        NetworkManager.Singleton.StartClient();
        canvas1.SetActive(false); 
        Debug.Log("Client started");
    }
   
    */

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
