using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSFX;
    private bool isLevelCompleted = false;
    void Start()
    {
        finishSFX = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !isLevelCompleted)
        {
            finishSFX.Play();
            isLevelCompleted = true;
            Invoke("CompleteLevel", 2f);
        }

    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
