using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    //Script was written by Remond Elia

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RubyController rC = collision.gameObject.GetComponent<RubyController>();
            rC.speed = 5;
            rC.attackForce = 500;
            rC.PowerColor.color = new Color(255, 253, 0);
            rC.hasPowerup = true;
            Destroy(this.gameObject);
        }
    }
}
