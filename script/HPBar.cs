using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**************************
 * 조성원 작성
 * 
 * *************************/
public class HPBar : MonoBehaviour
{
    public static HPBar Instance;
    public Image hpbar;
    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHP(int hp, int maxhp)
    {
        hpbar.fillAmount = (float)hp / (float)maxhp;
    }
}
