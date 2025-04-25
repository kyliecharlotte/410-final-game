using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Stats : MonoBehaviour
{
    private int health;

    public bool HasWeapon;

    //public Animator animator;
    private float timeBetweenAttacks;

    private Animation maceAttack;

    bool Attacked;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 5;
        player = this.gameObject;
        //animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage (int dmg) {
        
        health -= dmg;

        if (health <= 0) {
            Invoke(nameof(EndGame), 0.5f);
        }
    }

    public void MaceAttack (GameObject mace, GameObject enemy) {
        player.GetComponent<Fighting_Script>().canMove = false;
        enemy.GetComponent<EnemyScript>().canMove = false;
        mace.GetComponent<Animator>().SetTrigger("mace_swing");
        mace.GetComponent<Animator>().Play("mace");

        if (!Attacked) {
            Attacked = true;
            enemy.GetComponent<EnemyScript>().canMove = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack () {
        Attacked = false;
        player.GetComponent<Fighting_Script>().canMove = true;
        return;
    }

    void EndGame() {
        SceneManager.LoadScene("Weapon_Test");
    }
}
