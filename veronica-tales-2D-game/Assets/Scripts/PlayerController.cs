using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private ControlPlayer controlPlayer;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;


    void Awake(){
        controlPlayer = new ControlPlayer();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
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
        }else{
            mySpriteRender.flipX = false;
        }   
    }

}
