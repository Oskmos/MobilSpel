using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegKicker : MonoBehaviour
{
    private Vector3 rotator;
    private Quaternion rotatorQ;

    private float LegKickerRot;
    private float LegKickerMaxRot;
    private float LegKickerRotVar = 1f;
    private float LegSiner = 100;
    
    public int LegPart = 0;

    private float timer = Mathf.PI / 2;

    private float delay;

    public bool DoneKick = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (LegPart == 0)
        {
            LegKickerRot = 100;
            LegKickerMaxRot = 180;
        }
        
        if (LegPart == 1)
        {
            LegKickerRot = 180;
            LegKickerMaxRot = 160;
        }
        
        if (LegPart == 2)
        {
            LegKickerRot = -60;
            LegKickerMaxRot = -90;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if (LegPart == 0)
        {
            if (!DoneKick)
            {
                if (LegKickerRotVar < LegKickerMaxRot)
                {
                    LegKickerRotVar += (180 * Time.deltaTime);
                }
                else
                {
                    DoneKick = true;
                }
            }


            rotator.z = LegKickerRot - LegKickerRotVar;
        }
        
        if (LegPart == 1)
        {
            if (LegKickerRotVar < LegKickerMaxRot)
            {
                LegKickerRotVar += (180 * Time.deltaTime);
            }

            rotator.z = LegKickerRot - LegKickerRotVar;

        }
        
        if (LegPart == 2)
        {
            var rigid = GetComponent<CapsuleCollider>();
            if (DoneKick) rigid.isTrigger = true;
            
            
            if (LegKickerRotVar < LegKickerMaxRot)
            {
                LegKickerRotVar -= 100*Time.deltaTime;
            }

            rotator.z = LegKickerRot - LegKickerRotVar;

        }
        
        
        rotatorQ.eulerAngles = rotator;
        this.transform.localRotation = rotatorQ;
        
        
        
    }
}
