using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**********************
 * 양진우 작성
 * 
 * ***************/
public class PlayerBullet : MonoBehaviour
{
    public static PlayerBullet Instance;
    public float speed = 8f;
    private Rigidbody bulletRigidbody;
   
    public int damage = 30;
    public bool rocket;
   
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

        rocket = false;
      
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
                
            Destroy(gameObject);                
                
         }
        else if (other.tag == "BulletSpawner")
        {
            BulletSpawner spawner = other.GetComponent<BulletSpawner>();
            if (spawner != null)
            {
                spawner.GetDamage(damage);
            }                
            Destroy(gameObject);                          
            
        }
        else if (other.tag == "MeleeMonster")
        {
            MeleeMonster MMonster = other.GetComponent<MeleeMonster>();
            if (MMonster != null)
            {
                MMonster.GetDamage(damage);
            }
            Destroy(gameObject);
        }
             
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void changedamage(int dam)
    {
        damage = dam;
    }

    public void changebool(bool bl)
    {
        rocket = bl;
    }
}
