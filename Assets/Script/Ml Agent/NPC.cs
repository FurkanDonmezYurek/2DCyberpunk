using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePosition;
    public float fireSpeed;

    float T_shootDelay;
    public float shootDelay;

    float T_rotatetDelay;
    public float rotateDelay;
    void Start()
    {
         T_shootDelay = shootDelay;
         T_rotatetDelay = rotateDelay;
    }

    // Update is called once per frame
    void Update()
    {
         if (T_shootDelay < shootDelay)
        {
            T_shootDelay += Time.deltaTime;
        }

         if (T_shootDelay >= shootDelay)
        {
            T_shootDelay = 0;
            Shoot();
        }


        if(T_rotatetDelay < rotateDelay){
            T_rotatetDelay += Time.deltaTime;
        }

        if(T_rotatetDelay >= rotateDelay){
            T_rotatetDelay = 0;
            transform.Rotate(0f,180f,0f);
        }
    }

    void Shoot(){
        GameObject bulletClone = Instantiate(
                                bullet,
                                firePosition.position,
                                Quaternion.identity
                            );
        bulletClone.GetComponent<Rigidbody2D>().velocity = firePosition.right * fireSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.TryGetComponent(out AI aI))
        {
            // aI.takeDamage = true;
            Debug.Log("MeleeTakeDamage");}
    }
}
