using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testobject : PoolObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * Time.deltaTime * 3;
        transform.Translate(Vector3.forward*Time.deltaTime * 25);
    }

    public override void OnObjectReuse()
    {
        transform.localScale = Vector3.one;
    }
}
