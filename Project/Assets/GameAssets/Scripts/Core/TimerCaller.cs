using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimerCaller : MonoBehaviour
{
    [SerializeField][Range(0, 20)] private float waitTime;
    public UnityEvent TimerCalback;
    public bool StartOnAvake;

    private void Awake()
    {
        if(StartOnAvake)
        StartCoroutine(Timer());
    }

    public void StartTimer(float waitTime)
    {
        this.waitTime = waitTime;
        StartCoroutine(Timer());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    private IEnumerator Timer()
    {
        float tempTime = waitTime;
        while (true)
        {
            tempTime -= Time.deltaTime;
            if (tempTime<=0)
            {
                TimerCalback?.Invoke();
                tempTime = waitTime;
            }
            yield return null;
        }
    }
}
