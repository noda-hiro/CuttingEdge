using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu_to_StageSelect_SceneControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DS4コントローラの十字とLRスティック以外のボタンを押すと、スタートメニューからステージセレクトへ
        if (Input.anyKey)
        {
            SceneManager.LoadScene("StageSelectScene");
        }
    }
}
