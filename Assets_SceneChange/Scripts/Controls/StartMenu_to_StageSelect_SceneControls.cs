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
        //DS4�R���g���[���̏\����LR�X�e�B�b�N�ȊO�̃{�^���������ƁA�X�^�[�g���j���[����X�e�[�W�Z���N�g��
        if (Input.anyKey)
        {
            SceneManager.LoadScene("StageSelectScene");
        }
    }
}
