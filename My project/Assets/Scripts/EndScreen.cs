using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public RubyController rC;
    public TextMeshProUGUI endScreen;
    

    // Start is called before the first frame update
    void Start()
    {
        endScreen.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        Display();
    }


    private void Display()
    {
        if(rC.scoreNum == 3)
        {
        
            endScreen.text = "You Win! Group # 31";
            rC.animator.SetBool("EndGame", true);
            rC.speed = 0;

            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("MainScene");
            }
        }

        if(rC.health == 0)
        {
            
            endScreen.text = "You Lose! Press R to restart";

            rC.animator.SetBool("EndGame", true);
            //rC.gameObject.SetActive(false);

            rC.speed = 0;

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    
}
