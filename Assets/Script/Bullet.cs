using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int comboCount;
    GameObject Enemy;
    Animator animatorEnemy;
    public GameObject bullet;
    public Transform firePosition;
    public float fireSpeed;
    public bool canUppercut;
    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        transform.rotation = player.transform.rotation;
    }

    private void Update()
    {
        Destroy(this.gameObject, 10f);
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        Enemy = other.gameObject;
        animatorEnemy = Enemy.GetComponent<Animator>();
        //Enemy damge-push-animation trigger function LayerMask.LayerToName(other.gameObject.layer)
        if (
             (other.transform.tag == "Enemy" || LayerMask.LayerToName(other.gameObject.layer) == "Minions")
            && other.gameObject.TryGetComponent(out EnemyHealthSystem healthEnemy)
        )
        {
            if (other.transform.tag == "Boss" && animatorEnemy.GetBool("Idle"))
            {
                if (healthEnemy.health > 0)
                {
                    healthEnemy.health--;
                    animatorEnemy.SetTrigger("TakeDamage");
                }
            }
            if (other.transform.tag == "Enemy" && healthEnemy.health > 0)
            {
                healthEnemy.health--;
                healthEnemy.EnemyPush();
                animatorEnemy.SetTrigger("TakeDamage");
            }
        }
    }
}
