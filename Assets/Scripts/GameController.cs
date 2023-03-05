using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;

    private PlayerController playerController;
    private Vector3 cameraPosition;
    private Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        cameraPosition = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Move camera with player
        playerPosition = player.transform.position;
        camera.transform.position = new Vector3(playerPosition.x + 7.33f,cameraPosition.y,cameraPosition.z);
    }
}
