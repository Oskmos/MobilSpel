using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact_SmallArms : PoolObject
{
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer > 0.1)
        {
            Destroy();
        }
    }
    
    public override void OnObjectReuse()
    {
        transform.localScale = Vector3.one;
        Timer = 0;

    }
}
