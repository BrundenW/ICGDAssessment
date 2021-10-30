using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
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
    }

    private void OnTriggerExit(Collider other)
    {
        solid = false;
    }
}
