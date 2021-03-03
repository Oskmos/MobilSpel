using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoldierPlacer : MonoBehaviour
{
    public GameObject prefab_placer;
    public GameObject prefab_soldier;
    public LayerMask layermask_placable;
    private GameObject poolHolder;
    private GameObject instance;


    private bool place;
    [HideInInspector]
    public bool placed;
    private void Start()
    {
        poolHolder = new GameObject("Soldier pool");
    }

    private void Update()
    {
        if (!checkIfMouseIsOverUI())
        {
            if (Input.GetMouseButtonDown(0) && !placed) place = true;
            if (place && !placed) placeAndDragPlacer();
            if (placed) rotatePlacer();
        }

    }

    void placeAndDragPlacer()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 500f))
            {
                instance = Instantiate(prefab_placer,hit.point,Quaternion.identity, poolHolder.transform);
            }
            
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 500f, layermask_placable))
            {
                instance.transform.position = hit.point;
            }

            if (Input.GetMouseButtonDown(1))
            {
                place = false;
                placed = false;
                Destroy(instance);
                instance = null;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            place = false;
            placed = true;

         
        }
    }

    void rotatePlacer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 500f, layermask_placable))
        {
            var origin = instance.transform.position;
            var target = hit.point;
            var dir = (target - origin).normalized;
            Quaternion rotation = Quaternion.LookRotation(dir,Vector3.up);

            instance.transform.rotation = rotation;
            
            if (Input.GetMouseButtonDown(0))
            {
                placeFinal(rotation);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            place = false;
            placed = false;
            Destroy(instance);
            instance = null;
        }

    }


    void placeFinal(Quaternion rotation)
    {
        
        Instantiate(prefab_soldier, instance.transform.position, rotation, poolHolder.transform);
        Destroy(instance);
        placed = false;
        instance = null;
    }

    private bool checkIfMouseIsOverUI()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return  (results.Count > 0);
    }
}
