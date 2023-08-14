using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : NetworkBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private GameObject ShipCaptain1 = null;
    [SerializeField] private GameObject ShipCaptain2 = null;
    [SerializeField] private Camera StationaryCamera;
    private Camera[] cameraArray = new Camera[2];

    private bool hasAssignedShip = false; // To track if the player has already been assigned a ship

    private void Start()
    {
        cameraArray[0] = ShipCaptain1.GetComponent<Camera>();
        cameraArray[1] = ShipCaptain2.GetComponent<Camera>();
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (!hasAssignedShip)
        {
            hostButton.onClick.AddListener(() =>
            {
                AssignShipToPlayerServerRpc(NetworkManager.Singleton.LocalClientId);
                hasAssignedShip = true;
            });

            clientButton.onClick.AddListener(() =>
            {
                AssignShipToPlayerServerRpc(NetworkManager.Singleton.LocalClientId);
                hasAssignedShip = true;
            });
        }
    }

    [ServerRpc]
    private void AssignShipToPlayerServerRpc(ulong clientID, ServerRpcParams serverRpcParams = default)
    {
        if (clientID == NetworkManager.Singleton.LocalClientId)
        {
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
}
