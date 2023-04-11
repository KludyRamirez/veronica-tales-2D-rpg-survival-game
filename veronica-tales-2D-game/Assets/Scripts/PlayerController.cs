using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private ControlPlayer controlPlayer;
    private Vector2 movement;
    private Rigidbody2D rb;


    private void Awake(){
        controlPlayer = new ControlPlayer();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable(){
        controlPlayer.Enable();
    }


    private void Update(){
        PlayerInput();
    }

    private void FixedUpdate(){
       Move();
    }

    private void PlayerInput(){
        movement = controlPlayer.Movement.Move.ReadValue<Vector2>();
        Debug.Log(movement.x);
    }

    private void Move(){
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

}
