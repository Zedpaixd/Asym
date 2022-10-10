using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MusicPlayerScript musicPlayerScript;

    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        musicPlayerScript.saveVolume();
    }

    public void QuitGame(){
        Application.Quit();
    }

}
