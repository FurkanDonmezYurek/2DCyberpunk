using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trainer : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
    }

    void Update()
    {
        float distance = transform.position.x - target.transform.position.x;
        if (distance < 0){
            transform.position = Vector2.MoveTowards(transform.position,new Vector3(target.transform.position.x-2.5f,1.5f),3.5f*Time.deltaTime);
        }else
        {
            transform.position = Vector2.MoveTowards(transform.position,new Vector3(target.transform.position.x+2.5f,1.5f),2f*Time.deltaTime);

        }
        
    }
}
