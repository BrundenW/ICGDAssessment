using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostController : MonoBehaviour
{
    public GameObject red;
    public GameObject purple;
    public GameObject green;
    public GameObject blue;
    public Text ghostTimer;
    public int globalState;
    public int numDead;
    private SoundManager sound;
    //private int state

    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Manager").GetComponent<SoundManager>();
        sound.normalMusic();
        globalState = 0;
        numDead = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scared()
    {
        if (numDead == 0)
        {
            sound.scaredMusic();
        }
        globalState = 1;
        StartCoroutine(ScaredCountdown());
        red.GetComponent<GhostState>().becomeScared();
        purple.GetComponent<GhostState>().becomeScared();
        green.GetComponent<GhostState>().becomeScared();
        blue.GetComponent<GhostState>().becomeScared();
    }

    public void scaredOver()
    {
        if (numDead == 0)
        {
            sound.normalMusic();
        }
        red.GetComponent<GhostState>().returnToNormal();
        purple.GetComponent<GhostState>().returnToNormal();
        green.GetComponent<GhostState>().returnToNormal();
        blue.GetComponent<GhostState>().returnToNormal();
        globalState = 0;
    }

    private IEnumerator ScaredCountdown()
    {
        ghostTimer.text = "10";
        ghostTimer.gameObject.SetActive(true);
        for (int i = 9; i >= 0; i--)
        {
            yield return new WaitForSeconds(1);
            ghostTimer.text = i.ToString();
        }
        ghostTimer.gameObject.SetActive(false);
        scaredOver();
    }

    public void deadMusic()
    {
        sound.deadMusic();
    }

    public void noDeadMusic()
    {
        if (globalState == 1)
        {
            sound.scaredMusic();
        }
        else
        {
            sound.normalMusic();
        }
    }
}
