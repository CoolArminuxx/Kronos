using UnityEngine;
using DG.Tweening;

public class Time_Cycle : MonoBehaviour
{
    private float cycleTime = 0;
    public Transform dirLight;
    public int puzzlesSolved = 0;
    private float counter;

    public int x;
    void Update()
    {
        /*
        Debug.Log(cycleTime);
        cycleTime += Time.deltaTime * 15;
        dirLight.eulerAngles = new Vector3(cycleTime, -180, 0);

        if (cycleTime >= 360)
        {
            cycleTime = 0;
        }
        */
        /*
        cycleTime += Time.deltaTime * 15;
        dirLight.DORotate(new Vector3(cycleTime, -180, 0), 10f);

        if(cycleTime >= 360)
        {
            cycleTime = 0;
        }
    }
*/
    }
    public void CycleTime()
    {
        if (puzzlesSolved < 8)
        {
            counter += 60;
            dirLight.DORotate(new Vector3(counter, -180, 0), 10f);
            puzzlesSolved++;
        }
    }
}
