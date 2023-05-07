using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class ShopController : MonoBehaviour
{
    public GameObject alert;
    public TextMeshProUGUI item1Text;
    public TextMeshProUGUI item2Text;
    public TextMeshProUGUI item3Text;
    public TextMeshProUGUI item4Text;
    public TextMeshProUGUI item5Text;

    private AudioSource sfx_buy;
    private AudioSource sfx_cantbuy;
    private int currentGem;
    private int item1;
    private int item2;
    private int item3;
    private int item4;
    private int item5;
    // Start is called before the first frame update
    void Start(){
        LoadGame();
        alert.SetActive(false);

        sfx_buy = GameObject.Find("SFX_Buy").GetComponent<AudioSource>();
        sfx_cantbuy = GameObject.Find("SFX_Can't Buy").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Gem_Count");
        temp[0].GetComponent<TextMeshProUGUI>().text = saveSplit[0].ToString();
        item1Text.text = saveSplit[1].ToString();
        item2Text.text = saveSplit[2].ToString();
        item3Text.text = saveSplit[3].ToString();
        item4Text.text = saveSplit[4].ToString();
        item5Text.text = saveSplit[5].ToString();

        currentGem = int.Parse(temp[0].GetComponent<TextMeshProUGUI>().text);
        item1 = int.Parse(item1Text.text);
        item2 = int.Parse(item2Text.text);
        item3 = int.Parse(item3Text.text);
        item4 = int.Parse(item4Text.text);
        item5 = int.Parse(item5Text.text);
    }

    private void Alert(){
        alert.SetActive(true);
        // yield return new WaitForSeconds(3);
        // alert.SetActive(false);
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

    public void backToMenu(){
        SaveGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    private void updateGem(){
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Gem_Count");
        temp[0].GetComponent<TextMeshProUGUI>().text = currentGem.ToString();
    }
    
    public void Item1(){
        if (currentGem<100){
            Debug.Log("Cant buy");
            sfx_cantbuy.Play(0);
        }
        else{
            currentGem-=100;
            item1+=1;
            setAmmount(1,item1);
            updateGem();
            sfx_buy.Play(0);
        }
    }


    public void Item2(){
        if (currentGem<100){
            Debug.Log("Cant buy");
            sfx_cantbuy.Play(0);
        }
        else{
            currentGem-=100;
            item2+=1;
            setAmmount(2,item2);
            updateGem();
            sfx_buy.Play(0);
        }
        
    }

    public void Item3(){
        if (currentGem<100){
            Debug.Log("Cant buy");
            sfx_cantbuy.Play(0);
        }
        else{
            currentGem-=100;
            item3+=1;
            setAmmount(3,item3);
            updateGem();
            sfx_buy.Play(0);
        }
    }

    public void Item4(){
        if (currentGem<250){
            Debug.Log("Cant buy");
            sfx_cantbuy.Play(0);
        }
        else{
            currentGem-=250;
            item4+=1;
            setAmmount(4,item4);
            updateGem();
            sfx_buy.Play(0);
        }
    }

    public void Item5(){
        if (currentGem<500){
            Debug.Log("Cant buy");
            sfx_cantbuy.Play(0);
        }
        else{
            currentGem-=500;
            item5+=1;
            setAmmount(5,item5);
            updateGem();
            sfx_buy.Play(0);
        }
    }
}
