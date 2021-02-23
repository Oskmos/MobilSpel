using System;
using UnityEngine;

    public class Crowd_Animation : MonoBehaviour
    {
        
        public Crowd_Movement playerMovement;
        private bool _doJumpAnimation;
        private bool _doLandingAnimation;
        
        private float width;
        private float height;
        private int JumpAnimHeightSeq;
        private bool InitAnimSeqWidth = true;

        private float sinTime = Mathf.PI;
        private float sinMagnitude = 0.3f;



     

        void ResetJumpAnimation()
        {
            sinTime = Mathf.PI;
            width = 0;
            height = 0;

            JumpAnimHeightSeq = 0;
            
            InitAnimSeqWidth = true;

            sinMagnitude = 0.3f;
        }
        
        
        void Start()
        {
  
        }

        void Update()
        {
            if (!_doJumpAnimation && !_doLandingAnimation) ResetJumpAnimation();
            
            if (_doJumpAnimation) JumpAnimation();
            if (_doLandingAnimation) LandingAnimation();

        }
        

        private void JumpAnimation()
        {
            
            if (InitAnimSeqWidth)
            {
                width += Time.deltaTime * 15;
                if (width >= 0.2f) InitAnimSeqWidth = false;
            }
            else
            {
                width -= Time.deltaTime * 5;
                if (width <= 0)
                {
                    width = 0f;
                }
            }


            if (JumpAnimHeightSeq == 0)
            {
                height -= Time.deltaTime * 5;
                if (height <= -0.3f) JumpAnimHeightSeq = 1;
            }
            else if (JumpAnimHeightSeq == 1)
            {
                height += Time.deltaTime * 10;
                if (height >= 0.4f)
                {
                    height = 0.4f;
                    JumpAnimHeightSeq = 2;
                    
                }
            }
            else if(JumpAnimHeightSeq == 2)
            {
                height -= Time.deltaTime * 2;
                if (height <= -0.2f)
                {
                    height = -0.2f;
                    JumpAnimHeightSeq = 3;
                }
            }
            else if (JumpAnimHeightSeq == 3)
            {
                height += Time.deltaTime * 0.5f;
            }
            
            
            this.transform.localScale = new Vector3(1f+width,1f+height * playerMovement.JumpMagnitude,1f+width );
        }
        
        
        private void LandingAnimation()
        {
            
            if (InitAnimSeqWidth)
            {
                width += Time.deltaTime * 15;
                if (width >= 0.4f) InitAnimSeqWidth = false;
            }
            else
            {
                width -= Time.deltaTime * 6;
                if (width <= 0) width = 0f;
            }
            
            
            sinMagnitude -= Time.deltaTime*1.5f;
            sinMagnitude = Mathf.Clamp(sinMagnitude, 0, 1);
            sinTime += Time.deltaTime * 20;
            var sin = (Mathf.Sin(sinTime) * sinMagnitude);
            
            
            if (sinMagnitude <= 0) _doLandingAnimation = false;
            this.transform.localScale = new Vector3(1f+width*playerMovement.JumpMagnitude,1f+sin,1f+width*playerMovement.JumpMagnitude );
            
        }
 

        
        
        public void JumpingAnimationDelegate()
        {
            ResetJumpAnimation();
            _doJumpAnimation = true;
        }
        public void LandingAnimationDelegate()
        {
            ResetJumpAnimation();
            _doJumpAnimation = false;
            _doLandingAnimation = true;
        }


    }
    

