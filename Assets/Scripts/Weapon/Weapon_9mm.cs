using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_9mm : MonoBehaviour
{
    public GameObject projectile;
    
    public GameObject impact;
    public GameObject muzzleflash;
    
    // Start is called before the first frame update
    void Start()
    {
        
        PoolManager.instance.CreatePool(projectile, 30);
        
        PoolManager.instance.CreatePool(impact, 10);
        PoolManager.instance.CreatePool(muzzleflash, 10);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    public void Fire()
    {
        Quaternion rotation = this.transform.rotation;
        PoolManager.instance.ReuseObject(projectile,this.transform.position, rotation);
        PoolManager.instance.ReuseObject(muzzleflash,this.transform.position, rotation);
    }
    
    public void BulletImpact(Vector3 position)
    {
        PoolManager.instance.ReuseObject(impact,position, Quaternion.identity);
    }


}

