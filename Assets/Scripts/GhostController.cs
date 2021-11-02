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
    private bool scared;
    private bool coroutineRunning;

    //the last direction each ghost moved in, to prevent backtracking
    //0 = no move, 1 = up, 2 = left, 3 = down, 4 = right
    private int lastMoveRed; 
    private int lastMoveGreen;
    private int lastMovePurple;
    private int lastMoveBlue;

    //lists of directions to enable different ghost movement types
    private List<Vector3> randomDirections = new List<Vector3>();
    private List<int> randomPreviousDirection = new List<int>();
    private List<Vector3> aggresiveDirections = new List<Vector3>();
    private List<int> aggresivePreviousDirection = new List<int>();
    private List<Vector3> scaredDirections = new List<Vector3>();
    private List<int> scaredPreviousDirection = new List<int>();
    private List<Vector3> clockwiseDirections = new List<Vector3>();
    private List<int> clockwisePreviousDirection = new List<int>();

    //to enable clockwise circulation of the map
    private Vector3 nextClockwise;
    private List<Vector3> clockwisePositions = new List<Vector3>();
    private int priority;

    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Manager").GetComponent<SoundManager>();
        sound.normalMusic();
        globalState = 0;
        numDead = 0;
        tweener = GetComponent<Tweener>();
        nextClockwise = new Vector3(1, -1, 0);
        clockwisePositions.AddRange(new List<Vector3> { new Vector3(26, -1, 0), new Vector3(26, -27, 0), new Vector3(1, -27, 0)});
        priority = 1;
        scared = false;
        coroutineRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!tweener.TweenExists(red.transform))
        {
            if (red.GetComponent<GhostState>().atSpawn)
            {
                moveOutSpawn(red);
                lastMoveRed = 1;
            }
            else if (red.GetComponent<GhostState>().state != 2)
            {
                lastMoveRed = scaredMovement(red, lastMoveRed);
            }
        }

        if (!tweener.TweenExists(purple.transform))
        {
            if (purple.GetComponent<GhostState>().atSpawn)
            {
                moveOutSpawn(purple);
                lastMovePurple = 1;
            }
            else if (purple.GetComponent<GhostState>().state == 0)
            {
                lastMovePurple = aggresiveMovement(purple, lastMovePurple);
            }
            else if (purple.GetComponent<GhostState>().state == 1 || purple.GetComponent<GhostState>().state == 3)
            {
                lastMovePurple = scaredMovement(purple, lastMovePurple);
            }
        }

        if (!tweener.TweenExists(green.transform))
        {
            if (green.GetComponent<GhostState>().atSpawn)
            {
                moveOutSpawn(green);
                lastMoveGreen = 1;
            }
            else if (green.GetComponent<GhostState>().state == 0)
            {
                lastMoveGreen = RandomMovement(green, lastMoveGreen);
            }
            else if (green.GetComponent<GhostState>().state == 1 || green.GetComponent<GhostState>().state == 3)
            {
                lastMoveGreen = scaredMovement(green, lastMoveGreen);
            }
        }

        if (!tweener.TweenExists(blue.transform))
        {
            if (blue.GetComponent<GhostState>().atSpawn)
            {
                moveOutSpawn(blue);
                lastMoveBlue = 1;
            }
            else if (blue.GetComponent<GhostState>().state == 0)
            {
                lastMoveBlue = clockwiseMovement(blue, lastMoveBlue);
            }
            else if (blue.GetComponent<GhostState>().state == 1 || blue.GetComponent<GhostState>().state == 3)
            {
                lastMoveBlue = scaredMovement(blue, lastMoveBlue);
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
        scared = true;
        if (!coroutineRunning)
        {
            StartCoroutine(ScaredCountdown());
        }
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

    private void recovering()
    {
        red.GetComponent<GhostState>().recovering();
        purple.GetComponent<GhostState>().recovering();
        green.GetComponent<GhostState>().recovering();
        blue.GetComponent<GhostState>().recovering();
        globalState = 3;
    }

    private IEnumerator ScaredCountdown()
    {
        coroutineRunning = true;
        ghostTimer.text = "10";
        ghostTimer.gameObject.SetActive(true);
        for (int i = 9; i >= 0; i--)
        {
            yield return new WaitForSeconds(1);
            ghostTimer.text = i.ToString();
            if (i == 5)
            {
                recovering();
            }
            if (scared)
            {
                i = 10;
                scared = false;
            }
        }
        ghostTimer.gameObject.SetActive(false);
        scaredOver();
        coroutineRunning = false;
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
        ghostAnimDirection(ghost, 1);
        Vector3 temp = new Vector3(ghost.transform.position.x, ghost.transform.position.y + 1, ghost.transform.position.z);
        tweener.AddTween(ghost.transform, ghost.transform.position, temp, 0.3f);
    }

    private int RandomMovement(GameObject ghost, int lastMove)
    {
        randomDirections.Clear();
        randomPreviousDirection.Clear();
        if (!ghost.GetComponent<GhostState>().up.test && lastMove != 3)
        {
            randomDirections.Add(new Vector3(ghost.transform.position.x, ghost.transform.position.y + 1, ghost.transform.position.z));
            randomPreviousDirection.Add(1);
        }
        if (!ghost.GetComponent<GhostState>().left.test && lastMove != 4)
        {
            randomDirections.Add(new Vector3(ghost.transform.position.x - 1, ghost.transform.position.y, ghost.transform.position.z));
            randomPreviousDirection.Add(2);
        }
        if (!ghost.GetComponent<GhostState>().down.test && lastMove != 1)
        {
            randomDirections.Add(new Vector3(ghost.transform.position.x, ghost.transform.position.y - 1, ghost.transform.position.z));
            randomPreviousDirection.Add(3);
        }
        if (!ghost.GetComponent<GhostState>().right.test && lastMove != 2)
        {
            randomDirections.Add(new Vector3(ghost.transform.position.x + 1, ghost.transform.position.y, ghost.transform.position.z));
            randomPreviousDirection.Add(4);
        }

        int movement = Random.Range(0, randomDirections.Count);
        tweener.AddTween(ghost.transform, ghost.transform.position, randomDirections[movement], 0.3f);
        ghostAnimDirection(ghost, randomPreviousDirection[movement]);
        return (randomPreviousDirection[movement]);

    }

    private int aggresiveMovement(GameObject ghost, int lastMove)
    {
        aggresiveDirections.Clear();
        aggresivePreviousDirection.Clear();
        float dist = Vector3.Distance(ghost.transform.position, pac.transform.position);
        if (!ghost.GetComponent<GhostState>().up.test && lastMove != 3 && Vector3.Distance(new Vector3(ghost.transform.position.x, ghost.transform.position.y + 1, 0), pac.transform.position) <= dist)
        {
            aggresiveDirections.Add(new Vector3(ghost.transform.position.x, ghost.transform.position.y + 1, 0));
            aggresivePreviousDirection.Add(1);
        }
        if (!ghost.GetComponent<GhostState>().left.test && lastMove != 4 && Vector3.Distance(new Vector3(ghost.transform.position.x - 1, ghost.transform.position.y, 0), pac.transform.position) <= dist)
        {
            aggresiveDirections.Add(new Vector3(ghost.transform.position.x - 1, ghost.transform.position.y, 0));
            aggresivePreviousDirection.Add(2);
        }
        if (!ghost.GetComponent<GhostState>().down.test && lastMove != 1 && Vector3.Distance(new Vector3(ghost.transform.position.x, ghost.transform.position.y - 1, 0), pac.transform.position) <= dist)
        {
            aggresiveDirections.Add(new Vector3(ghost.transform.position.x, ghost.transform.position.y - 1, 0));
            aggresivePreviousDirection.Add(3);
        }
        if (!ghost.GetComponent<GhostState>().right.test && lastMove != 2 && Vector3.Distance(new Vector3(ghost.transform.position.x + 1, ghost.transform.position.y, 0), pac.transform.position) <= dist)
        {
            aggresiveDirections.Add(new Vector3(ghost.transform.position.x + 1, ghost.transform.position.y, 0));
            aggresivePreviousDirection.Add(4);
        }

        if (aggresiveDirections.Count == 0)
        {
            return (RandomMovement(ghost, lastMove));
        }
        else
        {
            int movement = Random.Range(0, aggresiveDirections.Count);
            tweener.AddTween(ghost.transform, ghost.transform.position, aggresiveDirections[movement], 0.3f);
            ghostAnimDirection(ghost, aggresivePreviousDirection[movement]);
            return (aggresivePreviousDirection[movement]);
        }
    }

    private int scaredMovement(GameObject ghost, int lastMove)
    {
        scaredDirections.Clear();
        scaredPreviousDirection.Clear();
        float dist = Vector3.Distance(ghost.transform.position, pac.transform.position);
        if (!ghost.GetComponent<GhostState>().up.test && lastMove != 3 && Vector3.Distance(new Vector3(ghost.transform.position.x, ghost.transform.position.y + 1, 0), pac.transform.position) >= dist)
        {
            scaredDirections.Add(new Vector3(ghost.transform.position.x, ghost.transform.position.y + 1, 0));
            scaredPreviousDirection.Add(1);
        }
        if (!ghost.GetComponent<GhostState>().left.test && lastMove != 4 && Vector3.Distance(new Vector3(ghost.transform.position.x - 1, ghost.transform.position.y, 0), pac.transform.position) >= dist)
        {
            scaredDirections.Add(new Vector3(ghost.transform.position.x - 1, ghost.transform.position.y, 0));
            scaredPreviousDirection.Add(2);
        }
        if (!ghost.GetComponent<GhostState>().down.test && lastMove != 1 && Vector3.Distance(new Vector3(ghost.transform.position.x, ghost.transform.position.y - 1, 0), pac.transform.position) >= dist)
        {
            scaredDirections.Add(new Vector3(ghost.transform.position.x, ghost.transform.position.y - 1, 0));
            scaredPreviousDirection.Add(3);
        }
        if (!ghost.GetComponent<GhostState>().right.test && lastMove != 2 && Vector3.Distance(new Vector3(ghost.transform.position.x + 1, ghost.transform.position.y, 0), pac.transform.position) >= dist)
        {
            scaredDirections.Add(new Vector3(ghost.transform.position.x + 1, ghost.transform.position.y, 0));
            scaredPreviousDirection.Add(4);
        }

        if (scaredDirections.Count == 0)
        {
            return(RandomMovement(ghost, lastMove));
        }
        else
        {
            int movement = Random.Range(0, scaredDirections.Count);
            tweener.AddTween(ghost.transform, ghost.transform.position, scaredDirections[movement], 0.3f);
            ghostAnimDirection(ghost, scaredPreviousDirection[movement]);
            return (scaredPreviousDirection[movement]);
        }
    }

    private int clockwiseMovement(GameObject ghost, int lastMove)
    {
        clockwiseDirections.Clear();
        clockwisePreviousDirection.Clear();
        float dist = Vector3.Distance(ghost.transform.position, nextClockwise);
        if (dist <= 1)
        {
            clockwisePositions.Add(nextClockwise);
            nextClockwise = clockwisePositions[0];
            clockwisePositions.RemoveAt(0);
            priority++;
            if (priority == 5)
            {
                priority = 1;
            }
        }
        if (!ghost.GetComponent<GhostState>().up.test && lastMove != 3 && Vector3.Distance(new Vector3(ghost.transform.position.x, ghost.transform.position.y + 1, 0), nextClockwise) <= dist)
        {
            clockwiseDirections.Add(new Vector3(ghost.transform.position.x, ghost.transform.position.y + 1, 0));
            clockwisePreviousDirection.Add(1);
        }
        if (!ghost.GetComponent<GhostState>().left.test && lastMove != 4 && Vector3.Distance(new Vector3(ghost.transform.position.x - 1, ghost.transform.position.y, 0), nextClockwise) <= dist)
        {
            if (priority == 1)
            {
                clockwiseDirections.Insert(0, new Vector3(ghost.transform.position.x - 1, ghost.transform.position.y, 0));
                clockwisePreviousDirection.Insert(0,2);
            }
            else
            {
                clockwiseDirections.Add(new Vector3(ghost.transform.position.x - 1, ghost.transform.position.y, 0));
                clockwisePreviousDirection.Add(2);
            }
        }
        if (!ghost.GetComponent<GhostState>().down.test && lastMove != 1 && Vector3.Distance(new Vector3(ghost.transform.position.x, ghost.transform.position.y - 1, 0), nextClockwise) <= dist)
        {
            if (priority == 4)
            {
                clockwiseDirections.Insert(0, new Vector3(ghost.transform.position.x, ghost.transform.position.y - 1, 0));
                clockwisePreviousDirection.Insert(0, 3);
            }
            else
            {
                clockwiseDirections.Add(new Vector3(ghost.transform.position.x, ghost.transform.position.y - 1, 0));
                clockwisePreviousDirection.Add(3);
            }
        }
        if (!ghost.GetComponent<GhostState>().right.test && lastMove != 2 && Vector3.Distance(new Vector3(ghost.transform.position.x + 1, ghost.transform.position.y, 0), nextClockwise) <= dist)
        {
            if (priority == 3)
            {
                clockwiseDirections.Insert(0, new Vector3(ghost.transform.position.x + 1, ghost.transform.position.y, 0));
                clockwisePreviousDirection.Insert(0, 4);
            } 
            else
            {
                clockwiseDirections.Add(new Vector3(ghost.transform.position.x + 1, ghost.transform.position.y, 0));
                clockwisePreviousDirection.Add(4);
            }
        }

        if (clockwiseDirections.Count == 0)
        {
            return (RandomMovement(ghost, lastMove));
        }
        else
        {
            tweener.AddTween(ghost.transform, ghost.transform.position, clockwiseDirections[0], 0.3f);
            ghostAnimDirection(ghost, clockwisePreviousDirection[0]);
            return (clockwisePreviousDirection[0]);
        }
    }

    public void deadGhost(GameObject ghost)
    {
        tweener.TweenRemove(ghost.transform);
        tweener.AddTween(ghost.transform, ghost.transform.position, new Vector3(13, -13, 0), 5);
    }

    private void ghostAnimDirection(GameObject ghost, int direction)
    {
        resetAnimDirection(ghost);
        if (direction == 1)
            ghost.GetComponent<Animator>().SetBool("Up", true);
        else if (direction == 2)
            ghost.GetComponent<Animator>().SetBool("Left", true);
        else if (direction == 3)
            ghost.GetComponent<Animator>().SetBool("Down", true);
        else if (direction == 4)
            ghost.GetComponent<Animator>().SetBool("Right", true);
    }

    private void resetAnimDirection(GameObject ghost)
    {
        ghost.GetComponent<Animator>().SetBool("Up", false);
        ghost.GetComponent<Animator>().SetBool("Left", false);
        ghost.GetComponent<Animator>().SetBool("Down", false);
        ghost.GetComponent<Animator>().SetBool("Right", false);
    }

    public void resetGhosts()
    {
        StartCoroutine(reset());
    }

    private IEnumerator reset()
    {
        tweener.TweenRemove(red.transform);
        tweener.TweenRemove(blue.transform);
        tweener.TweenRemove(green.transform);
        tweener.TweenRemove(purple.transform);

        yield return new WaitForSecondsRealtime(2);

        tweener.TweenRemove(red.transform);
        tweener.TweenRemove(blue.transform);
        tweener.TweenRemove(green.transform);
        tweener.TweenRemove(purple.transform);

        red.transform.position = new Vector3(13, -13, 0);
        purple.transform.position = new Vector3(13, -14, 0);
        green.transform.position = new Vector3(14, -13, 0);
        blue.transform.position = new Vector3(14, -14, 0);

        red.GetComponent<GhostState>().atSpawn = true;
        purple.GetComponent<GhostState>().atSpawn = true;
        green.GetComponent<GhostState>().atSpawn = true;
        blue.GetComponent<GhostState>().atSpawn = true;

        lastMoveRed = 0;
        lastMovePurple = 0;
        lastMoveGreen = 0;
        lastMoveBlue = 0;
    }
}
