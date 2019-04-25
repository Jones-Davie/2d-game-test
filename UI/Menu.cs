using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject player;
    public GameObject demon;

    public Demon demonControler;
    public PlayerCombat playerCombat;
    public PlayerControler playerControler;
    

    private bool paused = false;

    void Start() {

        menuUI.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        playerControler = player.GetComponent<PlayerControler>();
        playerCombat = player.GetComponent<PlayerCombat>();
        demonControler = demon.GetComponent<Demon>();
    }

    void Update() {
        if (Input.GetButtonDown("Pause")) {
            paused = !paused;
        }

        if (paused) {
            menuUI.SetActive(true);
            Time.timeScale = 0;
            playerControler.enableInput = false;
        }

         if (!paused && !playerCombat.playerDead && !demonControler.gameOver ) {
            menuUI.SetActive(false);
            Time.timeScale = 1;
            playerControler.enableInput = true;
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
