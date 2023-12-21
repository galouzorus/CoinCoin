
using UnityEngine;
using UnityEngine.AI;

public class FinalBehaviour : MonoBehaviour
{
    public Transform[] points;
    NavMeshAgent agent;
    int destinationIndex = 0;
    public Transform player;

    [Header("Animation")]
    public Animator playerAnim;


    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null && player != null)
        {
            agent.destination = points[destinationIndex].transform.position;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Walk();
    }
    public void Walk()
    {
        playerAnim.SetBool("Run", true);
        //change destination
        float distance = agent.remainingDistance;
        if (distance <= 0.05f)
        {
            destinationIndex++;

            if (destinationIndex >= points.Length)
                destinationIndex = 2;
            agent.destination = points[destinationIndex].position;
            playerAnim.SetBool("Run", false);
            playerAnim.SetBool("Idle", true);
        }
    }
}