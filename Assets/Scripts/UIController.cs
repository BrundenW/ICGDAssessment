using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    //public UIController ui;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void death(int lives)
    {
        if (lives == 2)
        {
            life1.SetActive(false);
        }
        else if (lives == 1)
        {
            life2.SetActive(false);
        }
        else
        {
            life3.SetActive(false);
        }
    }
}
