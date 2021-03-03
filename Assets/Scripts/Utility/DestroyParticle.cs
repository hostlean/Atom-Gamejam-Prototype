using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    [SerializeField] private float timeLeft;

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        DestroyThis();
    }

    void DestroyThis()
    {
        if (timeLeft <= 0.0f)
            Destroy(this.gameObject);
    }
}
