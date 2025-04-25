using UnityEngine;

public class Character_Pickup : MonoBehaviour
{
    [SerializeField] private Transform holdArea;                  //Store location which prop will be held in
    private float pickupForce = 150.0f;  //How fast prop travels to towards player
    private float pickupRange = 2.0f;    //Distance between player and prop
    private GameObject heldObj;                           //Store object which will be held
    private Rigidbody heldObjRB;                          //Store rigidbody of held object
    private Transform cameraRot;

    void Start()
    {
        cameraRot = GetComponentInChildren<Camera>().gameObject.transform;
    }

    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(cameraRot.position, cameraRot.TransformDirection(Vector3.forward * pickupRange));          //Draw raycast line in scene view

        if (Physics.Raycast(cameraRot.position, cameraRot.TransformDirection(Vector3.forward), out hit, pickupRange))
        {
            if (hit.transform.gameObject.tag == "item")//Check if ray hits box object
            {
                if (Input.GetMouseButtonDown(0))//Check if LMB is pressed down
                {
                    if (heldObj == null)
                    {
                        PickupObject(hit.transform.gameObject);//Call on pickup function to pickup box
                    }
                    else
                    {
                        DropObject();//Call on drop function to drop box
                    }
                }
            }
        }
        if (heldObj != null)//If object is held
        {
            MoveObject();//Call on move function to move box around
        }
    }

    void MoveObject()//Controls the distance between the held object and the held area so that the object will always move towards holding area by the determined force
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }
    void PickupObject(GameObject pickObj)//Modify rigidbody of object so that the object is light, does not rotate, does not use gravity and contains some amount of drag force when moving it around
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.linearDamping = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
            heldObjRB.mass = 20;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }
    void DropObject()//Revert the properties of the object ridigbody back to its original state and has normal gravity
    {
        heldObjRB.useGravity = true;
        heldObjRB.linearDamping = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;
        heldObjRB.mass = 200;

        heldObj.transform.parent = null;
        heldObj = null;
    }
}
