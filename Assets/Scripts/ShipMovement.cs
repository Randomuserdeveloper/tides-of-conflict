using Unity.Netcode;
using UnityEngine;

public class ShipMovement : NetworkBehaviour
{
    public float rotationSpeed = 4f;
    public float accerlationSpeed = 4f;
    public float maxPositiveMovementSpeed = 16f;
    public float maxNegativeMovementSpeed = -8f;
    private float currentSpeed;

    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    public void ApplyRotation(Vector3 rotation)
    {
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }

    public void AccelerateShip(float acceleration)
    {
        currentSpeed += acceleration;

        if (currentSpeed > maxPositiveMovementSpeed)
        {
            currentSpeed = maxPositiveMovementSpeed;
        }
    }

    public void DecelerateShip(float deceleration)
    {
        currentSpeed -= deceleration;

        if (currentSpeed < maxNegativeMovementSpeed)
        {
            currentSpeed = maxNegativeMovementSpeed;
        }
    }

    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public void SetCurrentSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }

    public void ApplyTranslation(float translation)
    {
        transform.Translate(Vector3.forward * translation);
    }

    private void Update()
    {
        if (!IsOwner) return;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        // Handle ship rotation based on input...
        Vector3 rotation = new Vector3(0, input.x, 0);
        ApplyRotation(rotation);

        // Handle ship acceleration and speed...
        if (input.z > 0)
        {
            AccelerateShip(accerlationSpeed * Time.deltaTime);
        }
        else if (input.z < 0)
        {
            DecelerateShip(accerlationSpeed * Time.deltaTime);
        }

        Debug.Log(GetCurrentSpeed());

        // Handle ship translation...
        ApplyTranslation(GetCurrentSpeed() * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;

        // Synchronize the position change over the network
        if (Vector3.Distance(transform.position, lastPosition) > 0.01f)
        {
            lastPosition = transform.position;
            ApplyMovementOnServerRpc(transform.position);
        }
    }

    [ServerRpc]
    private void ApplyMovementOnServerRpc(Vector3 newPosition)
    {
        // Update ship's position on the server and sync to clients
        transform.position = newPosition;
    }
}
