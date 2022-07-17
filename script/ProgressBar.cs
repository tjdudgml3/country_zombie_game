using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*********************************
 * 서영희작성
 * 
 * **************************/
public class ProgressBar : MonoBehaviour
{
    public Image progressbar;
    public float progress;

    void Awake()
    {

    }
    
    void Start()
    {
        progress = 0.0f;
    }

    
    void Update()
    {
        progress += 0.25f;
        progressbar.fillAmount = progress / 100;//로딩처럼 보이게끔 프로그레스 바를 왼쪽부터 채운다.
        if (progress >= 100)
        {
            SceneManager.LoadScene("menuScene");
        }
    }
}
