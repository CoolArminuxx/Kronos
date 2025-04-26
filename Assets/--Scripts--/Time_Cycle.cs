using UnityEngine;

public class Time_Cycle : MonoBehaviour
{
    private float cycleTime;
    public Transform dirLight;

    void Update()
    {
        Debug.Log(cycleTime);
        cycleTime += Time.deltaTime * 15;
        dirLight.eulerAngles = new Vector3(cycleTime, -180, 0);

        if (cycleTime >= 360)
        {
            cycleTime = 0;
        }
    }
}
