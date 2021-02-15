using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoPop : MonoBehaviour
{

    float offsetX = -10.0f;

    private bool anticipation = true;
    
    private bool PopIn = true;

    public float ScaleSpeed = 1.2f;
    public float maxScale = 1.1f;
    public float retractSpeed = 0.95f;
    private int delay = 130;

    private float scale = 0.1f;
    bool done = false;
    bool retract = false;

    
    Color _color = Color.white;
    
    
    void Start()
    {
        this.GetComponent<Image>().enabled = false;
    }

    private void FixedUpdate()
    {
        delay--;
        if (PopIn && delay <= 0 ) ScaleUp();
        if (!PopIn && delay <= -100) PanAway();
       
        
    }


    
    void ScaleUp()
    {
        this.GetComponent<Image>().enabled = true;
        transform.localScale = new Vector3(scale, scale, scale);
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
                transform.localScale = new Vector3(scale, scale, scale);
                scale *= retractSpeed;
                if (scale <= 1)
                {
                    scale = 1;
                    done = true;
                    PopIn = false;
                }
            }
        }
    }

    
    void PanAway()
    {
        
        var VectorOffset = new Vector2(offsetX, 0);
        var rect = GetComponent<RectTransform>();
        
        rect.offsetMax += VectorOffset;
        rect.offsetMin += VectorOffset;

        if (anticipation)
        {
            offsetX *= 0.98f;
            if (offsetX <= 0.01) anticipation = false;
        }
        else
        {
  
            offsetX = Mathf.Abs(offsetX);
            offsetX *= 1.5f;
            

        }
   
        

        if (offsetX > 1000) Destroy(gameObject);
        
    }
    
}
