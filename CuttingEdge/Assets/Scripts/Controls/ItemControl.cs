using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ItemType2
{
    Dryer,      //ドライヤー
    Atomizer,   //霧吹き  
}

public class ItemControl : MonoBehaviour
{
    public HPControl php;               //hpcontrol
    //public bool dryerFlag = false;      
    //public bool atomizerFlag = false;
    private float coolTime = 5f;     //クールダウンの時間
    private float dcoolTime = 10f;     //クールダウンの時間
    private bool coolFlag = true;  //クールダウンフラグ
    private bool Dflag = true;
    public ItemType2 nowItem;           //現在のアイテム状態
    public PlayerStatus nowstatus;      //現在のplayerステータス状態
    private Image m_Image;              //image
    public Sprite[] m_Sprite;           //sprite
    public float Count = 1f;
    public float nowTime;
    public float dnowTime;

    // Start is called before the first frame update
    void Start()
    {
        m_Image = GetComponent<Image>();    //image取得
        php = GetComponent<HPControl>();    //Hpcotrol取得
        dnowTime = dcoolTime;
        nowTime = coolTime;

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(nowTime);
        //"H"を押したら選択されているアイテムを使用する
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (coolFlag == true || Dflag == true)
            {
                //アイテム使用呼び出す
                ItemUse();
                coolFlag = false;
                Dflag = false;
            }
        }
        //"J"を押したらドライヤーを現在のアイテムを変更
        if (Input.GetKeyDown(KeyCode.J))
        {
            //imageをドライヤーの画像にする
            m_Image.sprite = m_Sprite[0];
            //現在のアイテムをドライヤーに変更
            nowItem = ItemType2.Dryer;
            Debug.Log(nowItem);
        }
        //"K"を押したら霧吹きを現在のアイテムを変更
        if (Input.GetKeyDown(KeyCode.K))
        {
            //imageを霧吹きの画像にする
            m_Image.sprite = m_Sprite[1];
            //現在のアイテムを霧吹きに変更
            nowItem = ItemType2.Atomizer;
            Debug.Log(nowItem);
        }
        ItemCoolDown();
    }

    public void ItemUse()
    {
        //選んだアイテムがドライヤーなら
        if (nowItem == ItemType2.Dryer)
        {
            //hp回復
            //php.playerHp += 2;
            //playerの状態異常を乾燥にする
            nowstatus = PlayerStatus.dryness;
            Debug.Log(nowstatus);
        }
        //選んだアイテムが霧吹きなら
        if (nowItem == ItemType2.Atomizer /*&& nowstatus == PlayerStatus.dryness*/)
        {
            //playerの状態を通常に戻す
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
                Debug.Log("クールダウン終了");
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

