using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerAttack;
using CustomDebug;
public class ATKTest : MonoBehaviour
{
    public GameObject prefab;
    public GameObject Enemy;
    public float atkSpeed = 10.0f;  //攻撃スピード
    public float atkRange = 20.0f;  //攻撃範囲
    public bool pushStartTimeFlag = true;
    public float comboLimitTime = 2;//コンボ可能時間
    public float nowLimitTime = 0; //現在のコンボ可能時間残り時間
    public bool collisonEnemy = false;
    public float PonyMoveRange = 10.0f;//仮
    public float PonyMoveSpeed = 5.0f;//仮
    public float twinSpCoolDown = 1.0f;//再利用化のまでの時間
    public bool twinEndSpCoolDown = false;//現在のクールダウン終了フラグ
    public int hairChangeCount = 0;
    private bool isFloor = false;   //着地フラグ
    public float atkCount = 0;          //現在の攻撃段階カウント
    Rigidbody2D rb2d;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private PlayerControl playerControl;
    [SerializeField]
    private PlayerAttack pAtk;
    [SerializeField]
    private HPControl php;
    [SerializeField]
    public FloorCheck floor;
    private bool _isAtkFrag = false;//攻撃中確認フラグ
    private StateType nowHairStyle;
    //[SerializeField]
    // private PlayerAttack plyAtk;
    Vector2 playerPos;
    Vector2 playerAtkPos;
    Vector3 offset;
    Vector3 target;
    Vector3 collisonEnemyPos;
    float deg;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //プレイヤーの座標代入
        playerPos = player.transform.position;
        //自身の座標を代入
        playerAtkPos = this.transform.position;

        player.transform.position = new Vector2(playerPos.x, playerPos.y);
        playerControl = GetComponentInParent<PlayerControl>();
        pAtk = GetComponent<PlayerAttack>();
        nowHairStyle = StateType.PONYTAIL;
        this.transform.position = new Vector2(playerPos.x, playerPos.y + 200);
    }

    // Update is called once per frame
    void Update()
    {
        isFloor = floor.IsFloor();

        CDebug.Log(hairChangeCount);
        //プレイヤーの座標代入
        playerPos = player.transform.position;
        //自身の座標を代入
        playerAtkPos = this.transform.position;
        //攻撃選択呼び出し
        AttackSelection();
        //通常攻撃のコンボ用クールダウン呼び出し
        CoolDown();
        //ツインテールのクールダウンフラグがtrueだった場合
        if (twinEndSpCoolDown)
        {
            //ツインテールのクールダウン呼び出し
            TwinSpCoolDown();
        }

    }
    //攻撃選択(通常、特殊攻撃、髪型変更)
    public void AttackSelection()
    {
        //通常攻撃ボタン
        if (Input.GetKeyDown(KeyCode.B))
        {
            //通常攻撃呼び出し
            NormalAttack();
            //CDebug.Log("押されました");
            pushStartTimeFlag = true;
            //フラグをtrueに
            _isAtkFrag = true;
            //  Debug.Log(nowMode);
            //現在のコンボ可能時間が次コンボ可能時間より小さかったなら
            if (nowLimitTime < comboLimitTime)
            {
                //次コンボカウントを+する(1段目だったら2段目へ)
                atkCount++;
                //コンボ可能時間をリセット
                nowLimitTime = 0;
            }
        }
        //特殊攻撃ボタン
        else if (Input.GetKeyDown(KeyCode.N))
        {
            //特殊攻撃呼び出し
            SpecialAttack();
        }
        //髪型変更ボタン
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //髪型変更カウントを1進める
            hairChangeCount++;
            //髪型変更呼び出し
            HairStyleChange();
        }
    }

    //特殊攻撃
    public void SpecialAttack()
    {
        //現在の髪型
        switch (nowHairStyle)
        {
            //現在の髪の毛がポニーテールだったら
            case StateType.PONYTAIL:
                PonyTailSpecialAttack();
                break;
            //現在の髪の毛がツインテールだったら
            case StateType.TWINTAIL:
                TwinTailSpecialAttack();
                break;
            //現在の髪の毛がお団子だったら
            case StateType.DUMPLING:
                //ブロックに向かって60度の角度で射出
                SetTarget(Enemy.transform.position, 55);
                break;
        }
    }
    //髪型変更
    private void HairStyleChange()
    {
        //現在の髪型変更カウント
        switch (hairChangeCount)
        {
            case 0:
                nowHairStyle = StateType.PONYTAIL;
                break;
            case 1:
                nowHairStyle = StateType.TWINTAIL;
                break;
            case 2:
                nowHairStyle = StateType.DUMPLING;
                break;
        }
        //髪型変更カウント
        if (hairChangeCount == 3)
        {
            //髪型変更カウントが3になったら0に戻す
            hairChangeCount = 0;
        }

    }


    //通常攻撃
    private void NormalAttack()
    {

        float temp = playerPos.x + atkRange;    //tempにプレイヤー座標+攻撃範囲を足す(右)
        float temp2 = playerPos.x - atkRange;   //tempにプレイヤー座標-攻撃範囲を引く(左)

        //Playerが右に進んだら右側に攻撃
        if (playerControl.isRight)//プレイヤーの向きが右)
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
        if (temp < playerAtkPos.x || temp2 > playerAtkPos.x)
        {
            this.transform.position = new Vector2(playerPos.x, playerPos.y + 200);
            _isAtkFrag = false;
            Debug.Log("成功しました");
        }
    }

    //お団子特殊攻撃 
    //お団子特殊攻撃コルーチン
    IEnumerator ThrowBall()
    {
        float b = Mathf.Tan(deg * Mathf.Deg2Rad);
        float a = (target.y - b * target.x) / (target.x * target.x);

        for (float X = 0; X <= this.target.x; X += 20f)
        {
            float y = a * X * X + b * X;
            transform.position = new Vector3(X, y, 0) + offset;
            yield return null;
        }
    }
    public void SetTarget(Vector3 target, float deg)
    {
        this.offset = transform.position;
        this.target = target - this.offset;
        this.deg = deg;
        StartCoroutine("ThrowBall");
    }
    

    //ポニーテール特殊攻撃
    public void PonyTailSpecialAttack()
    {


    }

    //ツインテール特殊攻撃
    public void TwinTailSpecialAttack()
    {
        if (twinEndSpCoolDown == false)
        {
            twinEndSpCoolDown = true;
            //  StartCoroutine(E(() => Debug.Log("hello")));
            this.transform.localScale = new Vector3(4, 1, 1);
            this.transform.position = new Vector2(playerAtkPos.x, playerAtkPos.y);
            php.playerATK += 5;
        }
    }
    private void TwinSpCoolDown()
    {
        //ツインテールのクールダウンから毎frame-1
        twinSpCoolDown -= 1f * Time.deltaTime;
        //twinSpCoolDownが０よりも小さかったら処理を実行
        if (twinSpCoolDown < 0)
        {
            //値をもとに戻す
            twinSpCoolDown = 1.0f;
            //当たり判定を元の大きさに戻す
            this.transform.localScale = new Vector3(1, 1, 1);
            //flagをfalseに
            twinEndSpCoolDown = false;
        }
    }

    //IEnumerator E(System.Action call = null)
    //{
    //    bool t = false;
    //    var v = new WaitForSeconds(2f);
    //    while (!t)
    //    {
    //        yield return v;
    //        t = true;
    //    }
    //    if(null != call) call();
    //}
    //攻撃のクールダウン用
    private void CoolDown()
    {
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
            NormalAttack();
        //Nが押された場合処理開始
        if (pushStartTimeFlag)
            nowLimitTime += Time.deltaTime;
        //攻撃段階カウントが3異常になったら処理を開始
        if (atkCount > 3)
        {
            //攻撃段階カウントを0にする
            atkCount = 0;
            //現在の経過時間を0にする
            nowLimitTime = 0;
        }
    }
}

/*
ツインテール中2段ジャンプ 


 */
