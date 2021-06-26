using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
//[System.Serializable]
public enum PlayerStatus
{
    None,          //�ʏ�
    Dryness,       //����
    Humidity,       //���G��
   
}

public class HPControl : MonoBehaviour
{
   
    public PlayerStatus psType = PlayerStatus.None;


    const float MIN = 0;                //playerHp�̍ŏ��l
    const float MAX = 100;
    //readonly
    //playerHp�̍ő�l
    public  float playerHp = 100;        //player�̗̑�(��)
    public float playerATK = 10;
    public GameObject slider;       //�̗̓Q�[�W�Ɏw�肷��Slider
    public ItemType2 nowItem;
    public PlayerStatus nowstatus;

    [SerializeField]
    private Slider _slider;         //Slider�̒l��������_slider��錾

    // Start is called before the first frame update
    void Start()
    {
        _slider = slider.GetComponent<Slider>();
        nowstatus = psType=PlayerStatus.None;
    }

    // Update is called once per frame
    void Update()
    {
        //�ő�l�𒴂�����ő�l��n��
        playerHp = System.Math.Min(playerHp, MAX);
        //�ŏ��l�����������ŏ��l��n��
        playerHp = System.Math.Max(playerHp, MIN);
      nowstatus= PlayerStatus.None;
        //Debug.Log(m_pstype);
        //�X���C�_�[��HP�̕R�Â�
        _slider.value = playerHp;
    }

    public void Damege(int dame)
    {
        switch (psType)
        {
            case PlayerStatus.None: break;
            case PlayerStatus.Dryness: playerHp -= dame; break;
           // case PlayerStatus.AttackRaise:playerATK += 1;   break;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�Փ˂�������̃^�O��Enemy�Ȃ�
        if (collision.gameObject.tag == "Enemy")
        {
            //playerHp��10���炷
            playerHp -= 10;
            //this.gameObject.SetActive(false);
        }
        //����hp��0�ȉ��Ȃ�
        if (playerHp <= 0)
        {
            //GameOver�ƃR���\�[���ɕ\������
            print("GameOver");
        }
        //�A�C�e���ɓ���������
        //if (collision.gameObject.tag == "Item")
        //{
        //    //�A�C�e���N���X�擾
        //    ItemControl hititem = collision.gameObject.GetComponent<ItemControl>();
        //    if (hititem == null) return;
        //    switch (hititem.nowItem)
        //    {
        //        //�h���C���[�Ȃ�
        //        case ItemType2.Dryer:
        //            playerHp += hititem.m_num;
        //           psType= PlayerStatus.Dryness;
        //            playerATK -= hititem.m_num2;
        //            //��Ԉُ�������ɂ���
        //            Debug.Log("Drai");
        //            break;
        //            //�������Ȃ�
        //        case ItemType2.Atomizer:
        //            //��Ԉُ��ʏ�ɖ߂�
        //            psType = PlayerStatus.None;
        //            playerATK = 10;
        //            Debug.Log("None");
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}
