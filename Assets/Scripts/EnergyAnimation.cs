using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyAnimation : MonoBehaviour
{
    private float up;
    private float down;
    private bool isUp;

    // Start is called before the first frame update
    void Start()
    {
        up = transform.position.y + 0.25f;
        down = transform.position.y - 0.25f;
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
            transform.position = new Vector3(transform.position.x,transform.position.y+0.001f,0);
        }
        else{
            transform.position = new Vector3(transform.position.x,transform.position.y-0.001f,0);
        }
    }
}
