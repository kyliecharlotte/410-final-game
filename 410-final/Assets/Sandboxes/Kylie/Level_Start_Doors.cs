using System.Collections;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

public class Level_Start_Doors : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject player;

    public bool opened;
    public GameObject doors;
    public GameObject mainScript;

    void Start()
    {
        Main_PlayerSelect script = mainScript.GetComponent<Main_PlayerSelect>();
        player = script.returnPlayer();
        opened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            CheckDoor();
        }
        
    }

    void CheckDoor() {

        if (((doors.transform.position - player.transform.position).magnitude < 10f) && (opened == false)) {
            GameObject door_one = doors.gameObject.transform.GetChild(0).gameObject;
            GameObject door_two = doors.gameObject.transform.GetChild(1).gameObject;
            AnimateOpeningDoor(door_one, door_two);
            

        }
    }

    private IEnumerator Rotate90(GameObject door_one, GameObject door_two) {
        float timeElapsed = 0;
        Quaternion one_start = door_one.transform.rotation;
        Quaternion one_target = door_one.transform.rotation * Quaternion.Euler(0,270,0);
        
        Quaternion two_start = door_two.transform.rotation;
        Quaternion two_target = door_two.transform.rotation * Quaternion.Euler(0,-270,0);
        
        while (timeElapsed < 0.9f) {
            door_one.transform.rotation = Quaternion.Lerp(one_start, one_target, timeElapsed);
            door_two.transform.rotation = Quaternion.Lerp(two_start, two_target, timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        door_one.transform.rotation = one_target;
        door_two.transform.rotation = two_target;
        door_one.transform.position = new Vector3(door_one.transform.position.x - 0.5f, door_one.transform.position.y, door_one.transform.position.z);
        door_two.transform.position = new Vector3(door_two.transform.position.x+0.1f, door_two.transform.position.y, door_two.transform.position.z + 0.7f);

    }

    void AnimateOpeningDoor(GameObject door_one, GameObject door_two) {
            opened = true;
            door_one.transform.position = new Vector3(door_one.transform.position.x - 1.8f, door_one.transform.position.y, door_one.transform.position.z + 0.9f);
            door_two.transform.position = new Vector3(door_two.transform.position.x + 0.3f, door_two.transform.position.y, door_two.transform.position.z + 1.5f);
            doors.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(Rotate90(door_one, door_two));
    }

}
