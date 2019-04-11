using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bileScript : MonoBehaviour
{
    private float lifeTime;
     public GameObject player;
     public GameObject playerBody;
    public PlayerCombat playerCombatScript;
    private float damageValue = 10;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 1f;    
        player = GameObject.FindGameObjectWithTag("Player");
        playerCombatScript = player.GetComponent<PlayerCombat>();
        playerBody = GameObject.FindGameObjectWithTag("playerBody");
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0f){
            Destroy(gameObject);
        }
    }

        public void beenHit(bool hit)
    {
        if (hit)
        {
            Destroy(gameObject);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerBody) {

        Debug.Log("collission player");      
        playerCombatScript.playerHealth -= damageValue;
        Destroy(gameObject);

        }
        
    }
}



