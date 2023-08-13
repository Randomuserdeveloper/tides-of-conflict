using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : NetworkBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private GameObject ShipCaptain1;
    [SerializeField] private GameObject ShipCaptain2;
    [SerializeField] private Camera StationaryCamera;
    private Camera[] cameraArray = new Camera[2];


    // Start is called before the first frame update
    void Start()
    {
        cameraArray[0] = ShipCaptain1.GetComponent<Camera>();
        cameraArray[1] = ShipCaptain2.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        hostButton.onClick.AddListener(() =>
        {
            AssignShipToPlayerServerRpc();
        });

        clientButton.onClick.AddListener(() =>
        {
            AssignShipToPlayerServerRpc();
        });
    }

    [ServerRpc]
    public void AssignShipToPlayerServerRpc(ServerRpcParams serverRpcParams = default)
    {
        var clientID = serverRpcParams.Receive.SenderClientId;

        if (clientID == 0)
        {
            ShipCaptain1.GetComponent<NetworkObject>().ChangeOwnership(clientID);
            StationaryCamera.gameObject.SetActive(false);
            cameraArray[0].gameObject.SetActive(true);
        }

        if (clientID == 1)
        {
            ShipCaptain2.GetComponent<NetworkObject>().ChangeOwnership(clientID);
            StationaryCamera.gameObject.SetActive(false);
            cameraArray[1].gameObject.SetActive(true);
        }
    }
}
