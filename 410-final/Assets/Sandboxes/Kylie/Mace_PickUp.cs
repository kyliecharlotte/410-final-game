using Unity.VisualScripting;
using UnityEngine;

public class Mace_PickUp : MonoBehaviour
{

    private bool collide;
    public GameObject player;
    public GameObject mace;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collide = false;
        //mace.GetComponent<Animator>().Play(
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // 8 = Player layer
        if (collide == false) {
        if (other.gameObject.layer == 8) {
            player = other.gameObject;
            mace.gameObject.transform.position = mace.gameObject.transform.position - new Vector3(0, mace.transform.position.y, 0);
            mace.gameObject.transform.parent = other.gameObject.transform;
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
