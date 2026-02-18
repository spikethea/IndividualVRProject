using UnityEngine;
using UnityEngine.AI;

public class Commuter : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    
    [SerializeField] string areaMaskName;
    [SerializeField] GameObject AnimatorRoot;

    public float walkingSpeed = 1.0f; // Speed at which the commuter walks
    public bool isWalking = false;

    private Animator animator;
    private float moveTimer;
    private int areaMask;

    void Awake()
    {
        if (areaMaskName != string.Empty)
        {
            //Filter to AreaMask to only walk on the "Platform" area
            int index = NavMesh.GetAreaFromName(areaMaskName);
            areaMask = 1 << index;
        }
        else {
            areaMask = NavMesh.AllAreas;
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = AnimatorRoot.GetComponent<Animator>();

        agent.isStopped = false;
        agent.stoppingDistance = 0f;

        moveTimer = 0f;
        isWalking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= 0.2f)
        {
            moveTimer += Time.deltaTime;

            if (moveTimer > Random.Range(8, 13))
            {
                
                MoveToRandomNearbyPoint(10f);
                moveTimer = 0;
            }
        }

        if (agent.velocity.x > 0.1 || agent.velocity.z > 0.1) {
            isWalking = true;
            animator.SetBool("isWalking", true);
        } else {
            isWalking = false;
            animator.SetBool("isWalking", false);
        }
    }

    private void MoveToRandomNearbyPoint(float radius)
    {
        
        int areaIndex = NavMesh.GetAreaFromName("Platform");
        int areaMask = 1 << areaIndex;

        Vector3 randomDir = Random.insideUnitSphere * radius;
        randomDir += transform.position;

        if (NavMesh.SamplePosition(
            randomDir,
            out NavMeshHit hit,
            radius,
            areaIndex))
        {
            agent.SetDestination(hit.position);
        }
    }

}
