using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ԉُ�
public enum ItemType
{
None,       //�ʏ�
Dryer,      //�h���C���[
Atomizer,   //������  
}


public class Item : MonoBehaviour
{
    public GameObject m_gameObj;
    public ItemType m_type = ItemType.None;//���݂̃A�C�e���^�C�v��ʏ�ɂ���
    public float m_num = 0.0f;  //�X�e�[�^�X�̒l�𑝂₷��
    public float m_num2 = 10;   //�X�e�[�^�X�̒l�𑝂₷��2
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Instantiate(m_gameObj, transform.position, transform.rotation);
        this.gameObject.SetActive(false);
    }
}
