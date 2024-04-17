using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.Rendering;
//using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public TextMeshProUGUI score;
    

    public ParticleSystem smokeEffect;
    protected Rigidbody2D rigidbody2D;  //Changed to protected Sadie Raghunand
    public float timer;
    protected int direction = 1; //Changed to protected Sadie Raghunand
    public bool broken = true;

    protected Animator animator; //Changed to protected Sadie Raghunand
    [SerializeField] private AudioSource audioSource;

    public RubyController rC;

    // Start is called before the first frame update
    protected virtual void Start() //Changed to protected virtual by Sadie Raghunand
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        score.text = "Fixed Robots: " + rC.scoreNum.ToString();
    }

     protected virtual void Update() //Changed to protected virtual by Sadie Raghunand
    {

        if(!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    // Update is called once per frame
     void FixedUpdate()
    {

        if(!broken)
        {
            return;
        }

        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        
        smokeEffect.Stop();
        animator.SetBool("Fixed", true);

        audioSource.Play();

        rC.scoreNum++;
        score.text = "Fixed Robots: " + rC.scoreNum.ToString();

    }
}