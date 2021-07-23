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
    public float atkSpeed = 4.0f;  //�U���X�s�[�h
    public float atkRange = 400.0f;  //�U���͈�
    public bool pushStartTimeFlag = true;
    public float comboLimitTime = 2;//�R���{�\����
    public float nowLimitTime = 0; //���݂̃R���{�\���Ԏc�莞��
    public bool collisonEnemy = false;
    public float PonyMoveRange = 10.0f;//��
    public float PonyMoveSpeed = 5.0f;//��
    public float twinSpCoolDown = 2f;//�ė��p���̂܂ł̎���
    public bool twinEndSpCoolDown = false;//���݂̃N�[���_�E���I���t���O
    public int hairChangeCount = 1;
    public bool yes = true;
    private bool isFloor = false;   //���n�t���O
    public float atkCount = 0;          //���݂̍U���i�K�J�E���g
    public float hairChangeCoolDownTimer = 5f;
    public bool hairChangeCoolDownFlag = false;
    [SerializeField]
    public float DangSpCoolDown = 3.0f;//�ė��p���̂܂ł̎���
    [SerializeField]
    public float DangSpCoolDown2 = 3.0f;//�ė��p���̂܂ł̎���
    public bool DangEndSpCoolDownFlag = false;//���݂̃N�[���_�E���I���t���O
    public bool DangEndSpCoolDown2Flag = false;//���݂̃N�[���_�E���I���t���O
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
    // ���f�[�^�̍Đ����u���i�[����ϐ�
    [SerializeField]
    private AudioSource audio;
    // ���f�[�^���i�[����ϐ��iInspector�^�u������l��ύX�ł���悤�ɂ���js
    [SerializeField]
    private AudioClip[] attack1Sound;
    //[SerializeField]
    //private Animator anim;
    // [SerializeField]
    // private AudioClip attack2Sound;
    //[SerializeField]
    // private AudioClip attack3Sound;
    private int count = 0;
    private bool _isAtkFrag = false;//�U�����m�F�t���O
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
        //�v���C���[�̍��W���
        playerPos = player.transform.position;
        //���g�̍��W����
        playerAtkPos = this.transform.position;

        player.transform.position = new Vector2(playerPos.x, playerPos.y);
        playerControl = GetComponentInParent<PlayerControl>();
        pAtk = GetComponent<PlayerAttack>();
        nowHairStyle = StateType.PONYTAIL;
        this.transform.position = new Vector2(playerPos.x, playerPos.y + 200);
        dang1.SetActive(false);
        dang2.SetActive(false);
        // �Q�[���X�^�[�g����AudioSource�i���Đ����u�j�̃R���|�[�l���g��������
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
        //�v���C���[�̍��W���
        playerPos = player.transform.position;
        //���g�̍��W����
        playerAtkPos = this.transform.position;
        //�U���I���Ăяo��
        AttackSelection();
        //�ʏ�U���̃R���{�p�N�[���_�E���Ăяo��
        CoolDown();
        //�c�C���e�[���̃N�[���_�E���t���O��true�������ꍇ
        if (twinEndSpCoolDown)
        {
            //�c�C���e�[���̃N�[���_�E���Ăяo��
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
    //�U���I��(�ʏ�A����U���A���^�ύX)
    public void AttackSelection()
    {
        //�ʏ�U���{�^��
        if (Input.GetKeyDown(KeyCode.B) && ControlFlag == false)
        {
            //�e�X�g�p�U��SE
            //  audio.PlayOneShot(attack1Sound);
            //�ʏ�U���Ăяo��
            NormalAttack();
            //CDebug.Log("������܂���");
            pushStartTimeFlag = true;
            //�t���O��true��
            _isAtkFrag = true;
            //  Debug.Log(nowMode);
            //���݂̃R���{�\���Ԃ����R���{�\���Ԃ�菬���������Ȃ�
            if (nowLimitTime < comboLimitTime)
            {
                
                //���R���{�J�E���g��+����(1�i�ڂ�������2�i�ڂ�)
                atkCount++;
                //�R���{�\���Ԃ����Z�b�g
                nowLimitTime = 0;
                if (nowHairStyle == StateType.PONYTAIL)
                {
                    switch (atkCount)
                    {
                        case 1:
                            //�e�X�g�p�U��SE
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
                            //�e�X�g�p�U��SE
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
                            //�e�X�g�p�U��SE
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
        //����U���{�^��
        else if (Input.GetKeyDown(KeyCode.N))
        {
            //����U���Ăяo��
            SpecialAttack();
            // playerControl.isJump = false;
        }
       
        //���^�ύX�{�^��
        else if (Input.GetKeyDown(KeyCode.Alpha3) && cutIn.hairChangeCoolDownFlag == false)
        {
            //���^�ύX�J�E���g��1�i�߂�
            hairChangeCount++;
            //���^�ύX�Ăяo��
            HairStyleChange();
            cutIn.StartCoroutine("Cut_In");
            // hairChangeCoolDownTimer -= 1.0f;

        }
    }

    //����U��
    public void SpecialAttack()
    {
        //���݂̔��^
        switch (nowHairStyle)
        {
            //���݂̔��̖т��|�j�[�e�[����������
            case StateType.PONYTAIL:
                ControlFlag = true;
                PonyTailSpecialAttack();
                audio.PlayOneShot(attack1Sound[0]);
                break;
            //���݂̔��̖т��c�C���e�[����������
            case StateType.TWINTAIL:
               
                ControlFlag = true;
                TwinTailSpecialAttack();
                audio.PlayOneShot(attack1Sound[0]);
                specialTwinJumpFlag = true;
                playerControl.isJump = true;
                break;
            //���݂̔��̖т����c�q��������
            case StateType.DUMPLING:
                //�u���b�N�Ɍ�������60�x�̊p�x�Ŏˏo
                //count++;
                ControlFlag = true;
                DumpSp();
              //  audio.PlayOneShot(attack1Sound[0]);
                // SetTarget(EnemyFLoor.transform.position, 55);
                break;
        }
    }
    //���^�ύX
    private void HairStyleChange()
    {

        if (hairChangeCount >= 4)
        {
            //���^�ύX�J�E���g��3�ɂȂ�����0�ɖ߂�
            hairChangeCount = 1;

        }
        //���݂̔��^�ύX�J�E���g
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


    //�ʏ�U��
    private void NormalAttack()
    {
        float temp = playerPos.x + atkRange;    //temp�Ƀv���C���[���W+�U���͈͂𑫂�(�E)
        float temp2 = playerPos.x - atkRange;   //temp�Ƀv���C���[���W-�U���͈͂�����(��)
        //�ʏ�U���͈͐ݒ�(�|�j�e�A�c�C���e�A���c�q�A���݂̔��^&���݂̍U���R���{��)
        if (nowHairStyle == StateType.PONYTAIL && atkCount == 1) { this.transform.localScale = new Vector3(6, 1, 1); }
        else if (nowHairStyle == StateType.PONYTAIL && atkCount == 2) { this.transform.localScale = new Vector3(6, 1, 1); }
        else if (nowHairStyle == StateType.PONYTAIL && atkCount == 3) { this.transform.localScale = new Vector3(6, 1, 1); }
        else if (nowHairStyle == StateType.TWINTAIL && atkCount == 1) { this.transform.localScale = new Vector3(4, 1, 1); }
        else if (nowHairStyle == StateType.TWINTAIL && atkCount == 2) { this.transform.localScale = new Vector3(4, 1, 1); }
        else if (nowHairStyle == StateType.TWINTAIL && atkCount == 3) { this.transform.localScale = new Vector3(4, 1, 1); }
        else if (nowHairStyle == StateType.DUMPLING && atkCount == 1) { this.transform.localScale = new Vector3(4, 4, 1); }
        else if (nowHairStyle == StateType.DUMPLING && atkCount == 2) { this.transform.localScale = new Vector3(4, 4, 1); }
        else if (nowHairStyle == StateType.DUMPLING && atkCount == 3) { this.transform.localScale = new Vector3(4, 4, 1); }
        //Player���E�ɐi�񂾂�E���ɍU��
        if (playerControl.isRight)//�v���C���[�̌������E)
        {
            this.transform.localPosition += new Vector3(atkSpeed, 0) * (Time.deltaTime * 300);

            //Player.GetComponent<PlayerControl>().speed = 5;
        }
        //Player�����ɐi�񂾂獶���ɍU��
        else
        {
            this.transform.localPosition += new Vector3(atkSpeed, 0) * (Time.deltaTime * 300);
        }
        //���̈ʒu�ɒB������obj�����݂�player���W�ɖ߂�
        if (temp < playerAtkPos.x || temp2 > playerAtkPos.x)
        {
            this.transform.position = new Vector2(playerPos.x, playerPos.y + 200);
            this.transform.localScale = new Vector3(1, 1, 1);
            _isAtkFrag = false;
            // Debug.Log("�������܂���");
        }

    }



    //���c�q�N�[���_�E��
    private void DumpSpCoolDown()
    {
        //���c�q�̃N�[���_�E�����疈frame-1
        if (fallboll1)
            DangSpCoolDown -= 1f * Time.deltaTime;
        if (fallboll2)
            DangSpCoolDown2 -= 1f * Time.deltaTime;
        //DangSpCoolDown���O���������������珈�������s
        if (DangSpCoolDown < 0)
        {
            //�l�����Ƃɖ߂�
            DangSpCoolDown = 3.0f;
            //�����蔻������̑傫���ɖ߂�
            // new1.transform.position = new Vector3(playerPos.x - 70, playerPos.y + 360);
            new1.transform.position = backNew.transform.position;
            CDebug.ColorLog("ppp");
            new1.transform.localScale = new Vector3(1, 1, 1);
            //flag��false��
            fallboll1 = false;
            DangEndSpCoolDownFlag = false;
            ControlFlag = false;
            dangThorrw1.transform.SetParent(pPos);
            new1.img.sprite = new1.dangoImage[0];
            //�R���[�`���딚�����Ȃ����߂�flag
            new1.dangoExplosionFlag = false;
            //dango�̓����x��10�ɐݒ肷��(�߂�)
            new1.c.a = 10;
            //dango�̉摜�����x�ɕۑ�
            new1.img.color = new1.c;
            CDebug.Log("dango" + new1.img.color);
            DangoColliderActive(false);
        }
        if (DangSpCoolDown2 < 0)
        {
            //�l�����Ƃɖ߂�
            DangSpCoolDown2 = 3.0f;
            //�����蔻������̑傫���ɖ߂�
            //new2.transform.position = new Vector3(playerPos.x - 70, playerPos.y + 360);
            new2.transform.position = backNew.transform.position;
            new2.transform.localScale = new Vector3(1, 1, 1);
            // flag��false��
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
    //���c�q����
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

    //�|�j�[�e�[������U��
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
    //�c�C���e�[������U��
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
    //�c�C���e�[���N�[���_�E��
    private void TwinSpCoolDown()
    {
        
        //�c�C���e�[���̃N�[���_�E�����疈frame-1
        twinSpCoolDown -= 1f * Time.deltaTime;
        //twinSpCoolDown���O���������������珈�������s
        if (twinSpCoolDown < 0)
        {
            effect[1].SetActive(false);
            effect[2].SetActive(false);
            anim.SetBool("twinSpecial", false);
            //playerControl.isJump = false;
            //�l�����Ƃɖ߂�
            twinSpCoolDown = 1f;
            //�����蔻������̑傫���ɖ߂�
            this.transform.localScale = new Vector3(1, 1, 1);
            //flag��false��
            twinEndSpCoolDown = false;
            ControlFlag = false;
            //  playerControl.Jumping();
            //  specialTwinJumpFlag = false;
        }
    }


    //�U���̃N�[���_�E���p
    private void CoolDown()
    {
        //���݂̃R���{�\���Ԃ����R���{�\���Ԃ��傫�������Ȃ�
        if (nowLimitTime > comboLimitTime)
        {
            effect[5].SetActive(false);
            effect[1].SetActive(false);
            effect[2].SetActive(false);
            effect[0].SetActive(false);
            effect[3].SetActive(false);
            effect[4].SetActive(false);
            effect[6].SetActive(false);
            //���R���{�J�E���g�����Z�b�g(1�i�ڂɖ߂�)
            atkCount = 0;
            //�R���{�\���Ԃ����Z�b�g
            nowLimitTime = 0;
        }
        //_isAtkFlag��true�Ȃ�
        if (_isAtkFrag)
            NormalAttack();
        //N�������ꂽ�ꍇ�����J�n
        if (pushStartTimeFlag)
            nowLimitTime += Time.deltaTime;
        //�U���i�K�J�E���g��3�ُ�ɂȂ����珈�����J�n
        if (atkCount > 3)
        {
            //�U���i�K�J�E���g��0�ɂ���
            atkCount = 0;
            //���݂̌o�ߎ��Ԃ�0�ɂ���
            nowLimitTime = 0;
        }
    }
}

/*
�c�C���e�[����2�i�W�����v 


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