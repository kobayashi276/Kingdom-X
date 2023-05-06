using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolumne(float volumne)
    {
        audioMixer.SetFloat("volumne", volumne);
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mission");
    }

        public void goToUpgrades()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Upgrades");
    }

    public void goToAbout()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Abouts Menu");
    }
    public void loadLevel_1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Name_of_SceneLevel");
    }
        public void loadLevel_2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Name_of_SceneLevel");
    }
        public void loadLevel_3()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Name_of_SceneLevel");
    }
}
