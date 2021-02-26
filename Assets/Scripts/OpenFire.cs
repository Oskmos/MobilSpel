using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFire : MonoBehaviour
{
    public Weapon weapon;

    private float timer;

    private float ROF;
    
    // Start is called before the first frame update
    void Start()
    {
        ROF = (1f / (weapon.RoundsPerMinute / 60f));
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > ROF)
        {
            weapon.Fire();
            timer = 0;
        }
    }
}
