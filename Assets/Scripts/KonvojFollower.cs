using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class KonvojFollower : MonoBehaviour
{

    private PathCreator pathCreator;
    public float speed = 5;
    private float distanceTravelled;
    

    // Start is called before the first frame update
    void Start()
    {
        pathCreator = FindObjectOfType<PathCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

        
    }
}
