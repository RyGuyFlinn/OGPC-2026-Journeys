using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;

    public NavMeshAgent agent;
    public Rigidbody2D rb2;
    public bool IsChasing = false;
    private float SightDistance = 15f;
    public LayerMask Obstacles;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        Vector2 directionToPlayer = (target.position - transform.position).normalized;
        float playerdistance = Vector2.Distance(target.position, transform.position);
        if (playerdistance <= SightDistance)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, SightDistance, Obstacles);

            if (hit.collider != null)
            {
                if (hit.collider.transform == target)
                {
                    IsChasing = true;
                    
                   // Debug.DrawRay(transform.position, directionToPlayer * playerdistance, Color.green);
                }
                else
                {
                    
                 //   Debug.DrawRay(transform.position, directionToPlayer * hit.distance, Color.red);
                }
            }
            
        }
        if (IsChasing) agent.SetDestination(target.position);
    }
}
