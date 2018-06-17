using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAdapter : MonoBehaviour
{

    public float radius = 100;
    [HideInInspector]
    public Vector2 vector = Vector2.zero;
    [HideInInspector]
    public bool isControlled = false;

}
