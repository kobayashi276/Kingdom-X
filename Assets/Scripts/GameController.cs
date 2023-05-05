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
    public GameObject energy;
    public GameObject background;
    public GameObject eatItem;
    public GameObject destroyEnemies;
    public GameObject enemies;
    // public GameObject terrainDestroyer;

    private PlayerController playerController;
    private Vector3 cameraPosition;
    private Vector3 playerPosition;
    private bool isInitEnabled;
    private bool isPreviousNullTerrian;
    private bool isInitBackgroundEnabled;
    private bool isInitEnemiesEnabled;
    // Start is called before the first frame update
    void Start()
    {
        isPreviousNullTerrian = false;
        isInitBackgroundEnabled = false;
        isInitEnemiesEnabled = false;
        playerController = player.GetComponent<PlayerController>();
        cameraPosition = camera.transform.position;
        isInitEnabled = true;
    }

    public void setEnergy(int ammount){
        int energy = int.Parse(energyCount.text.Substring(1));
        int newEnergy = energy + ammount;
        energyCount.text = "x " + newEnergy.ToString();
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
                spawnEnergy();
            }   
            else{
                isPreviousNullTerrian = true;
            }
            isInitEnabled = false;
        }
        else if (Math.Round(playerPosition.x)%3!=0){
            isInitEnabled = true;
        }

        //Spawn background
        random = new System.Random();
        randomNumber = random.NextDouble();

        if (Math.Round(playerPosition.x)%24==0 && isInitBackgroundEnabled){
            GameObject instance = Instantiate(background);
            instance.transform.position = new Vector3((float)Math.Round(playerPosition.x) + 48,0,0);
            isInitBackgroundEnabled = false;
        }
        else if (Math.Round(playerPosition.x)%24!=0){
            isInitBackgroundEnabled = true;
        }

        //Spawn enemies
        random = new System.Random();
        randomNumber = random.NextDouble();

        if (Math.Round(playerPosition.x)%24==0 && isInitEnemiesEnabled){
            GameObject instance = Instantiate(enemies);
            instance.transform.position = new Vector3((float)Math.Round(playerPosition.x) + 24,0,0);
            isInitEnemiesEnabled = false;
        }
        else if(Math.Round(playerPosition.x)%24!=0){
            isInitEnemiesEnabled = true;
        }

    }

    //Spawn energy
    private void spawnEnergy(){
        System.Random random = new System.Random();
        double randomNumber = random.NextDouble();
        if (randomNumber<0.4){
            GameObject instance = Instantiate(energy);
            random = new System.Random();
            float position = (float)random.NextDouble() * 3;
            instance.transform.position = new Vector3((float)Math.Round(playerPosition.x) + 30,position-3,0);
        }
        
    }

    public void eatItemAnimation(Vector3 position){
        GameObject instance = Instantiate(eatItem);
        instance.transform.position = position;
    }

    public void destroyEnemiesAnimation(Vector3 position){
        GameObject instance = Instantiate(destroyEnemies);
        instance.transform.position = position;
    }

}
