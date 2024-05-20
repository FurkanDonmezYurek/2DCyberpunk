using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Player playerController;
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController.currentHealth == 5)
            {
                return;
            }
            else
            { 
                playerController.currentHealth += 1;
                Destroy(gameObject);
            }
        }
    }
}
