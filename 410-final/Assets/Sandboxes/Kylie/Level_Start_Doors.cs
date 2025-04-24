using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

public class Level_Start_Doors : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject player;
    public GameObject doors;
    public GameObject mainScript;

    void Start()
    {
        Main_PlayerSelect script = mainScript.GetComponent<Main_PlayerSelect>();
        player = script.returnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            CheckDoor();
        }
        
    }

    void CheckDoor() {

        Debug.Log((doors.transform.position - player.transform.position).magnitude);
        if ((doors.transform.position - player.transform.position).magnitude < 10f) {
            GameObject door_one = doors.gameObject.transform.GetChild(0).gameObject;
            GameObject door_two = doors.gameObject.transform.GetChild(1).gameObject;
            door_one.transform.position = new Vector3(0,0,0);
            door_two.transform.position = new Vector3(0,0,0);

        }
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == doors) {
            GameObject door_one = other.gameObject.transform.GetChild(0).gameObject;
            GameObject door_two = other.gameObject.transform.GetChild(1).gameObject;
            door_one.transform.position = new Vector3(0,0,0);
        }
    }*/
    

}
