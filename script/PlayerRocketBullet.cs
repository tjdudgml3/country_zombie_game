using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*********************************
 * 양진우 작성
 * 
 * **************************/
public class PlayerRocketBullet : MonoBehaviour
{
    public static PlayerRocketBullet Instance;
    public float speed = 8f;
    private Rigidbody bulletRigidbody;
   
    public int damage = 100;
    public bool rocket;
   
    void Awake()
    {
        Instance = this;
        
    }
    
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();

        bulletRigidbody.velocity = transform.forward * speed;

        

        Destroy(gameObject, 3f);

        rocket = true;
      
    }

    void OnTriggerEnter(Collider other)
    { 
        
            if (other.tag == "Bullet")
            {
                Bullet bullet = other.GetComponent<Bullet>();

                if (bullet != null)
                {
                    Destroy(bullet.gameObject);
                }
               
            }
            else if (other.tag == "BulletSpawner")
            {
                BulletSpawner spawner = other.GetComponent<BulletSpawner>();
                if (spawner != null)
                {
                    spawner.GetDamage(damage);
                }
                         
            
            }
        else if (other.tag == "MeleeMonster")
        {
            MeleeMonster MMonster = other.GetComponent<MeleeMonster>();
            if (MMonster != null)
            {
                MMonster.GetDamage(damage);
            }
                    

        }

    }

    
    void Update()
    {
       
    }

    public void changedamage(int dam)
    {
        damage = dam;
    }

 
}
