using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
	// �{�^��
	Button Stage1;
	Button Stage2;

	// Start is called before the first frame update
	void Start()
	{
		// �{�^���R���|�[�l���g�̎擾
		Stage1 = GameObject.Find("/Canvas/Stage1_Select_Button").GetComponent<Button>();
		Stage2 = GameObject.Find("/Canvas/Stage2_Select_Button").GetComponent<Button>();

		// �ŏ��ɑI����Ԃɂ������{�^���̐ݒ�
		Stage1.Select();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}