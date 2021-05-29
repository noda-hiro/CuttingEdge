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
    public float atkSpeed = 10.0f;  //�U���X�s�[�h
    private bool _isAtkFrag = false;//�U�����m�F�t���O
   // public float offensiveAbility = 10;//player�̍U����
    private StateType nowMode;      //���݂̃��[�h
    Vector2 PlayerPos;
    Vector2 PlayerAtkPos;
    public float atkRange = 20.0f;  //�U���͈�

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //�v���C���[�̍��W���
        PlayerPos = player.transform.position;
        //���g�̍��W����
        PlayerAtkPos = this.transform.position;

        player.transform.position = new Vector2(PlayerPos.x, PlayerPos.y);
        playerControl = GetComponent<PlayerControl>();
        pAtk = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    public void Update()
    {
        //�v���C���[�̍��W���
        PlayerPos = player.transform.position;

        //���g�̍��W����
        PlayerAtkPos = this.transform.position;

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //�t���O��true��
            _isAtkFrag = true;
            Debug.Log(nowMode);
            // Debug.Log("�����܂���");
        }

        //_isAtkFlag��true�Ȃ�
        if (_isAtkFrag)
        {
            //���݂̔��^
            switch (nowMode)
            {
                //���݂̔��^���|�j�e�Ȃ�
                case StateType.PONYTAIL:
                    
                    PlayerAttack1();
                    break;
                //���݂̔��^���c�C���e�[���Ȃ�
                case StateType.TWINTAIL:
                    PlayerAttack1();
                    break;
            }
            // PlayerAttack1();
        }
        //���^�ύX�Ăяo��
        ModeChange();
    }
    //�U��
    private void PlayerAttack1()
    {
        float temp = PlayerPos.x + atkRange;    //temp�Ƀv���C���[���W+�U���͈͂𑫂�(�E)
        float temp2 = PlayerPos.x - atkRange;   //temp�Ƀv���C���[���W-�U���͈͂�����(��)

        //Player���E�ɐi�񂾂�E���ɍU��
        if (player.GetComponent<PlayerControl>().isRight)//�v���C���[�̌������E)
        {
            this.transform.localPosition += new Vector3(atkSpeed, 0);

            //Player.GetComponent<PlayerControl>().speed = 5;
        }
        //Player�����ɐi�񂾂獶���ɍU��
        else
        {
            this.transform.localPosition += new Vector3(atkSpeed, 0);
        }
        //���̈ʒu�ɒB������obj�����݂�player���W�ɖ߂�
        if (temp < PlayerAtkPos.x || temp2 > PlayerAtkPos.x)
        {
            this.transform.position = new Vector2(PlayerPos.x, PlayerPos.y + 83);
            _isAtkFrag = false;
            Debug.Log("�������܂���");
        }

    }

    //���^�ύX
    private void ModeChange()
    {
        //���݂̔��^����c�C���e�[���ɕύX
        if (Input.GetKeyDown(KeyCode.T))
        {
            nowMode = StateType.TWINTAIL;
        }
        //���݂̔��^����|�j�[�e�[���ɕύX
        if (Input.GetKeyDown(KeyCode.P))
        {
            nowMode = StateType.PONYTAIL;
        }
    }
}
