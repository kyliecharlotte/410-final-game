
using UnityEngine;

public class Mace_PickUp : MonoBehaviour
{

    //public bool collide;

    public string script;
    public bool collide;
    public GameObject player;
    private bool attacked;
    public GameObject mace;

    public LayerMask enemyLayer;

    public float sightRange, attackRange;
    public bool enemyInSightRange, enemyInAttackRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //collide = false;
        //mace.GetComponent<Animator>().Play(
    }

    // Update is called once per frame
    void Update()
    {

        /*Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, enemyLayer);
        enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, enemyLayer);

        foreach (Collider enemy in enemiesInRange) {
            Vector3 dir = (player.transform.position - enemy.transform.position).normalized;
            float angle = Vector3.Angle(player.transform.forward.normalized, dir);
            //float angle = Vector3.Angle(new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")),dir);
            if (enemyInAttackRange && enemyInSightRange && Input.GetKeyDown(KeyCode.E) && (180 - angle) < 55f) {
                    player.GetComponent<Player_Stats>().MaceAttack(mace, enemy.gameObject);
            }
        }*/
        if (collide == true) {
            if (Input.GetMouseButtonDown(0)) {
                player.GetComponent<Player_Stats>().MaceAttack_1(mace);
            }
        }



    }

    void OnTriggerEnter(Collider other)
    {
        if (collide == true) {
            if (mace.gameObject.GetComponent<Animator>().GetBool("mace_swing")) {
                if (other.gameObject.layer == 9) {
                    if (player.GetComponent<Player_Stats>().attacked == false) {
                        player.GetComponent<Player_Stats>().attacked = true;
                        Debug.Log("HIT");
                        other.gameObject.GetComponent<EnemyScript>().TakeDamage(1);
                        player.GetComponent<Player_Stats>().ResetAttack();
                    }
                }
            }
        }
        // 8 = Player layer
        if (collide == false) {
        if (other.gameObject.layer == 8) {
            player = other.gameObject;
            player.GetComponent<Player_Stats>().HasWeapon = true;
            player.GetComponent<Player_Stats>().curr_weapon = mace;
            mace.gameObject.transform.position = mace.gameObject.transform.position - new Vector3(0, mace.transform.position.y, 0);
            mace.gameObject.transform.parent = other.gameObject.transform;
            mace.gameObject.GetComponent<Light>().enabled= false;
            mace.gameObject.transform.rotation = Quaternion.Euler(80,0,0);
            mace.GetComponent<Animator>().applyRootMotion = false;
            //mace.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            collide = true;
        }
        }
        /*if (collide == true) {
                if (other.gameObject.layer == 9) {
                    if (Input.GetKeyDown(KeyCode.E)) {
                        player.GetComponent<Player_Stats>().MaceAttack(mace, other.gameObject);
                    }
                }
        }*/
    }

    void OnTriggerStay(Collider other)
    {
        if (collide == true) {
            if (mace.gameObject.GetComponent<Animator>().GetBool("mace_swing")) {
                Debug.Log("true");
                if (other.gameObject.layer == 9) {
                    if (player.GetComponent<Player_Stats>().attacked == false) {
                        player.GetComponent<Player_Stats>().attacked = true;
                        Debug.Log("HIT");
                        other.gameObject.GetComponent<EnemyScript>().TakeDamage(1);
                        player.GetComponent<Player_Stats>().ResetAttack();
                    }
                }
            }
        }
    }
    /*void OnTriggerStay(Collider other) {
        if (collide == true) {
            if (other.gameObject.layer == 9) {
                if (Input.GetKeyDown(KeyCode.E)) {
                        player.GetComponent<Player_Stats>().MaceAttack(mace, other.gameObject);
                }
            }
        }
    }*/
}
