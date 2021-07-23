using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerAttack;
using CustomDebug;
public class Cut_In_view_Manager : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField] ATKTest pAtk;
    [SerializeField] public Sprite[] Img = new Sprite[3];
    //[SerializeField]  public GameObject image;
    [SerializeField] UnityEngine.UI.Image nowImage;
    public bool hairChangeCoolDownFlag = false;
    // Start is called before the first frame update
    public IEnumerator Cut_In()
    {
        
        hairChangeCoolDownFlag = true;
        switch (pAtk.nowHairStyle)
        {
            case StateType.PONYTAIL: nowImage.sprite = Img[0]; Debug.Log("img"); break;
            case StateType.TWINTAIL: nowImage.sprite = Img[1]; Debug.Log("img2"); break;
            case StateType.DUMPLING: nowImage.sprite = Img[2]; Debug.Log("img3"); break;
        }
        anim.SetBool("Cut_in", true);
        CDebug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        yield return new WaitForSeconds(3f);
        anim.SetBool("Cut_in", false);
        hairChangeCoolDownFlag = false;
    }
}
