using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public static Bullet Instance;
    public float speed = 8f;
    private Rigidbody bulletRigidbody;
    public int damage = 30;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();

        bulletRigidbody.velocity = transform.forward * speed;

        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if(playerController != null)
            {
                playerController.GetDamage(damage);
               // 양진우
               //playerController.Die(); die when hit에서 수정함
            }
            Destroy(gameObject);
        }
    }

   
}
