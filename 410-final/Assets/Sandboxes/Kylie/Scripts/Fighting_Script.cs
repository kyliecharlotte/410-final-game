using System;
using Unity.VisualScripting;
using UnityEngine;

public class Fighting_Script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CharacterController controller;
    public GameObject mainScript;

    public bool canMove = true;
    public Transform playerBody;
    
    public Vector3 inputDirection;
    public GameObject player;
    public float playerSpeed = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = player.GetComponent<CharacterController>();
        playerBody = player.transform;
        //Cursor.SetCursor(null, new Vector2(0,0), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove == true)
        {

            controller.gameObject.SetActive(true);

            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            //float x = Input.GetAxis("Mouse X");
            //float z = Input.GetAxis("Mouse Y");

            playerBody.Rotate(0, Input.GetAxis("Mouse X") * 9.0f, 0);
            inputDirection = new Vector3(x, 0, z).normalized;
            Vector3 worldInputDirection = playerBody.TransformDirection(inputDirection);

            controller.Move(worldInputDirection * Time.deltaTime * playerSpeed);

        }
        else
        {
            controller.gameObject.SetActive(false);
        }
    }
}
