using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public float runningSpeed;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-1,rb.velocity.y) * runningSpeed;
        if (transform.position.y < -10){
            Destroy(gameObject);
        }
    }
}
