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
	// 二足歩行の移動速度
	[SerializeField]
	float SPEED = 0.0f;

	// おぎゃりたてかどうか
	bool spawnFlag = true;

	// IEnumeratorで停止する秒数
	float StopTime = 1f;

	// プレイヤーに向かって移動するときに必要なやつ
	Vector3 targetPosition;

	// 可視状態かどうか
	private bool isVisible = false;
	// ↑のプロパティー
	public bool IsVisible
	{
		get { return isVisible; }
	}

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		// HPなくなったら死ぬ。大人しく死にな！
		Death();

		// 座標を取得するよ～ん^^
		Coordinate();

		// おぎゃおぎゃしたので実行！
		babubabu();
	}

	/*----------------------------------------------------------------------------------------------------*/

	// HPがなくなったら自分を破棄する関数
	private void Death()
	{
		if (HP == 0)
		{
			Destroy(this.gameObject);
		}
	}

	// 座標を取得する関数
	private void Coordinate()
	{
		// プレイヤーのオブジェクトの座標を取得
		targetPosition = GameObject.Find("Test_Player").transform.position;
	}

	// エネミーが、プレイヤーに向かって移動する関数
	private void EM_forPlayer()
	{
		float move = SPEED * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, move);
	}

	// スポーンしたてのときに実行する関数
	private void babubabu()
	{
		// おぎゃりたての間だけ実行する
		if (spawnFlag == true)
		{
			// 画面内に入ってなかったら、プレイヤーに向かってずっと動き続ける
			if (isVisible == false)
			{
				// うごうご
				EM_forPlayer();
			}
			// 画面内に入ったら、しばらく動いた後に止まるまる
			else if (isVisible == true)
			{
				// 一定時間後に止まる
				StartCoroutine("Stop");
			}
		}
	}

	// 可視状態になったら呼ばれる関数
	private void OnBecameVisible()
	{
		isVisible = true;
	}

	// オブジェクトの停止関数
	IEnumerator Stop()
	{
		EM_forPlayer();
		// 1秒待つ
		yield return new WaitForSeconds(StopTime);
		// おぎゃりたてでなくなる
		spawnFlag = false;
		// Debug.Log(spawnFlag);
	}
}
