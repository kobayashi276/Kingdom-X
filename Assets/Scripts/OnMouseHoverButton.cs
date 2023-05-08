using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OnMouseHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    public GameObject InfoUI;

    private string item1;
    private string item2;
    private string item3;
    private string item4;
    private string item5;
    // Start is called before the first frame update
    void Start()
    {
        InfoUI.SetActive(false);
        item1 = "Give you 10 energy per use";
        item2 = "Speed increase 50% in 10 seconds";
        item4 = "Make you invincible with enemies";
        item3 = "Jump power increase 50% in 10 seconds";
        item5 = "Give you one more life per game (automatic use when dying)";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnPointerEnter(PointerEventData eventData)
     {
         InfoUI.SetActive(true);
         ShowTip();

     }
     public void OnPointerExit(PointerEventData eventData)
     {
         InfoUI.SetActive(false);
     }

     private void ShowTip(){
        Debug.Log(gameObject.name);
        GameObject text = InfoUI.transform.GetChild(0).gameObject;

        switch (gameObject.name)
        {
            case "Button1":
                text.GetComponent<TextMeshProUGUI>().text = item1;
                break;
            case "Button2":
                text.GetComponent<TextMeshProUGUI>().text = item2;
                break;
            case "Button3":
                text.GetComponent<TextMeshProUGUI>().text = item3;
                break;
            case "Button4":
                text.GetComponent<TextMeshProUGUI>().text = item4;
                break;
            case "Button5":
                text.GetComponent<TextMeshProUGUI>().text = item5;
                break;
        }

     }
}
