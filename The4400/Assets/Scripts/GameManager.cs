using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isPaused;
    public GameObject player;

    public void quitGame() {
        Application.Quit();
    }

    public void newGame()
    {
        SceneManager.LoadScene(1);
    }

    void Start()
    {
        isPaused = false;
    }
    void Update()
    {
        if (Input.touchCount > 3 && SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene(0);
    }
}
