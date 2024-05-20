using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EnemyAgent : Agent
{
    public Transform playerTransform;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shootInterval = 1.0f;
    public float detectionRange = 5.0f;
    private float shootTimer;

    public override void Initialize()
    {
        shootTimer = shootInterval;
    }

    public override void OnEpisodeBegin()
    {
        // Enemy konumu rasgele belirliyor ama level�a g�re ayarlanmas� laz�m
        transform.localPosition = new Vector3(Random.Range(-22, 22), 0, 0);
         if (transform.localPosition.y < 0)
        {
             transform.localPosition = new Vector3(Random.Range(-22, 22), 0, 0);
        }
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Enemy ve Player aras�ndaki mesafeyi �l��yor
        Vector3 distanceToPlayer = playerTransform.localPosition - transform.localPosition;
        sensor.AddObservation(distanceToPlayer);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Hareket ve sald�r� i�lemleri
        float moveX = actions.ContinuousActions[0];

        Vector3 move = new Vector3(moveX,0, 0) * Time.deltaTime * 5.0f;
        transform.localPosition += move;

        // Oyuncuyu g�r�p g�rmedi�ini kontrol ediyor
        float distanceToPlayer = Vector3.Distance(transform.localPosition, playerTransform.localPosition);
        if (distanceToPlayer < detectionRange)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = shootInterval;
            }
        }

        
        if (distanceToPlayer < 1.5f)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        if (transform.localPosition.y < 0)
        {
            SetReward(-1f);
            EndEpisode();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector2 direction = (playerTransform.position - bulletSpawnPoint.position).normalized;
        bullet.transform.right = direction; 
        bullet.GetComponent<Rigidbody2D>().velocity = direction * 10.0f;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
    }
}
