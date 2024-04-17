using System.Collections;
using System.Collections.Generic;
//using System.Numerics;

//using System.Numerics;

//using System.Numerics;
//using Unity.VisualScripting;
// using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using TMPro;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;

    public GameObject projectilePrefab;

    public AudioClip throwSound;
    public AudioClip hitSound;
    
    public int health { get { return currentHealth; }}
    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    public Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    AudioSource audioSource;

    public ParticleSystem gainHealth;
    public ParticleSystem looseHealth;

    public int scoreNum;

    [SerializeField] private GameObject boss; //Added by Sadie Raghunand for boss mechanic lines 44 - 51
    [SerializeField] private GameObject spawnPos;
    private int count = 0;
    public GameObject arena;
    public Vector2 bossFightPos;
    public TextMeshProUGUI scoreText;
    [SerializeField] private Vector2 powerupPos;
    public GameObject powerup; //Added by Remond Elia for powerup mechanic, lines 52-54
    public float attackForce = 300;
    public SpriteRenderer PowerColor;
    public bool hasPowerup;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }


        //Lines 99 - 121 written by Sadie Raghunand
        if(scoreNum == 3 && count == 0)
        {
            arena.SetActive(true);
            Instantiate(boss, spawnPos.transform.position, boss.transform.rotation);
            Instantiate(powerup, powerupPos, powerup.transform.rotation);
            count++;
            transform.position = bossFightPos;
            
        }

        //Lines 124 - 128 written by Remond Elia
        if(hasPowerup)
        {
            StartCoroutine(PowerupTimer());
        }
    }

    // Change the f value to modify Rudy's speed
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            PlaySound(hitSound);

            looseHealth.Play();
            
        }

        if(amount > 0)
        {
            gainHealth.Play();
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, attackForce);

        animator.SetTrigger("Launch");

        PlaySound(throwSound);

    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public IEnumerator PowerupTimer() //Lines 178 - 185 were written by Remond Elia
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        speed = 3;
        attackForce = 300;
        PowerColor.color = new Color(255, 255, 255);

    }
}