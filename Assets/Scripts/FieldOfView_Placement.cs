using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView_Placement : MonoBehaviour
{
   public bool placed = false;
   public float viewRadius;
   
   private float viewAngle= 360;
   
   
   public LayerMask obstacleMask;

   private SoldierPlacer _soldierPlacer;

   public float meshResolution;
   public MeshFilter viewMeshFilter;
   private Mesh viewMesh;

   public int edgeResolveIterations;
   public float edgeDistanceThreshold;
   
   private void Start()
   {
      viewMesh = new Mesh();
      viewMesh.name = "View Mesh";
      viewMeshFilter.mesh = viewMesh;


      _soldierPlacer = FindObjectOfType<SoldierPlacer>();
      placed = false;
      
      
   }
   
   private void LateUpdate()
   {
      DrawFieldOfView();

      if (_soldierPlacer.placed) ReduceRadius();
   }
   
   void ReduceRadius()
   {
      viewAngle = Mathf.Lerp(viewAngle, 90, 0.02f);
   }
   
   void DrawFieldOfView()
   {
      int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
      float stepAngleSize = viewAngle / stepCount;

      List<Vector3> viewPoints = new List<Vector3>();
      
      ViewCastInfo oldViewCast = new ViewCastInfo();
      
      for (int i = 0; i <= stepCount; i++)
      {
         float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
         ViewCastInfo newViewCast = ViewCast(angle);
         
         if (i > 0)
         {
            bool edgeDistanceThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDistanceThreshold;
            if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDistanceThresholdExceeded))
            {
               EdgeInfo edge = FindEdge(oldViewCast, newViewCast);

               if (edge.pointA != Vector3.zero)
               {
                  viewPoints.Add(edge.pointA);
               }
               if (edge.pointB != Vector3.zero)
               {
                  viewPoints.Add(edge.pointB);
               }
               
            }
         }
         
         viewPoints.Add(newViewCast.point);
         oldViewCast = newViewCast;
      }

      int vertexCount = viewPoints.Count + 1;
      Vector3[] verteces = new Vector3[vertexCount];
      int[] triangles = new int[(vertexCount-2) * 3];
      
      verteces[0] = Vector3.zero;
      for (int i = 0; i < vertexCount-1; i++)
      {
         verteces[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

         if (i < vertexCount - 2)
         {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
         }
      }
      
      
      viewMesh.Clear();
      viewMesh.vertices = verteces;
      viewMesh.triangles = triangles;
      viewMesh.RecalculateNormals();
      

   }

   EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
   {
      float minAngle = minViewCast.angle;
      float maxAngle = maxViewCast.angle;
      Vector3 minPoint = Vector3.zero;
      Vector3 maxPoint = Vector3.zero;

      for (int i = 0; i < edgeResolveIterations; i++)
      {
         float angle = (minAngle + maxAngle) / 2;
         ViewCastInfo newViewCast = ViewCast(angle);

         
         bool edgeDistanceThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDistanceThreshold;
         if (newViewCast.hit == minViewCast.hit && !edgeDistanceThresholdExceeded)
         {
            minAngle = angle;
            minPoint = newViewCast.point;
         }
         else
         {
            maxAngle = angle;
            maxPoint = newViewCast.point;
         }
      }
      
      return new EdgeInfo(minPoint, maxPoint);
   }

   ViewCastInfo ViewCast(float globalAngle)
   {
      Vector3 dir = DirFromAngle(globalAngle, true);
      RaycastHit hit;

      if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
      {
         return new ViewCastInfo(true,hit.point,hit.distance,globalAngle);
      }
      else
      {
         return new ViewCastInfo(false,transform.position + dir * viewRadius, viewRadius, globalAngle);
      }
   }
   
   public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
   {
      if (!angleIsGlobal)
      {
         angleInDegrees += transform.eulerAngles.y;
      }
      return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
   }

   public struct ViewCastInfo
   {
      public bool hit;
      public Vector3 point;
      public float dst;
      public float angle;


      public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
      {
         hit = _hit;
         point = _point;
         dst = _dst;
         angle = _angle;
      }
   }


   public struct EdgeInfo
   {
      public Vector3 pointA;
      public Vector3 pointB;

      public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
      {
         pointA = _pointA;
         pointB = _pointB;
      }
   }
   
}
