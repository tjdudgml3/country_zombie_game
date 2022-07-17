using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*********************************
 * �������ۼ�
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
        progressbar.fillAmount = progress / 100;//�ε�ó�� ���̰Բ� ���α׷��� �ٸ� ���ʺ��� ä���.
        if (progress >= 100)
        {
            SceneManager.LoadScene("menuScene");
        }
    }
}
