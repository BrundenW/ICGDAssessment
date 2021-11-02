using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private SoundManager sound;
    public Text score;
    public Text time;

    private void Start()
    {
        sound = GetComponent<SoundManager>();
        if (PlayerPrefs.HasKey("Score"))
        {
            score.text = "Previous High Score: " + PlayerPrefs.GetInt("Score").ToString();
            time.text = "Time: " + PlayerPrefs.GetInt("Mins").ToString("00") + ":" + PlayerPrefs.GetInt("Secs").ToString("00") + ":" + (PlayerPrefs.GetFloat("Decisecs")*100).ToString("00");
        }
    }
    public void LevelOneButton()
    {
        sound.musicStop();
        Time.timeScale = 0;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LevelTwoButton()
    {
        Debug.Log("pressed");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(0);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            Button button = GameObject.FindGameObjectWithTag("ExitButton").GetComponent<Button>();
            button.onClick.AddListener(ExitButton);
        }

        if (scene.buildIndex == 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Manager"));
        }
    }
}
