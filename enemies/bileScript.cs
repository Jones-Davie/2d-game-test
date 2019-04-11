using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bileScript : MonoBehaviour
{
    private float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 1f;    
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
}
