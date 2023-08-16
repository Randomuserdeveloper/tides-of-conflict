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
    [SerializeField] private List<int> ShipIdList;
    [SerializeField] private Camera StationaryCamera;
    private Camera[] cameraArray = new Camera[2];
    private float playerShipID = 0;

    private bool hasAssignedShip = false;

    private void Start()
    {
        cameraArray[0] = ShipCaptain1.GetComponent<Camera>();
        cameraArray[1] = ShipCaptain2.GetComponent<Camera>();
        Debug.Log(NetworkManager.Singleton.LocalClientId);
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (!hasAssignedShip)
        {
            hostButton.onClick.AddListener(() =>
            {
                AssignShipToPlayerClientRpc(NetworkManager.Singleton.LocalClientId);
                hasAssignedShip = true;
            });

            clientButton.onClick.AddListener(() =>
            {
                AssignShipToPlayerClientRpc(NetworkManager.Singleton.LocalClientId);
                hasAssignedShip = true;
            });
        }
    }

    [ClientRpc]
    private void AssignShipToPlayerClientRpc(ulong clientID)
    {
        if (!IsOwner) return;

            if (clientID == 0)
            {
                ShipCaptain1.GetComponent<NetworkObject>().ChangeOwnership(clientID);
                StationaryCamera.gameObject.SetActive(false);
                cameraArray[0].gameObject.SetActive(true);
            }
            else if (clientID == 1)
            {
                ShipCaptain2.GetComponent<NetworkObject>().ChangeOwnership(clientID);
                StationaryCamera.gameObject.SetActive(false);
                cameraArray[1].gameObject.SetActive(true);
            }
    }
}
