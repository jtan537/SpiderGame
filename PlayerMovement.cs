using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instinct import
// LEGACY Probably dont want to support
public class PlayerMovement : MonoBehaviour {
    /*
     * Cameron Hadfield
     * PlayerMovement.cs
     * Used to control the keyboard based player movement system
     * Used alongside the CameraController
     */
    [SerializeField]Vector3 movementVector;
    Rigidbody rb;
    CapsuleCollider coll;
    public float speed;
    [SerializeField]private bool grounded;
    //This gives a scene based static reference to this instance of playermovement
    public static PlayerMovement comp;
    Transform cameraObject;
    float colliderHeight;
    bool crouching;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        cameraObject = transform.GetChild(0);
        comp = this;
        colliderHeight = coll.height;
    }
    public void Crouch(bool crouching)
    {
        if(crouching){
            coll.height= colliderHeight*.8f;
            transform.position = transform.position - new Vector3(0, colliderHeight/4, 0);
        }
        else{
            coll.height = colliderHeight;
        }
        this.crouching = crouching;
        cameraObject.localPosition = new Vector3(0,coll.height/3,0);
    }
    public void Move()
    {
        //Take Input
        var xmove = Input.GetAxis("Horizontal");
        var zmove = Input.GetAxis("Vertical");

        if(xmove == 0 && zmove == 0)
        { rb.drag = 100; }
        else
        { rb.drag = 0; }
        //Create movement vector
        movementVector = transform.forward * zmove + transform.right * xmove;
        RaycastHit hit;
        //Cast downwards to determine what the normal of the plane below is
        //(If there is one)
        if (Physics.SphereCast(new Ray(gameObject.transform.position, Vector3.down), coll.radius , out hit, coll.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        //Change the movement vector to fit the plane on which the character stands
        //movementVector = Vector3.ProjectOnPlane(movementVector.normalized, hit.normal);
        //Add custom speed
        movementVector = movementVector.normalized * speed * (crouching? .3f : 1f);
        //Add falling if the player is not grounded
        
        //Finally, move the player
        rb.AddForce(movementVector, ForceMode.Force);
    }
}

