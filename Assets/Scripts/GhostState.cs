using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class GhostState : MonoBehaviour
{

    //private Text timer;
    public int state; //0 = alive, 1 = scared, 2 = dead, 3 = recovering;
    public GhostController controller;
    public WallCollision left;
    public WallCollision right;
    public WallCollision up;
    public WallCollision down;
    public bool atSpawn;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        atSpawn = true;
        left = transform.GetChild(0).GetComponent<WallCollision>();
        right = transform.GetChild(1).GetComponent<WallCollision>();
        up = transform.GetChild(2).GetComponent<WallCollision>();
        down = transform.GetChild(3).GetComponent<WallCollision>();

        //globalState = controller.publicState;
        //timer = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void becomeScared()
    {
        if (state != 2)
        {
            state = 1;
            resetAnimState();
            GetComponent<Animator>().SetBool("Scared", true);
        }
        //StartCoroutine(ScaredCountdown());
    }

    public void returnToNormal()
    {
        if (state != 2)
        {
            resetAnimState();
            state = 0;
        }
    }

    public void ghostEaten()
    {
        if(controller.numDead == 0)
        {
            controller.deadMusic();
        }
        resetAnimState();
        GetComponent<Animator>().SetBool("Dead", true);
        state = 2;
        controller.numDead++;
        //GetComponent<SpriteRenderer>().enabled = false;
        controller.deadGhost(gameObject);
        StartCoroutine(deadCountdown());
    }

    private void returnToGlobalState()
    {
        if (controller.globalState == 1)
        {
            becomeScared();
        }

        else if (controller.globalState == 3)
        {
            recovering();
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
        //GetComponent<SpriteRenderer>().enabled = true;
        state = -1;
        atSpawn = true;
        returnToGlobalState();
        checkNumDead();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ghost")
        {
            atSpawn = false;
        }
    }

    private void resetAnimState()
    {
        GetComponent<Animator>().SetBool("Scared", false);
        GetComponent<Animator>().SetBool("Dead", false);
        GetComponent<Animator>().SetBool("Recovering", false);
    }

    public void recovering()
    {
        if (state != 2)
        {
            resetAnimState();
            state = 3;
            GetComponent<Animator>().SetBool("Recovering", true);
        }
    }
}
