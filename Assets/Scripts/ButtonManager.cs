using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    public void LevelOneButton()
    {
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

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
}
