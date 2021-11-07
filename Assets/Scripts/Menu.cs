using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //this is to check if my plastic is still working 
    public void StartFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LOadMainMenu()
{
        StartCoroutine(LevelWait());
    SceneManager.LoadScene(0);
}
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator LevelWait()
    {
        yield return new WaitForSeconds(2f);
    }
}
