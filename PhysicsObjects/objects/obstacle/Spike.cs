using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameObject player;
    public PlayerCombat playerCombatScript;

    private float damageValue = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCombatScript = player.GetComponent<PlayerCombat>();
        Debug.Log("started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

       private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered");
        if (collision.gameObject == player) {

        Debug.Log("collission player");      
        playerCombatScript.playerHealth -= damageValue;

        }
        
    }
}
