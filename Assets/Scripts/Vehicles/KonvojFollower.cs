using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class KonvojFollower : MonoBehaviour
{

    public int queuePosition;
    
    private PathCreator pathCreator;
    public float speed = 5;
    private float currentSpeed;
    private float offsetX = 0.001f;
    private float time;

    private float distanceTravelled;
    private bool dead = false;

    public Material wreckMaterial;
    private Transform[] objects;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        pathCreator = FindObjectOfType<PathCreator>();

        
        objects = GetComponentsInChildren<Transform>();
        
        var HealthInstance = GetComponentInChildren<Health>();
        HealthInstance.onDeath += Death;

    }

    // Update is called once per frame
    void Update()
    {

        if (!dead)
        {
            distanceTravelled += currentSpeed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled + (queuePosition*10));
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled + (queuePosition*10));
            RayCheck();
        }
        else
        {
            //DEATH EFFECT
            offsetX += (currentSpeed * 0.003f);
            
            distanceTravelled += currentSpeed * Time.deltaTime;
           Vector3 position = pathCreator.path.GetPointAtDistance(distanceTravelled + (queuePosition*10));
           Quaternion rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled + (queuePosition*10));
           position.x += offsetX;
           Vector3 EulerRotation = Vector3.zero;
           EulerRotation.y = transform.rotation.eulerAngles.y;
           EulerRotation.y -= (currentSpeed*0.05f);
           EulerRotation.z = -90;
           rotation.eulerAngles = EulerRotation;
           transform.position = position;
           transform.rotation = rotation;
           currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.01f);
        }
    }
    
    
    void RayCheck()
    {
        Vector3 position = transform.position;
        position.y += 0.5f;
        RaycastHit hit;
        
        Debug.DrawRay(position, transform.forward*5 , Color.blue ,0.1f);
        if (Physics.Raycast(position, transform.forward , out hit ,5))
        {
            if (hit.collider.GetComponentInParent<KonvojFollower>())
            {
                var otherSpeed = hit.collider.GetComponentInParent<KonvojFollower>().currentSpeed;
                if (otherSpeed < currentSpeed)
                {
                    currentSpeed = Mathf.Lerp(currentSpeed, -0.2f, 0.02f);
                }
            }
        }
        else 
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speed, 0.002f);
        }

    }

    void Death()
    {
        
        foreach (var material in objects)
        {
            if (material.GetComponent<MeshRenderer>())
            {
                material.GetComponent<MeshRenderer>().material = wreckMaterial;
            }
        }
        
        speed = 0;

        dead = true;
    }
}
