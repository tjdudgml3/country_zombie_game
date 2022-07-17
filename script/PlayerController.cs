using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**********************
 * 양진우,조성원 공동 작성
 * 
 * ***************/
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private Rigidbody playerRigidbody;
    public float speed ;
    public float rotSpeed = 120.0f;

    private Transform tr;

    public int maxhp;
    public int hp ;
    public HPBar hpbar;

    public float spawnRate; // 플레이어는 0.2초마다 총알 발사! 
    private float timerAfterSpawn;
    public GameObject playerbulletPrefab;
    public GameObject playerrocketbulletPrefab;
    public GameObject machinegunbulletPrefab;
    public GameObject pistolhole;
    public GameObject machinehole;
    public GameObject rockethole;
    public GameObject laserhole;
   

    public GameObject pistol;
    public GameObject machinegun;
    public GameObject lasergun;
    public GameObject rocketgun;


    public int upgradenumber;
    //총 종류
    public int weaponnum;
    // pistol:0
    // machinegun:1
    // lasergun:2
    // rocketlauncher:3
   

    void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
        
        playerRigidbody = GetComponent<Rigidbody>();
        timerAfterSpawn = 0f;
        tr = GetComponent<Transform>();
       // speed = 8f;
        weaponnum = 0;
       
        upgradenumber = 0;

       // fireAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;
        Vector3 newVelocity;
        newVelocity = new Vector3(xSpeed, -8f, zSpeed);
        playerRigidbody.velocity = newVelocity;
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            //Debug.Log();
            Vector3 proejctedPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 currentPos = transform.position;
            Vector3 rotation = proejctedPos - currentPos;
            tr.forward = rotation;
        }

        //총
        /*********************
         * 
         * 4가지 총 구현 양진우 작성
         * 
         * ******************/
        if (weaponnum == 0)//pistol
        {
            machinegun.SetActive(false);
            lasergun.SetActive(false);
            rocketgun.SetActive(false);
            pistol.SetActive(true);
           
            
            int a = 30 + upgradenumber * 10;
            spawnRate = 0.4f;
            playerbulletPrefab.GetComponent<PlayerBullet>().changedamage(a);
            
         

            timerAfterSpawn += Time.deltaTime;

            if (Input.GetButton("Fire1") && timerAfterSpawn >= spawnRate)
            {
                timerAfterSpawn = 0;
                GameObject bullet = Instantiate(playerbulletPrefab, pistolhole.transform.position, pistolhole.transform.rotation);             

                
            }
        }
        if (weaponnum == 1)//machine
        {
            
            lasergun.SetActive(false);
            rocketgun.SetActive(false);
            pistol.SetActive(false);
            machinegun.SetActive(true);
            //Debug.Log("machineGun");
            int a = 20 + upgradenumber * 20;
            spawnRate = 0.1f;
            machinegunbulletPrefab.GetComponent<PlayerBullet>().changedamage(a); 
            timerAfterSpawn += Time.deltaTime;

            if (Input.GetButton("Fire1") && timerAfterSpawn >= spawnRate)
            {
                timerAfterSpawn = 0;
                GameObject bullet = Instantiate(machinegunbulletPrefab, machinehole.transform.position, machinehole.transform.rotation);
            }
        }
        if (weaponnum == 2)//laser
        {           
            rocketgun.SetActive(false);
            pistol.SetActive(false);
            machinegun.SetActive(false);
            lasergun.SetActive(true);

            //Debug.Log("laserGun");
            int a = 2 + upgradenumber * 2;
            spawnRate = 0.01f;

            playerbulletPrefab.GetComponent<PlayerBullet>().changedamage(a);
            timerAfterSpawn += Time.deltaTime;

            if (Input.GetButton("Fire1") && timerAfterSpawn >= spawnRate)
            {
                timerAfterSpawn = 0;

                GameObject bullet = Instantiate(playerbulletPrefab, laserhole.transform.position, laserhole.transform.rotation);

            }
        }
        if (weaponnum  == 3)//rocket
        {            
            pistol.SetActive(false);
            machinegun.SetActive(false);
            lasergun.SetActive(false);
            rocketgun.SetActive(true);
            
            // Debug.Log("rocketlauncher");
            spawnRate = 1.5f - upgradenumber*0.3f;
           

              playerbulletPrefab.GetComponent<PlayerBullet>().changedamage(125); //for ui
           // PlayerBullet.Instance.changebool(true);            
          
            timerAfterSpawn += Time.deltaTime;

            if (Input.GetButton("Fire1") && timerAfterSpawn >= spawnRate)
            {
                timerAfterSpawn = 0;

                GameObject bullet = Instantiate(playerrocketbulletPrefab, rockethole.transform.position, rockethole.transform.rotation);

            }
        }

    }



    public void GetDamage(int damage) //조성원 작성
    {
        hp -= damage;
        hpbar.SetHP(hp, maxhp);
        if (hp <= 0)
        {
            Die();
        }
    }

    public void GetHeal(int heal) //조성원 작성
    {
        hp += heal;
        if (hp > maxhp)
        {
            hp = maxhp;
        }
        hpbar.SetHP(hp, maxhp);
    }
    public void ChangeWeapon()//양진우작성
    {
        while (true)
        {
            int i = (int)Random.Range(1, 4);
            if (weaponnum != i)
            { 
                weaponnum = i; 
                break; 
            }     
        }
        
    }

    public void UpgradeWeapon(int one)//양진우작성
    {
        if (upgradenumber < 4)
        {
            upgradenumber += one;
        }
    }

    public void DegradeWeapon(int one)//양진우작성
    {
       
        if(upgradenumber > 0)
        {
            upgradenumber -= one; 
        }
    }

    void Die()//양진우작성
    {
        gameObject.SetActive(false);

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.EndGame();
    }
}
