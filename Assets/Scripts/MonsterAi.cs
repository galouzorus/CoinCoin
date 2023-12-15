using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAi : MonoBehaviour
{   //range detection
    [Range(0.5f, 50)]
    public float detectDistance = 3;

    public Transform[] points;
    NavMeshAgent agent;
    int destinationIndex = 0;
    Transform player;
    float runSpeed = 4f;

    [Header("Animation")]
    public Animator playerAnim;


    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.destination = points[destinationIndex].transform.position;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Walk();
        SearchPlayer();
    }
    
    public void SearchPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectDistance) 
        {//detected
         agent.destination = player.position;
            agent.speed = runSpeed;
         playerAnim.SetBool("Walk",false);
        }
        else 
        {
            agent.destination = points[destinationIndex].position;
            agent.speed = 1.5f;
        }
    }
    public void Walk()
    {
        playerAnim.SetBool("Walk", true);
        //change destination
        float distance = agent.remainingDistance;
        if (distance <= 0.05f)
        {
            destinationIndex++;
            
            if (destinationIndex >= points.Length)
                destinationIndex = 0;
            agent.destination = points[destinationIndex].position;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }
}
