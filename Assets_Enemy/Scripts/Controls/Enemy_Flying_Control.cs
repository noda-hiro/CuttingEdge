using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private SpriteRenderer SR = null;

	// Start is called before the first frame update
	void Start()
	{
		SR = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if(SR.isVisible)
		{
			Debug.Log("飛んでるやつが画面に入ったよ");
		}

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
