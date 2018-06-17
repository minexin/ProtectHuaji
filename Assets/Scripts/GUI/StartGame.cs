using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject btnObj1 = GameObject.Find("Start");//"Button"为你的Button的名称  
        Button btn1 = btnObj1.GetComponent<Button>();  
        btn1.onClick.AddListener(delegate ()  
        {  
            this.GoNextScene1(btnObj1);  
        });  
        GameObject btnObj2 = GameObject.Find("Record");//"Button"为你的Button的名称  
        Button btn2 = btnObj2.GetComponent<Button>();  
        btn2.onClick.AddListener(delegate ()  
        {  
            this.GoNextScene2(btnObj2);  
        });  
        GameObject btnObj3 = GameObject.Find("End");//"Button"为你的Button的名称  
        Button btn3 = btnObj3.GetComponent<Button>();  
        btn3.onClick.AddListener(delegate ()  
        {  
            this.End(btnObj3);  
        });  

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))  
            Application.Quit(); 
	}

	public void GoNextScene1(GameObject NScene)  
    {  
        SceneManager.LoadScene("game"); 
        Utils.Open();
    }  

    public void GoNextScene2(GameObject NScene)  
    {  
        SceneManager.LoadScene("record"); 
    }  

    public void End(GameObject NScene)  
    {  
        Application.Quit();
    }  

}
