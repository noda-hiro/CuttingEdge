using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;

public class Enemy_Flying_Control : MonoBehaviour
{
    // �G�l�~�[�E���Ă̗̑�
    [SerializeField]
    int HP = 35;
    // �G�l�~�[�E���Ă̍U����
    [SerializeField]
    int ATK = 8;
    [SerializeField]
    // �񑫕��s�̈ړ����x
    float SPEED = 0.0f;

    Vector3 targetPosition;
    public PlayerStatus nowStatus;

    private SpriteRenderer SR = null;
    private HPControl playerHP;
    //��c�ǉ��_
    //[SerializeField]
    //public PlayerStatus nowStatus;
    //[SerializeField]
    //public HPControl playerHP;
    // Start is called before the first frame update
    void Start()
    {

        SR = GetComponent<SpriteRenderer>();
        playerHP = GameObject.Find("Player").GetComponent<HPControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SR.isVisible)
        {
            //   Debug.Log("���ł�����ʂɓ�������");
        }

        if (HP == 0)
        {
            Destroy(this.gameObject);
        }

        // �v���C���[�̃I�u�W�F�N�g�̈ʒu���擾
        targetPosition = GameObject.Find("Player").transform.position;

        //�����ƒǂ�������
        float move = SPEED * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, move);
    }

    //��c�ǉ��_2
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            var p = collision.gameObject.GetComponent<HPControl>();
            if (null != p) p.Damege(ATK);
           // this.gameObject.SetActive(false);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            this.gameObject.SetActive(false);
            nowStatus = playerHP.psType;

            CDebug.Log("aaaaaaa");
            if (nowStatus == PlayerStatus.Dryness)
            {
                playerHP.playerHp -= 5;
            }
        }
    }

}
