using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MlBullet : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Target");
        transform.rotation = player.transform.rotation;
    }

    private void Update()
    {
        Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out AI aI))
        {
            // aI.takeDamage = true;
            Debug.Log("TakeDamage");
             Destroy(this.gameObject);
        }
           
            
    }
}

