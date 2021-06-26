using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;

public class Enemy_Flying_Control : MonoBehaviour
{
    // エネミー・飛翔の体力
    [SerializeField]
    int HP = 35;
    // エネミー・飛翔の攻撃力
    [SerializeField]
    int ATK = 8;
    [SerializeField]
    // 二足歩行の移動速度
    float SPEED = 0.0f;

    Vector3 targetPosition;
    public PlayerStatus nowStatus;

    private SpriteRenderer SR = null;
    private HPControl playerHP;
    //野田追加点
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
            //   Debug.Log("飛んでるやつが画面に入ったよ");
        }

        if (HP == 0)
        {
            Destroy(this.gameObject);
        }

        // プレイヤーのオブジェクトの位置を取得
        targetPosition = GameObject.Find("Player").transform.position;

        //ずっと追いかける
        float move = SPEED * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, move);
    }

    //野田追加点2
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
