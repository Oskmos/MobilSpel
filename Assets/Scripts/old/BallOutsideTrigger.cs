using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOutsideTrigger : MonoBehaviour
{
    public GameObject button;
    public Transform canvas;
    
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other == GameObject.Find("BallCube").GetComponent<Collider>())
        {
            if (GameObject.Find("ResetButton(BallCube)") == null)
            {
                Instantiate(button, canvas);
            }
        }
     
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == GameObject.Find("BallCube").GetComponent<Collider>())
        {
            if (GameObject.Find("ResetButton(Clone)") != null)
            {
                var button = GameObject.Find("ResetButton(Clone)");
                Destroy(button);
            }
        }

    }
    
}
