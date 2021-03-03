using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleVibration : MonoBehaviour
{

    private Vector3 positionOffset;

    // Start is called before the first frame update
    void Start()
    {
        var HealthInstance = transform.parent.GetComponentInChildren<Health>();
        HealthInstance.onDeath += Death;

    }

    // Update is called once per frame
    void Update()
    {
        positionOffset = Vector3.zero;

        positionOffset.x = transform.position.x;
        positionOffset.z = transform.position.z;

        positionOffset.y = transform.position.y +(Mathf.Sin(Time.time * 30) * 0.0007f);
           
        
        this.transform.position = positionOffset;
        
        
        
        
        //positionOffset = Vector3.zero;
    }

    void Death()
    {
        positionOffset = Vector3.zero;
        positionOffset = transform.position;
        transform.position = positionOffset;
        this.enabled = false;
    }
}
