using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private bool solid = false;

    public bool test
    {
        get { return solid; }
    } 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        solid = true;
        Debug.Log("trigger stay: " + other.gameObject.name);
    }
    private void OnTriggerExit(Collider other)
    {
        solid = false;
        Debug.Log("out");
    }
}
