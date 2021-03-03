using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Health : MonoBehaviour
{
    public delegate void DeathAction();
    public DeathAction onDeath;



    public int MaxHealth;
    public int health
    {
        get => _health;
        set
        {
            Debug.Log(_health);
            if (value <= 0)
            {
                _health = 0;
               
                if (onDeath != null) onDeath();
            }
            else _health = value;
        }
    }
    
    private int _health;
    

    // 
    // Start is called before the first frame update
    void Start()
    {
 
        _health = MaxHealth;
        onDeath += death;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void death()
    {


        
        GetComponent<BoxCollider>().enabled = false;
        
    }

}

