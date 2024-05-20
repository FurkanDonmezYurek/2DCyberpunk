using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOver1 : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player"){
            player.GetComponent<Player>().currentHealth = 0;

        }
    }
}
