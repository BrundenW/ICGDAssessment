using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PacStudentController : MonoBehaviour
{
    int lastInput;
    int currentInput;
    private Tweener tweener;
    public WallCollision left;
    public WallCollision right;
    public WallCollision up;
    public WallCollision down;
    private GameObject pac;
    private Animator anim;
    public bool stillMove;
    private new AudioSource audio;
    public AudioClip normal;
    public AudioClip pellet;
    public AudioClip wall;
    public bool wallHit;
    private ParticleSystem particle;
    public PacCollision pacCollision;
    public Timer timer;
    public LevelStart start;


    // Start is called before the first frame update
    void Start()
    {
        //solid = gameObject.GetComponent<Collision1>();
        tweener = GetComponent<Tweener>();
        pac = GameObject.FindGameObjectWithTag("PacStudent");
        anim = pac.GetComponent<Animator>();
        audio = pac.GetComponent<AudioSource>();
        particle = pac.GetComponent<ParticleSystem>();
        wallHit = true;

    }
    // Update is called once per frame
    void Update()
    {
        stillMove = false;
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = 1;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = 2;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = 3;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = 4;
        }

        //if (!tweener.TweenExists(pac.transform))
        //{
        if (!tweener.TweenExists(pac.transform) && pacCollision.teleport != 0)
        {
            if (pacCollision.teleport == 1)
            {
                pac.transform.position = new Vector3(26, -14, 0);
            }
            else
            {
                pac.transform.position = new Vector3(1, -14, 0);
            }
            pacCollision.teleport = 0;
        }
        if (lastInput == 1)
        {
            if (up.test == false)
            {       
                Vector3 temp = new Vector3(pac.transform.position.x, pac.transform.position.y + 1, pac.transform.position.z);
                if (tweener.AddTween(pac.transform, pac.transform.position, temp, 0.3f)) { 
                    stillMove = true;
                    particle.Play();
                    resetAnim();
                    ResetAudio();
                    anim.SetBool("Up", true);
                    currentInput = 1;
                }
            }
            else
            {
                continueMove();
            }
        }
        else if (lastInput == 2)
        {
            if (left.test == false)
            {
                Vector3 temp = new Vector3(pac.transform.position.x - 1, pac.transform.position.y, pac.transform.position.z);
                if (tweener.AddTween(pac.transform, pac.transform.position, temp, 0.3f)) {
                    stillMove = true;
                    particle.Play();
                    resetAnim();
                    ResetAudio();
                    anim.SetBool("Left", true);
                    currentInput = 2;
                }
            }
            else
            {
                continueMove();
            }
        }
        else if (lastInput == 3)
        {
            if (down.test == false)
            {
                Vector3 temp = new Vector3(pac.transform.position.x, pac.transform.position.y - 1, pac.transform.position.z);
                if (tweener.AddTween(pac.transform, pac.transform.position, temp, 0.3f)) {
                    resetAnim();
                    particle.Play();
                    ResetAudio();
                    anim.SetBool("Down", true);
                    stillMove = true;
                    currentInput = 3;
                }
            }
            else
            {
                continueMove();
            }
        }
        else if (lastInput == 4)
        {
            if (right.test == false) 
            {
                Vector3 temp = new Vector3(pac.transform.position.x + 1, pac.transform.position.y, pac.transform.position.z);
                if (tweener.AddTween(pac.transform, pac.transform.position, temp, 0.3f)) {
                    resetAnim();
                    particle.Play();
                    ResetAudio();
                    anim.SetBool("Right", true);
                    stillMove = true;
                    currentInput = 4;
                }
            }
            else
            {
                continueMove();
            }
        }

        if (stillMove == false && !tweener.TweenExists(pac.transform))
        {
            if (wallHit == false)
            {
                audio.Stop();
                audio.clip = wall;
                audio.loop = false;
                audio.Play();
                wallHit = true;
            }
            resetAnim();
        }

        stillMove = false;
            
            //bool test = gameObject.transform.GetChild(0).GetComponent<Collision>();
            //check lastInput to see if you can go in that direction {
                //if you can, currentInput = lastInput;
                //lerp in currentInput direction
            //else {
                //check currentInput to see if you can go in that direction {
                    //if you can, lerp in currentInput direction
    }

    private void continueMove()
    {
        //tweener.AddTween(pac.transform, pac.transform.position, new Vector3(0, 0, 0), 1f);
        if (currentInput == 1)
        {
            if (up.test == false)
            {
                Vector3 temp = new Vector3(pac.transform.position.x, pac.transform.position.y + 1, pac.transform.position.z);
                if (tweener.AddTween(pac.transform, pac.transform.position, temp, 0.3f))
                {
                    particle.Play();
                    ResetAudio();
                    stillMove = true;
                }
            }
        }
        else if (currentInput == 2)
        {
            if (left.test == false)
            {
                Vector3 temp = new Vector3(pac.transform.position.x - 1, pac.transform.position.y, pac.transform.position.z);
                if (tweener.AddTween(pac.transform, pac.transform.position, temp, 0.3f))
                {
                    particle.Play();
                    ResetAudio();
                    stillMove = true;
                }
            }
        }
        else if (currentInput == 3)
        {
            if (down.test == false)
            {
                Vector3 temp = new Vector3(pac.transform.position.x, pac.transform.position.y - 1, pac.transform.position.z);
                if (tweener.AddTween(pac.transform, pac.transform.position, temp, 0.3f))
                {
                    particle.Play();
                    ResetAudio();
                    stillMove = true;
                }
            }
        }
        else if (currentInput == 4)
        {
            if (right.test == false)
            {
                Vector3 temp2 = new Vector3(pac.transform.position.x + 1, pac.transform.position.y, pac.transform.position.z);
                if (tweener.AddTween(pac.transform, pac.transform.position, temp2, 0.3f))
                {
                    particle.Play();
                    ResetAudio();
                    stillMove = true;
                }
            }
        }
    }

    private void resetAnim()
    {
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);
        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
        anim.SetBool("Dead", false);
    }
    private void ResetAudio()
    {
        //audio.clip = normal;
        //audio.loop = true;
        playAudio();
        //audio.Play();
        wallHit = false;
    }


    private void playAudio()
    {
        if (Time.timeScale != 0)
        {
            if (pacCollision.pellet)
            {
                audio.clip = pellet;
                audio.Play();
                pacCollision.pellet = false;
            }
            else
            {
                //audio.loop = true;
                audio.clip = normal;
                audio.Play();
            }
        }
    }

    public void death()
    {
        //Debug.Log("dead1");
        currentInput = 0;
        lastInput = 0;
        tweener.TweenRemove(pac.transform);
        resetAnim();
        StartCoroutine(deathAnim());

    }
    public void finsihed()
    {
        StartCoroutine(finalMove());
    }
    private IEnumerator finalMove()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        currentInput = 0;
        lastInput = 0;
        tweener.TweenRemove(pac.transform);
        resetAnim();
        //resetAnim();
    }

    private IEnumerator deathAnim()
    {
        //Debug.Log("dead");
        anim.SetBool("Dead", true);
        yield return new WaitForSecondsRealtime(2);
        anim.SetBool("Dead", false);
        pac.transform.position = new Vector3(1, -1, 0);
        currentInput = 0;
        lastInput = 0;
        Time.timeScale = 1;
    }

    private IEnumerator gameOverAnim()
    {
        anim.SetBool("Dead", true);
        start.gameOver();
        yield return new WaitForSecondsRealtime(2);
        anim.SetBool("Dead", false);
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private IEnumerator finishedGame()
    {
        start.gameOver();
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void GameOver(bool dead)
    {
        //save 
        float total = timer.total;
        int mins = timer.minutes;
        int secs = timer.seconds;
        float decisecs = timer.deciseconds;
        int score = pacCollision.scoreCount;
        if (PlayerPrefs.HasKey("Score"))
        {
            if (PlayerPrefs.GetInt("Score") < score)
            {
                PlayerPrefs.SetInt("Mins", mins);
                PlayerPrefs.SetInt("Secs", secs);
                PlayerPrefs.SetFloat("Decisecs", decisecs);
                PlayerPrefs.SetFloat("Time", total);
                PlayerPrefs.SetInt("Score", score);
            }
            else if (PlayerPrefs.GetInt("Score") == score && PlayerPrefs.GetFloat("Time") > total)
            {
                PlayerPrefs.SetInt("Mins", mins);
                PlayerPrefs.SetInt("Secs", secs);
                PlayerPrefs.SetFloat("Decisecs", decisecs);
                PlayerPrefs.SetFloat("Time", total);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Mins", mins);
            PlayerPrefs.SetInt("Secs", secs);
            PlayerPrefs.SetFloat("Decisecs", decisecs);
            PlayerPrefs.SetFloat("Time", total);
            PlayerPrefs.SetInt("Score", score);
        }
        //resetAnim();
        if (dead)
        {
            resetAnim();
            StartCoroutine(gameOverAnim());
        }
        else
        {
            StartCoroutine(finishedGame());
        }
    }
}
