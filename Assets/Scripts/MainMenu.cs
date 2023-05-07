using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    void Start(){

    }


    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
    public void QuitGame() {
        {
            Application.Quit();
        }
    }
    public void backToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void goToSetting()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Setting Menu");
    }

        public void goToMission()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
    }

        public void goToUpgrades()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
    }

    public void goToAbout()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Abouts Menu");
    }
}
