using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpPower;
    public float runningSpeed;
    public GameObject playerDeadAnimation;

    private Rigidbody2D rb;
    private Animator animation;
    // private Transform transform;
    private bool isJumping;
    private GameController gameController;

    private enum MovementState
    {
        idle, running, falling, jumping
    }

    private MovementState state = MovementState.idle;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameControllerObject = GameObject.FindGameObjectsWithTag("GameController");
        if (gameControllerObject.Length!=0)
            gameController = gameControllerObject[0].GetComponent<GameController>();


        MovementState state;
        rb =  GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animation = GetComponent<Animator>();
        state = MovementState.running;
        animation.SetInteger("state",(int)state);
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Falling to dead
        if (transform.position.y<-3.5f && (int)state!=0){
            transform.position = new Vector3(transform.position.x,transform.position.y-0.1f,transform.position.z);
        }
        if (transform.position.y<-7){
            Dead();
        }

        // Debug.Log(isJumping);
        if (Input.GetKey("space") && !isJumping)
        {
            Debug.Log("Jump");
            rb.velocity = new Vector3(0, jumpPower, 0); 
            isJumping = true; 
        }
        
        rb.velocity = new Vector2(1 * runningSpeed,rb.velocity.y);
        UpdateAnimation();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Energy")){
            other.gameObject.SetActive(false);
            gameController.setEnergy(1);
            Debug.Log("Eat energy");
            gameController.eatItemAnimation(other.gameObject.transform.position);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Gem")){
            other.gameObject.SetActive(false);
            gameController.setGem(1);
            Debug.Log("Eat gem");
            gameController.eatItemAnimation(other.gameObject.transform.position);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Terrian")){
            isJumping = false;
        }

        if (other.gameObject.CompareTag("Enemies")){
            if (gameController.getEnergy()<4){
                Dead();
            } 
            else{
                gameController.destroyEnemiesAnimation(other.transform.position);
                Destroy(other.gameObject);
                gameController.setEnergy(-4);
                gameController.setGem(2);
            } 
        }
    }

    private void Dead(){
        gameObject.SetActive(false);
        GameObject instance = Instantiate(playerDeadAnimation);
        instance.transform.position = transform.position;
        gameController.gameOver();
    }

    private void UpdateAnimation(){
        if (rb.velocity.x ==0){
            state = MovementState.idle;
        }
        else if (rb.velocity.y > .1f){
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
