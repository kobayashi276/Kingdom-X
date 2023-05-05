using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;
    public TextMeshProUGUI energyCount;
    public GameObject terrain;
    // public GameObject terrainDestroyer;

    private PlayerController playerController;
    private Vector3 cameraPosition;
    private Vector3 playerPosition;
    private bool isInitEnabled;
    private bool isPreviousNullTerrian;
    // Start is called before the first frame update
    void Start()
    {
        isPreviousNullTerrian = false;
        playerController = player.GetComponent<PlayerController>();
        cameraPosition = camera.transform.position;
        isInitEnabled = true;
    }

    public void setEnergy(int ammount){
        int energy = int.Parse(energyCount.text.Substring(1));
        int newEnergy = energy + ammount;
        energyCount.text = "x" + newEnergy.ToString();
    }


    public int getEnergy(){
        return int.Parse(energyCount.text.Substring(1));

    }
 
    // Update is called once per frame
    void Update()
    {
        //Move camera with player
        playerPosition = player.transform.position;
        camera.transform.position = new Vector3(playerPosition.x + 7.33f,cameraPosition.y,cameraPosition.z);

        //Destroy terrian unuse
    //    terrainDestroyer.transform.position = new Vector3(playerPosition.x-0,0,0);
        GameObject[] terrains = GameObject.FindGameObjectsWithTag("Terrian");
        if (terrains.Length>20){
            Destroy(terrains[0].gameObject);
        }

        //Spawn terrian
        System.Random random = new System.Random();
        double randomNumber = random.NextDouble();

        if (Math.Round(playerPosition.x)%3==0 && isInitEnabled){
            Debug.Log("Chance: " + randomNumber.ToString());
            if (randomNumber<0.7 || isPreviousNullTerrian){
                GameObject instance = Instantiate(terrain);
                instance.transform.position = new Vector3((float)Math.Round(playerPosition.x) + 30,0,0);
                isPreviousNullTerrian = false;
            }   
            else{
                isPreviousNullTerrian = true;
            }
            isInitEnabled = false;
        }
        else if (Math.Round(playerPosition.x)%3!=0){
            isInitEnabled = true;
        }
    }
}
