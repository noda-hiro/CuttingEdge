using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ItemType2
{
    Dryer,      //�h���C���[
    Atomizer,   //������  
}

public class ItemControl : MonoBehaviour
{
    public HPControl php;               //hpcontrol
    //public bool dryerFlag = false;      
    //public bool atomizerFlag = false;
    private float coolTime = 5f;     //�N�[���_�E���̎���
    private float dcoolTime = 10f;     //�N�[���_�E���̎���
    private bool coolFlag = true;  //�N�[���_�E���t���O
    private bool Dflag = true;
    public ItemType2 nowItem;           //���݂̃A�C�e�����
    public PlayerStatus nowstatus;      //���݂�player�X�e�[�^�X���
    private Image m_Image;              //image
    public Sprite[] m_Sprite;           //sprite
    public float Count = 1f;
    public float nowTime;
    public float dnowTime;

    // Start is called before the first frame update
    void Start()
    {
        m_Image = GetComponent<Image>();    //image�擾
        php = GetComponent<HPControl>();    //Hpcotrol�擾
        dnowTime = dcoolTime;
        nowTime = coolTime;

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(nowTime);
        //"H"����������I������Ă���A�C�e�����g�p����
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (coolFlag == true || Dflag == true)
            {
                //�A�C�e���g�p�Ăяo��
                ItemUse();
                coolFlag = false;
                Dflag = false;
            }
        }
        //"J"����������h���C���[�����݂̃A�C�e����ύX
        if (Input.GetKeyDown(KeyCode.J))
        {
            //image���h���C���[�̉摜�ɂ���
            m_Image.sprite = m_Sprite[0];
            //���݂̃A�C�e�����h���C���[�ɕύX
            nowItem = ItemType2.Dryer;
            Debug.Log(nowItem);
        }
        //"K"���������疶���������݂̃A�C�e����ύX
        if (Input.GetKeyDown(KeyCode.K))
        {
            //image�𖶐����̉摜�ɂ���
            m_Image.sprite = m_Sprite[1];
            //���݂̃A�C�e���𖶐����ɕύX
            nowItem = ItemType2.Atomizer;
            Debug.Log(nowItem);
        }
        ItemCoolDown();
    }

    public void ItemUse()
    {
        //�I�񂾃A�C�e�����h���C���[�Ȃ�
        if (nowItem == ItemType2.Dryer)
        {
            //hp��
            //php.playerHp += 2;
            //player�̏�Ԉُ�������ɂ���
            nowstatus = PlayerStatus.dryness;
            Debug.Log(nowstatus);
        }
        //�I�񂾃A�C�e�����������Ȃ�
        if (nowItem == ItemType2.Atomizer /*&& nowstatus == PlayerStatus.dryness*/)
        {
            //player�̏�Ԃ�ʏ�ɖ߂�
            nowstatus = PlayerStatus.None;
            Debug.Log(nowstatus);
        }
    }


    public void ItemCoolDown()
    {
        if (nowItem == ItemType2.Dryer)
        {
            if (Dflag == false)
                dnowTime -= Time.deltaTime;
            if (dnowTime <= 0)
            {
               dnowTime = 0;
                dnowTime = 5;
                Dflag = true;
                Debug.Log("�N�[���_�E���I��");
            }
        }
        if (nowItem == ItemType2.Atomizer)
        {

            if (coolFlag == false)
                nowTime -= Time.deltaTime;
            if (nowTime <= 0)
            {
                 nowTime = 0;
                nowTime = 5;
                coolFlag = true;
            }

        }

    }
}

