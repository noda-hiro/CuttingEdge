using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerAttack;

public class Cut_In : MonoBehaviour
{
    [SerializeField] ATKTest pAtk;
   
    // [SerializeField] private Sprite[] img=new Sprite[3]; // まっくらな画像
    [SerializeField] public Sprite[] Img = new Sprite[3];
    //[SerializeField]  public GameObject image;
    [SerializeField] UnityEngine.UI.Image nowImage;
    public int count = 0;
    public float fadeSpeed = 0.01f;
    public float startTestAlfa = 1.0f;
    public float startTest2Alfa = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //  nowImage =image.GetComponent<Image>();
        //nowImage = GetComponent<Image>();
        // nowImage = Img[0];
        // Img[0] = Img[0];
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //StartCoroutine(Test());

            StartCoroutine(Test());
        }

    }
    IEnumerator Test()
    {
        switch (pAtk.nowHairStyle)
        {
            case StateType.PONYTAIL: nowImage.sprite = Img[0]; Debug.Log("img"); break;
            case StateType.TWINTAIL: nowImage.sprite = Img[1]; Debug.Log("img2"); break;
            case StateType.DUMPLING: nowImage.sprite = Img[2]; Debug.Log("img3"); break;
        }
        //true
        Color c = nowImage.color;
        c.a = startTest2Alfa; //0.0f
        nowImage.color = c;
        while (c.a <= startTestAlfa)
        {
            Debug.Log("第一コルーチン");
            c.a += (Time.deltaTime * fadeSpeed);
            nowImage.color = c;
            Debug.Log(this.nowImage.color);
            //if (c.a <= startTestAlfa)
            //{
            //    c.a = startTestAlfa;
            //    img.color = c;
            //    break;
            //}
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(Test2());
        // false

    }
    IEnumerator Test2()
    {
        Color c = nowImage.color;
        c.a = startTestAlfa; //1.0f
        nowImage.color = c;
        while (startTest2Alfa <= c.a)
        {
            Debug.Log("第二コルーチン");
            c.a -= (Time.deltaTime * fadeSpeed);
            nowImage.color = c;
            Debug.Log(this.nowImage.color);
            yield return null;
            //if (c.a >= startTest2Alfa)
            //{
            //    c.a = startTest2Alfa;
            //    img.color = c;
            //    break;
            //}
        }

        Debug.Log(this.nowImage.color);
    }

}

