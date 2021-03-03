using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsSpin : MonoBehaviour
{
    private Quaternion rotationQuaternion;

    private Vector3 rotation;

    public KonvojFollower KonvojFollower;
    
    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        rotation.x += KonvojFollower.speed * 90f * Time.deltaTime;
        rotation.y = KonvojFollower.transform.rotation.eulerAngles.y;
       
        rotationQuaternion.eulerAngles = rotation;
        transform.rotation = rotationQuaternion;
        
        
    }
    
    
}
