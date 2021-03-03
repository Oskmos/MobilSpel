using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public int Level;

    public enum SoldierType
    {
        Recruit,
        Rifleman,
        AT,
        MG,
        Medic
    }

    public SoldierType soldierType;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
