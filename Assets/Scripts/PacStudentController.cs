using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    int lastInput;
    int currentInput;
    private Tweener tweener;
    public Collision left;
    public Collision right;
    public Collision up;
    public Collision down;
    private GameObject pac;
    private Animator anim;
    public bool stillMove;
    private new AudioSource audio;
    public AudioClip normal;
    public AudioClip pellet;
    public AudioClip wall;
    public bool wallHit;
    private ParticleSystem particle;


    // Start is called before the first frame update
    void Start()
    {
        //solid = gameObject.GetComponent<Collision1>();
        tweener = GetComponent<Tweener>();
        pac = GameObject.FindGameObjectWithTag("PacStudent");
        anim = pac.GetComponent<Animator>();
        audio = pac.GetComponent<AudioSource>();
        particle = pac.GetComponent<ParticleSystem>();
        wallHit = false;

    }
    // Update is called once per frame
    void Update()
    {
        stillMove = false;
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = 0;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = 2;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = 3;
        }

        //if (!tweener.TweenExists(pac.transform))
        //{
        if (lastInput == 0)
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
                    currentInput = 0;
                }
            }
            else
            {
                continueMove();
            }
        }
        else if (lastInput == 1)
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
            if (down.test == false)
            {
                Vector3 temp = new Vector3(pac.transform.position.x, pac.transform.position.y - 1, pac.transform.position.z);
                if (tweener.AddTween(pac.transform, pac.transform.position, temp, 0.3f)) {
                    resetAnim();
                    particle.Play();
                    ResetAudio();
                    anim.SetBool("Down", true);
                    stillMove = true;
                    currentInput = 2;
                }
            }
            else
            {
                continueMove();
            }
        }
        else
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
                    currentInput = 3;
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
        if (currentInput == 0)
        {
            if (up.test == false)
            {
                Vector3 temp = new Vector3(pac.transform.position.x, pac.transform.position.y + 1, pac.transform.position.z);
                if (tweener.AddTween(pac.transform, pac.transform.position, temp, 0.3f))
                {
                    particle.Play();
                    stillMove = true;
                }
            }
        }
        else if (currentInput == 1)
        {
            if (left.test == false)
            {
                Vector3 temp = new Vector3(pac.transform.position.x - 1, pac.transform.position.y, pac.transform.position.z);
                if (tweener.AddTween(pac.transform, pac.transform.position, temp, 0.3f))
                {
                    particle.Play();
                    stillMove = true;
                }
            }
        }
        else if (currentInput == 2)
        {
            if (down.test == false)
            {
                Vector3 temp = new Vector3(pac.transform.position.x, pac.transform.position.y - 1, pac.transform.position.z);
                if (tweener.AddTween(pac.transform, pac.transform.position, temp, 0.3f))
                {
                    particle.Play();
                    stillMove = true;
                }
            }
        }
        else
        {
            if (right.test == false)
            {
                Vector3 temp2 = new Vector3(pac.transform.position.x + 1, pac.transform.position.y, pac.transform.position.z);
                if (tweener.AddTween(pac.transform, pac.transform.position, temp2, 0.3f))
                {
                    particle.Play();
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
    }
    private void ResetAudio()
    {
        audio.clip = pellet;
        audio.loop = true;
        audio.Play();
        wallHit = false;
    }
}
