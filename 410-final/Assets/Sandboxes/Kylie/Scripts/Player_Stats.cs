using System;
using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Stats : MonoBehaviour
{
    [SerializeField] private Stat healthStat;
    [SerializeField] private HealthBarUI healthBarUI;
    public WeaponIconUI weaponIconUI;

    private int health;

    public GameObject arrowPrefab;

    public Texture2D bow_curser;

    public bool attacked;
    public bool collide;
    private bool enter = false;

    public GameObject player_dad;

    public LayerMask enemyLayer;

    public bool HasWeapon;

    public GameObject curr_weapon;

    private bool aim_bow;

    public AudioSource audioSource;

    //public Animator animator;
    private float timeBetweenAttacks;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {

    //     health = 5;

    //     if (healthStat == null)
    //         healthStat = new Stat(health);

    //     healthStat.MaxVal = health;  // You must set MaxVal too!
    //     healthStat.CurrentVal = health;
    //     healthStat.Initialize();

    //     if (healthBarUI == null)
    //     {
    //         Debug.LogError("HealthBarUI is not assigned in the Inspector!");
    //     }
    //     else
    //     {
    //         healthBarUI.SetMaxValue(healthStat.MaxVal);
    //         // UpdateHealthUI(); // Ensure UI starts with correct health
    //     }

    //     player = this.gameObject;
    //     attacked = false;
    //     collide = false;
    //     Cursor.lockState = CursorLockMode.Locked;
    //     //animator = gameObject.GetComponent<Animator>();
    // }

    void Start()
    {
        health = 5;

        if (healthStat == null)
            healthStat = new Stat(health);

        healthStat.MaxVal = health;
        healthStat.CurrentVal = health;
        healthStat.Initialize();

        player = this.gameObject;
        attacked = false;
        collide = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Ensuring HealthBarUI is correctly initialized
        StartCoroutine(InitializeHealthBarUI());
    }

    IEnumerator InitializeHealthBarUI()
    {
        yield return new WaitForEndOfFrame(); // Wait for UI to be created

        if (healthBarUI == null)
        {
            healthBarUI = FindObjectOfType<HealthBarUI>(); // Find the UI component after it's created
        }

        if (healthBarUI == null)
        {
            Debug.LogError("HealthBarUI is not assigned in the Inspector!");
        }
        else
        {
            healthBarUI.SetMaxValue(healthStat.MaxVal);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (HasWeapon == true) {
                DropWeapon(curr_weapon);
            }
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            TakeDamage(1);
        }
    }

    public void DropWeapon(GameObject curr) {
        curr.transform.parent = null;
        
        curr.GetComponent<Light>().enabled = true;

        if (curr.tag == "Mace_Weapon") {
                curr.GetComponent<Animator>().applyRootMotion = true;
                //if (!Physics.CheckSphere(curr.transform.position, 5f, 8)) {
                StartCoroutine(drop_item_timer(curr));
                //}
        }

        if (curr.tag == "Crossbow_Weapon") {
                
                //if (!Physics.CheckSphere(curr.transform.position, 5f, 8)) {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                StartCoroutine(drop_item_timer(curr));
                //}
        }
        weaponIconUI.UpdateWeaponIcon(curr_weapon);

    
    }

    IEnumerator drop_item_timer(GameObject curr) {

        enter = true;
        yield return new WaitForSeconds(0.5f);
        collide = false;
        curr_weapon = null;
        HasWeapon = false;
        enter = false;
        weaponIconUI.UpdateWeaponIcon(curr_weapon);

    }

    public void TakeDamage (int dmg) {
        
        health -= dmg;
        healthStat.CurrentVal = health;

        healthBarUI.UpdateValue(healthStat.CurrentVal, healthStat.MaxVal);

        if (healthStat.CurrentVal <= 0)
        {
            Invoke(nameof(EndGame), 1.0f);
        }
    }

    public void MaceAttack_1(GameObject mace) {
        mace.GetComponent<Animator>().SetTrigger("mace_swing");
        StartCoroutine(ResetMace(mace));
    }

    IEnumerator ResetMace(GameObject mace) {
        yield return new WaitForSeconds(1f);
        mace.GetComponent<Animator>().ResetTrigger("mace_swing");
    }
/*
    public void MaceAttack (GameObject mace, GameObject enemy) {
        
        player.GetComponent<Fighting_Script>().canMove = false;
        enemy.GetComponent<EnemyScript>().canMove = false;
        audioSource = mace.GetComponent<AudioSource>();
        mace.GetComponent<Animator>().SetTrigger("mace_swing");

        //mace.GetComponent<Animator>().Play("mace");

        if (!attacked) {
            attacked = true;
            audioSource.Play();
            if (Physics.CheckSphere(enemy.transform.position, 0.75f, enemyLayer)) {
                enemy.GetComponent<EnemyScript>().TakeDamage(1);
                
            }
            //enemy.GetComponent<EnemyScript>().TakeDamage(1);
            enemy.GetComponent<EnemyScript>().canMove = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

*/

    // public int returnHealth() {
    //     return health;
    // }
    public void CrossbowAttack(GameObject crossbow) {
        
        float mouseY = Mathf.Clamp01(Input.mousePosition.y / Screen.height);

        float release = Mathf.Lerp(18f, -50f, mouseY);
        Vector3 dir = transform.forward;
        Vector3 axis = transform.right;
        float arrow_speed = 16.5f;

        Vector3 angle = Quaternion.AngleAxis(release, axis) * dir;
        GameObject arrow = Instantiate(arrowPrefab, crossbow.transform.position, crossbow.transform.rotation);
        arrow.GetComponent<Rigidbody>().linearVelocity = angle.normalized * arrow_speed;

        arrow.GetComponent<Arrow_Shot>().shot = true;
    }

    public void ResetAttack () {
        Invoke(nameof(VariableChange),1.2f);
        return;
    }

    private void VariableChange() {
        attacked = false;
    }


    void EndGame() {
        SceneManager.LoadScene("castle");
    }


}
