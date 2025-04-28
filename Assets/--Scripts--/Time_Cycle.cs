using UnityEngine;
using DG.Tweening;

public class Time_Cycle : MonoBehaviour
{
    private float cycleTime;
    public Transform dirLight;
    private float counter;

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
    }

    public void CycleTime()
    {
        counter += 20;
        dirLight.DORotate(new Vector3(counter, -180, 0), 10f);
    }
}
