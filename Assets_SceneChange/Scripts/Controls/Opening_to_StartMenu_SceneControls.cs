using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening_to_StartMenu_SceneControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //�I�[�v�j���O�̒����ɍ��킹�āA�b���𒲐߂���        
        Invoke("ChangeScene",10.0f);     //����10f�ɂ��Ă��܂�
    }

    // Update is called once per frame
    void Update()
    {
        //DS4�R���g���[���̏\����LR�X�e�B�b�N�ȊO�̃{�^���������ƁA�I�[�v�j���O����X�^�[�g���j���[��
        if (Input.anyKey)
        {
            SceneManager.LoadScene("StartMenuScene");
        }
    }

    //�X�^�[�g���j���[�֑J�ڂ���֐�
    void ChangeScene()
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}