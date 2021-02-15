using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegKicker_PopAnim : MonoBehaviour
{
    float offsetX = -10.0f;

    private bool anticipation = true;

    private bool PopIn = true;

    public float ScaleSpeed = 1.2f;
    public float maxScale = 1.1f;
    public float retractSpeed = 0.95f;
    private int delay;
    

    private float scale = 0.1f;
    private float scalawayer = 1.1f;
    
    bool done = false;
    bool retract = false;

    bool done2 = false;
    bool retract2 = false;
    
    public LegKicker kicker;
    public Transform Transform;
    Color _color = Color.white;




    private void FixedUpdate()
    {
        Debug.Log(delay);

        if (PopIn) ScaleUp();

        if (kicker.DoneKick)
        {
            delay++;
        }
        if (delay > 5) ScaleDown();
    }



    void ScaleUp()
    {

        Transform.localScale = new Vector3(scale, scale, scale);
        if (!done)
        {
            if (!retract)
            {
                scale = Mathf.Clamp(scale, 0, maxScale);
                scale *= ScaleSpeed;
                if (scale >= maxScale) retract = true;

            }
            else
            {
                Transform.localScale = new Vector3(scale, scale, scale);
                scale *= retractSpeed;
                if (scale <= 1)
                {
                    scale = 1.0f;
                    done = true;
                    PopIn = false;
                }
            }
        }
    }
    void ScaleDown()
    {
 
        Transform.localScale = new Vector3(scale, scale, scale);
        if (!done2)
        {

        
                Transform.localScale = new Vector3(scale, scale, scale);
                scale *= scalawayer;
                scalawayer *= 0.96f;
                if (scale <= 0)
                {
                    scale = 0.0f;
                    done2 = true;
                    Destroy(this.gameObject);
                }
            
        }

    }

   
}
