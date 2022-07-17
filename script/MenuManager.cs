using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/**********************
 * ������ �ۼ�
 * ������ �ۼ� total���� ���� ��.
 * ***************/
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public Button start;
    
    public GameObject james_lock;//Jamesĳ���Ͱ� ��������� �˸��� ���ӿ�����Ʈ
    public GameObject tom_lock;//Tomĳ���Ͱ� ��������� �˸��� ���ӿ�����Ʈ
    public static int playernumber = 1;//� ĳ���͸� �÷��� ���� �˷��ִ� ����
    public int totalcoin;//������ ����Ʈ(�ٸ� ĳ����ī�带 ���� �ʿ���)
    private int james_coin;//���ӽ��� �÷����ϱ� ���� ����Ʈ ����
    private int Tom_coin;//���� �÷����ϱ����� ����Ʈ����

    void Awake()
    {
        MenuManager Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        james_coin = 500;
        Tom_coin = 200;
        totalcoin = PlayerPrefs.GetInt("TotalCoin"); //�����鿡�� ���ݱ��� ������ ����Ʈ�� start()�Լ����� �޾ƿ�
        playernumber = 1;
        if (totalcoin > james_coin) //����Ʈ�� 500�� ������ Jamse_lock�� set false�� �Ͽ� �÷����ϰ���
        {
            james_lock.SetActive(false);
        }
        if (totalcoin > Tom_coin)//����Ʈ�� 500�� ������ Tom_lock�� set false�� �Ͽ� �÷����ϰ���
        {
            tom_lock.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startbutton()//�÷����ѹ��� 1�� �ϰ� �÷��̾����� �Ѿ
    {
        playernumber = 1;
        SceneManager.LoadScene("PlayScene");
    }
    public void startbutton2()
    {
        if(totalcoin > Tom_coin)//�÷����ѹ��� 2�� �ϰ� �÷��̾����� �Ѿ���� ����Ʈ�� �Ѿ������ Ŭ������
        {
            playernumber = 2;
            SceneManager.LoadScene("PlayScene");
        }
        
    }
    public void startbutton3()//�÷��� 3
    {
        if (totalcoin > james_coin)
        {
            playernumber = 3;
            SceneManager.LoadScene("PlayScene");
        }
        
    }
}
