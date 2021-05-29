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
    private StateType nowMode;      //現在のモード
    Vector2 PlayerPos;
    Vector2 PlayerAtkPos;
    public float atkRange = 20.0f;  //攻撃範囲

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
            //フラグをtrueに
            _isAtkFrag = true;
            Debug.Log(nowMode);
            // Debug.Log("押しました");
        }

        //_isAtkFlagがtrueなら
        if (_isAtkFrag)
        {
            //現在の髪型
            switch (nowMode)
            {
                //現在の髪型がポニテなら
                case StateType.PONYTAIL:
                    
                    PlayerAttack1();
                    break;
                //現在の髪型がツインテールなら
                case StateType.TWINTAIL:
                    PlayerAttack1();
                    break;
            }
            // PlayerAttack1();
        }
        //髪型変更呼び出す
        ModeChange();
    }
    //攻撃
    private void PlayerAttack1()
    {
        float temp = PlayerPos.x + atkRange;    //tempにプレイヤー座標+攻撃範囲を足す(右)
        float temp2 = PlayerPos.x - atkRange;   //tempにプレイヤー座標-攻撃範囲を引く(左)

        //Playerが右に進んだら右側に攻撃
        if (player.GetComponent<PlayerControl>().isRight)//プレイヤーの向きが右)
        {
            this.transform.localPosition += new Vector3(atkSpeed, 0);

            //Player.GetComponent<PlayerControl>().speed = 5;
        }
        //Playerが左に進んだら左側に攻撃
        else
        {
            this.transform.localPosition += new Vector3(atkSpeed, 0);
        }
        //一定の位置に達したらobjを現在のplayer座標に戻す
        if (temp < PlayerAtkPos.x || temp2 > PlayerAtkPos.x)
        {
            this.transform.position = new Vector2(PlayerPos.x, PlayerPos.y + 83);
            _isAtkFrag = false;
            Debug.Log("成功しました");
        }

    }

    //髪型変更
    private void ModeChange()
    {
        //現在の髪型からツインテールに変更
        if (Input.GetKeyDown(KeyCode.T))
        {
            nowMode = StateType.TWINTAIL;
        }
        //現在の髪型からポニーテールに変更
        if (Input.GetKeyDown(KeyCode.P))
        {
            nowMode = StateType.PONYTAIL;
        }
    }
}
