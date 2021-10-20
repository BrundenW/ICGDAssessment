using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    enum direction { Left, Right, Up, Down}
    int lastInput;
    int currentInput;
    private Tweener tweener;
    public Collision left;
    public Collision right;
    public Collision up;
    public Collision down;


    // Start is called before the first frame update
    void Start()
    {
        //solid = gameObject.GetComponent<Collision1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = 0;
            Debug.Log(left.test);
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

        /*if (!tweener.TweenExists(gameObject.transform))
        {
            //bool test = gameObject.transform.GetChild(0).GetComponent<Collision>();
            //check lastInput to see if you can go in that direction {
                //if you can, currentInput = lastInput;
                //lerp in currentInput direction
            //else {
                //check currentInput to see if you can go in that direction {
                    //if you can, lerp in currentInput direction
        }*/
    }
}
