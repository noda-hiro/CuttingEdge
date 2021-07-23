using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;
public enum ItemType2
{
    Dryer,      //ドライヤー
    Atomizer,   //霧吹き  
    HairIron    //ヘアアイロン
}

public class ItemControl : MonoBehaviour
{
    public float dryerNowTime;                   //現在のDryerクールダウンの時間
    public float m_num = 0.0f;                   //ステータスの値を増やす箱
    public float m_num2 = 10;                    //ステータスの値を増やす箱2
    public float atomizerNowTime;                //現在のAtomizerクールダウンの時間
    public ItemType2 nowItem;                    //現在のアイテム状態
    public PlayerStatus nowstatus;               //現在のplayerステータス状態 
    public HPControl php;                        //Hpcontrolスクリプト
    [SerializeField]
    public Sprite[] m_Sprite;                   //sprite
    public float hairIronNowTime = 0.0f;
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private AudioClip audioClip;


    private float atomizerCoolTime = 5f;        //クールダウンの時間
    private bool atomizerCoolFlag = false;      //Atomizerのクールダウンフラグ
    private float dryerCoolTime = 10f;          //クールダウンの時間 
    private bool dryerCoolFlag = false;         //Dryerのクールダウンフラグ
    private bool hairIronFlag = false;
    private float hairIronCoolTime = 10f;
    [SerializeField]
    private Image m_Image;                      //image
    [SerializeField]
    private GameObject DryerPs;                 //ドライヤーアイコンobj
    [SerializeField]
    private GameObject AtomizerPs2;             //水濡れアイコンobj
    [SerializeField]
    private GameObject PlayerAtomizerPs2;             //player用水濡れアイコンobj
    [SerializeField]
    private GameObject PlayerDrayPs2;             //player用水濡れアイコンobj
    [SerializeField]
    private PlayerControl ply;                  //playerControl
    [SerializeField]
    PlayerNowAction nowAction;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        //image取得
        m_Image = GetComponent<Image>();
        //現在のDryerクールダウンに現在のクールダウン時間を入れる
        dryerNowTime = dryerCoolTime;
        //現在のAtomizerクールダウンに現在のクールダウン時間を入れる
        atomizerNowTime = atomizerCoolTime;
        //現在のHairIronクールダウンに現在のクールダウン時間を入れる
        hairIronNowTime = hairIronCoolTime;
        //現在のステータスを通常に設定する
        nowstatus = php.psType = PlayerStatus.None;
        

    }

    // Update is called once per frame
    void Update()
    {

        // "H"を押しdryerFlagがfalseなら選択されているアイテムを使用する
        if (Input.GetKeyDown(KeyCode.H) && dryerCoolFlag == false)
        {
            //ドライヤー
            //現在のアイテムがドライヤーで現在のステータスが(乾燥、通常、水濡れ)なら処理を実行
            if (nowstatus == PlayerStatus.None && nowItem == ItemType2.Dryer
                || nowstatus == PlayerStatus.Dryness && nowItem == ItemType2.Dryer
                || nowstatus == PlayerStatus.Humidity && nowItem == ItemType2.Dryer)
            {
               
                //ドライヤーのクールダウンフラグをtrueにする
                dryerCoolFlag = true;
                //アイテム使用関数を呼び出す
                ItemUse();
                CDebug.Log(nowstatus + "現在のステータス");
            }
        }
        // "H"を押しAtomizerFlagがfalseなら選択されているアイテムを使用する
        if (Input.GetKeyDown(KeyCode.H) && atomizerCoolFlag == false)
        {
            //現在のステータスが"乾燥"ならば処理を実行する
            if (nowstatus == PlayerStatus.Dryness && nowItem == ItemType2.Atomizer
                || nowstatus == PlayerStatus.None && nowItem == ItemType2.Atomizer)
            {
                
                //霧吹きのクールダウンフラグをtrueにする
                atomizerCoolFlag = true;
                //アイテム使用関数を呼び出す
                ItemUse();
            }
        }
        // "H"を押しhairIronFlagがfalseで現在のアイテムがヘアアイロンなら選択されているアイテムを使用する
        if (Input.GetKeyDown(KeyCode.H) && hairIronFlag == false&&nowItem==ItemType2.HairIron)
        {
            CDebug.Log(audioClip);
           
            // CDebug.Log(nowstatus + "eeeeeeeee");
            php.playerATK += 1;
                hairIronFlag = true;
                AtomizerPs2.SetActive(false);
                DryerPs.SetActive(false);
                CDebug.Log(nowstatus + "攻撃力１UP");
        }

        //アイテム選択
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            //imageをヘアアイロンの画像にする
            m_Image.sprite = m_Sprite[2];
            //現在のアイテムをヘアアイロンに変更
            nowItem = ItemType2.HairIron;
            Debug.Log("aironn");
        }



        //ItemCoolDown();
        //Atomizerのクールダウンフラグがtrueなら
        if (atomizerCoolFlag)
        {
            //現在のAtomizerクールダウンから経過時間分引く
            atomizerNowTime -= Time.deltaTime;
            // Debug.Log("nowTIme" + atomizerNowTime);
            //現在のAtomizerクールダウンが0以下になったら処理を実行する
            if (atomizerNowTime < 0)
            {
                //現在のAtomizerクールダウンに5足す
                atomizerNowTime = 5;
                //Atomizerのクールダウンフラグをfalseにする
                atomizerCoolFlag = false;
                Debug.Log("atomizerクールダウン終了");
            }
        }
        //Dryerのクールダウンフラグがtrueなら
        if (dryerCoolFlag)
        {
            //現在のDryerクールダウンから経過時間分引く
            dryerNowTime -= Time.deltaTime;
            //Debug.Log("dnowTime" + dryerNowTime);
            //現在のDryerクールダウンが0以下になったら処理を実行する
            if (dryerNowTime < 0)
            {
                //現在のAtomizerクールダウンに10足す
                dryerNowTime = 10;
                //Dryerのクールダウンフラグをfalseにする
                dryerCoolFlag = false;
                Debug.Log("dryerクールダウン終了");
            }
        }
        //HairIronのクールダウンフラグがtrueなら
        if (hairIronFlag)
        {
            //現在のDryerクールダウンから経過時間分引く
            hairIronNowTime -= Time.deltaTime;
            //Debug.Log("dnowTime" + dryerNowTime);
            //現在のDryerクールダウンが0以下になったら処理を実行する
            if (hairIronNowTime < 0)
            {
                //現在のAtomizerクールダウンに10足す
                hairIronNowTime = 10;
                //Dryerのクールダウンフラグをfalseにする
                hairIronFlag = false;
                Debug.Log("dryerクールダウン終了");
                //10秒たったらplayerATkをもとに戻す
                php.playerATK -= 1;
               // nowstatus = php.psType = PlayerStatus.None;
            }

        }

    }



    //アイテム使用関数
    public void ItemUse()
    {
        //if(nowAction==)
        audio.PlayOneShot(audioClip);
        //選んだアイテムがドライヤーで現在のステータスが水濡れなら
        if (nowItem == ItemType2.Dryer && nowstatus == PlayerStatus.Humidity)
        {
            //playerの状態異常を通常にする
            nowstatus = php.psType = PlayerStatus.None;
            //水濡れアイコンを非表示
            AtomizerPs2.SetActive(false);
            PlayerAtomizerPs2.SetActive(false);
            //playerの移動速度を10に戻す
            ply.speed = 10;
            //DryerPs.SetActive(false);
            CDebug.Log(nowstatus + "通常のはず1");
        }
        //選んだアイテムがドライヤーで現在のステータスが通常なら
        else if (nowItem == ItemType2.Dryer && dryerCoolFlag
             || nowItem == ItemType2.Dryer && nowstatus == PlayerStatus.None)
        {
            //hp回復
            php.playerHp = 100;
            //playerの状態異常を乾燥にする
            nowstatus = php.psType = PlayerStatus.Dryness;
            //乾燥アイコンを表示
            DryerPs.SetActive(true);
            PlayerDrayPs2.SetActive(true);
            //AtomizerPs2.SetActive(false);
            CDebug.Log(nowstatus + "Drynessのはず");

        }

        //選んだアイテムが霧吹きで現在のステータスが乾燥なら
        if (nowItem == ItemType2.Atomizer && nowstatus == PlayerStatus.Dryness)
        {
            //playerの状態を通常に戻す
            nowstatus = php.psType = PlayerStatus.None;
            //乾燥アイコン非表示
            DryerPs.SetActive(false);
            PlayerDrayPs2.SetActive(false);
            //AtomizerPs2.SetActive(false);
            //Debug.Log(nowstatus);
            CDebug.Log(nowstatus + "通常のはず");
        }
        //選んだアイテムが霧吹きで現在のステータスが通常なら
        else if (nowItem == ItemType2.Atomizer && nowstatus == PlayerStatus.None)
        {
            //playerのステータスを水濡れにする
            nowstatus = php.psType = PlayerStatus.Humidity;
            //水濡れアイコン表示
            AtomizerPs2.SetActive(true);
            PlayerAtomizerPs2.SetActive(true);
            //現在のplayerの移動速度(10)から-４引く
            ply.speed -= 4;
            //DryerPs.SetActive(false);
            CDebug.Log(nowstatus + "水濡れのはず");
        }

    }
}


