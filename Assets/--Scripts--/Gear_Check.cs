using UnityEngine;
using System.Linq;

public class Gear_Check : MonoBehaviour
{
    [SerializeField] private Gear_Detect[] gears;
    [SerializeField] private Time_Cycle timeRef;
    private bool allTrue;

    public void Check()
    {
        allTrue = true;
        for (int i = 0; i < gears.Length; i++)
        {
            if (gears[i].active == false)
            {
                allTrue = false;
                break;
            }
        }

        if (allTrue == true)
        {
            timeRef.CycleTime();
        }
    }
}
