using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/******** 
 
 update() 함수는 책을 참고하였고, GetDamage() 함수 조성원 작성, 나머진 양우철 작성. 

 *******/
public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner Instance;
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 0.5f;

    private Transform target;
    private float spawnRate;
    private float timerAfterSpawn;

    public int maxhp = 100;
    public int hp = 100;
    public HPBar hpbar;
    //public GameObject level;

    public bool isMoving = false; //서서 공격인지 아니면 쫓아가면서 공격상태인지를 판단하는 변수
    private NavMeshAgent nvAgent; // 네비게이션을 위한 변수
    

    void Awake()
    {
        Instance = this;
    }
    

    void Start()
    {
        timerAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        target = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(MonsterAI());//몬스터 AI활성화
        nvAgent = GetComponent<NavMeshAgent>(); 
       
    }
    // 코루틴으로 만드는 몬스터 AI, 몬스터가 살아 있는 동안 0.2초씩 잠깐 쉬었다가
    // 움직이는 상태가 참이면 플레이어를 쫓아가고, 거짓이면 정지해있음
    IEnumerator MonsterAI()
    {
        while (hp > 0)
        {
            yield return new WaitForSeconds(0.2f);

            if (isMoving)
            {
                nvAgent.destination = target.position;
                nvAgent.isStopped = false;
               
            }
            else
            {
                nvAgent.isStopped = true;
              
            }
        }

    }


    public void GetDamage(int damage)
    {
        if (hp <= 0)
            return;

        hp -= damage;
        hpbar.SetHP(hp, maxhp);
        
        if (hp <= 0)
        {
            
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.DieBulletSpawner(gameObject);
            Destroy(gameObject, 0f); //1초뒤에 삭제

        }
    }




    
    void Update()
    {
        if (hp <= 0)
            return;

        timerAfterSpawn += Time.deltaTime;

        if (timerAfterSpawn >= spawnRate)
        {
            timerAfterSpawn = 0;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.LookAt(target);
            transform.LookAt(target);
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
           

        }
    }


}
