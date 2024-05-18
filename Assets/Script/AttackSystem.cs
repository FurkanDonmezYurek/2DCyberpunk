using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    int comboCount;
    GameObject Player;
    GameObject Enemy;
    Animator animator;
    Animator animatorEnemy;
    Player playerSC;
    public GameObject bullet;
    public Transform firePosition;
    public float fireSpeed;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = Player.GetComponent<Animator>();
        playerSC = Player.GetComponent<Player>();
        firePosition = GameObject.Find("FirePosition").transform;

    }

    void Update()
    {
        //Attack and combo -----Input.GetMouseButtonDown(0) ||
        if (Input.GetMouseButtonDown(0) ||Input.GetKeyDown(KeyCode.K))
        {
            if (animator.GetBool("Jump") == false && playerSC.character ==1)
            {
                switch (comboCount)
                {
                    case 0:
                        animator.SetTrigger("Attack");
                        comboCount = 0;
                        break;
                    // case 1:
                    //     animator.SetTrigger("Attack1");
                    //     comboCount =0;
                    //     break;
                }
            }
            else if(playerSC.character == 2){
                animator.SetTrigger("Attack");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy = other.gameObject;
        animatorEnemy = Enemy.GetComponent<Animator>();
        //Enemy damge-push-animation trigger function
        if (
            (LayerMask.LayerToName(other.gameObject.layer) == "Enemy" || LayerMask.LayerToName(other.gameObject.layer) == "Minions")
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

    void Shoot(){
        
    }
}
