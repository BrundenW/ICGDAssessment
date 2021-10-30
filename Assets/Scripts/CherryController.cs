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

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        transform.position = new Vector3(-25, 0, 0);
        startTween = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (itemList.Count == 0)
        {
            startTween = false;
            int randomPos = Random.Range(0, -29);
            Vector3 start = new Vector3(-25, randomPos, 0);
            Vector3 end;

            GameObject newItem = Instantiate(item, start, Quaternion.identity);
            itemList.Add(newItem);

            if (randomPos < -14)
            {
                end = new Vector3(50, -14 + (-14 - randomPos), 0);
            }
            else
            {
                end = new Vector3(50, -14 - (14 + randomPos), 0);
            }

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
