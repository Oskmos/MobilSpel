using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.EventSystems;

public class CameraPathFollower : MonoBehaviour
{
    private Vector3 mouseOriginPosition = Vector3.zero;
    Vector3 mouseCurrentPosition = Vector3.zero;
    Vector3 direction = Vector3.zero;
    
    private PathCreator pathCreator;

    public float distance;

    private float currentDistance;
    private float savedDistance;
    Vector3 roadDirection = new Vector3(1,1,0);
    
    
    // Start is called before the first frame update
    void Start()
    {
        pathCreator = FindObjectOfType<PathCreator>();
        currentDistance = pathCreator.path.length * 0.5f;
        savedDistance = currentDistance;
        distance = currentDistance;
    }

    // Update is called once per frame
    void Update()
    {

        distance = DragMouse();
        transform.position = pathCreator.path.GetPointAtDistance(distance);
  
        
    }


    float DragMouse()
    {
      
        if (Input.GetMouseButtonDown(0))
        {
            mouseOriginPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            mouseCurrentPosition = Input.mousePosition;
            direction = (mouseCurrentPosition - mouseOriginPosition);
            
            currentDistance = direction.y*0.02f + direction.x*0.02f + savedDistance;
            currentDistance = Mathf.Clamp(currentDistance, 65, pathCreator.path.length - 80);
        }

        if (Input.GetMouseButtonUp(0))
        {
            savedDistance = currentDistance;
        }


        return currentDistance;

    }
    
    private bool checkIfMouseIsOverUI()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);





        return (results.Count > 0);
    }
}
