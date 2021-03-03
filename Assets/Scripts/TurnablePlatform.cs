using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnablePlatform : MonoBehaviour
{
    [SerializeField] float turnWaitTime;
    [SerializeField] float speed;
    float timer;
    bool canTurn = false;

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = turnWaitTime;
            canTurn = !canTurn;
        }
        
    }

    private void FixedUpdate()
    {
        if(canTurn == true)
            TurnPlatform();
    }

    private void TurnPlatform()
    {
        if(gameObject.transform.rotation.z <= 90)
            gameObject.transform.Rotate(0, 0, 1 * speed);
    }
}
