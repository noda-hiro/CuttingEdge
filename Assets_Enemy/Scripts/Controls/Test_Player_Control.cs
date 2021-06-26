using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
 #####�e�X�g�ō쐬�������̂Ȃ̂ŁA�����Ă�����Ă��\���܂��� #####

  */

public class Test_Player_Control : MonoBehaviour
{
	[SerializeField]
	int HP = 10;

	// HP�̃v���p�e�B�[
	public int hp
	{
		get { return HP; }
		set { HP = value; }
	}


	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log(HP);

		if(HP <= 0)
		{
			Destroy(this.gameObject);
		}

		 if (Input.GetKey(KeyCode.LeftArrow))
		{
			this.transform.Translate(-5f, 0.0f, 0.0f);
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			this.transform.Translate(5f, 0.0f, 0.0f);
		}
	}
}
