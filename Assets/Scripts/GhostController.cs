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
    public GameObject pac;
    public Text ghostTimer;
    public int globalState;
    public int numDead;
    private SoundManager sound;
    private Tweener tweener;
    private int lastMoveRed; //0 = no move, 1 = up, 2 = left, 3 = down, 4 = right
    private int lastMoveGreen;
    private int lastMovePurple;
    private int lastMoveBlue;
    private List<Vector3> directions = new List<Vector3>();
    private List<int> previousDirection = new List<int>();
    private List<Vector3> directions1 = new List<Vector3>();
    private List<int> previousDirection1 = new List<int>();
    //private int state

    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Manager").GetComponent<SoundManager>();
        sound.normalMusic();
        globalState = 0;
        numDead = 0;
        tweener = GetComponent<Tweener>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!tweener.TweenExists(purple.transform))
        {
            if (purple.GetComponent<GhostState>().atSpawn)
            {
                moveOutSpawn(purple);
                lastMovePurple = 1;
            }
            else if (purple.GetComponent<GhostState>().state != 2)
            {
                PurpleMovement();
            }
        }

        if (!tweener.TweenExists(green.transform))
        {
            if (green.GetComponent<GhostState>().atSpawn)
            {
                moveOutSpawn(green);
                lastMoveGreen = 1;
            }
            else if (green.GetComponent<GhostState>().state != 2)
            {
                lastMoveGreen = RandomMovement(green, lastMoveGreen);
            }
        }

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
    private void moveOutSpawn(GameObject ghost)
    {
        Vector3 temp = new Vector3(ghost.transform.position.x, ghost.transform.position.y + 1, ghost.transform.position.z);
        tweener.AddTween(ghost.transform, ghost.transform.position, temp, 0.3f);
    }

    private int RandomMovement(GameObject ghost, int lastMove)
    {
        directions.Clear();
        previousDirection.Clear();
        if (!ghost.GetComponent<GhostState>().up.test && lastMove != 3)
        {
            directions.Add(new Vector3(ghost.transform.position.x, ghost.transform.position.y + 1, ghost.transform.position.z));
            previousDirection.Add(1);
        }
        if (!ghost.GetComponent<GhostState>().left.test && lastMove != 4)
        {
            directions.Add(new Vector3(ghost.transform.position.x - 1, ghost.transform.position.y, ghost.transform.position.z));
            previousDirection.Add(2);
        }
        if (!ghost.GetComponent<GhostState>().down.test && lastMove != 1)
        {
            directions.Add(new Vector3(ghost.transform.position.x, ghost.transform.position.y - 1, ghost.transform.position.z));
            previousDirection.Add(3);
        }
        if (!ghost.GetComponent<GhostState>().right.test && lastMove != 2)
        {
            directions.Add(new Vector3(ghost.transform.position.x + 1, ghost.transform.position.y, ghost.transform.position.z));
            previousDirection.Add(4);
        }

        /*foreach (Vector3 temp in directions)
        {
            Debug.Log(temp);
        }*/
        int movement = Random.Range(0, directions.Count);
        tweener.AddTween(ghost.transform, ghost.transform.position, directions[movement], 0.3f);
        return (previousDirection[movement]);

    }

    private void PurpleMovement()
    {
        directions1.Clear();
        previousDirection1.Clear();
        float dist = Vector3.Distance(purple.transform.position, pac.transform.position);
        Debug.Log("Pac Distance = " + dist);
        Debug.Log("up.test = " + purple.GetComponent<GhostState>().up.test + ", lastMovePurple = " + lastMovePurple + ", distance = " + Vector3.Distance(new Vector3(purple.transform.position.x, purple.transform.position.y + 1, 0), pac.transform.position)); 
        if (!purple.GetComponent<GhostState>().up.test && lastMovePurple != 3 && Vector3.Distance(new Vector3(purple.transform.position.x, purple.transform.position.y + 1, 0), pac.transform.position) <= dist)
        {
            Debug.Log("here1");
            directions1.Add(new Vector3(purple.transform.position.x, purple.transform.position.y + 1, purple.transform.position.z));
            previousDirection1.Add(1);
            Debug.Log("Vector = " + new Vector3(purple.transform.position.x, purple.transform.position.y + 1, purple.transform.position.z) + ", Distance = " + Vector3.Distance(purple.transform.GetChild(3).transform.position, pac.transform.position));
        }
        Debug.Log("here5");
        if (!purple.GetComponent<GhostState>().left.test && lastMovePurple != 4 && Vector3.Distance(new Vector3(purple.transform.position.x - 1, purple.transform.position.y, 0), pac.transform.position) <= dist)
        {
            Debug.Log("here2");
            directions1.Add(new Vector3(purple.transform.position.x - 1, purple.transform.position.y, purple.transform.position.z));
            previousDirection1.Add(2);
            Debug.Log("Left detector = " + purple.transform.GetChild(0).transform.position + ", right detector = " + purple.transform.GetChild(1).transform.position);
            Debug.Log("Vector = " + new Vector3(purple.transform.position.x - 1, purple.transform.position.y, purple.transform.position.z) + ", Distance = " + Vector3.Distance(purple.transform.GetChild(3).transform.position, pac.transform.position));
        }
        Debug.Log("here6");
        if (!purple.GetComponent<GhostState>().down.test && lastMovePurple != 1 && Vector3.Distance(new Vector3(purple.transform.position.x, purple.transform.position.y - 1, 0), pac.transform.position) <= dist)
        {
            Debug.Log("here3");
            directions1.Add(new Vector3(purple.transform.position.x, purple.transform.position.y - 1, purple.transform.position.z));
            previousDirection1.Add(3);
            Debug.Log("Vector = " + new Vector3(purple.transform.position.x, purple.transform.position.y - 1, purple.transform.position.z) + ", Distance = " + Vector3.Distance(purple.transform.GetChild(3).transform.position, pac.transform.position));
        }
        Debug.Log("here7");
        if (!purple.GetComponent<GhostState>().right.test && lastMovePurple != 2 && Vector3.Distance(new Vector3(purple.transform.position.x + 1, purple.transform.position.y, 0), pac.transform.position) <= dist)
        {
            Debug.Log("here4");
            Debug.Log("Left detector = " + purple.transform.GetChild(0).transform.position + ", right detector = " + purple.transform.GetChild(1).transform.position);
            Debug.Log("Vector = " + new Vector3(purple.transform.position.x + 1, purple.transform.position.y, purple.transform.position.z) + ", Distance = " + Vector3.Distance(purple.transform.GetChild(3).transform.position, pac.transform.position));
            directions1.Add(new Vector3(purple.transform.position.x + 1, purple.transform.position.y, purple.transform.position.z));
            previousDirection1.Add(4);
        }

        Debug.Log("here8 " + directions1.Count);
        /*foreach (Vector3 temp1 in directions1)
        {
            Debug.Log(temp1);
        }*/
        if (directions1.Count == 0)
        {
            lastMovePurple = RandomMovement(purple, lastMovePurple);
        }
        else
        {
            int movement1 = Random.Range(0, directions1.Count);
            tweener.AddTween(purple.transform, purple.transform.position, directions1[movement1], 0.3f);
            lastMovePurple = previousDirection1[movement1];
        }
    }

    public void deadGhost(GameObject ghost)
    {
        tweener.TweenRemove(ghost.transform);
        tweener.AddTween(ghost.transform, ghost.transform.position, new Vector3(13, -13, 0), 5);
    }
}
