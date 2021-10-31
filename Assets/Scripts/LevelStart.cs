using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStart : MonoBehaviour
{
    public Text countdown;
    // Start is called before the first frame update
    void Start()
    {
        countdown.fontSize = 200;
        StartCoroutine(levelStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        countdown.fontSize = 100;
        countdown.text = "Game Over!";
        countdown.gameObject.SetActive(true);
    }

    private IEnumerator levelStart()
    {
        countdown.text = "3";
        yield return new WaitForSecondsRealtime(1);
        countdown.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "2";
        countdown.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        countdown.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "1";
        countdown.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        countdown.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "GO!";
        countdown.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        countdown.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
