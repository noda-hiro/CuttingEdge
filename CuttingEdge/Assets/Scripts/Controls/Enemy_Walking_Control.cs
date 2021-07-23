using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;


public class Enemy_Walking_Control : MonoBehaviour
{
    // �񑫕��s�̗̑�
    [SerializeField]
    int HP = 40;
    // �񑫕��s�̍U����
    [SerializeField]
    int ATK = 10;
    [SerializeField]
    // �񑫕��s�̈ړ����x
    float SPEED = 0.0f;

    Transform target;
   
    // �Q�[���I�u�W�F�N�g������ϐ�
    GameObject EW;
    // Enemy_Create�̃X�N���v�g������ϐ�
    Enemy_Create EW_Script;

    //��c�ǉ��_
    public PlayerStatus nowStatus;
    
    private HPControl playerHP;

    // Start is called before the first frame update
    void Start()
    {
        // �Q�[���I�u�W�F�N�g�𖼑O����擾���ĕϐ��ɓ����
        EW = GameObject.Find("Test");
        // EW�̒��ɂ���Enemy_Create���擾���ĕϐ��ɓ����
        EW_Script = EW.GetComponent<Enemy_Create>();
        CDebug.ColorLog(gameObject.name, "blue");
        //playerHP = GetComponent<HPControl>();
        playerHP = GameObject.Find("Player").GetComponent<HPControl>();
        // �v���C���[�̃I�u�W�F�N�g�̈ʒu���擾
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        // HP���Ȃ��Ȃ����玩�g��j��
        if (HP == 0)
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }

        

        //�����ƒǂ�������
        float move = SPEED * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, move);

    }
    //��c�ǉ��_2
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
