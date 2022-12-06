using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MusicPlayerScript musicPlayerScript;

    public void PlayGame(){
        /*SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);*/
        StaticLevelSelector.GoToLevelX(SceneManager.GetActiveScene().buildIndex + 1,this);
        musicPlayerScript.saveVolume();
    }

    public void mainMenu()
    {
        StaticLevelSelector.GoToLevelX(0, this);
    }

    public void QuitGame(){
        Application.Quit();
    }

}
