using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour {

    public MonsterGenerator[] Generators;
    public GameObject boss1;
    public float LevelTimeLimit = 60;
    public GameObject supplyPoint;
    private float timer = 0;
    private int level = 0;
    private bool run = false;
    private bool bossOpen = false;

	// Use this for initialization
	void Start () {
        run = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(Input.GetKeyDown(KeyCode.Escape))  
            Application.Quit(); 
        if(run&&GlobalSceneGame.Running){
            timer += Time.deltaTime;
            //level up//补给点出现//出boss
            if (timer >= TimeCaculate(LevelTimeLimit)) {
                NextLevel();
                ShowLevelUp();
                timer = 0;
            }
            //隐藏level up
            if(timer >= 2){
                HideLevelUp();
            }
            //补给点消失
            if(timer >= 10){
                supplyPoint.SetActive(false);
            }
            //判断捡到补给
            if(timer<10&&IsGetSupply()){
                GetTheSupply();
            }
            //控制普通怪物生成器状态
            ControlGenerators();
            if(bossOpen){
                GenerateBoss();
                bossOpen = false;
            }
        }
	}

    private void ControlGenerators(){
        foreach(var i in Generators){
            if(i.GetMonsterNum() < AllGeneratorMonsterNum(level)){
                i.RunIt();
            }
            if(i.GetMonsterNum() >= AllGeneratorMonsterNum(level)){
                i.StopIt();
            }
        }
    }
    private void GenerateBoss(){
        Instantiate(boss1,new Vector3(0,0,0),Quaternion.identity);
    }

    private float TimeCaculate(float t){
        return t*(Mathf.Log(level+1)+1);
    }
    private int AllGeneratorMonsterNum(int l){
        return (l+1)*(l+2);
    }

    private void ShowLevelUp(){
        GuiGame gui = GameObject.Find("Gui Canvas").gameObject.GetComponent<GuiGame>();
        gui.ShowLevelUp(level);
    }
    private void HideLevelUp(){
        GuiGame gui = GameObject.Find("Gui Canvas").gameObject.GetComponent<GuiGame>();
        gui.HideLevelUp();
    }
    private void NextLevel()
    {
        level++;
        // gui.ShowLevel(level);
        supplyPoint.transform.position = new Vector2(Random.Range(-25f,25f),Random.Range(-15f,15f));
        supplyPoint.SetActive(true);
        foreach(var i in Generators){
            if(level % 5==0&&level!=0){
                i.LevelUp();
                //生成boss
                bossOpen = true;
            }
        }
    }

    public void StopAll(){
        run = false;
        foreach(var i in Generators){
            i.StopIt();
        }
    }

    bool IsGetSupply(){
        return Vector3.Distance(GameObject.Find("Player").gameObject.transform.position,supplyPoint.transform.position)<=1;
    }

    void GetTheSupply(){
        GunEntity _temp = GameObject.Find("GunEntity").gameObject.GetComponent<GunEntity>();
        _temp.ammo[0]=_temp.ammoMax[0];
        _temp.ammo[1]=_temp.ammoMax[1];
        _temp.ammo[2]=_temp.ammoMax[2];
        GuiGame gui = GameObject.Find("Gui Canvas").gameObject.GetComponent<GuiGame>();
        gui.RefreshGunAmmo();
        supplyPoint.SetActive(false);
    }
}
