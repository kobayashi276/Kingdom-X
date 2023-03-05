using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpPower;
    public float runningSpeed;

    private Rigidbody2D rb;
    private Animator animation;
    private Transform transform;
    private bool isJumping;

    private enum MovementState
    {
        idle, running, falling, jumping
    }

    private MovementState state = MovementState.idle;

    // Start is called before the first frame update
    void Start()
    {
        MovementState state;
        rb =  GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animation = GetComponent<Animator>();
        state = MovementState.running;
        animation.SetInteger("state",(int)state);
        isJumping = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space") && !isJumping)
        {
           rb.velocity = new Vector3(0, jumpPower, 0); 
           isJumping = true; 
        }
        rb.velocity = new Vector2(1,rb.velocity.y) * runningSpeed;
        UpdateAnimation();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Terrian")){
            isJumping = false;
        }
        if (other.gameObject.CompareTag("Enemies")){
            gameObject.SetActive(false);
        }
    }

    private void UpdateAnimation(){
        if (rb.velocity.y > .1f){
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f){
            state = MovementState.falling;
        }
        else{
            state = MovementState.running;
        }
        animation.SetInteger("state",(int)state);
        Debug.Log((int)state);
    }

}
