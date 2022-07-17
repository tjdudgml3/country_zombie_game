using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/**********************
 * 서영희 작성
 * 조성원 작성 total코인 변수 씀.
 * ***************/
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public Button start;
    
    public GameObject james_lock;//James캐릭터가 잠겨있음을 알리는 게임오브젝트
    public GameObject tom_lock;//Tom캐릭터가 잠겨있음을 알리는 게임오브젝트
    public static int playernumber = 1;//어떤 캐릭터를 플레이 할지 알려주는 변수
    public int totalcoin;//게임의 포인트(다른 캐릭터카드를 쓸때 필요함)
    private int james_coin;//제임스를 플레이하기 위한 포인트 갯수
    private int Tom_coin;//톰을 플레이하기위한 포인트갯수

    void Awake()
    {
        MenuManager Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        james_coin = 500;
        Tom_coin = 200;
        totalcoin = PlayerPrefs.GetInt("TotalCoin"); //프리펩에서 지금까지 누적된 포인트를 start()함수에서 받아옴
        playernumber = 1;
        if (totalcoin > james_coin) //포인트가 500개 넘으면 Jamse_lock을 set false를 하여 플레이하게함
        {
            james_lock.SetActive(false);
        }
        if (totalcoin > Tom_coin)//포인트가 500개 넘으면 Tom_lock을 set false를 하여 플레이하게함
        {
            tom_lock.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startbutton()//플레여넘버를 1로 하고 플레이씬으로 넘어감
    {
        playernumber = 1;
        SceneManager.LoadScene("PlayScene");
    }
    public void startbutton2()
    {
        if(totalcoin > Tom_coin)//플레여넘버를 2로 하고 플레이씬으로 넘어가지만 포인트가 넘어가야지만 클릭가능
        {
            playernumber = 2;
            SceneManager.LoadScene("PlayScene");
        }
        
    }
    public void startbutton3()//플레여 3
    {
        if (totalcoin > james_coin)
        {
            playernumber = 3;
            SceneManager.LoadScene("PlayScene");
        }
        
    }
}
