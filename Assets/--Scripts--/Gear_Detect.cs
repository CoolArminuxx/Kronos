using UnityEngine;

public class Gear_Detect : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject placed;
    [SerializeField] private Character_Pickup pickupRef;
    [SerializeField] private Time_Cycle timeRef;
    private bool inRange;

    private void Update()
    {
        if (inRange == true && Input.GetMouseButtonDown(1))
        {
            Destroy(pickupRef.heldObj);
            placed.SetActive(true);
            highlight.SetActive(false);
            timeRef.CycleTime();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "item")
        {
            inRange = true;
            highlight.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "item")
        {
            inRange = false;
            highlight.SetActive(false);
        }
    }
}
