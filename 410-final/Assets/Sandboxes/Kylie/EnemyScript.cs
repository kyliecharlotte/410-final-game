using System.Xml.Serialization;
using GLTFast;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    public float health = 5;
    public float speed = 5.2f;

    public bool canMove;

    public NavMeshAgent donut;
    public Transform player;
    public GameObject player_obj;

    public GameObject enemy_dad;
    public LayerMask groundLayer, playerLayer;
    public Vector3 walkPoint;
    bool walkPointExist;
    public float walkPointRange;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool Attacked;
    public float timeBetweenAttacks;
    public GameObject mainScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        Main_PlayerSelect script = mainScript.GetComponent<Main_PlayerSelect>();
        player_obj = script.returnPlayer();
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
        donut.SetDestination(player_obj.transform.position);
    }

    private void AttackPlayer() {
        // stand still
        donut.SetDestination(transform.position);
        bool looking = Physics.CheckSphere(transform.position, 3f, playerLayer);

        if (!Attacked) {
            Attacked = true;
            this.GetComponent<Animator>().SetTrigger("attack");
            this.GetComponent<Animator>().Play("broccoli_attack");
            //this.GetComponent<EnemyScript>().canMove = true;
            Debug.Log(player_obj);
            player_obj.GetComponent<Player_Stats>().TakeDamage(5);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack () {
        Attacked = false;
        this.GetComponent<Animator>().ResetTrigger("attack");
        //player_obj.GetComponent<CapsuleCollider>().enabled = true;
        //player_obj.GetComponent<Fighting_Script>().canMove = true;
        //this.GetComponent<CapsuleCollider>().enabled = true;
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
