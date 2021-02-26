using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_SmallArms : PoolObject
{
    private Weapon_SmallArms _weapon9Mm;
    private float Velocity = 200f;
    

    private Vector3 direction;
    private Vector3 startScale = new Vector3(0.5f, 0.5f, 0.01f);
    private Vector3 scale;
    
    
    // Start is called before the first frame update
    void Start()
    {

        
        if (_weapon9Mm == null)
        {
            _weapon9Mm = FindObjectOfType<Weapon_SmallArms>();
        }
    }

    void Update()
    {
        RayCheck();
        transform.localScale = scale;
        

    }

    public override void OnObjectReuse()
    {
        scale = startScale;
    }
    
    public override void OnProjectilImpact()
    {
        
        Destroy();
    }

    void RayCheck()
    {
        Vector3 transformer;

        RaycastHit hit;


        transformer = Vector3.zero;
        Debug.DrawRay(transform.position, transform.forward , Color.blue ,0.1f);
        if (!Physics.Raycast(transform.position, transform.forward , out hit ,1*Velocity*Time.deltaTime))
        {

            transformer += transform.forward * (Velocity * Time.deltaTime);
            if (scale.z < 1) scale.z += (0.5f*Velocity * Time.deltaTime);
            transform.position += transformer;
        }
        else
        {
            _weapon9Mm.BulletImpact(hit.point);
            scale = startScale;
            Destroy();

            //hit.collider.enabled = false;
            //IMPACT
        }
    }
}
