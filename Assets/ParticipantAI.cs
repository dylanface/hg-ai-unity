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

    public float sightRange, attackRange;
    public Collider[] targetInSightRange, targetInAttackRange;

    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider sightHit, attackHit;
        bool sightArrayLength, attackArrayLength;

        targetInSightRange = Physics.OverlapSphere(transform.position, sightRange, whatIsTarget);
        sightArrayLength = targetInSightRange.Length > 1;
        for(int i = 0; i < targetInSightRange.Length; i++)
            {
                if(targetInSightRange[i].gameObject.name != transform.name)
                {
                    sightHit = targetInSightRange[i]; 
        target = sightHit.transform;
                }
            }

        targetInAttackRange = Physics.OverlapSphere(transform.position, attackRange, whatIsTarget);
        attackArrayLength = targetInAttackRange.Length > 1;
        for(int i = 0; i < targetInAttackRange.Length; i++)
            {
                if(targetInSightRange[i].gameObject.name != transform.name)
                {
                    attackHit = targetInAttackRange[i]; 
        target = attackHit.transform;
                }
            }



        if (!sightArrayLength && !attackArrayLength) Patroling();
        else if (sightArrayLength && !attackArrayLength) Chasing();
        else if (sightArrayLength && attackArrayLength) Attacking();
        
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
        agent.SetDestination(target.position);

        transform.LookAt(target);

        if (!attackReady)
        {
            Health h = target.GetComponent<Health>();
            if (h != null)
            {
                h.currentHealth -= 10f * Time.deltaTime;
            }

            attackReady = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        attackReady = false;
    }
}
