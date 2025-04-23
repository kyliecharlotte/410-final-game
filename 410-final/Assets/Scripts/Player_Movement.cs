using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject mainScript;
    public Transform playerBody;
    
    public Vector3 inputDirection;
    public GameObject player;
    private float playerSpeed = 7.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Main_PlayerSelect script = mainScript.GetComponent<Main_PlayerSelect>();
        player = script.returnPlayer();
        controller = player.GetComponent<CharacterController>();
        playerBody = player.transform;
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(x,0,z);
        Vector3 worldInputDirection = playerBody.TransformDirection(inputDirection);
        
        controller.Move(worldInputDirection * Time.deltaTime * playerSpeed);

       /* if (Input.GetAxisRaw("Horizontal") > 0) {
            RotatePlayer(1);
        } else if (Input.GetAxisRaw("Horizontal") < 0) {
            RotatePlayer(-1);
        }*/

        if (inputDirection.magnitude > 0.01f) {
            inputDirection.Normalize();
            if (z >= 0) {
                if (x > 0) {
                //playerBody.rotation = Quaternion.Lerp(playerBody.rotation, Quaternion.LookRotation(inputDirection), 10f * Time.deltaTime);
                    playerBody.Rotate(Vector3.up * 200f * Time.deltaTime);
                } else if (x < 0) {
                    playerBody.Rotate(-1 * Vector3.up * 200f * Time.deltaTime);
                }
            }
            //playerBody.rotation = Quaternion.Lerp(playerBody.rotation, Quaternion.LookRotation(inputDirection), 10f * Time.deltaTime);
        }
    }  
}
