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
    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animation = GetComponent<Animator>();
        animation.SetBool("Running",true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
           rb.velocity = new Vector3(0, jumpPower, 0);  
        }
       rb.velocity = new Vector2(1,rb.velocity.y) * runningSpeed;
    }
}
