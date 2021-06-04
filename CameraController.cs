using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instinct import
// LEGACY Probably dont want to support
public class CameraController : MonoBehaviour {
    /*
     * Cameron Hadfield
     * CameraController.cs
     * The Camera controller is in charge of all viewport management
     * Currently only extends to mouse movement
     */
    CursorLockMode lockstate = CursorLockMode.Locked;
    Vector3 currentPos = Vector3.zero;

    public PlayerMovement playermovement;
    public float sensitivity = 1f;
    public float cameraWobble;

    public InventoryManager manager;

	void Awake () {
        Initialize();
	}
	void FixedUpdate(){
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            CameraLook();
        }
    }
	void Update () {
        
        if(Input.GetButtonDown("Unlock View"))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Initialize();
            }
            
        }
        if (Input.GetButtonDown("Fire1"))
        {
            manager.MainAction();
        }
        playermovement.Move();
	}

    public void Initialize()
    {
        Cursor.lockState = lockstate;
        currentPos = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(currentPos);
    }
    public void CameraLook()
    {
        //Get Input
        var xmovement = Input.GetAxis("Mouse X") * sensitivity;
        var ymovement = -Input.GetAxis("Mouse Y") * sensitivity;
        //Calculate new rotation
        currentPos = new Vector3(Mathf.Clamp(currentPos.x + ymovement, -85, 85), currentPos.y + xmovement, 0);
        playermovement.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, currentPos.y, 0));
        gameObject.transform.localRotation = Quaternion.Euler(new Vector3(currentPos.x, 0, 0));

    }
}
