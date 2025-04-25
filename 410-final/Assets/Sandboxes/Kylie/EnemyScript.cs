using System.Xml.Serialization;
using GLTFast;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    public float health = 5;
    public float speed = 5f;

    public bool canMove;

    public NavMeshAgent donut;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    public Vector3 walkPoint;
    bool walkPointExist;
    public float walkPointRange;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool Attacked;
    public float timeBetweenAttacks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        donut = GetComponent<NavMeshAgent>(); 
        canMove = true;
    }

    private void Patroling() {

        if (!walkPointExist) 
        { 
            SearchWalkPoint();
        }

        if (walkPointExist) {
            donut.SetDestination(walkPoint);
        }

        Vector3 distToWalkPoint = transform.position - walkPoint;

        if (distToWalkPoint.magnitude < 1f) {
            walkPointExist = false;
        }

    }

    private void ChasePlayer() {
        donut.SetDestination(player.position);
    }

    private void AttackPlayer() {
        // stand still
        donut.SetDestination(transform.position);

        transform.LookAt(player);
        if (!Attacked) {
            Attacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack () {
        Attacked = false;
        return;
    }

    private void SearchWalkPoint() {

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer)) {
            walkPointExist = true;
        }

        return;
    }

    public void TakeDamage (int dmg) {
        
        health -= dmg;

        if (health <= 0) {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    public float ShowHealth () {
        return health;
    }

    private void DestroyEnemy() {
        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
    
        if (canMove == true) {
        if (!playerInSightRange && !playerInAttackRange) {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange) {
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange) {
            AttackPlayer();
        }
        }
    }
}
