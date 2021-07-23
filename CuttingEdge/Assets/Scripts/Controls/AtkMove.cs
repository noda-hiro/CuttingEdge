using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerAttack;

public class AtkMove : MonoBehaviour
{
    Rigidbody2D rb2d;
    public GameObject player;
    public PlayerControl playerControl;
    public PlayerAttack pAtk;
    public float atkSpeed = 10.0f;  //攻撃スピード
    private bool _isAtkFrag = false;//攻撃中確認フラグ
   // public float offensiveAbility = 10;//playerの攻撃力
    Vector2 PlayerPos;
    Vector2 PlayerAtkPos;
    public float atkRange = 20.0f;  //攻撃範囲
    public float atkCount = 0;
    public float comboLimitTime = 2;
    public float nowLimitTime = 0;
    public bool LimitFlag = false;
    public bool pushStartTimeflag = true;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //プレイヤーの座標代入
        PlayerPos = player.transform.position;
        //自身の座標を代入
        PlayerAtkPos = this.transform.position;

        player.transform.position = new Vector2(PlayerPos.x, PlayerPos.y);
        playerControl = GetComponent<PlayerControl>();
        pAtk = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    public void Update()
    {
        //プレイヤーの座標代入
        PlayerPos = player.transform.position;

        //自身の座標を代入
        PlayerAtkPos = this.transform.position;

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            pushStartTimeflag = true;
            //フラグをtrueに
            _isAtkFrag = true;
          //  Debug.Log(nowMode);
          //現在のコンボ可能時間が次コンボ可能時間より小さかったなら
          if(nowLimitTime<comboLimitTime)
            {
                //次コンボカウントを+する(1段目だったら2段目へ)
                atkCount++;
                //コンボ可能時間をリセット
                nowLimitTime = 0;
            }
            // Debug.Log("押しました");
        }
        //現在のコンボ可能時間が次コンボ可能時間より大きかったなら
        if (nowLimitTime > comboLimitTime)
        {
            //次コンボカウントをリセット(1段目に戻す)
            atkCount = 0;
            //コンボ可能時間をリセット
            nowLimitTime = 0;
        }
        //_isAtkFlagがtrueなら
        if (_isAtkFrag)
        {
            //現在の髪型
            //switch (nowMode)
            //{
            //    //現在の髪型がポニテなら
            //    case StateType.PONYTAIL:
                    
            //        PlayerAttack1();
            //        break;
            //    //現在の髪型がツインテールなら
            //    case StateType.TWINTAIL:
            //        PlayerAttack1();
            //        break;
            //}
             PlayerAttack1();
        }
        //髪型変更呼び出す
        ModeChange();
        //Bsボタンを押したら処理を実行
        if (pushStartTimeflag)
        {
            nowLimitTime += Time.deltaTime;
        }
    }
    //攻撃
    private void PlayerAttack1()
    {
        float temp = PlayerPos.x + atkRange;    //tempにプレイヤー座標+攻撃範囲を足す(右)
        float temp2 = PlayerPos.x - atkRange;   //tempにプレイヤー座標-攻撃範囲を引く(左)

        //Playerが右に進んだら右側に攻撃
        if (player.GetComponent<PlayerControl>().isRight)//プレイヤーの向きが右)
        {
            this.transform.localPosition += new Vector3(atkSpeed, 0) * (Time.deltaTime * 300);

            //Player.GetComponent<PlayerControl>().speed = 5;
        }
        //Playerが左に進んだら左側に攻撃
        else
        {
            this.transform.localPosition += new Vector3(atkSpeed, 0) * (Time.deltaTime * 300);
        }
        //一定の位置に達したらobjを現在のplayer座標に戻す
        if (temp < PlayerAtkPos.x || temp2 > PlayerAtkPos.x)
        {
            this.transform.position = new Vector2(PlayerPos.x, PlayerPos.y + 83);
            _isAtkFrag = false;
           // Debug.Log("成功しました");
        }

    }

    //髪型変更
    private void ModeChange()
    {
        //現在の髪型からツインテールに変更
        if (Input.GetKeyDown(KeyCode.T))
        {
            //nowMode = StateType.TWINTAIL;
        }
        //現在の髪型からポニーテールに変更
        if (Input.GetKeyDown(KeyCode.P))
        {
            //nowMode = StateType.PONYTAIL;
        }
    }
}
