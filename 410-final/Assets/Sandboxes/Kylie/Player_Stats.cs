using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Stats : MonoBehaviour
{
    private int health;

    private bool enter = false;

    public GameObject player_dad;

    public LayerMask enemyLayer;

    public bool HasWeapon;

    public GameObject curr_weapon;

    public AudioSource audioSource;

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
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (HasWeapon == true) {
                DropWeapon(curr_weapon);
            }
        }
    }

    public void DropWeapon(GameObject curr) {
        curr.transform.parent = null;
        curr.GetComponent<Animator>().applyRootMotion = true;
        curr.GetComponent<Light>().enabled = true;

        if (curr.tag == "Mace_Weapon") {
                //if (!Physics.CheckSphere(curr.transform.position, 5f, 8)) {
                StartCoroutine(drop_item_timer(curr));
                //}
        }
    
    }

    IEnumerator drop_item_timer(GameObject curr) {
        enter = true;
        yield return new WaitForSeconds(0.5f);
        curr.GetComponent<Mace_PickUp>().collide = false;
        enter = false;
    }

    public void TakeDamage (int dmg) {
        
        health -= dmg;

        if (health <= 0) {
            Invoke(nameof(EndGame), 1.0f);
        }
    }

    public void MaceAttack (GameObject mace, GameObject enemy) {
        
        player.GetComponent<Fighting_Script>().canMove = false;
        enemy.GetComponent<EnemyScript>().canMove = false;
        audioSource = mace.GetComponent<AudioSource>();
        mace.GetComponent<Animator>().SetTrigger("mace_swing");

        //mace.GetComponent<Animator>().Play("mace");

        if (!Attacked) {
            Attacked = true;
            audioSource.Play();
            if (Physics.CheckSphere(enemy.transform.position, 0.65f, enemyLayer)) {
                enemy.GetComponent<EnemyScript>().TakeDamage(1);
                Debug.Log("hit");
                Debug.Log(enemy.GetComponent<EnemyScript>().ShowHealth());
            }
            //enemy.GetComponent<EnemyScript>().TakeDamage(1);
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
        SceneManager.LoadScene("MainScene");
    }
}
