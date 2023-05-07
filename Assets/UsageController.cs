using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class UsageController : MonoBehaviour
{
    private TextMeshProUGUI item1Text;
    private TextMeshProUGUI item2Text;
    private TextMeshProUGUI item3Text;
    private TextMeshProUGUI item4Text;
    private TextMeshProUGUI item5Text;
    private PlayerController player;

    private int currentGem;
    private int item1;
    private int item2;
    private int item3;
    private int item4;
    private int item5;
    
    private bool isBoosted;
    private bool isJumpBoosted;
    private bool isInvin;
    private float timeSpeedBoosted;
    private float timeJumpBoosted;
    private float timeInvin;

    private AudioSource sfx_boost;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Item1");
        item1Text = temp[0].GetComponent<TextMeshProUGUI>();
        temp = GameObject.FindGameObjectsWithTag("Item2");
        item2Text = temp[0].GetComponent<TextMeshProUGUI>();
        temp = GameObject.FindGameObjectsWithTag("Item3");
        item3Text = temp[0].GetComponent<TextMeshProUGUI>();
        temp = GameObject.FindGameObjectsWithTag("Item4");
        item4Text = temp[0].GetComponent<TextMeshProUGUI>();
        temp = GameObject.FindGameObjectsWithTag("Item5");
        item5Text = temp[0].GetComponent<TextMeshProUGUI>();

        temp = GameObject.FindGameObjectsWithTag("Player");
        player = temp[0].GetComponent<PlayerController>();
        isBoosted = false;
        LoadGame();

        sfx_boost = GameObject.Find("Boost").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1")){
            Item1();
        }
        else if (Input.GetKey("2")){
            Item2();
        }
        else if (Input.GetKey("3")){
            Item3();
        }
        else if (Input.GetKey("4")){
            Item4();
        }

        if (player.Respawn(item5)){
            item5-=1;
            setAmmount(5,item5);
        }


        if (isBoosted && Time.time - timeSpeedBoosted > 10){
            isBoosted = false;
            player.resetBoostSpeed();
        }

        if (isJumpBoosted && Time.time - timeJumpBoosted > 10){
            isJumpBoosted = false;
            player.resetJumpBoost();
        }

        if (isInvin && Time.time - timeInvin > 5){
            isInvin = false;
            player.removeInvin();
        }
    }

    private void SaveGame(){
        //Gem, Item1, Item2, Item3, Item4, Item,5
        string saveString = "";
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Gem_Count");
        int gem = int.Parse(temp[0].GetComponent<TextMeshProUGUI>().text);
        saveString += gem.ToString() + "," + item1.ToString() + "," + item2.ToString() + "," + item3.ToString() + "," + item4.ToString() + "," + item5.ToString() + ",";
        File.WriteAllText(Application.dataPath + "/data.save", saveString);
        Debug.Log(saveString);
    }

    private void LoadGame(){
        string saveString = "";
         if (File.Exists(Application.dataPath + "/data.save")){
            saveString = File.ReadAllText(Application.dataPath + "/data.save");
         }
         else{
            saveString = "0,0,0,0,0,0,";
         }
         ReadData(saveString);
    }

    private void ReadData(string saveString){
        string[] saveSplit = saveString.Split(",");
        Debug.Log(saveSplit.Length);
        // GameObject[] temp = GameObject.FindGameObjectsWithTag("Gem_Count");
        // temp[0].GetComponent<TextMeshProUGUI>().text = saveSplit[0].ToString();
        item1Text.text = saveSplit[1].ToString();
        item2Text.text = saveSplit[2].ToString();
        item3Text.text = saveSplit[3].ToString();
        item4Text.text = saveSplit[4].ToString();
        item5Text.text = saveSplit[5].ToString();

        // currentGem = int.Parse(temp[0].GetComponent<TextMeshProUGUI>().text);
        item1 = int.Parse(item1Text.text);
        item2 = int.Parse(item2Text.text);
        item3 = int.Parse(item3Text.text);
        item4 = int.Parse(item4Text.text);
        item5 = int.Parse(item5Text.text);
    }


    private void setAmmount(int index, int ammount){
        switch (index)
        {
            case 1:
                item1Text.text = ammount.ToString();
                break;
            case 2:
                item2Text.text = ammount.ToString();
                break;
            case 3:
                item3Text.text = ammount.ToString();
                break;
            case 4:
                item4Text.text = ammount.ToString();
                break;
            case 5:
                item5Text.text = ammount.ToString();
                break;
        }
    }

    public void Item1(){
        if (item1>0){
            player.setEnergy(10);
            item1-=1;
            setAmmount(1,item1);
            sfx_boost.Play(0);
        }
    }

    public void Item2(){
        if (item2>0 && !isBoosted){
            player.setBoostSpeed(50);
            item2-=1;
            setAmmount(2,item2);
            isBoosted = true;
            timeSpeedBoosted = Time.time;
            sfx_boost.Play(0);
        }
    }

    public void Item3(){
        if (item3>0 && !isJumpBoosted){
            player.setJumpBoost(50);
            item3-=1;
            setAmmount(3,item3);
            isJumpBoosted = true;
            timeJumpBoosted = Time.time;
            sfx_boost.Play(0);
        }
    }

    public void Item4(){
        if (item4>0 && !isInvin){
            player.Invin();
            item4-=1;
            setAmmount(4,item4);
            isInvin = true;
            timeInvin = Time.time;
            sfx_boost.Play(0);
        }
    }
}
