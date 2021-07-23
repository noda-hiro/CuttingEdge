using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheck : MonoBehaviour
{
    private string floorTag = "Floor";  //Floor�^�O�p
    private bool isFloor = false;       //������
    private bool isFloorEnter, isFloorStay, isFloorExit;    //(���ɓ������@���ɂ���@������o��)�t���O
    [SerializeField]
    private PlayerControl p;
    public AudioSource audio;
    public AudioClip audioClip;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    //�ڒn�����Ԃ����\�b�h
    //��������̍X�V���ɌĂԕK�v������
    public bool IsFloor()
    {
        if (isFloorEnter || isFloorStay)
        {
            isFloor = true;
        }
        else if (isFloorExit)
        {
            isFloor = false;
        }

        isFloorEnter = false;
        isFloorStay = false;
        isFloorExit = false;
        return isFloor;
    }
    //Player��Floor�ɓ�������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == floorTag)
        {
            audio.PlayOneShot(audioClip);
            p.isJump = false;
            p.jumpCount = 0;
            isFloorEnter = true;
            // Debug.Log("1");
        }
    }
    //Player��Floor�Ɍp���I�ɂ����
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == floorTag)
        {
            isFloorStay = true;
            //Debug.Log("2");
        }
    }
    //Player��Floor����o����
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == floorTag)
        {
            isFloorExit = true;
            // Debug.Log("3");
        }
    }
}
