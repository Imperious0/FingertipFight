using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionCapturer : MonoBehaviour
{
    private Vector2 beginTouch = Vector2.zero;
    private Vector2 currentTouch = Vector2.zero;

    private Vector2 movementDir = Vector2.zero;

    MotionType currentMotion = MotionType.NONE;
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    //Used Cuz FPS Rate > FixedUpdate Frequency
    private void Update()
    {
        if (Input.GetKey("Escape"))
        {
            Application.Quit();
        }
        checkMotions();
    }
    private void checkMotions() 
    {
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
        if (Input.GetMouseButtonDown(0))
        {
            beginTouch = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButton(0))
        {
            currentTouch = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if((beginTouch - currentTouch).magnitude > 0.1f)
            {
                currentMotion = MotionType.MOVEMENT;
                movementDir = (beginTouch - currentTouch).normalized;
            }
            else
            {
                movementDir = Vector2.zero;
                currentMotion = MotionType.NONE;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            movementDir = Vector2.zero;
            currentMotion = MotionType.NONE;
        }
#else
        if(Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                beginTouch = new Vector2(t.position.x, t.position.y);
            }else if(t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
            {
                currentTouch = new Vector2(t.position.x, t.position.y);
            }
            
            if(t.phase == TouchPhase.Canceled || t.phase == TouchPhase.Ended)
            {
                currentMotion = MotionType.NONE;
                movementDir = Vector2.zero;
                return;
            }

            Vector2 tmpDir = beginTouch - currentTouch;

            if(tmpDir.magnitude > 0.1)
            {
                currentMotion = MotionType.MOVEMENT;
                movementDir = tmpDir.normalized;
                
            }else{
                currentMotion = MotionType.NONE;
                movementDir = Vector2.zero;
            }
        }
#endif
    }

    /// <summary>
    /// Return Motion Vector if there is motion
    /// </summary>
    /// <returns>
    /// <see cref="Vector2"/> Movement Direction If Not <see cref="Vector2.zero"/>
    /// </returns>
    public Vector2 getMovementVector()
    {
        Vector2 tmpVector = Vector2.zero;
        if (currentMotion.Equals(MotionType.MOVEMENT))
        {
            //Last Normalize for set vector length exact -1,1 range
            tmpVector = movementDir;
            tmpVector = tmpVector.normalized;
        }
        return tmpVector;
    }
}

public enum MotionType
{
    NONE,
    TAP,//Not Necessary
    MOVEMENT
}

