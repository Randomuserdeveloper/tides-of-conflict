using Unity.Netcode;
using UnityEngine;

public class ShipNetwork : NetworkBehaviour
{
    [SerializeField] private NetworkObject shipNetworkObject;

    private ShipMovement shipMovement;

    private void Awake()
    {
        shipMovement = GetComponent<ShipMovement>();
    }

    [ServerRpc]
    void HandleMovementInputServerRpc(Vector3 input)
    {
        // Handle ship rotation based on input...
        Vector3 rotation = new Vector3(0, input.x, 0);
        shipMovement.ApplyRotation(rotation * shipMovement.rotationSpeed * Time.deltaTime);

        // Handle ship acceleration and speed...
        if (input.z > 0)
        {
            shipMovement.AccelerateShip(shipMovement.accerlationSpeed * Time.deltaTime);

            if (shipMovement.GetCurrentSpeed() > shipMovement.maxPositiveMovementSpeed)
            {
                shipMovement.SetCurrentSpeed(shipMovement.maxPositiveMovementSpeed);
            }
        }
        else if (input.z < 0)
        {
            shipMovement.DecelerateShip(shipMovement.accerlationSpeed * Time.deltaTime);

            if (shipMovement.GetCurrentSpeed() < shipMovement.maxNegativeMovementSpeed)
            {
                shipMovement.SetCurrentSpeed(shipMovement.maxNegativeMovementSpeed);
            }
        }

        // Handle ship translation...
        shipMovement.ApplyTranslation(shipMovement.GetCurrentSpeed() * Time.deltaTime);

        // Update ship position on the server and sync to clients
        shipNetworkObject.transform.position = shipMovement.GetCurrentPosition();
    }
}
