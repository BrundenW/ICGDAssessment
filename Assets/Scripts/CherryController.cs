using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    private Tweener tweener;
    private bool startTween;
    private List<GameObject> itemList = new List<GameObject>();
    [SerializeField]
    private GameObject item;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        transform.position = new Vector3(13.5f - cam.orthographicSize*cam.aspect - 1 , 0, 0);
        startTween = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (itemList.Count == 0)
        {
            startTween = false;
            float randomPos;
            int randomStart = Random.Range(0, 4);
            
            Vector3 start;
            Vector3 end;
            if (randomStart == 0) //start top
            {
                randomPos = Random.Range(13.5f - cam.orthographicSize * cam.aspect, 13.5f + cam.orthographicSize * cam.aspect);
                start = new Vector3(randomPos, -15 + cam.orthographicSize + 1, 0);
                end = new Vector3(13.5f*2 - randomPos, -15 - cam.orthographicSize - 1, 0);
            }
            else if (randomStart == 1) //start down
            {
                randomPos = Random.Range(13.5f - cam.orthographicSize * cam.aspect, 13.5f + cam.orthographicSize * cam.aspect);
                start = new Vector3(randomPos, -15 - cam.orthographicSize - 1, 0);
                end = new Vector3(13.5f*2 - randomPos, -15 + cam.orthographicSize + 1, 0);
            }
            else if (randomStart == 2) //start left
            {
                randomPos = Random.Range(-15f - cam.orthographicSize, 15f+ cam.orthographicSize);  
                start = new Vector3(13.5f - cam.orthographicSize * cam.aspect - 1, randomPos, 0);
                end = new Vector3(13.5f + cam.orthographicSize * cam.aspect + 1, -14*2 -randomPos, 0);
            }
            else //start rights
            {
                randomPos = Random.Range(-15f - cam.orthographicSize, 15f + cam.orthographicSize);
                start = new Vector3(13.5f + cam.orthographicSize * cam.aspect + 1, randomPos, 0);
                end = new Vector3(13.5f - cam.orthographicSize * cam.aspect - 1, -14*2 - randomPos, 0);
            }

            GameObject newItem = Instantiate(item, start, Quaternion.identity);
            itemList.Add(newItem);
            StartCoroutine(cherryTween(start, end));
        }
        else if (startTween && !tweener.TweenExists(itemList[0].transform))
        {
            Destroy(itemList[0]);
            itemList.RemoveAt(0);
        }
    }

    private IEnumerator cherryTween(Vector3 origin, Vector3 destination)
    {
        yield return new WaitForSeconds(10);
        tweener.AddTween(itemList[0].transform, origin, destination, 5f);
        startTween = true;
    }

    public void CherryDelete()
    {
        tweener.TweenRemove(itemList[0].transform);
    }
}
