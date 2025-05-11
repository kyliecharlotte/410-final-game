using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Crossbow_PickUp : MonoBehaviour
{

    //public bool collide;

    public Main_PlayerSelect script;
    public bool collide;
    public GameObject player;
    private bool attacked;
    public GameObject crossbow;

    public LayerMask enemyLayer;

    public float sightRange, attackRange;
    public bool enemyInSightRange, enemyInAttackRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       player = script.returnPlayer();
       collide = player.GetComponent<Player_Stats>().collide;
    }

    // Update is called once per frame
    void Update()
    {

        if (collide == true) {
            if (Input.GetMouseButtonDown(0)) {
                //player.GetComponent<Player_Stats>().CrossbowShot(crossbow);
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (collide == true) {
            if (crossbow.gameObject.GetComponent<Animator>().GetBool("crossbow_shot")) {
                if (other.gameObject.layer == 9) {
                    if (player.GetComponent<Player_Stats>().attacked == false) {
                        player.GetComponent<Player_Stats>().attacked = true;
                        Debug.Log("HIT");
                        other.gameObject.GetComponent<EnemyScript>().TakeDamage(4);
                        player.GetComponent<Player_Stats>().ResetAttack();
                    }
                }
            }
        }
        // 8 = Player layer
        if (collide == false) {
        if (other.gameObject.layer == 8) {
            player = other.gameObject;
            if (player.GetComponent<Player_Stats>().HasWeapon == false) {
                player.GetComponent<Player_Stats>().HasWeapon = true;
                player.GetComponent<Player_Stats>().curr_weapon = crossbow;

                crossbow.transform.rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y + 90, 0);
                crossbow.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);

                crossbow.gameObject.transform.position = crossbow.gameObject.transform.position + new Vector3(0,0.1f,0);

                crossbow.gameObject.transform.parent = other.gameObject.transform;
                crossbow.gameObject.GetComponent<Light>().enabled= false;
                //crossbow.GetComponent<Animator>().applyRootMotion = false;
                collide = true;
            }
        }
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (collide == true) {
            if (crossbow.gameObject.GetComponent<Animator>().GetBool("crossbow_shot")) {
                Debug.Log("true");
                if (other.gameObject.layer == 9) {
                    if (player.GetComponent<Player_Stats>().attacked == false) {
                        player.GetComponent<Player_Stats>().attacked = true;
                        Debug.Log("HIT");
                        other.gameObject.GetComponent<EnemyScript>().TakeDamage(4);
                        player.GetComponent<Player_Stats>().ResetAttack();
                    }
                }
            }
        }
    }

}
