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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        Vector3 rotation = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed += accerlationSpeed * Time.deltaTime;

            if (currentSpeed > maxPositiveMovementSpeed)
            {
                currentSpeed = maxPositiveMovementSpeed;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            currentSpeed -= accerlationSpeed * Time.deltaTime;

            if (currentSpeed < maxNegativeMovementSpeed)
            {
                currentSpeed = maxNegativeMovementSpeed;
            }
        }

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}
