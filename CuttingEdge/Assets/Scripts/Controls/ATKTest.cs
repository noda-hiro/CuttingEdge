using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerAttack;
using CustomDebug;
public class ATKTest : MonoBehaviour
{
    public GameObject prefab;
    public GameObject Enemy;
    public float atkSpeed = 10.0f;  //�U���X�s�[�h
    public float atkRange = 20.0f;  //�U���͈�
    public bool pushStartTimeFlag = true;
    public float comboLimitTime = 2;//�R���{�\����
    public float nowLimitTime = 0; //���݂̃R���{�\���Ԏc�莞��
    public bool collisonEnemy = false;
    public float PonyMoveRange = 10.0f;//��
    public float PonyMoveSpeed = 5.0f;//��
    public float twinSpCoolDown = 1.0f;//�ė��p���̂܂ł̎���
    public bool twinEndSpCoolDown = false;//���݂̃N�[���_�E���I���t���O
    public int hairChangeCount = 0;
    private bool isFloor = false;   //���n�t���O
    public float atkCount = 0;          //���݂̍U���i�K�J�E���g
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
    private bool _isAtkFrag = false;//�U�����m�F�t���O
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
        //�v���C���[�̍��W���
        playerPos = player.transform.position;
        //���g�̍��W����
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

    }
    //�U���I��(�ʏ�A����U���A���^�ύX)
    public void AttackSelection()
    {
        //�ʏ�U���{�^��
        if (Input.GetKeyDown(KeyCode.B))
        {
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
            }
        }
        //����U���{�^��
        else if (Input.GetKeyDown(KeyCode.N))
        {
            //����U���Ăяo��
            SpecialAttack();
        }
        //���^�ύX�{�^��
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //���^�ύX�J�E���g��1�i�߂�
            hairChangeCount++;
            //���^�ύX�Ăяo��
            HairStyleChange();
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
                PonyTailSpecialAttack();
                break;
            //���݂̔��̖т��c�C���e�[����������
            case StateType.TWINTAIL:
                TwinTailSpecialAttack();
                break;
            //���݂̔��̖т����c�q��������
            case StateType.DUMPLING:
                //�u���b�N�Ɍ�������60�x�̊p�x�Ŏˏo
                SetTarget(Enemy.transform.position, 55);
                break;
        }
    }
    //���^�ύX
    private void HairStyleChange()
    {
        //���݂̔��^�ύX�J�E���g
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
        //���^�ύX�J�E���g
        if (hairChangeCount == 3)
        {
            //���^�ύX�J�E���g��3�ɂȂ�����0�ɖ߂�
            hairChangeCount = 0;
        }

    }


    //�ʏ�U��
    private void NormalAttack()
    {

        float temp = playerPos.x + atkRange;    //temp�Ƀv���C���[���W+�U���͈͂𑫂�(�E)
        float temp2 = playerPos.x - atkRange;   //temp�Ƀv���C���[���W-�U���͈͂�����(��)

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
            _isAtkFrag = false;
            Debug.Log("�������܂���");
        }
    }

    //���c�q����U�� 
    //���c�q����U���R���[�`��
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
    

    //�|�j�[�e�[������U��
    public void PonyTailSpecialAttack()
    {


    }

    //�c�C���e�[������U��
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
        //�c�C���e�[���̃N�[���_�E�����疈frame-1
        twinSpCoolDown -= 1f * Time.deltaTime;
        //twinSpCoolDown���O���������������珈�������s
        if (twinSpCoolDown < 0)
        {
            //�l�����Ƃɖ߂�
            twinSpCoolDown = 1.0f;
            //�����蔻������̑傫���ɖ߂�
            this.transform.localScale = new Vector3(1, 1, 1);
            //flag��false��
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
    //�U���̃N�[���_�E���p
    private void CoolDown()
    {
        //���݂̃R���{�\���Ԃ����R���{�\���Ԃ��傫�������Ȃ�
        if (nowLimitTime > comboLimitTime)
        {
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
