using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timer;
    public float deciseconds;
    public int minutes;
    public int seconds;
    public float total;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
        //timer.text = Time.timeSinceLevelLoad.ToString();
    }

    private void updateTimer()
    {
        deciseconds += Time.deltaTime;
        total += Time.deltaTime;
        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + ((int)(deciseconds*100)).ToString("00");
        if (deciseconds >= 1)
        {
            seconds++;
            deciseconds %= 1;
        }
        if (seconds >= 60)
        {
            minutes++;
            seconds %= 60;
        }
        
    }
}
