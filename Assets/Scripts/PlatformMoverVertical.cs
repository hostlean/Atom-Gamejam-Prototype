using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoverVertical : MonoBehaviour
{
    [SerializeField] private Transform downPos;
    [SerializeField] private Transform upPos;

    [SerializeField] public float _speed;
    Vector2 moveDirVertical;

    void Start()
    {
        moveDirVertical = Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        MoveVertical();
    }


    private void MoveVertical()
    {
        gameObject.transform.Translate(moveDirVertical * _speed * Time.deltaTime);

        if(gameObject.transform.position.y > upPos.position.y)
            moveDirVertical =  new Vector2(Mathf.Abs(moveDirVertical.x) * -1, Mathf.Abs(moveDirVertical.y) * -1);
        if(gameObject.transform.position.y < downPos.position.y)
            moveDirVertical = new Vector2(Mathf.Abs(moveDirVertical.x), Mathf.Abs(moveDirVertical.y));
    }
}
