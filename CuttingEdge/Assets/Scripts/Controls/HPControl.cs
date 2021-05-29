using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
public enum PlayerStatus
{
None,          //通常
dryness,       //乾燥
}

public class HPControl : MonoBehaviour
{
    public PlayerStatus m_pstype = PlayerStatus.None;
    public float playerHp = 100;        //playerの体力(仮)
    public float playerATK = 10;
    private Slider _slider;         //Sliderの値を代入する_sliderを宣言
    public GameObject slider;       //体力ゲージに指定するSlider
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
        //スライダーとHPの紐づけ
        _slider.value = playerHp;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //衝突した相手のタグがEnemyなら
        if (collision.gameObject.tag == "Enemy")
        {
            //playerHpを10減らす
            playerHp -= 10;
            //this.gameObject.SetActive(false);
        }
        //もしhpが0以下なら
        if (playerHp <= 0)
        {
            //GameOverとコンソールに表示する
            print("GameOver");
        }
        //アイテムに当たったら
        if (collision.gameObject.tag == "Item")
        {
            //アイテムクラス取得
            Item hititem = collision.gameObject.GetComponent<Item>();
            if (hititem == null) return;
            switch (hititem.m_type)
            {
                //ドライヤーなら
                case ItemType.Dryer:
                    playerHp += hititem.m_num;
                   m_pstype= PlayerStatus.dryness;
                    playerATK -= hititem.m_num2;
                    //状態異常を乾燥にする
                    Debug.Log("Drai");
                    break;
                    //霧吹きなら
                case ItemType.Atomizer:
                    //状態異常を通常に戻す
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
