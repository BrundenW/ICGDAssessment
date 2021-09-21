using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    [SerializeField]
    private GameObject pac;
    private Tween activeTween;
    private static Vector3[] vectors = {new Vector3(6f, -1f, -0.1f), new Vector3(6f, -5f, -0.1f), new Vector3(1f, -5f, -0.1f), new Vector3(1f, -1f, -0.1f)};
    private List<Vector3> points = new List<Vector3>(vectors);
    private static string[] aniNames = { "pac-student-right", "pac-student-down", "pac-student-left", "pac-student-up" };
    //[SerializeField]
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        activeTween = new Tween(pac.transform, pac.transform.position, points[0], Time.time, Vector3.Distance(pac.transform.position, points[0])/2);
        //pac.GetComponent<Animation>().Play("pac-student-right");
        anim = pac.GetComponent<Animator>();
        //anim.Play("pac-student-right");
    }

        // Update is called once per frame
        void Update()
    {
        if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) >  0.1f)
        {
            activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, (Time.time - activeTween.StartTime)/activeTween.Duration);
        }
        else
        {
            anim.SetTrigger("Next Animation");
            points.Add(points[0]);
            points.RemoveAt(0);
            activeTween = new Tween(pac.transform, pac.transform.position, points[0], Time.time, Vector3.Distance(pac.transform.position, points[0]) / 2);
        }
    }
}
