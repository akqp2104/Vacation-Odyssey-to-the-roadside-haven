using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public void ExitGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(ExitToMainMenu());
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator ExitToMainMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
