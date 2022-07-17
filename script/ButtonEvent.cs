using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
 �������ۼ�.
    */
public class ButtonEvent : MonoBehaviour
{
    
    public GameObject itemImage; //������ �����̹���
    public GameObject monsterImage;//���� �����̹���
    public GameObject character_card;//ĳ���� ����ȭ�� �̹���
    public GameObject button_obj;// ������ ��ư Active�ϰ��ϴ� ���ӿ�����Ʈ
    public GameObject weaponImage;//���� �����̹���
    public Button next;//������ ��ư
    public static ButtonEvent Instance;
 

    void Awake()
    {
        ButtonEvent Instance = this;
    }

    public void getMenuScene()
    {
        //nextScene = sceneName;
        SceneManager.LoadScene("menuScene");
    }

    public void onclick()//������ ��ư�� Ŭ���ϸ� ��� ������ setActive(false)�Ⱥ��̰Բ��ϰ�, ĳ����ī�带 ���̰���
    {
        character_card.SetActive(true);
        itemImage.SetActive(false);
        monsterImage.SetActive(false);
        button_obj.SetActive(false);
        weaponImage.SetActive(false);

    }
    public void onclick_item()//�����ۼ��� ��ư�� Ŭ���� �����ۼ���
    {
        character_card.SetActive(false);
        itemImage.SetActive(true);
        monsterImage.SetActive(false);
        button_obj.SetActive(true);
        weaponImage.SetActive(false);
    }
    public void onclick_monster()//���� �����ư�� Ŭ���� ���� ����
    {
        character_card.SetActive(false);
        itemImage.SetActive(false);
        monsterImage.SetActive(true);
        button_obj.SetActive(true);
        weaponImage.SetActive(false);
    }
    public void onclick_weapon()//���� �����ư�� Ŭ���� ���� ����
    {
        character_card.SetActive(false);
        itemImage.SetActive(false);
        monsterImage.SetActive(false);
        button_obj.SetActive(true);
        weaponImage.SetActive(true);
    }
}
