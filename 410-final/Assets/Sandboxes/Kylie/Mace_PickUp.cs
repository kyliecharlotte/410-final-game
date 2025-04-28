using Unity.VisualScripting;
using UnityEngine;

public class Mace_PickUp : MonoBehaviour
{

    //public bool collide;

    public string script;
    public bool collide;
    public GameObject player;
    public GameObject mace;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //collide = false;
        script = "Mace_PickUp";
        //mace.GetComponent<Animator>().Play(
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        Debug.Log(collide);
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
            mace.GetComponent<CapsuleCollider>().radius = 1.5f;
            mace.GetComponent<Animator>().applyRootMotion = false;
            //mace.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            collide = true;
        }
        }

        if (collide == true) {
                if (other.gameObject.layer == 9) {
                    if (Input.GetKeyDown(KeyCode.E)) {
                        player.GetComponent<Player_Stats>().MaceAttack(mace, other.gameObject);
                    }
                }
        }
    }

    void OnTriggerStay(Collider other) {
        if (collide == true) {
            if (other.gameObject.layer == 9) {
                if (Input.GetKeyDown(KeyCode.E)) {
                        player.GetComponent<Player_Stats>().MaceAttack(mace, other.gameObject);
                }
            }
        }
    }
}
