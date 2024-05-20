using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEditor;

public class AI : Agent
{
    public Transform target;
    // private Transform closestBlockTransform;
    // private Transform closestBulletTransform;
    private Rigidbody2D rb;
    public float speed;
    private float originalSpeed;
    // public float jumpForce = 5f;
    // public int hitCount;
    //  float T_hitDelay;
    // public float hitDelay;
    //  float T_regenDelay;
    // public float regenDelay;
    //  bool canHit = true;

    // public int health;
    //  bool jumping;
    // public bool takeDamage;


    Animator animator;
    public float range;
    public float attackRange;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = speed;
    }

    public void Update()
    {
        
        float distanceAbs = Mathf.Abs(transform.position.x - target.position.x);
        float distance = transform.position.x - target.position.x;
        if(distanceAbs > range)
        {
            speed = 0;
            animator.SetBool("CanRun",false);

        }
        else{

            if(distance > 0){
            transform.rotation = Quaternion.Euler(0, 180f,0);
            }
            else
            {transform.rotation = Quaternion.Euler(0, 0f,0);}

            
            if(distanceAbs > attackRange)
            {
            animator.ResetTrigger("Attack");
            speed = originalSpeed;
            animator.SetBool("CanRun",true);
            }
            else
            {
                speed = 0;
                animator.SetBool("CanRun",false);
                animator.SetTrigger("Attack");
            }
        }
        // if (!canHit)
        // {
        //     T_hitDelay += Time.deltaTime;
        //     if (T_hitDelay >= hitDelay)
        //     {
        //         T_hitDelay = 0f;
        //         canHit = true;
        //     }
        // }
        // FindClosestBlock();
        // FindClosestBullet();
        //  T_regenDelay += Time.deltaTime;
        //  if (T_regenDelay >= regenDelay && !canHit)
        //     {
        //         T_regenDelay = 0f;
        //         health ++;
        //     }
        // if(health > 5){
        //     health = 5;
        // }    

    }

    // void FindClosestBlock()
    // {
    //     GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Blocks");
    //     float closestDistance = Mathf.Infinity;
    //     Transform currentTransform = null;

    //     foreach (GameObject obj in gameObjects)
    //     {
    //         float distance = Vector3.Distance(transform.position, obj.transform.position);
    //         if (distance < closestDistance)
    //         {
    //             closestDistance = distance;
    //             currentTransform = obj.transform;
    //         }
    //     }

    //     closestBlockTransform = currentTransform;
    // }

    //  void FindClosestBullet()
    // {
    //     GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Bullet");
    //     float closestDistance = Mathf.Infinity;
    //     Transform currentTransform = null;

    //     foreach (GameObject obj in gameObjects)
    //     {
    //         float distance = Vector3.Distance(transform.position, obj.transform.position);
    //         if (distance < closestDistance)
    //         {
    //             closestDistance = distance;
    //             currentTransform = obj.transform;
    //         }
    //     }

    //     // closestBulletTransform = currentTransform;
    // }

    public override void OnEpisodeBegin()
    {
        // T_hitDelay = 0f;
        // hitDelay = 1f;
        // T_regenDelay = 0f;
        // regenDelay = 3f;
        // health = 5;
        // hitCount = 0;
        // canHit = true;
        // takeDamage = false;
        // transform.localPosition = new Vector3(Random.Range(-25 , 25), 1.6f, 0);
        // target.localPosition = new Vector3(Random.Range(-25 , 25), 1.6f, 0);
        // if (transform.localPosition.y < 0)
        // {
        //     rb.velocity = Vector3.zero;
        //     transform.localPosition = new Vector3(Random.Range(-25 , 25), 1.6f, 0);
        //     target.localPosition = new Vector3(Random.Range(-25 , 25), 1.6f, 0);
        // }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(transform.localPosition);
        // sensor.AddObservation(target.rotation);
        // sensor.AddObservation(closestBlockTransform);
        // sensor.AddObservation(closestBulletTransform);
        
        // sensor.AddObservation(health);
        // sensor.AddObservation(hitCount);
        // sensor.AddObservation(jumping);
        // sensor.AddObservation(takeDamage);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Hareket
        Vector3 velocity = Vector3.zero;
        velocity.x = actions.ContinuousActions[0];
        transform.position += velocity * speed * Time.deltaTime;

        // Zıplama
        // float jump = actions.ContinuousActions[1];
        // if (jump > 0.5f && !jumping)
        // {
        //     rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        //     jumping = true;
        // }
        // if (Mathf.Approximately(rb.velocity.y, 0f))
        // {
        //     jumping = false;
        // }

        // Ödül Sistemi
        float distance = Mathf.Abs(transform.position.x - target.position.x);

        // Belli bir mesafeye gelip vurursa ödül  && jumping == false && canHit
        // if (distance < 2.5f)
        // {
        //     // canHit = false;
        //     // hitCount++;
        //     // Debug.Log("Hit");
        //     SetReward(1f);
        //     EndEpisode();
        // }


        // Öldürürse - Ödül
        // if (hitCount >= 5)
        // {
        //     SetReward(3f);
        //     EndEpisode();
        // }

        // //Canı Azalırsa - Ceza
        // if(takeDamage)
        // {
        //     takeDamage = false;
        //     SetReward(-1f);
        // }


        // // Ölürse - Ceza
        // if (health <= 0)
        // {
        //     SetReward(-2f);
        //     EndEpisode();
        // }

        // Düşerse - Ceza
        // if (transform.localPosition.y < 0)
        // {
        //     SetReward(-1f);
        //     EndEpisode();
        // }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        // continuousActions[1] = Input.GetKey(KeyCode.Space) ? 1f : 0f;
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Unity.MLAgents;
// using Unity.MLAgents.Sensors;
// using Unity.MLAgents.Actuators;

// public class AI : Agent
// {

//     // GameObject[] blocks;
//     public Transform target;
//     public Transform startPos;
//     private Rigidbody2D rb;
//     public float speed = 5f;
//     public float jumpForce = 5f;
//     public int hitCount;
//     public float T_hitDelay;
//     public float hitDelay;
//     public bool canHit;

//     public int health;
//     public bool jumping;
//     void Start()
//     {
//         // blocks = GameObject.FindGameObjectsWithTag("Block");
//         startPos.localPosition = transform.localPosition;
//         rb = GetComponent<Rigidbody2D>();
//     }
//     public void Update(){
//         if (T_hitDelay < hitDelay)
//         {
//             T_hitDelay += Time.deltaTime;
//         }
//         if (canHit==false){
//         if (T_hitDelay >= hitDelay)
//         {
//             hitDelay = 0;
//             canHit = true;
               
//         }}
        
//     }
//     public override void OnEpisodeBegin()
//     {
//         T_hitDelay = hitDelay;
//         health = 5;
//         hitCount = 0;
//         hitDelay = 1;
//         if(transform.localPosition.y < 0){
//             rb.velocity = Vector3.zero;
//             transform.localPosition = startPos.localPosition;
//         }
//     }


//     public override void CollectObservations(VectorSensor sensor)
//     {
//         sensor.AddObservation(target.localPosition);
//         sensor.AddObservation(transform.localPosition);
//         sensor.AddObservation(health);
//         sensor.AddObservation(hitCount);
//         sensor.AddObservation(jumping);


//     }

//     public override void OnActionReceived(ActionBuffers actions)
//     {
//         //Hareket
//         Vector3  velocity = Vector3.zero;
//         velocity.x = actions.ContinuousActions[0];
//         transform.position += velocity * speed * Time.deltaTime;
//         //zıplama
//         float jump = actions.ContinuousActions[1];
//         if (jump > 0.5f && jumping == false)
//         {
//             rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
//             jumping = true;
//         }
//         if(Mathf.Approximately(rb.velocity.y, 0f))
//         {
//             jumping = false;
//         }

//         //Ödül Sistemi
//         float distance = Mathf.Abs(transform.position.x - target.position.x);

//         //Yakınlaşırsa - Ödül
//         if(distance < 3f){
//             SetReward(0.2f);
//         }
        
//         //belli bir mesafeye gelip vurursa ödül
//         if(distance < 1.5f)
//         {
//             if(canHit){
//                 canHit = false;
//                  hitCount ++;
//                 Debug.Log("Hit");
//                 SetReward(5f);
//             }
            
//         }
        
//         //Öldürürse - Ödül
//         if(hitCount >= 5){
//             SetReward(10f);
//             EndEpisode();
//         }

//         //Canı azalırsa - Ceza
//         if(health<3){
//             SetReward(-0.5f);
//         }

//         //Ölürse - Ceza
//         if(health <=0){
//             SetReward(-5f);
//             EndEpisode();
//         }
//         //Düşerse - Ceza
//         if(transform.localPosition.y <0){
//             SetReward(-1f);
//             EndEpisode();
//         }

//     }

//      public override void Heuristic(in ActionBuffers actionsOut)
//     {
//          var continuousActions = actionsOut.ContinuousActions;
//         continuousActions[0] = Input.GetAxis("Horizontal");
//         continuousActions[1] = Input.GetKey(KeyCode.Space) ? 1f : 0f;
//     }
// }
