using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//状態異常
public enum ItemType
{
None,       //通常
Dryer,      //ドライヤー
Atomizer,   //霧吹き  
}


public class Item : MonoBehaviour
{
    public GameObject m_gameObj;
    public ItemType m_type = ItemType.None;//現在のアイテムタイプを通常にする
    public float m_num = 0.0f;  //ステータスの値を増やす箱
    public float m_num2 = 10;   //ステータスの値を増やす箱2
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Instantiate(m_gameObj, transform.position, transform.rotation);
        this.gameObject.SetActive(false);
    }
}
