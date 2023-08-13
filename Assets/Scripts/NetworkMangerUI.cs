using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkMangerUI : NetworkBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private GameObject ShipSystem;

    private void Awake()
    {
        ShipSystem.SetActive(false);

        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
/*            ShipSystem.SetActive(true);*/
            Destroy(hostButton.gameObject);
            Destroy(clientButton.gameObject);
        });

        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
/*            ShipSystem.SetActive(true);*/
            Destroy(hostButton.gameObject);
            Destroy(clientButton.gameObject);
        });
    }
}
