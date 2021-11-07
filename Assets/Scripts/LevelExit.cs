using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float waitingTime = 3f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ProcessNextLevel());
    }
    IEnumerator ProcessNextLevel()
    {
        yield return new WaitForSeconds(waitingTime);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex+1);
    }
}

