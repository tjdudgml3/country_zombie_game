using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
 서영희작성.
    */
public class ButtonEvent : MonoBehaviour
{
    
    public GameObject itemImage; //아이템 설명이미지
    public GameObject monsterImage;//몬스터 설명이미지
    public GameObject character_card;//캐릭터 선택화면 이미지
    public GameObject button_obj;// 오케이 버튼 Active하게하는 게임오브젝트
    public GameObject weaponImage;//무기 설명이미지
    public Button next;//오케이 버튼
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

    public void onclick()//오케이 버튼을 클릭하면 모든 설명은 setActive(false)안보이게끔하고, 캐릭터카드를 보이게함
    {
        character_card.SetActive(true);
        itemImage.SetActive(false);
        monsterImage.SetActive(false);
        button_obj.SetActive(false);
        weaponImage.SetActive(false);

    }
    public void onclick_item()//아이템설명 버튼을 클릭시 아이템설명
    {
        character_card.SetActive(false);
        itemImage.SetActive(true);
        monsterImage.SetActive(false);
        button_obj.SetActive(true);
        weaponImage.SetActive(false);
    }
    public void onclick_monster()//몬스터 설명버튼을 클릭시 몬스터 설명
    {
        character_card.SetActive(false);
        itemImage.SetActive(false);
        monsterImage.SetActive(true);
        button_obj.SetActive(true);
        weaponImage.SetActive(false);
    }
    public void onclick_weapon()//무기 설명버튼을 클릭시 무기 설명
    {
        character_card.SetActive(false);
        itemImage.SetActive(false);
        monsterImage.SetActive(false);
        button_obj.SetActive(true);
        weaponImage.SetActive(true);
    }
}
