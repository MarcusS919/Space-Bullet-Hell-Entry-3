using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    //handles movements
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    // Handles projectile spawning
    public Rigidbody2D projectile;          // What to spawn
    public Transform projectileSpawnPoint;  // Where to spawn 
    public float projectileForce;           // How fast the projectile goes
    public float fireWaitTime;              // How fast the player can shoot
    public bool AllowFire;                  // Check if the player can shoot or not

    public float PowerUpASBoost;            // How much the power up would increase the attack speed by
    public int Health;                 // How much Health the player has
    public float DamageWaitTime;            // How long before the player can take damage again
    public bool AllowDamage;                // Check if the player can take damage or not

    public Text HealthText;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

        //set the fireWaitTime as 1 if it wasn't set
        if (fireWaitTime==0)
        {
            fireWaitTime = 1;
        }

        //set the PowerUpASBoost as 0.2 if it wasn't set
        if (PowerUpASBoost == 0)
        {
            PowerUpASBoost = 0.2f;
        }

        //set the PlayerHealth as 1 if it wasn't set
        if (Health == 0)
        {
            Health = 1; 
        }

        //set the DamageWaitTime as 1 if it wasn't set
        if (DamageWaitTime == 0)
        {
            DamageWaitTime = 1;
        }

        if (!HealthText)
        {
            // Prints a warning message to the Console
            // - Open Console by going to Window-->Console (or Ctrl+Shift+C)
            Debug.LogError("HealthText not found on " + name);
        }
        else
        {
            HealthText = GameObject.Find("Text_Health").GetComponent<Text>();
        }

        if (HealthText)
        {
            HealthText.text = "Health: " + Health;
        }

        AllowFire = true;
        AllowDamage = true;
    }
	
	// Update is called once per frame
	void Update () {


        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        // Check if 'Fire1' input was pressed
        // - Left Ctrl or Left Click
        if (Input.GetButton("Fire1"))
        {
            if (AllowFire == true)
            fire();

        }

    }

    //controls what happens when the player collides with a power up or enemy
    void OnTriggerEnter2D(Collider2D collision)
    {

        //when collides with PowerUpAS it decreases the fire wait time so the player can shoot faster
        if (collision.gameObject.tag == "PowerUpAS")
        {
            fireWaitTime -= PowerUpASBoost;
            Debug.Log("Fire wait time is now " + fireWaitTime);
            Destroy(collision.gameObject);
        }

        //when collides with an enemy it decreases the player's Health
        if (collision.gameObject.tag == "Enemy")
        {
            if (AllowDamage == true)
            {
                Health -= 1;
                Destroy(collision.gameObject);
            }

            if (HealthText)
            {
                HealthText.text = "Health: " + Health;
            }

            //make it so the player is invinicible for a set amount of time after taking damage
            AllowDamage = false;
            StartCoroutine(DamageWait());

            if (Health == 0)
            {
                death();
            }
        }
    }

        private void FixedUpdate()
    {

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

    }

    void death()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    void fire()
    {
        Debug.Log("Pew Pew");

        // Check if 'projectileSpawnPoint' and 'projectile' exist
        if (projectileSpawnPoint && projectile)
        {

            // Create the 'Projectile' and add to Scene
            Rigidbody2D temp = Instantiate(projectile, projectileSpawnPoint.position,
                projectileSpawnPoint.rotation);

            // Stop 'Character' from hitting 'Projectile'
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),
                temp.GetComponent<Collider2D>(), true);

            temp.AddForce(projectileSpawnPoint.up * projectileForce, ForceMode2D.Impulse);
        }
        AllowFire = false;
        StartCoroutine(FireWait());
    }

    IEnumerator FireWait()
    {
        yield return new WaitForSeconds(fireWaitTime);
        AllowFire = true;
    }

    // Invincibility timer 
    IEnumerator DamageWait()
    {
        yield return new WaitForSeconds(DamageWaitTime);
        AllowDamage = true;
    }
}
