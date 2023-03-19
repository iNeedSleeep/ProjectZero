using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public bool inputEnable = true;
    [Header("------- Keys Mapping -------")]

    public KeyCode KeyLeft = KeyCode.A;
    public KeyCode KeyRight = KeyCode.D;
    public KeyCode KeyUp = KeyCode.W;
    public KeyCode keyDown = KeyCode.S;
    public KeyCode KeyFocus = KeyCode.Space;
    public KeyCode KeyFire = KeyCode.Mouse0;
    public KeyCode KeyEffect = KeyCode.Mouse1;

    [Header("------- Status -------")]
    
    public bool jump;
    public bool fire;
    public bool effect;
    public float smoothTime = 0.05f;

    
    private float Dup;
    private float Dright;
    public Vector2 Dvec{
        get {return (Dup*Vector2.up + Dright*Vector2.right).normalized;}
    }
    
    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;

    public float Dmagitude
    {
        get{return Mathf.Clamp01(Mathf.Abs(Dright*Dright + Dup*Dup));;}
    }

    public float getDUp()
    {
        return Dup;
    }
    public float getDRight()
    {
        return Dright;
    }
    
    public Vector3 getDVec()
    {
        return Dvec;
    }

    // Update is called once per frame
    void Update()
    {
        targetDup = (Input.GetKey(KeyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(KeyRight) ? 1.0f : 0) - (Input.GetKey(KeyLeft) ? 1.0f : 0);
        if (inputEnable == false)
        {
            targetDup = 0;
            targetDright = 0;
        }
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, smoothTime);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, smoothTime);
        fire = Input.GetKeyDown(KeyFire);
        effect = Input.GetKeyDown(KeyEffect);
    }
}
