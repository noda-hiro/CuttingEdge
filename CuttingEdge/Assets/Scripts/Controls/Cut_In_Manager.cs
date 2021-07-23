using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;
public class Cut_In_Manager : MonoBehaviour
{
 
   
    public GameObject MoveImg;
    public GameObject Startimg;
    public GameObject goolimg;
    public GameObject iimg;
    private bool finishFlag = false;
    Vector3 startPos;
    Vector3 interimPos;
    Vector3 endPos;
    private void Start()
    {
        MoveImg.gameObject.SetActive(false);
        startPos = Startimg.transform.position;
        interimPos = iimg.transform.position;
        endPos = goolimg.transform.position;

        Debug.Log(startPos);
        Debug.Log(endPos);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && !finishFlag)
            StartCoroutine(CutIn());
    }
    IEnumerator CutIn()
    {
        finishFlag = true;
        MoveImg.gameObject.SetActive(true);
        while (startPos.x >= interimPos.x)
        {
            CDebug.Log("aa");
            MoveImg.transform.position -= new Vector3(50, 0, 0);
            startPos = MoveImg.transform.position;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(CutIN2());
        finishFlag = false;
    }
    IEnumerator CutIN2()
    {
        Debug.Log("�Ă΂ꂽ");
        while (interimPos.x >= endPos.x)
        {
            Debug.Log("�Ă΂ꂽpart2");
            MoveImg.transform.position -= new Vector3(50, 0, 0);
            interimPos = MoveImg.transform.position;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        MoveImg.transform.position = Startimg.transform.position;
        startPos = MoveImg.transform.position;
        interimPos = iimg.transform.position;
        MoveImg.gameObject.SetActive(false);
    }
}
  



