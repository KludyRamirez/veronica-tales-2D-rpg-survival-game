using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; }}
    public static PlayerController Instance;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float dashSpeed = 4f;
    // [SerializeField] private TrailRenderer myTrailRenderer;

    private ControlPlayer controlPlayer;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    private bool facingLeft = false;
    private bool isDashing = false;



    private void Awake(){
        Instance = this;
        controlPlayer = new ControlPlayer();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    private void Start(){
        controlPlayer.Combat.Dash.performed += _ => Dash();
    }

    private void OnEnable(){
        controlPlayer.Enable();
    }


    private void Update(){
        PlayerInput();
    }

    private void FixedUpdate(){
        AdjustplayerFacingDirection();
        Move();
    }

    private void PlayerInput(){
        movement = controlPlayer.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move(){
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustplayerFacingDirection(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x){
            mySpriteRender.flipX = true;
            FacingLeft = true;
        }else{
            mySpriteRender.flipX = false;
            FacingLeft = false;
        }   
    }

    private void Dash(){
        if (!isDashing){
        isDashing = true;
        moveSpeed *= dashSpeed;
        // myTrailRenderer.emitting = true;
        StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine(){
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed /= dashSpeed;
        // myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
