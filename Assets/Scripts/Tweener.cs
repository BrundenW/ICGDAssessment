using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    /*[SerializeField]
    private GameObject pac;
    private Tween activeTween;
    private static Vector3[] vectors = {new Vector3(6f, -1f, -0.1f), new Vector3(6f, -5f, -0.1f), new Vector3(1f, -5f, -0.1f), new Vector3(1f, -1f, -0.1f)};
    private List<Vector3> points = new List<Vector3>(vectors);
    private static string[] aniNames = { "pac-student-right", "pac-student-down", "pac-student-left", "pac-student-up" };
    //[SerializeField]
    Animator anim;*/

    private List<Tween> activeTweens = new List<Tween>();

    // Start is called before the first frame update
    void Start()
    {
        //activeTween = new Tween(pac.transform, pac.transform.position, points[0], Time.time, Vector3.Distance(pac.transform.position, points[0])/2);
        //pac.GetComponent<Animation>().Play("pac-student-right");
        //anim = pac.GetComponent<Animator>();
        //anim.Play("pac-student-right");
    }

        // Update is called once per frame
    void Update()
    {
        if (activeTweens != null)
        {
            for (int i = 0; i < activeTweens.Count; i++)
            {
                if (Vector3.Distance(activeTweens[i].Target.position, activeTweens[i].EndPos) > 0.1f)
                {
                    //mathf.pow is x to the power of y for floats, converts this time fraction from linear to cubic easing in
                    float timeFraction = (Time.time - activeTweens[i].StartTime) / activeTweens[i].Duration;
                    activeTweens[i].Target.position = Vector3.Lerp(activeTweens[i].StartPos, activeTweens[i].EndPos, timeFraction);
                }
                else
                {
                    activeTweens[i].Target.position = activeTweens[i].EndPos;
                    activeTweens.RemoveAt(i);
                }
            }
        }
        /*if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) >  0.1f)
        {
            activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, (Time.time - activeTween.StartTime)/activeTween.Duration);
        }
        else
        {
            anim.SetTrigger("Next Animation");
            points.Add(points[0]);
            points.RemoveAt(0);
            activeTween = new Tween(pac.transform, pac.transform.position, points[0], Time.time, Vector3.Distance(pac.transform.position, points[0]) / 2);
        }*/
    }

    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float Duration)
    {
        if (!TweenExists(targetObject))
        {
            Tween activeTween = new Tween(targetObject, startPos, endPos, Time.time, Duration);
            activeTweens.Add(activeTween);
            return true;
        }
        return false;
    }

    public bool TweenExists(Transform target)
    {
        for (int i = 0; i < activeTweens.Count; i++)
        {
            if (activeTweens[i].Target == target)
                return true;
        }
        return false;
    }
}
