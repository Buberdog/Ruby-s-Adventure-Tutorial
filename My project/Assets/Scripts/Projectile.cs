using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);
    }

    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null && other.gameObject.tag != "Boss")  //Sadie Raghunand added "&& other.gameObject. layer != 12"
        {
            e.Fix();
        }

        //Lines 38 - 55 written by Sadie Raghunand for boss fight
        else if(e != null && other.gameObject.tag == "Boss")
        {
            BossRobot _bossScript = other.gameObject.GetComponent<BossRobot>();

            if(_bossScript.invincible)
            {
                return;
            }

            
            _bossScript.countHits++;
            _bossScript.invincible = true;
            
            if(_bossScript.countHits == 3)
            {
                _bossScript.Fix();
            }
        }

        Destroy(gameObject);
    }

    
}
