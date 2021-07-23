using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;
public enum ItemType2
{
    Dryer,      //�h���C���[
    Atomizer,   //������  
    HairIron    //�w�A�A�C����
}

public class ItemControl : MonoBehaviour
{
    public float dryerNowTime;                   //���݂�Dryer�N�[���_�E���̎���
    public float m_num = 0.0f;                   //�X�e�[�^�X�̒l�𑝂₷��
    public float m_num2 = 10;                    //�X�e�[�^�X�̒l�𑝂₷��2
    public float atomizerNowTime;                //���݂�Atomizer�N�[���_�E���̎���
    public ItemType2 nowItem;                    //���݂̃A�C�e�����
    public PlayerStatus nowstatus;               //���݂�player�X�e�[�^�X��� 
    public HPControl php;                        //Hpcontrol�X�N���v�g
    [SerializeField]
    public Sprite[] m_Sprite;                   //sprite
    public float hairIronNowTime = 0.0f;
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private AudioClip audioClip;


    private float atomizerCoolTime = 5f;        //�N�[���_�E���̎���
    private bool atomizerCoolFlag = false;      //Atomizer�̃N�[���_�E���t���O
    private float dryerCoolTime = 10f;          //�N�[���_�E���̎��� 
    private bool dryerCoolFlag = false;         //Dryer�̃N�[���_�E���t���O
    private bool hairIronFlag = false;
    private float hairIronCoolTime = 10f;
    [SerializeField]
    private Image m_Image;                      //image
    [SerializeField]
    private GameObject DryerPs;                 //�h���C���[�A�C�R��obj
    [SerializeField]
    private GameObject AtomizerPs2;             //���G��A�C�R��obj
    [SerializeField]
    private GameObject PlayerAtomizerPs2;             //player�p���G��A�C�R��obj
    [SerializeField]
    private GameObject PlayerDrayPs2;             //player�p���G��A�C�R��obj
    [SerializeField]
    private PlayerControl ply;                  //playerControl
    [SerializeField]
    PlayerNowAction nowAction;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        //image�擾
        m_Image = GetComponent<Image>();
        //���݂�Dryer�N�[���_�E���Ɍ��݂̃N�[���_�E�����Ԃ�����
        dryerNowTime = dryerCoolTime;
        //���݂�Atomizer�N�[���_�E���Ɍ��݂̃N�[���_�E�����Ԃ�����
        atomizerNowTime = atomizerCoolTime;
        //���݂�HairIron�N�[���_�E���Ɍ��݂̃N�[���_�E�����Ԃ�����
        hairIronNowTime = hairIronCoolTime;
        //���݂̃X�e�[�^�X��ʏ�ɐݒ肷��
        nowstatus = php.psType = PlayerStatus.None;
        

    }

    // Update is called once per frame
    void Update()
    {

        // "H"������dryerFlag��false�Ȃ�I������Ă���A�C�e�����g�p����
        if (Input.GetKeyDown(KeyCode.H) && dryerCoolFlag == false)
        {
            //�h���C���[
            //���݂̃A�C�e�����h���C���[�Ō��݂̃X�e�[�^�X��(�����A�ʏ�A���G��)�Ȃ珈�������s
            if (nowstatus == PlayerStatus.None && nowItem == ItemType2.Dryer
                || nowstatus == PlayerStatus.Dryness && nowItem == ItemType2.Dryer
                || nowstatus == PlayerStatus.Humidity && nowItem == ItemType2.Dryer)
            {
               
                //�h���C���[�̃N�[���_�E���t���O��true�ɂ���
                dryerCoolFlag = true;
                //�A�C�e���g�p�֐����Ăяo��
                ItemUse();
                CDebug.Log(nowstatus + "���݂̃X�e�[�^�X");
            }
        }
        // "H"������AtomizerFlag��false�Ȃ�I������Ă���A�C�e�����g�p����
        if (Input.GetKeyDown(KeyCode.H) && atomizerCoolFlag == false)
        {
            //���݂̃X�e�[�^�X��"����"�Ȃ�Ώ��������s����
            if (nowstatus == PlayerStatus.Dryness && nowItem == ItemType2.Atomizer
                || nowstatus == PlayerStatus.None && nowItem == ItemType2.Atomizer)
            {
                
                //�������̃N�[���_�E���t���O��true�ɂ���
                atomizerCoolFlag = true;
                //�A�C�e���g�p�֐����Ăяo��
                ItemUse();
            }
        }
        // "H"������hairIronFlag��false�Ō��݂̃A�C�e�����w�A�A�C�����Ȃ�I������Ă���A�C�e�����g�p����
        if (Input.GetKeyDown(KeyCode.H) && hairIronFlag == false&&nowItem==ItemType2.HairIron)
        {
            CDebug.Log(audioClip);
           
            // CDebug.Log(nowstatus + "eeeeeeeee");
            php.playerATK += 1;
                hairIronFlag = true;
                AtomizerPs2.SetActive(false);
                DryerPs.SetActive(false);
                CDebug.Log(nowstatus + "�U���͂PUP");
        }

        //�A�C�e���I��
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            //image���w�A�A�C�����̉摜�ɂ���
            m_Image.sprite = m_Sprite[2];
            //���݂̃A�C�e�����w�A�A�C�����ɕύX
            nowItem = ItemType2.HairIron;
            Debug.Log("aironn");
        }



        //ItemCoolDown();
        //Atomizer�̃N�[���_�E���t���O��true�Ȃ�
        if (atomizerCoolFlag)
        {
            //���݂�Atomizer�N�[���_�E������o�ߎ��ԕ�����
            atomizerNowTime -= Time.deltaTime;
            // Debug.Log("nowTIme" + atomizerNowTime);
            //���݂�Atomizer�N�[���_�E����0�ȉ��ɂȂ����珈�������s����
            if (atomizerNowTime < 0)
            {
                //���݂�Atomizer�N�[���_�E����5����
                atomizerNowTime = 5;
                //Atomizer�̃N�[���_�E���t���O��false�ɂ���
                atomizerCoolFlag = false;
                Debug.Log("atomizer�N�[���_�E���I��");
            }
        }
        //Dryer�̃N�[���_�E���t���O��true�Ȃ�
        if (dryerCoolFlag)
        {
            //���݂�Dryer�N�[���_�E������o�ߎ��ԕ�����
            dryerNowTime -= Time.deltaTime;
            //Debug.Log("dnowTime" + dryerNowTime);
            //���݂�Dryer�N�[���_�E����0�ȉ��ɂȂ����珈�������s����
            if (dryerNowTime < 0)
            {
                //���݂�Atomizer�N�[���_�E����10����
                dryerNowTime = 10;
                //Dryer�̃N�[���_�E���t���O��false�ɂ���
                dryerCoolFlag = false;
                Debug.Log("dryer�N�[���_�E���I��");
            }
        }
        //HairIron�̃N�[���_�E���t���O��true�Ȃ�
        if (hairIronFlag)
        {
            //���݂�Dryer�N�[���_�E������o�ߎ��ԕ�����
            hairIronNowTime -= Time.deltaTime;
            //Debug.Log("dnowTime" + dryerNowTime);
            //���݂�Dryer�N�[���_�E����0�ȉ��ɂȂ����珈�������s����
            if (hairIronNowTime < 0)
            {
                //���݂�Atomizer�N�[���_�E����10����
                hairIronNowTime = 10;
                //Dryer�̃N�[���_�E���t���O��false�ɂ���
                hairIronFlag = false;
                Debug.Log("dryer�N�[���_�E���I��");
                //10�b��������playerATk�����Ƃɖ߂�
                php.playerATK -= 1;
               // nowstatus = php.psType = PlayerStatus.None;
            }

        }

    }



    //�A�C�e���g�p�֐�
    public void ItemUse()
    {
        //if(nowAction==)
        audio.PlayOneShot(audioClip);
        //�I�񂾃A�C�e�����h���C���[�Ō��݂̃X�e�[�^�X�����G��Ȃ�
        if (nowItem == ItemType2.Dryer && nowstatus == PlayerStatus.Humidity)
        {
            //player�̏�Ԉُ��ʏ�ɂ���
            nowstatus = php.psType = PlayerStatus.None;
            //���G��A�C�R�����\��
            AtomizerPs2.SetActive(false);
            PlayerAtomizerPs2.SetActive(false);
            //player�̈ړ����x��10�ɖ߂�
            ply.speed = 10;
            //DryerPs.SetActive(false);
            CDebug.Log(nowstatus + "�ʏ�̂͂�1");
        }
        //�I�񂾃A�C�e�����h���C���[�Ō��݂̃X�e�[�^�X���ʏ�Ȃ�
        else if (nowItem == ItemType2.Dryer && dryerCoolFlag
             || nowItem == ItemType2.Dryer && nowstatus == PlayerStatus.None)
        {
            //hp��
            php.playerHp = 100;
            //player�̏�Ԉُ�������ɂ���
            nowstatus = php.psType = PlayerStatus.Dryness;
            //�����A�C�R����\��
            DryerPs.SetActive(true);
            PlayerDrayPs2.SetActive(true);
            //AtomizerPs2.SetActive(false);
            CDebug.Log(nowstatus + "Dryness�̂͂�");

        }

        //�I�񂾃A�C�e�����������Ō��݂̃X�e�[�^�X�������Ȃ�
        if (nowItem == ItemType2.Atomizer && nowstatus == PlayerStatus.Dryness)
        {
            //player�̏�Ԃ�ʏ�ɖ߂�
            nowstatus = php.psType = PlayerStatus.None;
            //�����A�C�R����\��
            DryerPs.SetActive(false);
            PlayerDrayPs2.SetActive(false);
            //AtomizerPs2.SetActive(false);
            //Debug.Log(nowstatus);
            CDebug.Log(nowstatus + "�ʏ�̂͂�");
        }
        //�I�񂾃A�C�e�����������Ō��݂̃X�e�[�^�X���ʏ�Ȃ�
        else if (nowItem == ItemType2.Atomizer && nowstatus == PlayerStatus.None)
        {
            //player�̃X�e�[�^�X�𐅔G��ɂ���
            nowstatus = php.psType = PlayerStatus.Humidity;
            //���G��A�C�R���\��
            AtomizerPs2.SetActive(true);
            PlayerAtomizerPs2.SetActive(true);
            //���݂�player�̈ړ����x(10)����-�S����
            ply.speed -= 4;
            //DryerPs.SetActive(false);
            CDebug.Log(nowstatus + "���G��̂͂�");
        }

    }
}


