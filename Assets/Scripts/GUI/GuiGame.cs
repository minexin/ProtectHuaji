using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiGame : MonoBehaviour
{

    public Text textHp;
    public Text textScore;
    public GameObject[] switchButtons;
    public Text[] textAmmos;
    public GameObject gameOver;
    public GameObject levelUp;

    // Use this for initialization
    void Start()
    {
        UseGun(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTextHp(int hp, int hpMax)
    {
        textHp.text = string.Format("{0} / {1}", hp, hpMax);
    }

    public void SetTextScore(int score)
    {
        textScore.text = string.Format("{0}",score);
    }
    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void NotUseGun(int index)
    {
        switchButtons[index].GetComponent<Image>().color = Color.white;
    }

    public void UseGun(int index)
    {
        switchButtons[index].GetComponent<Image>().color = Color.blue;
    }

    public void SetGunAmmo(int index, int ammo)
    {
        textAmmos[index].text = ammo.ToString();
        if (ammo == 0) textAmmos[index].color = Color.red;
    }

    public void RefreshGunAmmo(){
        textAmmos[1].text = "999"; 
        textAmmos[1].color = Color.white;
        textAmmos[2].text = "99";
        textAmmos[2].color = Color.white;
    }

    public void ShowLevelUp(int level){
        levelUp.GetComponent<Text>().text = string.Format("level {0}",level);
        levelUp.SetActive(true);
    }

    public void HideLevelUp(){
        levelUp.SetActive(false);
    }
}
