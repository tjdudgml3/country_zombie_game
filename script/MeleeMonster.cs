using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/**********************
 * 양우철 작성
 * 체력관련 조성원
 * ***************/
public class MeleeMonster : MonoBehaviour
{
    public static MeleeMonster Instance;

    protected Transform target;

    public GameObject ArmLeft;
    public GameObject ArmRight;
    public int maxhp;
    public int hp;
    public HPBar hpbar;
    //public GameObject level;
    public float dist;

    public bool isMoving = false;
    protected NavMeshAgent nvAgent;
    Animator animator;
    PlayerController playerController;

    BoxCollider colliderArmLeft;
    BoxCollider colliderArmRight;


    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(MonsterAI());
        nvAgent = GetComponent<NavMeshAgent>();
        playerController = FindObjectOfType<PlayerController>();

        colliderArmLeft = ArmLeft.GetComponent<BoxCollider>();
        colliderArmRight = ArmRight.GetComponent<BoxCollider>();

        if (this.transform.localScale.x > 1f)
        {
            animator.SetBool("isTanker", true);
        }
    }
    protected IEnumerator MonsterAI()
    {
        while (hp > 0)
        {
            yield return new WaitForSeconds(0.2f);

            if (isMoving)
            {
                nvAgent.destination = target.position;
                nvAgent.isStopped = false;
                // animator.SetBool("isMoving", true);
            }
            else
            {
                nvAgent.isStopped = true;
                //  animator.SetBool("isMoving", false);
            }
        }

    }
    public virtual void GetDamage(int damage)
    {
        if (hp <= 0)
            return;

        hp -= damage;
        hpbar.SetHP(hp, maxhp);
        //Debug.Log("BulletSpawner:" + hp);
        if (hp <= 0)
        {
            //meObject.SetActive(false);
            // animator.SetTrigger("Die");
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.DieMeleeMonster(gameObject);
            Destroy(gameObject, 0f); //1초뒤에 삭제

        }
    }
    public void AttackStart()
    {
        colliderArmLeft.enabled = true;
        colliderArmRight.enabled = true;
    }
    public void AttackEnd()
    {
        colliderArmLeft.enabled = false;
        colliderArmRight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
            return;

        if (playerController != null)
        {
            if (Vector3.Distance(this.transform.position, playerController.transform.position) < dist)
            {
                animator.SetBool("canAttack", true);
                //playerController.GetDamage(damage);
                // playerController.Die(); die when hit
            }
            else
            {
                animator.SetBool("canAttack", false);
            }
        }
    }
}
