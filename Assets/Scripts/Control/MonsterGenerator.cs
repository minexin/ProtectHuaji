using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public float GenerateInterval = 1f;
    public Vector3 position;
    public GameObject[] monstersKind;
    private bool run = false;
    private int monsterNum;
    private int monsterCurrentNum;
    private float timer = 0;

    void Start()
    {
        run=true;
        position = transform.position;
        monsterNum = 0;
        monsterCurrentNum = 0;
    }

    void FixedUpdate()
    {
        if (run&&GlobalSceneGame.Running)
        {
            timer += Time.fixedDeltaTime;
            if(timer >= GenerateInterval)
            {
                GenerateMonster();
                timer = 0;
            }
        }
    }

    public void LevelUp()
    {
        foreach(var i in monstersKind){
            i.GetComponent<Monster>().nearDamage++;
        }
    }

    void GenerateMonster(){
        if(Random.Range(0f,1f)<0.7){
            GameObject themonster = Instantiate(monstersKind[1],position,Quaternion.identity);
            themonster.GetComponent<Monster>().SetGenerator(this);
        }else{
            GameObject themonster = Instantiate(monstersKind[0],position,Quaternion.identity);
            themonster.GetComponent<Monster>().SetGenerator(this);
        }
        monsterNum++;
        monsterCurrentNum++;
    }

    public void RunIt(){
        run = true;
    }
    public void StopIt(){
        run = false;
    }
    public int GetMonsterNum(){
        return monsterNum;
    }
    public int GetCurrentNum(){
        return monsterCurrentNum;
    }
    public void NoticeKilled(){
        monsterCurrentNum--;
    }
}
