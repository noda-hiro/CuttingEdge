using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerAttack;
using CustomDebug;
public class ATKTest : MonoBehaviour
{
    public GameObject[] effect = new GameObject[5] ;
    public Animator anim;
    public GameObject prefab;
    public GameObject dang1;
    public GameObject dang2;
    public GameObject ThrowBollPredictedPoint;
    public float atkSpeed = 4.0f;  //攻撃スピード
    public float atkRange = 400.0f;  //攻撃範囲
    public bool pushStartTimeFlag = true;
    public float comboLimitTime = 2;//コンボ可能時間
    public float nowLimitTime = 0; //現在のコンボ可能時間残り時間
    public bool collisonEnemy = false;
    public float PonyMoveRange = 10.0f;//仮
    public float PonyMoveSpeed = 5.0f;//仮
    public float twinSpCoolDown = 2f;//再利用化のまでの時間
    public bool twinEndSpCoolDown = false;//現在のクールダウン終了フラグ
    public int hairChangeCount = 1;
    public bool yes = true;
    private bool isFloor = false;   //着地フラグ
    public float atkCount = 0;          //現在の攻撃段階カウント
    public float hairChangeCoolDownTimer = 5f;
    public bool hairChangeCoolDownFlag = false;
    [SerializeField]
    public float DangSpCoolDown = 3.0f;//再利用化のまでの時間
    [SerializeField]
    public float DangSpCoolDown2 = 3.0f;//再利用化のまでの時間
    public bool DangEndSpCoolDownFlag = false;//現在のクールダウン終了フラグ
    public bool DangEndSpCoolDown2Flag = false;//現在のクールダウン終了フラグ
    private int fallDangCount = 0;
    public bool specialTwinJumpFlag = false;
    public float ponyCoolDownTimer = 1.0f;
    public bool ponyCoolDownFlag = false;
    public GameObject[] playerEffect = new GameObject[7];
    [SerializeField] Collider2D[] dangos = new Collider2D[2];
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
    [SerializeField]
    private DangoControl new1;
    [SerializeField]
    private DangoControl new2;
    [SerializeField]
    private GameObject backNew;
    [SerializeField]
    public DangoControl dangThorrw1;
    [SerializeField]
    public DangoControl dangThorrw2;
    [SerializeField]
    private Cut_In_view_Manager cutIn;
    // 音データの再生装置を格納する変数
    [SerializeField]
    private AudioSource audio;
    // 音データを格納する変数（Inspectorタブからも値を変更できるようにする）s
    [SerializeField]
    private AudioClip[] attack1Sound;
    //[SerializeField]
    //private Animator anim;
    // [SerializeField]
    // private AudioClip attack2Sound;
    //[SerializeField]
    // private AudioClip attack3Sound;
    private int count = 0;
    private bool _isAtkFrag = false;//攻撃中確認フラグ
    public StateType nowHairStyle;
    //[SerializeField]
    // private PlayerAttack plyAtk;
    Vector2 playerPos;
    Vector2 playerAtkPos;
    Vector3 offset;
    Vector3 target;
    Vector3 collisonEnemyPos;
    float deg;
    public Transform pPos;
    private bool ControlFlag = false;
    private bool fallboll1 = false;
    private bool fallboll2 = false;
    // Start is called before the first frame update
    void Start()
    {
        effect[0].SetActive(false);
        effect[1].SetActive(false);
        effect[2].SetActive(false);
        effect[3].SetActive(false);
        effect[4].SetActive(false);
        effect[5].SetActive(false);
        //  anim.SetBool("Cut_in", false);
        DangoColliderActive(false);
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
        dang1.SetActive(false);
        dang2.SetActive(false);
        // ゲームスタート時にAudioSource（音再生装置）のコンポーネントを加える
        audio = GetComponent<AudioSource>();
    }
    void DangoColliderActive(bool tri)
    {
        if (!tri) { dangos[0].enabled = false; dangos[1].enabled = false; }
        else { dangos[0].enabled = true; dangos[1].enabled = true; }

    }
    // Update is called once per frame
    void Update()
    {


        isFloor = floor.IsFloor();

        // CDebug.Log(hairChangeCount);
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
        if (DangEndSpCoolDownFlag || DangEndSpCoolDown2Flag)
        {
            DumpSpCoolDown();
        }
        if (ponyCoolDownFlag)
        {
            PonySpCoolDown();
        }
        //if(hairChangeCoolDownFlag)
        //{
        //    hairChangeCoolDownTimer -=Time.deltaTime;
        //    CDebug.Log(hairChangeCoolDownTimer);
        //    if (hairChangeCoolDownTimer>0)
        //    {

        //      //  anim.SetBool("Cut_in", false);
        //        hairChangeCoolDownFlag = false;
        //    }
        //}
    }
    //攻撃選択(通常、特殊攻撃、髪型変更)
    public void AttackSelection()
    {
        //通常攻撃ボタン
        if (Input.GetKeyDown(KeyCode.B) && ControlFlag == false)
        {
            //テスト用攻撃SE
            //  audio.PlayOneShot(attack1Sound);
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
                if (nowHairStyle == StateType.PONYTAIL)
                {
                    switch (atkCount)
                    {
                        case 1:
                            //テスト用攻撃SE
                          //  effect[3].SetActive(false);
                            effect[3].SetActive(true);
                            audio.PlayOneShot(attack1Sound[0]);
                            break;
                        case 2:
                            effect[4].SetActive(true);
                            effect[3].SetActive(false);
                            audio.PlayOneShot(attack1Sound[0]);
                            break;
                        case 3:
                            effect[4].SetActive(false);
                            effect[5].SetActive(true);
                            audio.PlayOneShot(attack1Sound[0]);
                            break;
                        default:
                            break;
                    }
                }
                else if (nowHairStyle == StateType.TWINTAIL)
                {
                    switch (atkCount)
                    {
                        case 1:
                            //テスト用攻撃SE
                            audio.PlayOneShot(attack1Sound[0]);
                            break;
                        case 2:
                            audio.PlayOneShot(attack1Sound[0]);
                            break;
                        case 3:
                           
                            audio.PlayOneShot(attack1Sound[0]);
                            break;
                        default:
                            break;
                    }
                }
                else if (nowHairStyle == StateType.DUMPLING)
                {
                    switch (atkCount)
                    {
                        case 1:
                            //テスト用攻撃SE
                            effect[0].SetActive(false);
                            audio.PlayOneShot(attack1Sound[0]);
                            break;
                        case 2:
                            effect[6].SetActive(true);
                            audio.PlayOneShot(attack1Sound[0]);
                            break;
                        case 3:
                            effect[0].SetActive(true);
                            audio.PlayOneShot(attack1Sound[0]);
                            break;
                        default:
                           
                            break;
                    }
                }
            }
        }
        //特殊攻撃ボタン
        else if (Input.GetKeyDown(KeyCode.N))
        {
            //特殊攻撃呼び出し
            SpecialAttack();
            // playerControl.isJump = false;
        }
       
        //髪型変更ボタン
        else if (Input.GetKeyDown(KeyCode.Alpha3) && cutIn.hairChangeCoolDownFlag == false)
        {
            //髪型変更カウントを1進める
            hairChangeCount++;
            //髪型変更呼び出し
            HairStyleChange();
            cutIn.StartCoroutine("Cut_In");
            // hairChangeCoolDownTimer -= 1.0f;

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
                ControlFlag = true;
                PonyTailSpecialAttack();
                audio.PlayOneShot(attack1Sound[0]);
                break;
            //現在の髪の毛がツインテールだったら
            case StateType.TWINTAIL:
               
                ControlFlag = true;
                TwinTailSpecialAttack();
                audio.PlayOneShot(attack1Sound[0]);
                specialTwinJumpFlag = true;
                playerControl.isJump = true;
                break;
            //現在の髪の毛がお団子だったら
            case StateType.DUMPLING:
                //ブロックに向かって60度の角度で射出
                //count++;
                ControlFlag = true;
                DumpSp();
              //  audio.PlayOneShot(attack1Sound[0]);
                // SetTarget(EnemyFLoor.transform.position, 55);
                break;
        }
    }
    //髪型変更
    private void HairStyleChange()
    {

        if (hairChangeCount >= 4)
        {
            //髪型変更カウントが3になったら0に戻す
            hairChangeCount = 1;

        }
        //現在の髪型変更カウント
        switch (hairChangeCount)
        {
            case 1:
                nowHairStyle = StateType.PONYTAIL;
                atkSpeed = 4f;
                atkRange = 400f;
                // CDebug.Log("Pony");
                dang1.SetActive(false);
                dang2.SetActive(false);
                //   anim.SetBool("Cut_in", true);
                hairChangeCoolDownFlag = true;
                //anim.SetBool("Cut_in", false);
                break;
            case 2:
                nowHairStyle = StateType.TWINTAIL;
                atkSpeed = 2f;
                atkRange = 280f;
                dang1.SetActive(false);
                dang2.SetActive(false);
                //   anim.SetBool("Cut_in", true);
                // CDebug.Log("Twin");
                break;
            case 3:
                nowHairStyle = StateType.DUMPLING;
                atkSpeed = 1f;
                atkRange = 160f;
                dang1.SetActive(true);
                dang2.SetActive(true);
                //   anim.SetBool("Cut_in", true);
                // CDebug.Log("Dump");
                break;
            default:

                // CDebug.Log("aaaaaaaaaaaaaaaaaaaaaa");
                break;
        }
        // if(hairChangeCoolDownTimer<0)
        // anim.SetBool("Cut_in", false);
    }


    //通常攻撃
    private void NormalAttack()
    {
        float temp = playerPos.x + atkRange;    //tempにプレイヤー座標+攻撃範囲を足す(右)
        float temp2 = playerPos.x - atkRange;   //tempにプレイヤー座標-攻撃範囲を引く(左)
        //通常攻撃範囲設定(ポニテ、ツインテ、お団子、現在の髪型&現在の攻撃コンボ数)
        if (nowHairStyle == StateType.PONYTAIL && atkCount == 1) { this.transform.localScale = new Vector3(6, 1, 1); }
        else if (nowHairStyle == StateType.PONYTAIL && atkCount == 2) { this.transform.localScale = new Vector3(6, 1, 1); }
        else if (nowHairStyle == StateType.PONYTAIL && atkCount == 3) { this.transform.localScale = new Vector3(6, 1, 1); }
        else if (nowHairStyle == StateType.TWINTAIL && atkCount == 1) { this.transform.localScale = new Vector3(4, 1, 1); }
        else if (nowHairStyle == StateType.TWINTAIL && atkCount == 2) { this.transform.localScale = new Vector3(4, 1, 1); }
        else if (nowHairStyle == StateType.TWINTAIL && atkCount == 3) { this.transform.localScale = new Vector3(4, 1, 1); }
        else if (nowHairStyle == StateType.DUMPLING && atkCount == 1) { this.transform.localScale = new Vector3(4, 4, 1); }
        else if (nowHairStyle == StateType.DUMPLING && atkCount == 2) { this.transform.localScale = new Vector3(4, 4, 1); }
        else if (nowHairStyle == StateType.DUMPLING && atkCount == 3) { this.transform.localScale = new Vector3(4, 4, 1); }
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
            this.transform.localScale = new Vector3(1, 1, 1);
            _isAtkFrag = false;
            // Debug.Log("成功しました");
        }

    }



    //お団子クールダウン
    private void DumpSpCoolDown()
    {
        //お団子のクールダウンから毎frame-1
        if (fallboll1)
            DangSpCoolDown -= 1f * Time.deltaTime;
        if (fallboll2)
            DangSpCoolDown2 -= 1f * Time.deltaTime;
        //DangSpCoolDownが０よりも小さかったら処理を実行
        if (DangSpCoolDown < 0)
        {
            //値をもとに戻す
            DangSpCoolDown = 3.0f;
            //当たり判定を元の大きさに戻す
            // new1.transform.position = new Vector3(playerPos.x - 70, playerPos.y + 360);
            new1.transform.position = backNew.transform.position;
            CDebug.ColorLog("ppp");
            new1.transform.localScale = new Vector3(1, 1, 1);
            //flagをfalseに
            fallboll1 = false;
            DangEndSpCoolDownFlag = false;
            ControlFlag = false;
            dangThorrw1.transform.SetParent(pPos);
            new1.img.sprite = new1.dangoImage[0];
            //コルーチン誤爆させないためのflag
            new1.dangoExplosionFlag = false;
            //dangoの透明度を10に設定する(戻す)
            new1.c.a = 10;
            //dangoの画像透明度に保存
            new1.img.color = new1.c;
            CDebug.Log("dango" + new1.img.color);
            DangoColliderActive(false);
        }
        if (DangSpCoolDown2 < 0)
        {
            //値をもとに戻す
            DangSpCoolDown2 = 3.0f;
            //当たり判定を元の大きさに戻す
            //new2.transform.position = new Vector3(playerPos.x - 70, playerPos.y + 360);
            new2.transform.position = backNew.transform.position;
            new2.transform.localScale = new Vector3(1, 1, 1);
            // flagをfalseに
            fallboll2 = false;
            DangEndSpCoolDown2Flag = false;
            ControlFlag = false;
            dangThorrw2.transform.SetParent(pPos);
            new2.img.sprite = new1.dangoImage[0];
            new2.c.a = 10;
            new2.img.color = new2.c;
            CDebug.Log("dango2" + new1.c.a);
            DangoColliderActive(false);
        }
    }
    //お団子特殊
    private void DumpSp()
    {
        count++;
        //CDebug.Log(count);
        switch (count)
        {
            case 1:
                new1.SetTarget(ThrowBollPredictedPoint.transform.position, 50);
                fallboll1 = true;
                DangoColliderActive(true);
                break;
            case 2:
                new2.SetTarget(ThrowBollPredictedPoint.transform.position, 50);
                fallboll2 = true;
                DangoColliderActive(true);
                break;
        }

        if (count == 2)
        {
            count = 0;
        }

    }

    //ポニーテール特殊攻撃
    public void PonyTailSpecialAttack()
    {
       
        if (ponyCoolDownFlag == false)
        {

            this.transform.position = new Vector2(playerPos.x + 400, playerPos.y + 200);
            this.transform.localScale = new Vector3(15, 1, 1);
            ponyCoolDownFlag = true;
        }
    }
    private void PonySpCoolDown()
    {

        ponyCoolDownTimer -= 1f * Time.deltaTime;
        if (ponyCoolDownTimer < 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.transform.position = new Vector2(playerPos.x, playerPos.y + 200);
            ponyCoolDownTimer = 1f;
            ponyCoolDownFlag = false;
            ControlFlag = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      //  ponyCoolDownFlag = false;
        if (collision.gameObject.tag == "Enemy" && !ponyCoolDownFlag)
        {
            CDebug.Log("test");
        }
    }
    //ツインテール特殊攻撃
    public void TwinTailSpecialAttack()
    {
        if (twinEndSpCoolDown == false && isFloor)
        {
            effect[1].SetActive(true);
            effect[2].SetActive(true);
            anim.SetBool("twinSpecial", true);
            //splayerControl.isJump = true;
            twinEndSpCoolDown = true;
            //  StartCoroutine(E(() => Debug.Log("hello")));
            this.transform.localScale = new Vector3(6, 6, 1);
            this.transform.position = new Vector2(playerAtkPos.x, playerAtkPos.y);
            php.playerATK += 5;
            // playerControl.rd2d.AddForce(Vector2.up * playerControl.flap,ForceMode2D.Impulse);

            //playerControl.rd2d.velocity= Vector2.up * playerControl.flap*20;

        }
    }
    //ツインテールクールダウン
    private void TwinSpCoolDown()
    {
        
        //ツインテールのクールダウンから毎frame-1
        twinSpCoolDown -= 1f * Time.deltaTime;
        //twinSpCoolDownが０よりも小さかったら処理を実行
        if (twinSpCoolDown < 0)
        {
            effect[1].SetActive(false);
            effect[2].SetActive(false);
            anim.SetBool("twinSpecial", false);
            //playerControl.isJump = false;
            //値をもとに戻す
            twinSpCoolDown = 1f;
            //当たり判定を元の大きさに戻す
            this.transform.localScale = new Vector3(1, 1, 1);
            //flagをfalseに
            twinEndSpCoolDown = false;
            ControlFlag = false;
            //  playerControl.Jumping();
            //  specialTwinJumpFlag = false;
        }
    }


    //攻撃のクールダウン用
    private void CoolDown()
    {
        //現在のコンボ可能時間が次コンボ可能時間より大きかったなら
        if (nowLimitTime > comboLimitTime)
        {
            effect[5].SetActive(false);
            effect[1].SetActive(false);
            effect[2].SetActive(false);
            effect[0].SetActive(false);
            effect[3].SetActive(false);
            effect[4].SetActive(false);
            effect[6].SetActive(false);
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