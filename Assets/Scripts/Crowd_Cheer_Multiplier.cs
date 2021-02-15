using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd_Cheer_Multiplier : MonoBehaviour
{
    public float CheerMult;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
            CheerMult = Mathf.Abs((this.transform.position.x * 100));





    }
}
