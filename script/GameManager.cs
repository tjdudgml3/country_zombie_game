using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameoverText;
    public Text recordText;
    public Text timeText;
    public Text buffText;
    public Text HPText;
    public GameObject bullerSpawnerPrefab;
    public GameObject MeleeMonsterPrefab;
    public GameObject TankerPrefab;
    public GameObject itemPrefab;
    public GameObject DitemPrefab;
    public GameObject weaponitemPrefab;
    public GameObject upgradeitemPrefab;
    public GameObject level; // �ҷ��� ���� ������ ����
    public GameObject bossPrefab;

    public GameObject Pistolimage;
    public GameObject Machineimage;
    public GameObject Laserimage;
    public GameObject Rocketimage;
   

    int prevTime;
    int weaponprevTime;
    //int spawnCounter = 0;
    private float surviveTime;
    private bool isGameover;

    bool isEvent = false;
    float eventCountTime;

    //public int coin;
    public Text coinText;
    int prevBosstime;
    int prevEventTime;
    int prevweaponEventTime;
    int prevSpawnTime;
    float eventTime;
    float weaponeventTime;

    public GameObject PlayerA;
    public GameObject PlayerB;
    public GameObject PlayerC;
    public GameObject PlayerAface;
    public GameObject PlayerBface;
    public GameObject PlayerCface;

    public GameObject warning;

    //setactive�����ϱ�

    //����Ʈ & ���ʸ��� ����ؾ���!
    List<GameObject> weaponitemList = new List<GameObject>();
    List<GameObject>upgradeitemList = new List<GameObject>();
    List<GameObject> itemList = new List<GameObject>();
    List<GameObject> DitemList = new List<GameObject>();
    List<GameObject> spawnerList = new List<GameObject>();
    List<GameObject> MMonsterList = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        
        initiatePlayer();
        surviveTime = 0;
        isGameover = false;
        prevTime = -1;
        weaponprevTime = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (!isGameover)  //������ �ۼ�
        {
            GamePlayTime();//������ �Լ�
            BuffUI();//������ �Լ�
            HPUI();//������ �Լ�
            WeaponUI();//������ �Լ�
            /*******************************************
         * ���� ���� �˰��� ���ö �ۼ�
         * 
         * *****************************************/
            int currTime = (int)(surviveTime % 5f);
            //Debug.Log(prevTime + ", " + currTime);
            //5�ʸ��� �ҷ����ĳ��߰�!
            if (currTime == 0 && prevTime != currTime)
            {

                int repeatnum = (int)(surviveTime / 15)+1; //15�ʸ��� �߰� ����
                //if (repeatnum >= 5) repeatnum = 4; //���û�������
                for (int i = 0; i < repeatnum ;i++)
                {
                    int rand = (int)Random.Range(0f, 10f);

                    if (rand < 6)
                    {
                        Vector3 randposBullet = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
                        GameObject bulletSpawner = Instantiate(bullerSpawnerPrefab, randposBullet, Quaternion.identity);
                        bulletSpawner.transform.parent = level.transform;
                        bulletSpawner.transform.localPosition = randposBullet;
                        spawnerList.Add(bulletSpawner); // �ҷ����ĳ��߰� ���Ÿ�
                    }
                    else if (rand < 9)
                    {
                        Vector3 randposMMonster = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
                        GameObject MMonster = Instantiate(MeleeMonsterPrefab, randposMMonster, Quaternion.identity);
                        //MMonster.transform.parent = level.transform;
                        MMonster.transform.localPosition = randposMMonster;
                        MMonsterList.Add(MMonster); // �����߰� �ٰŸ�
                    }
                    else
                    {
                        Vector3 randposTanker = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
                        GameObject Tanker = Instantiate(TankerPrefab, randposTanker, Quaternion.identity);
                        //MMonster.transform.parent = level.transform;
                        Tanker.transform.localPosition = randposTanker;
                        MMonsterList.Add(Tanker); // ��Ŀ�߰� ��Ŀ
                    }
                }
            
            }
            prevTime = currTime;

            int currTimeB = (int)(surviveTime % 60f);
            if (currTimeB == 0 && prevBosstime != currTime)
            {
                if (surviveTime != 0)
                {
                    Vector3 randposBoss = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
                    GameObject Boss = Instantiate(bossPrefab, randposBoss, Quaternion.identity);

                    Boss.transform.localPosition = randposBoss;
                    spawnerList.Add(Boss); // Boss�߰� 
                    warning.SetActive(false);
                    
                }
            }
            if (currTimeB == 57 && prevBosstime != currTime)
            {
                if (surviveTime != 0)
                {
                    warning.SetActive(true);
                }
            }

            prevBosstime = currTimeB;

            Create_items();//������ �Լ�

            Delete_items();//������ �Լ�

           
            /////////////////////////////////////////////////////////////////////////////////////
            int spawnertime = (int)(surviveTime % 2f);
            if (spawnertime == 0 && prevSpawnTime != spawnertime)
            {
                // �ҷ� ���ĳʵ��� �Ѿƿ��� �Ѵ�.
                foreach (GameObject spawner in spawnerList)
                {
                    spawner.GetComponent<BulletSpawner>().isMoving = true;
                }
                foreach (GameObject MMonster in MMonsterList)
                {
                    MMonster.GetComponent<MeleeMonster>().isMoving = true;
                }
                foreach (GameObject Tanker in MMonsterList)
                {
                    Tanker.GetComponent<MeleeMonster>().isMoving = true;
                }
                foreach (GameObject Boss in spawnerList)
                {
                    Boss.GetComponent<BulletSpawner>().isMoving = true;
                } 

                isEvent = true;
                eventCountTime = 0f;
            }
            prevSpawnTime = spawnertime;

            eventCountTime += Time.deltaTime;

            if (isEvent && eventCountTime > 2f)
            {
                eventCountTime = 0f;
                isEvent = false;

                foreach (GameObject spawner in spawnerList)
                {
                    spawner.GetComponent<BulletSpawner>().isMoving = false;
                }
                foreach (GameObject MMonster in MMonsterList)
                {
                    MMonster.GetComponent<MeleeMonster>().isMoving = false;
                }
                foreach (GameObject Tanker in MMonsterList)
                {
                    Tanker.GetComponent<MeleeMonster>().isMoving = false;
                }
                foreach (GameObject Boss in spawnerList)
                {
                    Boss.GetComponent<BulletSpawner>().isMoving = false;
                }
            }
            //////////////////////////////////////////////////////////////////////////////////���ö �ۼ��ڵ� �������
        }
        else
        {
            
            restartgame();//������ �Լ�
        }


    }

    #region Jinoo create_item
    /*******************************************
         * ������ ������ ���� �Լ� �ۼ�
         * 
         * *****************************************/
    public void Create_items()
    {
        //create weapon item
        int weapTime = (int)(surviveTime % 5f);
        
        if (surviveTime>1 && weapTime == 0 && weaponprevTime != weapTime)
        {
            Vector3 randposWeapItem = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            GameObject weapitem = Instantiate(weaponitemPrefab, randposWeapItem, Quaternion.identity);
            weapitem.transform.parent = level.transform;
            weapitem.transform.localPosition = randposWeapItem;
            weaponitemList.Add(weapitem); // �������߰�
                                        
            Vector3 randposupgradeItem = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            GameObject upgradeitem = Instantiate(upgradeitemPrefab, randposupgradeItem, Quaternion.identity);
            upgradeitem.transform.parent = level.transform;
            upgradeitem.transform.localPosition = randposupgradeItem;
            weaponitemList.Add(upgradeitem); // �������߰�
                                                                    
            Vector3 randposItem = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            GameObject item = Instantiate(itemPrefab, randposItem, Quaternion.identity);
            item.transform.parent = level.transform;
            item.transform.localPosition = randposItem;
            itemList.Add(item); // �������߰�

            Vector3 randposDItem = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            GameObject Ditem = Instantiate(DitemPrefab, randposDItem, Quaternion.identity);
            Ditem.transform.parent = level.transform;
            Ditem.transform.localPosition = randposDItem;
            itemList.Add(Ditem); // �������߰�


        }
        weaponprevTime = weapTime;
    }
    #endregion
    #region Jinoo delete item
    public void Delete_items()
    {
       ////////////delete weapon item
        int weaponeventTime = (int)(surviveTime % 10f);
        if (weaponeventTime == 0 && prevweaponEventTime != weaponeventTime)
        {
            // �����۵��� ��� ����!
            foreach (GameObject weaponitem in weaponitemList)
            {

                Destroy(weaponitem);
            }
            weaponitemList.Clear();
            ////
            foreach (GameObject upgradeitem in upgradeitemList)
            {

                Destroy(upgradeitem);
            }
            upgradeitemList.Clear();
            ////
            foreach (GameObject item in itemList)
            {

                Destroy(item);
            }
            itemList.Clear();
            foreach (GameObject Ditem in DitemList)
            {

                Destroy(Ditem);
            }
            DitemList.Clear();

        }
        prevweaponEventTime = weaponeventTime;
    }
    #endregion
    /*******************************************
         * UI���� �Լ� ������ �ۼ�.
         * 
         * *****************************************/
    #region SW gameplaytime
    public void GamePlayTime()//�������ۼ�
    {
        surviveTime += Time.deltaTime;
        timeText.text = "Time: " + (int)surviveTime;
    }
    #endregion  
    #region SW endgmae,besttime
    public void EndGame()//�������ۼ�
    {
        
        isGameover = true;
        gameoverText.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if (surviveTime > bestTime)
        {
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        recordText.text = "Best Time:" + (int)bestTime;
        CoinUI();
        HPText.text = "0/100";
    }
    #endregion
    #region SW restart
    public void restartgame()//�������ۼ�
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("menuScene"); //loat scene
        }
    }
    #endregion

    //���ö �ۼ�
    public void DieBulletSpawner(GameObject spawner)
    {
        spawnerList.Remove(spawner);
    }
    public void DieMeleeMonster(GameObject MMonster)
    {
        MMonsterList.Remove(MMonster);
    }



    #region SW BuffUI
    public void BuffUI()//�������ۼ�
    {
        if (PlayerController.Instance.upgradenumber < 4)
        {
            buffText.text = "WEAPON LV." + (int)(PlayerController.Instance.upgradenumber + 1);
        }
        else
        {
            buffText.text = "WEAPON LV.MAX" ;
        }
    }
    #endregion
    #region SW HPUI
    public void HPUI()//������ �ۼ�
    {
        HPText.text = PlayerController.Instance.hp + "/" + PlayerController.Instance.maxhp;
    }
    #endregion
    #region SW CoinUI
    
    public void CoinUI()//�������ۼ�
    {
        int totalcoin = PlayerPrefs.GetInt("TotalCoin");

        totalcoin = totalcoin + (int)(surviveTime / 10);       

        PlayerPrefs.SetInt("TotalCoin", totalcoin);

        coinText.text = "Total Coin : " + totalcoin;
    }
    #endregion
    #region SW WeaponUI
    public void WeaponUI()//�������ۼ�
    {
        if(PlayerController.Instance.weaponnum == 0)
        {
            Pistolimage.SetActive(true);
            Machineimage.SetActive(false);
            Laserimage.SetActive(false);
            Rocketimage.SetActive(false);
        }
        if (PlayerController.Instance.weaponnum == 1)
        {
            Pistolimage.SetActive(false);
            Machineimage.SetActive(true);
            Laserimage.SetActive(false);
            Rocketimage.SetActive(false);
        }
        if (PlayerController.Instance.weaponnum == 2)
        {
            Pistolimage.SetActive(false);
            Machineimage.SetActive(false);
            Laserimage.SetActive(true);
            Rocketimage.SetActive(false);
        }
        if (PlayerController.Instance.weaponnum == 3)
        {
            Pistolimage.SetActive(false);
            Machineimage.SetActive(false);
            Laserimage.SetActive(false);
            Rocketimage.SetActive(true);
        }
    }
    #endregion
    #region Jinoo initiatePlayer
    //�޴�â���� ���ϴ� ĳ���� ���� �ش� ĳ���� Ȱ��ȭ
    public void initiatePlayer()//������ �ۼ�
    {       
       if(MenuManager.playernumber == 2)
        {
            PlayerA.SetActive(false);
            PlayerB.SetActive(true);
            PlayerAface.SetActive(false);
            PlayerBface.SetActive(true);
        }
        else if (MenuManager.playernumber == 3)
        {
            PlayerA.SetActive(false);
            PlayerC.SetActive(true);
            PlayerAface.SetActive(false);
            PlayerCface.SetActive(true);
        
        }

    }
    #endregion 



}
