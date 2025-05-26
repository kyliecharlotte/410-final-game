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

    public bool sword_attack;
    public bool mace_attack;

    public GameObject player_dad;

    public LayerMask enemyLayer;

    public bool HasWeapon;

    public GameObject curr_weapon;

    public AudioSource audioSource;

    //public Animator animator;
    private float timeBetweenAttacks;
    public GameObject player;
    public GameObject deadMessage;

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
        sword_attack = false;
        mace_attack = false;
        collide = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Ensuring HealthBarUI is correctly initialized
        StartCoroutine(InitializeHealthBarUI());
        StartCoroutine(InitializeWeaponIconUI());

        deadMessage = GameObject.Find("Canvas").transform.Find("DiedMessage").gameObject;
    }

    public IEnumerator InitializeHealthBarUI()
    {
        yield return new WaitForEndOfFrame(); // Wait for UI to be created

        GameObject canvas = GameObject.Find("Canvas"); // Ensure this matches your actual Canvas name

        if (canvas != null)
        {
            healthBarUI = canvas.GetComponentInChildren<HealthBarUI>(); // Only search within the Canvas
        }
        else
        {
            Debug.LogError("Canvas not found! Make sure the name matches.");
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


    public IEnumerator InitializeWeaponIconUI()
    {
        yield return new WaitForEndOfFrame(); // Wait for UI to be created

        if (weaponIconUI == null)
        {
            weaponIconUI = FindObjectOfType<WeaponIconUI>(); // Find the UI component after it's created
        }

        if (weaponIconUI == null)
        {
            Debug.LogError("WeaponIconUI is not assigned in the Inspector!");
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

    }

    public void DropWeapon(GameObject curr)
    {
        curr.transform.parent = null;

        curr.GetComponent<Light>().enabled = true;

        if (curr.tag == "Mace_Weapon")
        {
            curr.GetComponent<Animator>().applyRootMotion = true;
            //if (!Physics.CheckSphere(curr.transform.position, 5f, 8)) {
            StartCoroutine(drop_item_timer(curr));
            //}
        }

        if (curr.tag == "Crossbow_Weapon")
        {

            //if (!Physics.CheckSphere(curr.transform.position, 5f, 8)) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            StartCoroutine(drop_item_timer(curr));
            //}
        }

        if (curr.tag == "Sword_Weapon")
        {
            curr.GetComponent<Animator>().ResetTrigger("hold_sword");
            curr.GetComponent<Animator>().applyRootMotion = true;
            StartCoroutine(drop_item_timer(curr));
        }
        // weaponIconUI.UpdateWeaponIcon(curr_weapon);


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

        Debug.Log("healthBarUI: " + healthBarUI);
        Debug.Log("healthStat: " + healthStat);

        healthBarUI.UpdateValue(healthStat.CurrentVal, healthStat.MaxVal);

        if (healthStat.CurrentVal <= 0)
        {
            // Invoke(nameof(EndGame), 1.0f);
            StartCoroutine(ShowDeadMessageAndEndGame());
        }
    }
    
    IEnumerator ShowDeadMessageAndEndGame()
    {
        deadMessage.SetActive(true); // Show the death message
        yield return new WaitForSeconds(1.0f); // Wait for 1 second
        deadMessage.SetActive(false); // Hide the message before transitioning
        EndGame(); // Call the EndGame function
    }

    public void MaceAttack_1(GameObject mace)
    {
        mace.GetComponent<Animator>().SetTrigger("mace_swing");
        mace_attack = true;
        StartCoroutine(ResetMace(mace));
    }

    public void Sword_Attack(GameObject sword) {
        sword.GetComponent<Animator>().SetTrigger("sword_swing");
        sword_attack = true;
        sword.GetComponent<Animator>().applyRootMotion = false;
        StartCoroutine(ResetSword(sword));
    }

    IEnumerator ResetMace(GameObject mace) {
        yield return new WaitForSeconds(1f);
        mace_attack = false;
        mace.GetComponent<Animator>().ResetTrigger("mace_swing");
    }

    IEnumerator ResetSword(GameObject sword)
    {
        yield return new WaitForSeconds(0.75f);
        sword_attack = false;
        sword.GetComponent<Animator>().ResetTrigger("sword_swing");
    }

    public void CrossbowAttack(GameObject crossbow)
    {

        float mouseY = Mathf.Clamp01(Input.mousePosition.y / Screen.height);

        float release = Mathf.Lerp(18f, -50f, mouseY);
        Vector3 dir = transform.forward;
        Vector3 axis = transform.right;
        float arrow_speed = 20f;

        Vector3 angle = Quaternion.AngleAxis(release, axis) * dir;
        GameObject arrow = Instantiate(arrowPrefab, crossbow.transform.position, crossbow.transform.rotation);
        arrow.GetComponent<Rigidbody>().linearVelocity = angle.normalized * arrow_speed;

        arrow.GetComponent<Arrow_Shot>().shot = true;
    }

    public void ResetAttack () {
        Invoke(nameof(VariableChange),1.0f);
        return;
    }

    private void VariableChange() {
        attacked = false;
    }

    void EndGame() {
        string scene = SceneManager.GetActiveScene().name.ToString().Trim();
        if (scene == "Tutorial")
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (scene == "castle 2") {
            SceneManager.LoadScene("castle 2");
        }
        else if (scene == "castle 3") {
            SceneManager.LoadScene("castle 3");
        }
        else
        {
            SceneManager.LoadScene("castle");
        }
    }


}
