using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticLevelSelector
{
    private static bool executed = false;
    public static void GoToLevelX(string level, MonoBehaviour instance)
    {
        if (executed == false)
        {
            executed = true;
            instance.StartCoroutine(loadLevelAsync(level));
        }
    }
    public static void GoToLevelX(int level, MonoBehaviour instance)
    {
        if (executed == false)
        {
            executed = true;
            instance.StartCoroutine(loadLevelAsync(level));
        }
    }
    static IEnumerator loadLevelAsync(int index)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(index);
        //disable auto activation of new scene
        asyncOp.allowSceneActivation = false;

        //check if done
        while (!asyncOp.isDone)
        {
            //check progress
            if (asyncOp.progress >= 0.9f)
            {
                asyncOp.allowSceneActivation = true;
                executed = false;
                //avoid infinite loop
                yield return null;
            }
            //avoid infinite loop
            yield return null;
        }
    }

    static IEnumerator loadLevelAsync(string sceneName)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;
        while (!asyncOp.isDone)
        {
            if (asyncOp.progress >= 0.9f)
            {
                asyncOp.allowSceneActivation = true;
                executed = false;
                yield return null;
            }
            yield return null;
        }
    }

}

