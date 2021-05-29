using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
public enum PlayerStatus
{
None,          //�ʏ�
dryness,       //����
}

public class HPControl : MonoBehaviour
{
    public PlayerStatus m_pstype = PlayerStatus.None;
    public float playerHp = 100;        //player�̗̑�(��)
    public float playerATK = 10;
    private Slider _slider;         //Slider�̒l��������_slider��錾
    public GameObject slider;       //�̗̓Q�[�W�Ɏw�肷��Slider
    public ItemType nowItem;
    // Start is called before the first frame update
    void Start()
    {
        _slider = slider.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(m_pstype);
        //�X���C�_�[��HP�̕R�Â�
        _slider.value = playerHp;
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
        if (collision.gameObject.tag == "Item")
        {
            //�A�C�e���N���X�擾
            Item hititem = collision.gameObject.GetComponent<Item>();
            if (hititem == null) return;
            switch (hititem.m_type)
            {
                //�h���C���[�Ȃ�
                case ItemType.Dryer:
                    playerHp += hititem.m_num;
                   m_pstype= PlayerStatus.dryness;
                    playerATK -= hititem.m_num2;
                    //��Ԉُ�������ɂ���
                    Debug.Log("Drai");
                    break;
                    //�������Ȃ�
                case ItemType.Atomizer:
                    //��Ԉُ��ʏ�ɖ߂�
                    m_pstype = PlayerStatus.None;
                    playerATK = 10;
                    Debug.Log("None");
                    break;
                default:
                    break;
            }
        }


    }

}
