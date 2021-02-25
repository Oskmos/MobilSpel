using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzleflash_01 : PoolObject
{
    private float Timer;



    void Update()
    {
        Timer += Time.deltaTime;
        transform.localScale += (Vector3.one * (20 * Time.deltaTime));
        
        if (Timer > 0.05)
        {
            Destroy();
        }
    }
    
    public override void OnObjectReuse()
    {
        transform.localScale = Vector3.zero;
        Timer = 0;

    }
}
