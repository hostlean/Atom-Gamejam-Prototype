using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoverHorizontal : MonoBehaviour
{
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform rightPos;

    [SerializeField] public float _speed;
    public Vector3 moveDirHorizontal;

    void Start()
    {
        moveDirHorizontal = Vector3.right;
        
    }

    void Update()
    {
        MoveSideways();

    }

    private void MoveSideways()
    {
        gameObject.transform.Translate(moveDirHorizontal * _speed * Time.deltaTime);

        if(gameObject.transform.position.x > rightPos.position.x)
            moveDirHorizontal = new Vector2(Mathf.Abs(moveDirHorizontal.x) * -1, Mathf.Abs(moveDirHorizontal.y) * -1);
        if(gameObject.transform.position.x < leftPos.position.x)
            moveDirHorizontal = new Vector2(Mathf.Abs(moveDirHorizontal.x), Mathf.Abs(moveDirHorizontal.y));
    }


}
