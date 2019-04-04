using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{

    public GameObject player;
    public PlayerCombat playerCombatScript;

    private float consumableValue = 50;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCombatScript = player.GetComponent<PlayerCombat>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

       private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject == player) {
      
        playerCombatScript.playerHealth += consumableValue;
        Destroy(gameObject);
        }
        
    }
}
