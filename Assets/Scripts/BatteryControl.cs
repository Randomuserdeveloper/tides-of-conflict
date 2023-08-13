using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BatteryControl : NetworkBehaviour
{
    public Camera gunnerCamera;
    public Camera captainCamera;
    public float mouseSensitivity = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        float horizontalMovement = mouseSensitivity * Input.GetAxis("Mouse X"); // Input.GetAxis("Mouse X") already uses Time.Deltatime so it doesn't need to be used here.
        transform.Rotate(0, Mathf.Clamp(horizontalMovement, -90f, 90f), 0);
    }
}
