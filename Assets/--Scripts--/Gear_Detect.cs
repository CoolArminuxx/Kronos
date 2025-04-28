using UnityEngine;

public class Gear_Detect : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject placed;
    [SerializeField] private Character_Pickup pickupRef;
    [SerializeField] private Time_Cycle timeRef;
    [SerializeField] private string cogTypes;
    [SerializeField] private GameObject placeText;
    [SerializeField] private bool active;
    [SerializeField] private Gear_Detect[] gears;
    private bool inRange;

    private void Update()
    {
        if (inRange == true && Input.GetMouseButtonDown(1))
        {
            Destroy(pickupRef.heldObj);
            placed.SetActive(true);
            highlight.SetActive(false);
            placeText.SetActive(false);
            active = true;
            Check();
        }
    }

    private void Check()
    {
        for (int i = 0; i < gears.Length; i++)
        {
            if (gears[i].active == false)
            {
                return;
            }
            else
            {
                timeRef.CycleTime();
            }
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
