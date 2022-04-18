using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionCapturer : MonoBehaviour
{
    private Vector2 beginTouch = Vector2.zero;
    private Vector2 currentTouch = Vector2.zero;

    private Vector2 movementDir = Vector2.zero;

    MotionType currentMotion = MotionType.NONE;

    //Used Cuz FPS Rate > FixedUpdate Frequency
    private void Update()
    {
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
                currentMotion = MotionType.NONE;
                beginTouch = Vector2.zero;
                currentTouch = Vector2.zero;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            currentMotion = MotionType.NONE;
            beginTouch = Vector2.zero;
            currentTouch = Vector2.zero;
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
                return;
            }

            Vector2 tmpDir = currentTouch - beginTouch;

            if(tmpDir.magnitude > 0.1)
            {
                currentMotion = MotionType.MOVEMENT;
                movementDir = tmpDir.normalized;
                
            }else{
                currentMotion = MotionType.NONE;
                beginTouch = Vector2.zero;
                currentTouch = Vector2.zero;
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

