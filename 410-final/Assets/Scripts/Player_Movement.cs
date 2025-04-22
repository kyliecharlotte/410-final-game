using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject mainScript;
    public GameObject player;
    private Vector3 movement;
    private float playerSpeed = 5.0f;
    private float gravity = -9.81f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Main_PlayerSelect script = mainScript.GetComponent<Main_PlayerSelect>();
        player = script.returnPlayer();
        controller = player.GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //controller.transform.LookAt(movement);

        controller.Move(movement * Time.deltaTime * playerSpeed);
    }
}
