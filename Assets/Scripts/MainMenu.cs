using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    private AudioSource sfx_click;
    void Start(){
        try
        {
            sfx_click = GameObject.Find("Click").GetComponent<AudioSource>();     
        }
        catch (System.Exception)
        {
            
        }

    }

    public void PlayGame()
    {
        sfx_click.Play(0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
    public void QuitGame() {
        {
            sfx_click.Play(0);
            Application.Quit();
        }
    }
    public void backToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void goToSetting()
    {
        sfx_click.Play(0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Setting Menu");
    }

        public void goToMission()
    {
        sfx_click.Play(0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
    }

        public void goToUpgrades()
    {
        sfx_click.Play(0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Upgrade");
    }

    public void goToAbout()
    {
        sfx_click.Play(0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Abouts Menu");
    }
}
