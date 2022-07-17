using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**********************
 * ¾ç¿ìÃ¶ÀÛ¼º
 * 
 * ***************/
public class monsterArm : MonoBehaviour
{
    public int damage;
    public float clock = 0;
    public float prevTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if ((clock - prevTime > 0.45f) && other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.GetDamage(damage);
                prevTime = clock;
                // playerController.Die(); die when hit
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        clock += Time.deltaTime;
    }
}
