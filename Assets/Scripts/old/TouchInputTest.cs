using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputTest : MonoBehaviour
{
    Vector2 vector;
    Vector3 Givevelocity;
    public GameObject field;
    public GameObject kickerPrefab;

    private Vector3 startPos;
    private Quaternion newRotation;

    private GameObject instance;

    // Update is called once per frame
    void Update()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
                if(Physics.Raycast(ray, out hit))
                {
                    
                    if (hit.collider == field.gameObject.GetComponent<Collider>())
                    {
                        startPos = hit.point;
                        instance = Instantiate(kickerPrefab,startPos,new Quaternion());
                        
                    }
                }
                
                

            }

            if (touch.phase == TouchPhase.Moved)
            {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
                if(Physics.Raycast(ray, out hit))
                {
                    

                    if (hit.collider == field.gameObject.GetComponent<Collider>())
                    {
                        var direction = (hit.point - startPos);
                        instance.transform.rotation = Quaternion.LookRotation(direction);
                        instance.transform.Rotate(0f,90f,0f);
                    }
                }
                
            }
            
        }



    }
}
