using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Walking_Control : MonoBehaviour
{
	// 二足歩行の体力
	[SerializeField]
	int HP = 40;
	// 二足歩行の攻撃力
	[SerializeField]
	int ATK = 10;
	[SerializeField]
	// 二足歩行の移動速度
	float SPEED = 0.0f;
	
	Vector3 targetPosition;

	// ゲームオブジェクトが入る変数
	GameObject EW;
	// Enemy_Createのスクリプトが入る変数
	Enemy_Create EW_Script;

	// Start is called before the first frame update
	void Start()
	{
		// ゲームオブジェクトを名前から取得して変数に入れる
		EW = GameObject.Find("Test");
		// EWの中にあるEnemy_Createを取得して変数に入れる
		EW_Script = EW.GetComponent<Enemy_Create>();
	}

	// Update is called once per frame
	void Update()
	{
		// HPがなくなったら自身を破棄
		if (HP == 0)
		{
			Destroy(this.gameObject);
		}

		// プレイヤーのオブジェクトの位置を取得
		targetPosition = GameObject.Find("Test_Player").transform.position;

		//ずっと追いかける
		float move = SPEED * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, move);
		
	}
}
