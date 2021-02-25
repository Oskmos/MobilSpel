using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleSmoke_01 : PoolObject
{
    private float Timer;


    void Update()
    {
        
        
        Timer += Time.deltaTime;
        transform.localScale += (Vector3.one * (5 * Time.deltaTime));
        
        if (Timer > 0.15)
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
