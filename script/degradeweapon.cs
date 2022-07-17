using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*양진우 작성*/
public class degradeweapon : MonoBehaviour
{
    public static degradeweapon Instance;
    //int weapon = 0;
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
        //weapon = Random.Range(0, 4);
        
        transform.Rotate(0f, 15f * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController playercontroller = other.GetComponent<PlayerController>();

            if(playercontroller != null)
            {
                playercontroller.DegradeWeapon(1);
            }

            Destroy(gameObject);
        }
    }

}
