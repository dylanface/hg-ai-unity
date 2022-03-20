using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParticipantAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public LayerMask whatIsGround, whatIsTarget;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool attackReady;

    public float sightrange, attackRange;
    public bool targetInSightRange, targetInAttackRange;

    private void Awake() {
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetInSightRange = Physics.CheckSphere(transform.position, sightrange, whatIsTarget);
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsTarget);

        if (!targetInSightRange && !targetInAttackRange) Patroling();
        else if (targetInSightRange && !targetInAttackRange) Chasing();
        else if (targetInAttackRange && targetInSightRange) Attacking();
        
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            walkPoint = new Vector3(Random.Range(-walkPointRange, walkPointRange), 0f, Random.Range(-walkPointRange, walkPointRange));
            walkPointSet = true;
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            if (agent.remainingDistance <= 0.5f)
            {
                walkPointSet = false;
            }
        }

    }

    private void Chasing()
    {
        agent.SetDestination(target.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);
    }
}
