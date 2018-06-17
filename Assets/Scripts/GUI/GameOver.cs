using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject btnObj1 = GameObject.Find("Upload");//"Button"为你的Button的名称  
        Button btn1 = btnObj1.GetComponent<Button>();  
        btn1.onClick.AddListener(delegate ()  
        {  
            this.GoNextScene1(btnObj1);  
        });  
        GameObject btnObj2 = GameObject.Find("Return");//"Button"为你的Button的名称  
        Button btn2 = btnObj2.GetComponent<Button>();  
        btn2.onClick.AddListener(delegate ()  
        {  
            this.GoNextScene2(btnObj2);  
        });  
        GameObject btnObj3 = GameObject.Find("Restart");//"Button"为你的Button的名称  
        Button btn3 = btnObj3.GetComponent<Button>();  
        btn3.onClick.AddListener(delegate ()  
        {  
            this.GoNextScene3(btnObj3);  
        });  
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GoNextScene1(GameObject NScene)  
    {  
        SceneManager.LoadScene("upload"); 
    }  

    public void GoNextScene2(GameObject NScene)  
    {  
        SceneManager.LoadScene("start"); 
    }  

    public void GoNextScene3(GameObject NScene)  
    {  
        SceneManager.LoadScene("game"); 
        Utils.Open();
    }  
}
