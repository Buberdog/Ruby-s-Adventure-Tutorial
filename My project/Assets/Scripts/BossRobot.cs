using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRobot : EnemyController
{

    //Whole script written by Sadie Raghunand

    private GameObject ruby;
   

    public int countHits = 0;
    public bool invincible;
    

    protected override void Start()
    {
        

        ruby = GameObject.FindGameObjectWithTag("Player");
        rC = ruby.GetComponent<RubyController>();
        smokeEffect = GetComponentInChildren<ParticleSystem>();
        score = rC.scoreText;
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
       

    }

    protected override void Update()
    {
        base.Update();

        if(invincible)
        {
            StartCoroutine(Invincibility());
        }
    }

    public IEnumerator Invincibility()
    {
        Debug.Log("Invincibilty starts");
        yield return new WaitForSeconds(2);

        Debug.Log("Invincible ends");
        invincible = false;
    }



}
