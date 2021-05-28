using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening_to_StartMenu_SceneControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //オープニングの長さに合わせて、秒数を調節する        
        Invoke("ChangeScene",10.0f);     //仮で10fにしています
    }

    // Update is called once per frame
    void Update()
    {
        //DS4コントローラの十字とLRスティック以外のボタンを押すと、オープニングからスタートメニューへ
        if (Input.anyKey)
        {
            SceneManager.LoadScene("StartMenuScene");
        }
    }

    //スタートメニューへ遷移する関数
    void ChangeScene()
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}