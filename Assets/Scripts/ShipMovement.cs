using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShipMovement : NetworkBehaviour
{
    public float rotationSpeed = 4f;
    public float accerlationSpeed = 4f;
    public float maxPositiveMovementSpeed = 16f;
    public float maxNegativeMovementSpeed = -8f;

    private float currentSpeed;

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

    private void Update()
    {
        if (!IsOwner) return;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        Vector3 rotation = new Vector3(0, input.x, 0);
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);

        if (input.z > 0)
        {
            AccelerateShip(accerlationSpeed * Time.deltaTime);
        }

        if (input.z <  0)
        {
            DecelerateShip(accerlationSpeed * Time.deltaTime);
        }

        transform.Translate(Vector3.forward * GetCurrentSpeed() * Time.deltaTime);
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public void SetCurrentSpeed(float currentSpeed)
    {
        this.currentSpeed = currentSpeed;
    }
}