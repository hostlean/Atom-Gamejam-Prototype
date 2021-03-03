// Add to walls that should be ignored while running at certain speed

using UnityEngine;

public class Test934 : MonoBehaviour
{
    BoxCollider2D col2D;

    [SerializeField] GameObject _player;

    /// Ignore limit
    [SerializeField] private float ignoreSpeed = 50f;

    [SerializeField] private GameObject _hitParticle;

    void Start()
    {
        col2D = GetComponent<BoxCollider2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        DisableCollider();
    }

    void DisableCollider()
    {
        if (Mathf.Abs(_player.GetComponent<Rigidbody2D>().velocity.x) >= ignoreSpeed)
        {
            col2D.isTrigger = true;
        }
        else
        {
            col2D.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(_hitParticle, transform.position, Quaternion.identity);
    }
}
