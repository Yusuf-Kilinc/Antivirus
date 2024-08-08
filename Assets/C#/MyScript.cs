using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    public FixedTouchField Touchfield;
    public MouseLook look;

    
    void Update()
    {
        look.LookAxis = Touchfield.TouchDist;
    }
}
