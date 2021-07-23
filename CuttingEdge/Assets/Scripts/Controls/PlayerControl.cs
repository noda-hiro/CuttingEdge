using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerAttack;
using CustomDebug;
//using UnityEngine.UI;
public class PlayerControl : MonoBehaviour
{
    //インスペクターで設定する
    public float speed;
    public float jumpMoveSpeed;//速度
    //[SerializeField] private 
    public float flap = 1200.0f;      //ジャンプ力
    public float gravity;           //重力
    //public int playerHp = 5;        //playerの体力(仮)
    public PlayerAttack att;
    public int jumpCount = 0;
    public int jumpMaxCount = 2;
    [SerializeField]
    public FloorCheck floor;
    [SerializeField]
    public Animator animator;
    [SerializeField]
    private AtkMove atk;
    [SerializeField]
    private ATKTest atkTest;
    [SerializeField]
    private StateType nowPlayerControlHairStyle;
    // public RectTransform hero = null;
    //public GroundDCheck head;     //頭をぶつけた判定
    //private bool isHead = false;  //頭衝突フラグ
    // private Slider _slider;         //Sliderの値を代入する_sliderを宣言
    //public GameObject slider;       //体力ゲージに指定するSlider
    //プライベート変数
    //フラグ一覧
    private bool isFloor = false;   //着地フラグ
    public bool isJump = false;    //ジャンプフラグ
    private bool isWrok = true;     //歩行フラグ
    public bool isRight = false;
    // float riSpeed, maxSpeed = 2f;
    public Rigidbody2D rd2d;
    // private bool nowbool;
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    public Vector3 movement;
    public PlayerNowAction playerNowAction;
    public bool fixedDirection = false;
    public enum PlayerAction
    {
        IDLE,
        RUN,
        JUMP,
    }
    public PlayerAction Nowaction;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        // hero = GetComponent<RectTransform>();
        rd2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //animator.SetBool("is1combo", false);
        //animator.SetBool("is2combo", false);
        atk.atkCount = 0;
        Nowaction = PlayerAction.IDLE;
        //_slider = slider.GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update()
    {

        //接地判定を得る
        //地面の判定
        isFloor = floor.IsFloor();

        if (isWrok)
        {


        }
        // if(!isWrok)
        //{
       // if(isFloor)
        //{ atkTest.specialTwinJumpFlag = false; }
       // CDebug.Log(atkTest.specialTwinJumpFlag);
        //}
        //地面についているとき
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            Jump();
            isJump = true;
        }
        
        Move();
        Jumping();
       
        //if (isJump)
        //{
        //    this.rd2d.velocity -= Vector2.down * Time.deltaTime * 20;
        //    animator.SetBool("Jump", true);
        //    Nowaction = PlayerAction.JUMP;
        //    fixedDirection = false;
        //}
        //else if (!isJump)
        //{
        //    rd2d.velocity = new Vector2(rd2d.velocity.x, 0);
        //    // Nowaction = PlayerAction.IDLE;
        //    fixedDirection = true;
        //}
        if (atkTest.specialTwinJumpFlag)
        {
            //isJump = true;
            specialTwinJump();
            //  atkTest.specialTwinJumpFlag = false;
        }
        //Jumping();
        atkTest.specialTwinJumpFlag = false;
        if (atkTest.nowHairStyle == StateType.PONYTAIL)
        {
            switch (atkTest.atkCount)
            {
                case 0:
                    animator.SetBool("is1combo", false);
                    animator.SetBool("is2combo", false);
                    animator.SetBool("is3combo", false);
                    animator.SetBool("Final", true);
                    break;
                case 1:
                    animator.SetBool("is1combo", true);
                    animator.SetBool("is2combo", false);
                    animator.SetBool("is3combo", false);
                    animator.SetBool("Final", false);
                    //CDebug.Log("1combo"+atkmv.atkCount);
                    break;
                case 2:
                    animator.SetBool("is2combo", true);
                    animator.SetBool("is1combo", false);
                    animator.SetBool("is3combo", false);
                    animator.SetBool("Final", false);
                    //CDebug.Log("2combo"+atkmv.atkCount);
                    break;
                case 3:
                    animator.SetBool("is2combo", false);
                    animator.SetBool("is1combo", false);
                    animator.SetBool("is3combo", true);
                    animator.SetBool("Final", false);
                    //CDebug.Log("3combo"+atkmv.atkCount);
                    break;
                case 4:
                    animator.SetBool("is2combo", false);
                    animator.SetBool("is1combo", false);
                    animator.SetBool("is3combo", false);
                    animator.SetBool("Final", true);
                    atk.atkCount = 0;
                    break;
            }
        }
       else if (atkTest.nowHairStyle == StateType.TWINTAIL)
        {
            switch (atkTest.atkCount)
            {
                case 0:
                    animator.SetBool("twin1combo", false);
                    animator.SetBool("twin2combo", false);
                    animator.SetBool("twin3combo", false);
                    animator.SetBool("TwinFinal", true);
                    break;
                case 1:
                    animator.SetBool("twin1combo", true);
                    animator.SetBool("twin2combo", false);
                    animator.SetBool("twin3combo", false);
                    animator.SetBool("TwinFinal", false);
                    //CDebug.Log("1combo"+atkmv.atkCount);
                    break;
                case 2:
                    animator.SetBool("twin2combo", true);
                    animator.SetBool("twin1combo", false);
                    animator.SetBool("twin3combo", false);
                    animator.SetBool("TwinFinal", false);
                    //CDebug.Log("2combo"+atkmv.atkCount);
                    break;
                case 3:
                    animator.SetBool("twin2combo", false);
                    animator.SetBool("twin1combo", false);
                    animator.SetBool("twin3combo", true);
                    animator.SetBool("TwinFinal", false);
                    //CDebug.Log("3combo"+atkmv.atkCount);
                    break;
                case 4:
                    animator.SetBool("twin2combo", false);
                    animator.SetBool("twin1combo", false);
                    animator.SetBool("twin3combo", false);
                    animator.SetBool("TwinFinal", true);
                    atk.atkCount = 0;
                    break;
            }
        }
     else   if (atkTest.nowHairStyle == StateType.DUMPLING)
        {
            switch (atkTest.atkCount)
            {
                case 0:
                    animator.SetBool("dumplings1combo", false);
                    animator.SetBool("dumplings2combo", false);
                    animator.SetBool("dumplings3combo", false);
                    animator.SetBool("DumplingsFinal", true);
                    break;
                case 1:
                    animator.SetBool("dumplings1combo", true);
                    animator.SetBool("dumplings2combo", false);
                    animator.SetBool("dumplings3combo", false);
                    animator.SetBool("DumplingsFinal", false);
                    //CDebug.Log("1combo"+atkmv.atkCount);
                    break;
                case 2:
                    animator.SetBool("dumplings2combo", true);
                    animator.SetBool("dumplings1combo", false);
                    animator.SetBool("dumplings3combo", false);
                    animator.SetBool("DumplingsFinal", false);
                    //CDebug.Log("2combo"+atkmv.atkCount);
                    break;
                case 3:
                    animator.SetBool("dumplings2combo", false);
                    animator.SetBool("dumplings1combo", false);
                    animator.SetBool("dumplings3combo", true);
                    animator.SetBool("DumplingsFinal", false);
                    //CDebug.Log("3combo"+atkmv.atkCount);
                    break;
                case 4:
                    animator.SetBool("dumplings2combo", false);
                    animator.SetBool("dumplings1combo", false);
                    animator.SetBool("dumplings3combo", false);
                    animator.SetBool("DumplingsFinal", true);
                    atk.atkCount = 0;
                    break;
            }
        }
    }


    private void Move()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        //isFloor = true;
        if (fixedDirection) { Nowaction = PlayerAction.RUN; }
        animator.SetBool("Jump", false);
        switch (atkTest.nowHairStyle)
        {
            case StateType.PONYTAIL:
                animator.SetBool("isrun", true);
                animator.SetBool("isTwinRun", false);
                animator.SetBool("isDumplingsRun", false);
                break;
            case StateType.TWINTAIL:
                animator.SetBool("isTwinRun", true);
                animator.SetBool("isrun", false);
                animator.SetBool("isDumplingsRun", false);
                break;
            case StateType.DUMPLING:
                animator.SetBool("isTwinRun", false);
                animator.SetBool("isrun", false);
                animator.SetBool("isDumplingsRun", true);
                break;
        }
        //Debug.Log(Input.GetAxis("Horizontal"));
        //右入力で右向きに動く
        if (horizontalKey > 0)
        {

            if (Nowaction == PlayerAction.RUN){
                audio.PlayOneShot(audioClip);
            }
            rd2d.velocity += new Vector2(speed, 0) * (Time.deltaTime * 300);
            if (Nowaction != PlayerAction.JUMP)
                transform.localScale = new Vector3(1, 1, 1);
            // Debug.LogError("push");
            // Debug.Log("右");
            isRight = true;
        }
        //左入力で左向きに動く
        else if (horizontalKey < 0)
        {
            rd2d.velocity += new Vector2(-speed, 0) * (Time.deltaTime * 300);
            if(Nowaction!=PlayerAction.JUMP)
            transform.localScale = new Vector3(-1, 1, 1);
            // Debug.Log("左");
            isRight = false;
        }
        else if (horizontalKey == 0 && isJump == false)
        {
            Nowaction = PlayerAction.IDLE;
            animator.SetBool("isrun", false);
            animator.SetBool("isTwinRun", false);
            animator.SetBool("isDumplingsRun", false);
            animator.SetBool("Jump", false);
            rd2d.velocity = Vector2.zero;
           
        }
    }

    public void Jump()
    {

        if (atkTest.nowHairStyle == StateType.TWINTAIL
            && jumpCount < jumpMaxCount
            &&atkTest.specialTwinJumpFlag==false)
        {
          //  audio.PlayOneShot(audioClip);
            jumpCount += 1;
            //CDebug.Log(atkTest.nowHairStyle);
            // CDebug.Log("aaaaaaaaaaaaaaaaa");
            // CDebug.Log(jumpCount + "jumo");
            // rd2d.velocity = Vector2.zero;
            rd2d.velocity = Vector2.up * flap;
        }
        else if (isFloor == true)            //上方向に力を加える(ジャンプ)
        {
           // audio.PlayOneShot(audioClip);
            //  CDebug.Log("osi");
            //rd2d.AddForce(Vector2.up * flap,ForceMode2D.Impulse);
            //  rd2d.velocity += new Vector2(0,flap);
            rd2d.velocity = Vector2.up * flap;
            
        }
       
    }
    public void Jumping()
    {
        
      // atkTest.specialTwinJumpFlag = false;
        if (isJump)
        {
            rd2d.velocity -= Vector2.down * Time.deltaTime * 20;
            animator.SetBool("Jump", true);
            Nowaction = PlayerAction.JUMP;
            fixedDirection = false;
        }
        else if (!isJump)
        {
            rd2d.velocity = new Vector2(rd2d.velocity.x, 0);
            // Nowaction = PlayerAction.IDLE;
            fixedDirection = true;
        }
    }
    public void specialTwinJump()
    {
        Jump();
        isJump = true;
    }
}
