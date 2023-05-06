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
    public TextMeshProUGUI gemCount;
    public GameObject terrain;
    public GameObject energy;
    public GameObject gem;
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

    public void setGem(int ammount){
        int gem = int.Parse(gemCount.text);
        int newGem = gem + ammount;
        gemCount.text = newGem.ToString();
    }


    public int getGem(){
        return int.Parse(gemCount.text.Substring(1));

    }
 
    // Update is called once per frame
    void Update()
    {
        //Move camera with player
        playerPosition = player.transform.position;
        camera.transform.position = new Vector3(playerPosition.x + 7.33f,cameraPosition.y,cameraPosition.z);

        //Destroy terrian unuse
        GameObject[] terrains = GameObject.FindGameObjectsWithTag("Terrian");
        if (terrains.Length>30){
            Destroy(terrains[0].gameObject);
        }

        //Destroy energy unuse
        GameObject[] energies = GameObject.FindGameObjectsWithTag("Energy");
        if (energies.Length>50){
            Destroy(energies[0].gameObject);
        }

        //Spawn terrian
        System.Random random = new System.Random();
        double randomNumber = random.NextDouble();

        if (Math.Round(playerPosition.x)%3==0 && isInitEnabled){
            if (randomNumber<0.8 || isPreviousNullTerrian){
                GameObject instance = Instantiate(terrain);
                instance.transform.position = new Vector3((float)Math.Round(playerPosition.x) + 30,0,0);
                isPreviousNullTerrian = false;
                 if (!spawnEnergy()){
                    spawnGem();
                 }
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
            Debug.Log("Spawn Enemy");
            GameObject instance = Instantiate(enemies);
            instance.transform.position = new Vector3((float)Math.Round(playerPosition.x) + 24,0,0);
            isInitEnemiesEnabled = false;
        }
        else if(Math.Round(playerPosition.x)%24!=0){
            isInitEnemiesEnabled = true;
        }

    }

    //Spawn energy
    private bool spawnEnergy(){
        System.Random random = new System.Random();
        double randomNumber = random.NextDouble();
        if (randomNumber<0.3){
            GameObject instance = Instantiate(energy);
            random = new System.Random();
            float position = (float)random.NextDouble() * 3;
            instance.transform.position = new Vector3((float)Math.Round(playerPosition.x) + 30,position-3,0);
            return true;
        }
        return false;
        
    }

    //Spawn gem
    private bool spawnGem(){
        System.Random random = new System.Random();
        double randomNumber = random.NextDouble();
        if (randomNumber<0.4){
            GameObject instance = Instantiate(gem);
            random = new System.Random();
            float position = (float)random.NextDouble() * 3;
            instance.transform.position = new Vector3((float)Math.Round(playerPosition.x) + 30,position-3,0);
            return true;
        }
        return false;
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
