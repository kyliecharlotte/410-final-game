using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Crossbow_PickUp : MonoBehaviour
{

    //public bool collide;

    public Main_PlayerSelect script;
    public Player_Stats player_script;
    public GameObject player;
    private bool attacked;
    public GameObject crossbow;
    //public GameObject arrow;

    public Texture2D bow_cursor;

    public LayerMask enemyLayer;

    public float sightRange, attackRange;
    public bool enemyInSightRange, enemyInAttackRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       player = script.returnPlayer();
       player_script = player.GetComponent<Player_Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        crossbow = this.gameObject;
        if (player_script.collide == true) {
            if (player.GetComponent<Player_Stats>().curr_weapon == crossbow) {
                if (Input.GetMouseButtonDown(0)) {
                    player_script.CrossbowAttack(crossbow);
                }
                //player.GetComponent<Player_Stats>().CrossbowShot(crossbow);
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        // 8 = Player layer
        if (player_script.collide == false) {
            if (other.gameObject.layer == 8) {
                player = other.gameObject;
                if (player.GetComponent<Player_Stats>().HasWeapon == false) {
                    player.GetComponent<Player_Stats>().HasWeapon = true;
                    player.GetComponent<Player_Stats>().curr_weapon = crossbow;
                    // change icon
                    if (player_script.weaponIconUI != null)
                    {
                        Debug.Log("weapon change");
                        player_script.weaponIconUI.UpdateWeaponIcon(crossbow);
                    }

                    crossbow.transform.rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y + 90, 0);
                    crossbow.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);

                    crossbow.gameObject.transform.position = crossbow.gameObject.transform.position + new Vector3(0,0.1f,0);

                    crossbow.gameObject.transform.parent = other.gameObject.transform;
                    crossbow.gameObject.GetComponent<Light>().enabled= false;
                    //crossbow.GetComponent<Animator>().applyRootMotion = false;
                    player_script.collide = true;
                }
            }
        }

    }

}
