using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**********************
 * 양진우 작성
 * 
 * ***************/
public class Item : MonoBehaviour
{
    public static Item Instance;
    int healAmount = 20;
    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 15f * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController playercontroller = other.GetComponent<PlayerController>();

            if(playercontroller != null)
            {
                playercontroller.GetHeal(healAmount);
            }

            Destroy(gameObject);
        }
    }

}
