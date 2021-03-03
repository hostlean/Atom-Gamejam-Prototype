using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    Movement movement;
    TrailRenderer trailRenderer;

    [SerializeField] private GameObject _speedBar;
    [SerializeField] private GameObject _jumpBar;

    [SerializeField] private GameObject _cheatSheet;
    [SerializeField] private GameObject _deathParticle;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject _cheatCanvas;

    [SerializeField] private float scrollMult;
    [SerializeField] private float defaultSpeed = 5.93f;

    [SerializeField] public float scrollValue;

    public Vector3 CheckPointPos { get; set; }

    private int count = 0;

    bool usingJump = false;
    bool usingSpeed = false;
    bool canUseSkills = false;
    bool canUseGravity = false;
    bool canUseJumpMult = false;
    bool canUseSpeedMult = false;
    bool canUseScaleMult = false;

    CheatSheet cheat;

    bool isColliding;

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cheat = _cheatSheet.GetComponent<CheatSheet>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.LoadLevelSelection();
        }


        isColliding = false;
        RenderTrail();

        if(canUseSkills)
            Skills();
        else return;
    }

    private void Skills()
    {
        if(canUseGravity)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                ChangeGravity();
                SFXChanger.Instance.PlayGravitySound();
            }
        }
        //if (Input.GetKeyDown(KeyCode.Tab))
        //    ActivateCanvas();

        if(canUseJumpMult)
        {
            if(Input.GetKey(KeyCode.Mouse0))
            {
                usingJump = true;
                _jumpBar.SetActive(true);
                cheat.ChangeJumpSlider();
            }
            else
            {
                usingJump = false;
                if(usingJump == false)
                    StartCoroutine(HideObject(_jumpBar));
            }
        }

        if(canUseSpeedMult)
        {
            if(Input.GetKey(KeyCode.Mouse1))
            {
                usingSpeed = true;
                _speedBar.SetActive(true);
                cheat.ChangeSpeedSlider();
            }
            else
            {
                usingSpeed = false;
                if(usingSpeed == false)
                    StartCoroutine(HideObject(_speedBar));
            }
        }

        if(canUseScaleMult)
        {
            scrollValue = Input.GetAxis("Mouse ScrollWheel") * scrollMult;
        }
    }

    void RenderTrail()
    {
        if (movement.MovementSpeed > defaultSpeed)
        {
            trailRenderer.enabled = true;
        }

        if (movement.MovementSpeed <= defaultSpeed)
        {
            trailRenderer.enabled = false;
        }
    }


    void ActivateCanvas()
    {
        _cheatCanvas.SetActive(!_cheatCanvas.activeSelf);
        if(Time.timeScale == 0)
            Time.timeScale = 1;
        else Time.timeScale = 0;
    }

    private void SetActiveCheckPoint(Transform pos)
    {
        CheckPointPos = pos.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Checkpoint")
            SetActiveCheckPoint(collision.gameObject.transform);
        if (collision.gameObject.tag == "Spike")
            StartCoroutine(GetDamage());
        if(isColliding)
            return;
        else
        {
            if(collision.gameObject.tag == "Card")
            {
                UIManager.Instance.AddOneKeyCard();
                GameManager.Instance.LoadNextLevel();
                isColliding = true;
            }
        }

        if(collision.gameObject.tag == "Tutorial")
        {
            collision.gameObject.GetComponent<Tutorial>().UseDialogue();
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            if(collision.gameObject.name == "Gravity Enabler")
                SetGravityCondition(true);
            if(collision.gameObject.name == "Scale Enabler")
                SetScaleMultCondition(true);
            if(collision.gameObject.name == "JumpMult Enabler")
                SetJumpMultCondition(true);
            if(collision.gameObject.name == "SpeedMult Enabler")
                SetSpeedMultCondition(true);
        }
            
            
    }

    private void ChangeGravity()
    {
        rb.gravityScale *= -1;
        spriteRenderer.flipY = !spriteRenderer.flipY;
    }

    IEnumerator GetDamage()
    {
        SFXChanger.Instance.PlayHurtSound();
        count++;
        if(count == 1)
        {
            Instantiate(_deathParticle, transform.position, Quaternion.identity);
            count--;
        }
        yield return new WaitForSeconds(0.01f);
        GameManager.Instance.SpawnPlayerInCheckpoint();
    }

    IEnumerator HideObject(GameObject go)
    {
        if(go.activeSelf == true)
        {
            yield return new WaitForSeconds(0.5f);
            go.SetActive(false);
        }
    }

    public void SetSKillCondition(bool b)
    {
        canUseSkills = b;
    }

    public void SetJumpMultCondition(bool b)
    {
        canUseJumpMult = b;
    }

    public void SetSpeedMultCondition(bool b)
    {
        canUseSpeedMult = b;
    }

    public void SetScaleMultCondition(bool b)
    {
        canUseScaleMult = b;
    }

    public void SetGravityCondition(bool b)
    {
        canUseGravity = b;
    }
}
