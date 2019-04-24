using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menuUI;

    private bool paused = false;

    void Start() {

        menuUI.SetActive(false);
    
    }

    void Update() {
        if (Input.GetButtonDown("Pause")) {
            paused = !paused;
        }

        if (paused) {
            menuUI.SetActive(true);
            Time.timeScale = 0;
        }

         if (!paused) {
            menuUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Resume () {

        paused = false;

    }

    public void Restart () {

        //Application.LoadLevel(Application.LoadedLevel);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);  

    }

    public void Quit () {

        Application.Quit();

    }

}
