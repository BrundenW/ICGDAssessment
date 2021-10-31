using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class GhostState : MonoBehaviour
{

    //private Text timer;
    public int state; //0 = alive, 1 = scared, 2 = dead;
    public GhostController controller;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        //globalState = controller.publicState;
        //timer = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void becomeScared()
    {
        state = 1;
        //StartCoroutine(ScaredCountdown());
    }

    public void returnToNormal()
    {
        if (state != 2)
        {
            state = 0;
        }
    }

    public void ghostEaten()
    {
        if(controller.numDead == 0)
        {
            controller.deadMusic();
        }
        state = 2;
        controller.numDead++;
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(deadCountdown());
    }

    private void returnToGlobalState()
    {
        if (controller.globalState == 1)
        {
            becomeScared();
        }
        
        else
        {
            returnToNormal();
        }
    }

    private void checkNumDead()
    {
        controller.numDead--;
        if (controller.numDead == 0)
        {
            controller.noDeadMusic();
        }
    }

    private IEnumerator deadCountdown()
    {
        yield return new WaitForSeconds(5);
        GetComponent<SpriteRenderer>().enabled = true;
        state = -1;
        returnToGlobalState();
        checkNumDead();
    }
}
