using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject ball;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var trans = this.transform.position;
        trans.x = ball.transform.position.x + 20f + (ball.transform.position.z *0.8f);
     
        this.transform.position = trans;
        
    }
}
