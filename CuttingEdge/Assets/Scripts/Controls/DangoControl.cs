using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;
using UnityEngine.UI;

public class DangoControl : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip audioClip;
    public bool dangoExplosionFlag = false;
    public float fadeSpeed = 100f;
    public float startTestAlfa = 1.0f;
    public float start2TestAlfa = 2.0f;
    Vector3 offset;
    Vector3 target;
    float deg;
    private bool explodingFlag = false;
    public Sprite[] dangoImage = new Sprite[2];
    [SerializeField]
    private ATKTest atk;
    [SerializeField]
    public Transform parentPosition;
    [SerializeField]
    private PlayerControl playerControl;
    public Image img;
    //[SerializeField]
    //public Transform pPos;
    public Color c /*= this.img.color*/;
    private void Start()
    {
        audio = GetComponent<AudioSource>();

        img = GetComponent<Image>();
        //     SetTarget(enmy.transform.position, 55);
    }
    private void Update()
    {
        //CDebug.Log(playerControl.isRight);
    }
    //private void ThrowBall()//
    IEnumerator ThrowBall()
    {
       
      
        float b = Mathf.Tan(deg * Mathf.Deg2Rad);
        float a = (target.y - b * target.x) / (target.x * target.x);

        CDebug.Log(playerControl.isRight);

        for (float X = 0; X <= this.target.x; X += 20f)
        {
            float y = a * X * X + b * X;
            // if (playerControl.isRight)
            //{
            transform.position = new Vector3(X, y, 0) + offset; CDebug.Log("左");
            //}
            //else if (playerControl.isRight == false)
            //{
            //    transform.position = new Vector3(-X, y, 0) + offset; CDebug.Log("左");
            //}

            yield return null;
        }

    }

    //IEnumerator ThrowBallLeft()
    //{
    //    float b = Mathf.Tan(deg * Mathf.Deg2Rad);
    //    float a = (target.y - b * target.x) / (target.x * target.x);
    //    for (float X = 0; X <= this.target.x; X += 20f)
    //    {
    //        float y = a * X * X + b * X;
    //        CDebug.Log("else");
    //        CDebug.Log(playerControl.isRight);
    //        if (playerControl.isRight == false)
    //        { transform.position = new Vector3(X, y, 0) + offset; CDebug.Log("右"); }
    //        else if (playerControl.isRight == true)
    //        { transform.position = new Vector3(X, y, 0) + offset; CDebug.Log("a"); }
    //        yield return null;
    //    }

    //}
    public void SetTarget(Vector3 target, float deg)
    {
        if (atk.DangEndSpCoolDownFlag == false)
        {
            atk.DangEndSpCoolDownFlag = true;
            this.offset = transform.position;
            this.target = target - this.offset;
            this.deg = deg;
            if (playerControl.isRight)
                StartCoroutine("ThrowBall");
            //if (playerControl.isRight == false)
            //   StartCoroutine("ThrowBallLeft");

        }
        else if (atk.DangEndSpCoolDown2Flag == false)
        {
            //Debug.Log("dang2");
            atk.DangEndSpCoolDown2Flag = true;
            this.offset = transform.position;
            this.target = target - this.offset;
            this.deg = deg;
            if (playerControl.isRight)
                StartCoroutine("ThrowBall");
            // if (playerControl.isRight == false)
            // StartCoroutine("ThrowBallLeft");
        }
    }
    IEnumerator ExplositonScaleDown()
    {
        c = this.img.color;
        c.a = startTestAlfa; //1.0f
        this.img.color = c;
        // Debug.Log("testttttttttttttttt");
        while (start2TestAlfa >= c.a && dangoExplosionFlag)
        {
            c.a -= (Time.deltaTime * fadeSpeed);
            img.color = c;
            // Debug.Log(this.img.color);
            this.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);

            if (c.a <= 0)
            {
                c.a = 0;
                img.color = c;
                this.transform.localScale = new Vector3(1, 1, 1);
                dangoExplosionFlag = false;
            }
            yield return null;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Attack"
            && collision.gameObject.tag != "Dumpling")
        {
            CDebug.Log("hit");
            //dangoの大きさを変更(爆発)
            this.transform.localScale = new Vector3(8, 8, 1);
            //親をcanvasに一時的に移動させる
            this.transform.SetParent(parentPosition);
            //爆発画像に差し替える
            img.sprite = dangoImage[1];
            dangoExplosionFlag = true;
            audio.PlayOneShot(audioClip);
            //dango爆発中flag
            if (dangoExplosionFlag)
                //(爆発範囲、透明度変化)のコルーチン開始
                StartCoroutine(ExplositonScaleDown());
        }
        //c.a = 1;
        //img.color = c;
        //CDebug.Log(c.a);
        // 団子画像 Image[] dango = new Image[2] ok
        // 投げる ok
        // 触れる ok
        // 爆発用の画像に切り替える ok
        // スケールを徐々にあげる、アルファーを徐々に下げる ok
        // 消えたら画像をもとに戻しスケールと位置を戻す まだ ok
    }

}
