using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public GameObject Player;

    [SerializeField] private GameObject gameOverUI;

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
           gameOverUI.SetActive(true);
            gameHasEnded = true;

            Invoke("Restart", 5f);
        }
    }


    void Restart()
    {
        Destroy(Player);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
