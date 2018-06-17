using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class RecordReturn : MonoBehaviour {


    public Text Name;
    public Text Score;
	// Use this for initialization
	void Start () {
		GameObject btnObj1 = GameObject.Find("Return");//"Button"为你的Button的名称  
        Button btn1 = btnObj1.GetComponent<Button>();  
        btn1.onClick.AddListener(delegate ()  
        {  
            this.GoNextScene1(btnObj1);  
        }); 
		//从数据库获取前十名单
		GetLeaderBoard();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))  
            Application.Quit(); 
	}

	public void GoNextScene1(GameObject NScene)  
    {  
        SceneManager.LoadScene("start"); 
    }  

	

    public void GetLeaderBoard(){
		StartCoroutine(GetLeaderBoardThread());
	}

	private IEnumerator GetLeaderBoardThread()
    {
		string url = "http://120.79.49.30:8080/huaji/leaderboard";
		
        using (UnityWebRequest www = UnityWebRequest.Get(url))  
        {
            yield return www.Send();
            if (www.error != null)  
            {
				// 网络连接错误, 执行异常处理
                Name.text = "网络连接失败！";
            }
            else if (www.responseCode == 200)
            {
				string str = www.downloadHandler.text;
                string str1 = "";
                string str2 = "";
                Records data = JsonUtility.FromJson<Records>(str);
				for (var i = 0; i < data.records.Count; i++)
                {
                    var record = data.records[i];
                    if(i == data.records.Count-1){
                        str1 += Order[i] + ' ' + record.name;
                        str2 += record.score.ToString();
                    }else{
                        str1 += Order[i] + ' ' + record.name + "\n";
                        str2 += record.score.ToString() + "\n";
                    }
                    Name.text = str1;
                    Score.text = str2;
                }
            }else{
                Name.text = "网络连接失败！";
            }
        }
    }

    static public string[] Order = new string[]{
        "第一名","第二名","第三名","第四名","第五名","第六名","第七名","第八名","第九名","第十名"
    };

}
