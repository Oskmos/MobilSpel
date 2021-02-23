using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    private Enemy target;
    bool targetAquired;
    [SerializeField]
    private LayerMask targetLayerMask;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RayCastScan();
    }


    void RayCastScan()
    {
        
        Vector3 origin = transform.position;
        Vector3 direction;
        float rand;
        float maxDistance = 10f;



        if (targetAquired)
        {
            var targetMinusOrigin = target.transform.position - origin;
            if (targetMinusOrigin.magnitude > maxDistance) targetAquired = false;
            
            
            direction = (targetMinusOrigin).normalized;
        }
        else
        {
            direction = transform.forward;
            rand = Random.Range(-0.7f, 0.7f);
            direction.x += rand;
            rand = Random.Range(-0.7f, 0.7f);
            direction.z += rand;
            direction = direction.normalized;
         
        }


        Debug.DrawRay(origin, direction * maxDistance , Color.red);
        Ray ray = new Ray(origin, direction);

        targetAquired = false;
        target = null;
        
        if (Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance))
        {
            var hitObject = raycastHit.collider.gameObject.GetComponent<Enemy>();
            
            if (hitObject != null)
            {
                target = hitObject;
                targetAquired = true;
            }
        }

        
    }
}
