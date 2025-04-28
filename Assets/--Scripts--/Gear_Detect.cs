using UnityEngine;

public class Gear_Detect : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject placed;
    [SerializeField] private Character_Pickup pickupRef;
    [SerializeField] private string cogTypes;
    [SerializeField] private GameObject placeText;
    [SerializeField] private Gear_Check machineCheck;
    public bool active;
    private bool inRange;

    private void Update()
    {
        if (inRange == true && Input.GetMouseButtonDown(1) && active == false)
        {
            Destroy(pickupRef.heldObj);
            placed.SetActive(true);
            highlight.SetActive(false);
            placeText.SetActive(false);
            active = true;
            machineCheck.Check();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == cogTypes)
        {
            inRange = true;
            highlight.SetActive(true);
            placeText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == cogTypes)
        {
            inRange = false;
            highlight.SetActive(false);
            placeText.SetActive(false);
        }
    }
}
