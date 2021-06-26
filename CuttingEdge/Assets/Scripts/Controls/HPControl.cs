using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
//[System.Serializable]
public enum PlayerStatus
{
    None,          //通常
    Dryness,       //乾燥
    Humidity,       //水濡れ
   
}

public class HPControl : MonoBehaviour
{
   
    public PlayerStatus psType = PlayerStatus.None;


    const float MIN = 0;                //playerHpの最小値
    const float MAX = 100;
    //readonly
    //playerHpの最大値
    public  float playerHp = 100;        //playerの体力(仮)
    public float playerATK = 10;
    public GameObject slider;       //体力ゲージに指定するSlider
    public ItemType2 nowItem;
    public PlayerStatus nowstatus;

    [SerializeField]
    private Slider _slider;         //Sliderの値を代入する_sliderを宣言

    // Start is called before the first frame update
    void Start()
    {
        _slider = slider.GetComponent<Slider>();
        nowstatus = psType=PlayerStatus.None;
    }

    // Update is called once per frame
    void Update()
    {
        //最大値を超えたら最大値を渡す
        playerHp = System.Math.Min(playerHp, MAX);
        //最小値を下回ったら最小値を渡す
        playerHp = System.Math.Max(playerHp, MIN);
      nowstatus= PlayerStatus.None;
        //Debug.Log(m_pstype);
        //スライダーとHPの紐づけ
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
        //if (collision.gameObject.tag == "Item")
        //{
        //    //アイテムクラス取得
        //    ItemControl hititem = collision.gameObject.GetComponent<ItemControl>();
        //    if (hititem == null) return;
        //    switch (hititem.nowItem)
        //    {
        //        //ドライヤーなら
        //        case ItemType2.Dryer:
        //            playerHp += hititem.m_num;
        //           psType= PlayerStatus.Dryness;
        //            playerATK -= hititem.m_num2;
        //            //状態異常を乾燥にする
        //            Debug.Log("Drai");
        //            break;
        //            //霧吹きなら
        //        case ItemType2.Atomizer:
        //            //状態異常を通常に戻す
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
