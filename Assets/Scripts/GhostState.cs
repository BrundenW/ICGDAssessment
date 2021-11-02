using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class GhostState : MonoBehaviour
{

    //private Text timer;
    public int state; //0 = alive, 1 = scared, 2 = dead;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(gameObject.name + ": left = " + left.test + " right = " + right.test + " up =" + up.test + " down = " + down.test);
        }
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
        controller.deadGhost(gameObject);
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
}
