using UnityEngine;

public class alpha_doors : MonoBehaviour
{

    public GameObject door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log(RiddleManager.instance.ReturnScore());
            if (RiddleManager.instance.ReturnScore() == 3) {
                 Debug.Log("hi");
                door.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log(RiddleManager.instance.ReturnScore());
            if (RiddleManager.instance.ReturnScore() == 3) {
                Debug.Log("hi");
                door.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
