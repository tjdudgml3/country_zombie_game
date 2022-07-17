using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/******** 
 
 update() �Լ��� å�� �����Ͽ���, GetDamage() �Լ� ������ �ۼ�, ������ ���ö �ۼ�. 

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

    public bool isMoving = false; //���� �������� �ƴϸ� �Ѿư��鼭 ���ݻ��������� �Ǵ��ϴ� ����
    private NavMeshAgent nvAgent; // �׺���̼��� ���� ����
    

    void Awake()
    {
        Instance = this;
    }
    

    void Start()
    {
        timerAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        target = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(MonsterAI());//���� AIȰ��ȭ
        nvAgent = GetComponent<NavMeshAgent>(); 
       
    }
    // �ڷ�ƾ���� ����� ���� AI, ���Ͱ� ��� �ִ� ���� 0.2�ʾ� ��� �����ٰ�
    // �����̴� ���°� ���̸� �÷��̾ �Ѿư���, �����̸� ����������
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
            Destroy(gameObject, 0f); //1�ʵڿ� ����

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
