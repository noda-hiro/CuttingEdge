using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;


public class Enemy_Walking_Control : MonoBehaviour
{
    // 二足歩行の体力
    [SerializeField]
    int HP = 40;
    // 二足歩行の攻撃力
    [SerializeField]
    int ATK = 10;
    [SerializeField]
    // 二足歩行の移動速度
    float SPEED = 0.0f;

    Transform target;
   
    // ゲームオブジェクトが入る変数
    GameObject EW;
    // Enemy_Createのスクリプトが入る変数
    Enemy_Create EW_Script;

    //野田追加点
    public PlayerStatus nowStatus;
    
    private HPControl playerHP;

    // Start is called before the first frame update
    void Start()
    {
        // ゲームオブジェクトを名前から取得して変数に入れる
        EW = GameObject.Find("Test");
        // EWの中にあるEnemy_Createを取得して変数に入れる
        EW_Script = EW.GetComponent<Enemy_Create>();
        CDebug.ColorLog(gameObject.name, "blue");
        //playerHP = GetComponent<HPControl>();
        playerHP = GameObject.Find("Player").GetComponent<HPControl>();
        // プレイヤーのオブジェクトの位置を取得
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        // HPがなくなったら自身を破棄
        if (HP == 0)
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }

        

        //ずっと追いかける
        float move = SPEED * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, move);

    }
    //野田追加点2
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var p = collision.gameObject.GetComponent<HPControl>();
            if(null != p) p.Damege(ATK);
            //this.gameObject.SetActive(false);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack"|| collision.gameObject.tag == "Dumpling")
        {
            Debug.Log("gogjgoog");
            //var p = collision.gameObject.GetComponent<HPControl>();
            this.gameObject.SetActive(false);
            nowStatus = playerHP.psType;
           // CDebug.Log(nowStatus);
            //Damege(p);
            /*= PlayerStatus.dryness*/
            if (nowStatus == PlayerStatus.Dryness)
            {
                playerHP.playerHp -= 5;
            }
        }
    }
}
