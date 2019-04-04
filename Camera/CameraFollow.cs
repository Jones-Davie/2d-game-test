using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector2 cameraspeed;

    public float easeY = 50;
    public float easeX = 50;

    private GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref cameraspeed.x, easeX);   
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref cameraspeed.y, easeY);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
