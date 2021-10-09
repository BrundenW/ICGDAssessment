using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void LevelOneButton()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelTwoButton()
    {

    }

    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }
}
