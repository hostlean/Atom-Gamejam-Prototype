using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeValuesByMouse : MonoBehaviour
{

    public float jumpValue;
    public float speedValue;

    Vector3 mousePos;
    Vector3 lastMousePos;
    
    void Start()
    {
        mousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        lastMousePos = mousePos;
        mousePos = Input.mousePosition;

        if(mousePos.x > lastMousePos.x)
        {
            jumpValue = Mathf.Abs(jumpValue);
            speedValue = Mathf.Abs(speedValue);
        }
        else if(mousePos.x < lastMousePos.x)
        {
            jumpValue = -Mathf.Abs(jumpValue);
            speedValue = -Mathf.Abs(speedValue);
        }

        lastMousePos = mousePos;


    }
}
