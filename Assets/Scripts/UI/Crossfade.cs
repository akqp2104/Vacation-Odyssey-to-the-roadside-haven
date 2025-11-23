using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crossfade : MonoBehaviour
{
    private Animator crossfade;
    private float crossfadeDuration = 1.3f;
    void Awake()
    {
        crossfade = gameObject.GetComponent<Animator>();
    }

    private IEnumerator NextLevel()
    {
        crossfade.SetTrigger("Start");
        yield return new WaitForSeconds(crossfadeDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(NextLevel());
    }
}
