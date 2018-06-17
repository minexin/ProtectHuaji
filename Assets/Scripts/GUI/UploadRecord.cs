using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UploadRecord : MonoBehaviour {

    public InputField InputName;
    private int Score;
	// Use this for initialization
	void Start () {
		GameObject btnObj1 = GameObject.Find("Upload");
        Button btn1 = btnObj1.GetComponent<Button>();  
        btn1.onClick.AddListener(delegate ()  
        {  
            this.GoNextScene1(btnObj1);  
        }); 
        Score = GlobalSceneGame.score; 
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))  
            Application.Quit(); 
	}

	public void GoNextScene1(GameObject NScene)  
    {
        SceneManager.LoadScene("record"); 
        SendRecord();
        
    }  


    public void SendRecord()
    {
        StartCoroutine(SendRecordThread());
    }

    private IEnumerator SendRecordThread()
    {
        string url = "http://120.79.49.30:8080/huaji/upload";

        var request = new UnityWebRequest(url, "POST");
        var record = new Record(InputName.text, Score);
        Debug.Log(record.ToJson());

        byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(record.ToJson());
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(postBytes);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Charset", "utf-8");

        yield return request.Send();

        if (request.responseCode == 200)
        {
            string text = request.downloadHandler.text;
        }
    }





}
