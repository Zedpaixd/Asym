using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InBoundsChecker : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;
    private bool moved = false;

    // Update is called once per frame
    void Update()
    {
        if (cube1.GetComponent<PlayerMovement>().direction != 0 && cube2.GetComponent<PlayerMovement>().direction != 0)
            moved = true;

        if (!cube1.GetComponent<Renderer>().isVisible && !cube2.GetComponent<Renderer>().isVisible && moved)
        {
            SongTime.updateProgress();
            StaticLevelSelector.GoToLevelX(SceneManager.GetActiveScene().buildIndex, this);
        }
        else if ((!cube1.GetComponent<Renderer>().isVisible || !cube2.GetComponent<Renderer>().isVisible) && moved)
        {
            SongTime.updateProgress();
            StaticLevelSelector.GoToLevelX(SceneManager.GetActiveScene().buildIndex, this);
        }
    }
}
