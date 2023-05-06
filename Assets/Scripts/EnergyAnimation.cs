using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyAnimation : MonoBehaviour
{
    public float range;
    public float speed;

    private float up;
    private float down;
    private bool isUp;

    // Start is called before the first frame update
    void Start()
    {
        up = transform.position.y + range;
        down = transform.position.y - range;
        isUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > up){
            isUp = false;
        }
        else if (transform.position.y < down){
            isUp = true;
        }

        if (isUp){
            transform.position = new Vector3(transform.position.x,transform.position.y+speed,0);
        }
        else{
            transform.position = new Vector3(transform.position.x,transform.position.y-speed,0);
        }
    }
}
