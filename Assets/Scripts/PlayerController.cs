using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpPower;
    public float runningSpeed;
    public GameObject playerDeadAnimation;
    public GameObject speedBoostedAnimation;
    public GameObject jumpBoostedAnimation;
    public GameObject respawnAnimation;

    private Rigidbody2D rb;
    private Animator animation;
    // private Transform transform;
    private bool isJumping;
    private bool isInvin;
    private bool isDead;
    private GameController gameController;
    private float boostSpeed;
    private float jumpBoost;

    private AudioSource sfx_jump;
    private AudioSource sfx_pickupenergy;
    private AudioSource sfx_pickupgem;
    private AudioSource sfx_hit;
    private AudioSource sfx_respawn;



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
        boostSpeed = 1;
        jumpBoost = 1;
        isInvin = false;
        speedBoostedAnimation.SetActive(false);
        jumpBoostedAnimation.SetActive(false);


        sfx_jump = GameObject.Find("Jump").GetComponent<AudioSource>();
        sfx_pickupenergy = GameObject.Find("Pickup_Energy").GetComponent<AudioSource>();
        sfx_pickupgem = GameObject.Find("Pickup_Gem").GetComponent<AudioSource>();
        sfx_hit = GameObject.Find("Hit").GetComponent<AudioSource>();
        sfx_respawn = GameObject.Find("Respawn").GetComponent<AudioSource>();
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
            rb.velocity = new Vector3(0, jumpPower*jumpBoost, 0); 
            isJumping = true;
            sfx_jump.Play(0);
        }

        //Show speed boosted animation
        if (boostSpeed!=1){
            speedBoostedAnimation.SetActive(true);
        }
        else{
            speedBoostedAnimation.SetActive(false);
        }

        if (jumpBoost!=1){
            jumpBoostedAnimation.SetActive(true);
        }
        else{
            jumpBoostedAnimation.SetActive(false);
        }
        
        speedBoostedAnimation.transform.position = new Vector3(transform.position.x-0.87f,transform.position.y-0.74f,0);
        jumpBoostedAnimation.transform.position = new Vector3(transform.position.x,transform.position.y-0.69f,0);
        rb.velocity = new Vector2(1 * runningSpeed * boostSpeed,rb.velocity.y);
        UpdateAnimation();
    }

    public bool Respawn(int item5){
        if (!isDead) return false;
        if (item5==0){
            gameController.gameOver();
            return false;
        } 
        isDead = false;
        float min = 99999999;
        GameObject nearest = null;
        Vector3 playerPos = transform.position;
        Debug.Log(playerPos);
        GameObject[] all = GameObject.FindGameObjectsWithTag("Terrian");
        foreach (var item in all)
        {
            float distance = Vector3.Distance(playerPos,item.transform.position);
            if (distance<min && playerPos.x<item.transform.position.x){
                min = distance;
                nearest = item;
            }
        }
        transform.position = new Vector3(nearest.transform.position.x-4.61f,nearest.transform.position.y-2.35f,0);
        gameObject.SetActive(true);
        GameObject instance = Instantiate(respawnAnimation);
        instance.transform.position = new Vector3(transform.position.x,transform.position.y-1.2f,0);
        resetBoostSpeed();
        resetJumpBoost();
        removeInvin();
        sfx_respawn.Play();
        return true;
    }

    public void setBoostSpeed(int boostSpeed){
        this.boostSpeed = this.boostSpeed + (float)((this.boostSpeed*boostSpeed)/100f);
    }

    public void resetBoostSpeed(){
        this.boostSpeed = 1;
    }

    public void setJumpBoost(int jumpBoost){
        this.jumpBoost = this.jumpBoost + (float)((this.jumpBoost*jumpBoost)/100f);
    }

    public void resetJumpBoost(){
        this.jumpBoost = 1;
    }

    public void Invin(){
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.5f;
        isInvin = true;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }

    public void removeInvin(){
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        isInvin = false;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Energy")){
            other.gameObject.SetActive(false);
            setEnergy(1);
            Debug.Log("Eat energy");
            gameController.eatItemAnimation(other.gameObject.transform.position);
            Destroy(other.gameObject);
            sfx_pickupenergy.Play(0);
        }
        if (other.gameObject.CompareTag("Gem")){
            other.gameObject.SetActive(false);
            gameController.setGem(1);
            Debug.Log("Eat gem");
            gameController.eatItemAnimation(other.gameObject.transform.position);
            Destroy(other.gameObject);
            sfx_pickupgem.Play(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Terrian")){
            isJumping = false;
        }

        if (other.gameObject.CompareTag("Enemies")){
            if (gameController.getEnergy()<4 && !isInvin){
                Dead();
            } 
            else{
                gameController.destroyEnemiesAnimation(other.transform.position);
                if (!isInvin) gameController.setEnergy(-4);
                gameController.setGem(2);
                sfx_hit.Play(0);
            } 
            Destroy(other.gameObject);

        }
    }

    private void Dead(){
        gameObject.SetActive(false);
        isDead = true;
        GameObject instance = Instantiate(playerDeadAnimation);
        instance.transform.position = transform.position;
        // gameController.gameOver();
    }

    public void GameOver(){
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

    public void setEnergy(int ammount){
        gameController.setEnergy(ammount);
    }
}
