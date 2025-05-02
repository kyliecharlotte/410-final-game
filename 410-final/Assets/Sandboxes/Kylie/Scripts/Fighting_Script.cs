using System;
using UnityEngine;

public class Fighting_Script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CharacterController controller;
    public GameObject mainScript;

    public bool canMove;
    public Transform playerBody;
    
    public Vector3 inputDirection;
    public GameObject player;
    private float playerSpeed = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = player.GetComponent<CharacterController>();
        playerBody = player.transform;
        canMove = true;
        //Cursor.SetCursor(null, new Vector2(0,0), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true) {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            //float x = Input.GetAxis("Mouse X");
            //float z = Input.GetAxis("Mouse Y");

            playerBody.Rotate(0, Input.GetAxis("Mouse X") * 18.5f, 0);

            inputDirection = new Vector3(x,0,z);
            Vector3 worldInputDirection = playerBody.TransformDirection(inputDirection);
            
            controller.Move(worldInputDirection * Time.deltaTime * playerSpeed);
            /*if (inputDirection.magnitude > 0.01f) {
                inputDirection.Normalize();
                if (z >= 0) {
                    if (x > 0) {
                        playerBody.Rotate(Vector3.up * 180f * Time.deltaTime);
                    } else if (x < 0) {
                        playerBody.Rotate(-1 * Vector3.up * 180f * Time.deltaTime);
                    }
                }
            }*/
        }
    }  
}
