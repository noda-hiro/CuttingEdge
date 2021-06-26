using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UI;
public class PlayerControl : MonoBehaviour
{
    //インスペクターで設定する
    public float speed;
    public float jumpMoveSpeed;//速度
    //[SerializeField] private 
    public float flap = 10.0f;      //ジャンプ力
    public float gravity;           //重力
    //public int playerHp = 5;        //playerの体力(仮)
    public PlayerAttack att;
   
    [SerializeField]
    public FloorCheck floor;
    [SerializeField]
    public Animator animator;
    [SerializeField]
    private AtkMove atk;
 
    
    // public RectTransform hero = null;
    //public GroundDCheck head;     //頭をぶつけた判定
    //private bool isHead = false;  //頭衝突フラグ
    // private Slider _slider;         //Sliderの値を代入する_sliderを宣言
    //public GameObject slider;       //体力ゲージに指定するSlider

    //プライベート変数
    //フラグ一覧
    private bool isFloor = false;   //着地フラグ
    private bool isJump = false;    //ジャンプフラグ
    private bool isWrok = true;     //歩行フラグ
    public bool isRight = false;
   // float riSpeed, maxSpeed = 2f;
    private Rigidbody2D rd2d;
   // private bool nowbool;

    public Vector3 movement;
    
    private void Start()
    {
       // hero = GetComponent<RectTransform>();
        rd2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("is1combo", false);
        animator.SetBool("is2combo", false);
        atk.atkCount = 0;
        
        //_slider = slider.GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    att.State = PlayerAttack.StateType.TWINTAIL;
        //    Debug.Log("Twin");
        //}
        
        //接地判定を得る
        //地面の判定
        isFloor = floor.IsFloor();

        
        if (isWrok)
        {
           

        }
        // if(!isWrok)
        //{

        //}
        //地面についているとき
        if (isFloor)
        {
            Move();
            Jump();
        }
        //if(isJump)
        //{

        //}
        switch (atk.atkCount)
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


    private void Move()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        //isFloor = true;
        
        //Debug.Log(Input.GetAxis("Horizontal"));
        //右入力で右向きに動く
        if (horizontalKey > 0)
        {
            rd2d.velocity += new Vector2(speed, 0) * (Time.deltaTime * 300);
            animator.SetBool("isrun", true);
            transform.localScale = new Vector3(1, 1, 1);
           // Debug.LogError("push");
           // Debug.Log("右");
            isRight = true;
        }
        //左入力で左向きに動く
        else if (horizontalKey < 0)
        {
            rd2d.velocity += new Vector2(-speed, 0) * (Time.deltaTime * 300);
            animator.SetBool("isrun", true);
            transform.localScale = new Vector3(-1, 1, 1);
            // Debug.Log("左");
            isRight = false;
        }
        if (horizontalKey == 0)
        {
            animator.SetBool("isrun", false);
            rd2d.velocity = Vector2.zero;
        }
    }

   public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (isFloor == true)            //上方向に力を加える(ジャンプ)
            {
                rd2d.AddForce(Vector2.up * flap);
               
            }
         
            
        }
    }
   
}
