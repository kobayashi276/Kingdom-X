using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDestroyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Destroy terrain");
        if (other.gameObject.CompareTag("Terrian")){
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Destroy terrain");
        if (other.gameObject.CompareTag("Terrian")){
            Destroy(other.gameObject);
        }
    }
}
