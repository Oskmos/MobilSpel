using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
   public float viewRadius;
   [Range(0,360)]
   public float viewAngle;

   private Quaternion defaultRotation;

   public LayerMask targetMask;
   public LayerMask obstacleMask;
   
   public List<Transform> visibleTargets = new List<Transform>();


   public GameObject turret;
   public OpenFire openFire;




   
   private void Start()
   {
      defaultRotation = Quaternion.Euler(0,180,0);
      StartCoroutine("FindTargetsWithDelay", .8f);

      var HealthInstance = GetComponentInParent<Health>();
      HealthInstance.onDeath += Death;
      
   }

   private void Update()
   {
      RotateTurret();
   }

   private void RotateTurret()
   {
  
      if (visibleTargets.Count > 0)
      {
         
         Vector3 direction = visibleTargets[0].transform.position - transform.position;
         direction.y = 0;
         Quaternion rotation = Quaternion.LookRotation(direction,Vector3.up);
         turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation,rotation, 0.3f);
         

        openFire.enabled = true;
      }
      else
      {
         turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation,defaultRotation, 0.1f);
            
         openFire.enabled = false;
      }
   }



   IEnumerator FindTargetsWithDelay(float delay)
   {
      while (true)
      {
         yield return new WaitForSeconds(delay);
         FindVisibleTargets();
      }
   }
   
   void FindVisibleTargets()
   {
      visibleTargets.Clear();
      Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

      for (int i = 0; i < targetsInViewRadius.Length; i++)
      {
         Transform target = targetsInViewRadius[i].transform;
         Vector3 dirToTarget = (target.position - transform.position).normalized;
         if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
         {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
            {
               visibleTargets.Add(target);
            }
         }
      }
   }

   void Death()
   {
      openFire.enabled = false;
      this.enabled = false;
   }



}
