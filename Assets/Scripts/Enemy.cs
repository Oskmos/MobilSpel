using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        var HealthInstance = transform.parent.GetComponentInChildren<Health>();
        HealthInstance.onDeath += Death;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Death()
    {
        
        Debug.Log("DEAD");
    }
}
