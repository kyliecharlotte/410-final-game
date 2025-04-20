using UnityEngine;

public class Main_CameraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.transform.position + new Vector3(-2.0f, 4, -6f);
        transform.LookAt(player.position);
    }
}
