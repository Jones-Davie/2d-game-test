using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibles: MonoBehaviour
{

    public GameObject player;
    public PlayerControler playerControlerScript;
    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerControlerScript = player.GetComponent<PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void beenHit(bool hit)
    {
        Debug.Log("called beenHit");
        if (hit)
        {
            Debug.Log("i've been hit");
            Destroy(gameObject);
        }
    }
}
