using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealthBar : MonoBehaviour
{
    private GameObject player;
    private PlayerCombat playerCombat;

    private float playerHealth = 1;
    private float playerOldHealth;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("healthBar init");
        player = GameObject.FindGameObjectWithTag("Player");
        playerCombat = player.GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        playerOldHealth = playerHealth;
        playerHealth = playerCombat.playerHealth;

        if (playerHealth != playerOldHealth) 
        {
        Debug.Log("health updated");
        transform.localScale = new Vector3 ((playerHealth/100), 1, 1);
        }
    }
}
