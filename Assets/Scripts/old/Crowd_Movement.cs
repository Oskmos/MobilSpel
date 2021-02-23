﻿using System;
 using System.Collections;
using System.Collections.Generic;
 using NUnit.Framework.Internal;
 using UnityEngine;
 using Random = UnityEngine.Random;

 public class Crowd_Movement : MonoBehaviour
 {

     
     public Crowd_Animation animation;
     public Crowd_Cheer_Multiplier CheerMultiplier;

     private int IntensityMult;
     public bool HasJumped;
     private float _jumpMagnitude;
   
     public float JumpMagnitude
    
     {
        get => _jumpMagnitude;
        set
        {
            value = Mathf.Clamp(value, 0.2f, 1f);
            _jumpMagnitude = value;
            
        } 
     }

     public bool pink;
    private int jumpMagRnd = 0;
    
    private float jumpStrength = 7f;
    private float jumpVar;
    private float FakeGravity = 50f;
    private float fakeGravityVar;

    private Vector3 initialPosition;
    

    private Vector3 inAirMoveDirection;
    private Vector3 jumper;
    private Vector3 masterMover;

    void Start()
    {
        initialPosition = this.transform.position;
        masterMover = initialPosition;

        
    }

    void Update()
    {
        CrowdJumping();


    }

    void CrowdJumping()
    {
        JumpChecker();

        fakeGravityVar = Mathf.Clamp(fakeGravityVar, 10, FakeGravity);

        if (HasJumped)
        {
            jumper.y += jumpVar * Time.deltaTime;
            jumpVar -= (fakeGravityVar * Time.deltaTime);


            if (jumper.y < 0.00001f)
            {
                HasJumped = false;
                jumper = Vector3.zero;
                animation.LandingAnimationDelegate();
            }
        }
        
        

        masterMover.y = jumper.y+initialPosition.y;

        this.transform.position = masterMover;
    }
    
    void JumpChecker()
    {
        var rndMax = 1400 - CheerMultiplier.CheerMult;
        rndMax = Mathf.Clamp(rndMax, 11, 2000);
        var rnd = Mathf.FloorToInt(Random.Range(0, rndMax));

        if (rnd == 10)
        {
            if (!HasJumped)
            {
                
                animation.JumpingAnimationDelegate();
                HasJumped = true;
                jumpVar = jumpStrength;
                fakeGravityVar = 10;
                JumpMagnitude = 0.2f;
                jumpMagRnd = Random.Range(0, 120);
               
            }
        }

        if (jumpMagRnd > 0 && jumpVar > 0)
        {
            fakeGravityVar += Time.deltaTime * 30;
            JumpMagnitude += Time.deltaTime*2;
            jumpMagRnd--;
        }
        else
        {
            fakeGravityVar += Time.deltaTime * 300;
            jumpMagRnd = 0;
        }
        
        
       
    }


    

}
