using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacCollision : MonoBehaviour
{
    public Text score;
    public int scoreCount;
    public bool pellet;
    public CherryController cherry;
    public int teleport;
    public int lives;
    public PacStudentController pacController;
    public UIController ui;
    public GhostController ghosts;
    public LevelGenerator lvlGen;

    // Start is called before the first frame update
    void Start()
    {
        scoreCount = 0;
        teleport = 0;
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (lvlGen.numPellets == 0)
        {
            pacController.finsihed();
            pacController.GameOver(false);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pellet")
        {
            Destroy(other.gameObject, 0.3f);
            scoreCount += 10;
            score.text = "Score: " + scoreCount.ToString();
            pellet = true;
            lvlGen.numPellets--;
        }
        if (other.tag == "Cherry")
        {
            cherry.CherryDelete();
            scoreCount += 100;
            score.text = "Score: " + scoreCount.ToString();
            pellet = true;
        }
        if (other.tag == "Teleporter")
        {
            if (other.transform.position.x == 0)
            {
                teleport = 1;
            }
            else
            {
                teleport = 2;
            }
        }
        if (other.tag == "Power")
        {
            ghosts.Scared();
            Destroy(other.gameObject, 0.3f);
            lvlGen.numPellets--;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            if (collision.gameObject.GetComponent<GhostState>().state == 0)
            {
                death();
            }

            if (collision.gameObject.GetComponent<GhostState>().state == 1)
            {
                collision.gameObject.GetComponent<GhostState>().ghostEaten();
                scoreCount += 300;
                score.text = "Score: " + scoreCount.ToString();
            }           
        }
    }

    private void death()
    {
        Time.timeScale = 0;
        if (lives == 1)
        {
            //gameover - save highscore
            ui.death(0);
            Time.timeScale = 0;
            pacController.GameOver(true);
        }
        else
        {
            lives--;
            pacController.death();
            ui.death(lives);
            
        }
    }
}
