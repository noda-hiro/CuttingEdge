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
	// エネミー・飛翔の移動速度
	float SPEED = 0.0f;

	// おぎゃりたてかどうか
	bool spawnFlag = true;

	// IEnumeratorで停止する秒数
	float StopTime = 0.5f;

	// EM_forPlayerでプレイヤーに向かって移動するときに必要なやつ
	private GameObject PlayerObject;

	// 可視状態かどうか
	private bool isVisible = false;
	// ↑のプロパティー
	public bool IsVisible
	{
		get { return isVisible; }
	}

	// Babuが終わったかどうかのフラグ
	bool babu_endFlag = false;
	// Flying_Actのifが実行中かどうかのフラグ
	bool duringFlag = false;
	// Flying_Act -> 移動用のフラグ
	bool moveFlag = false;
	// Flying_Act -> 待機用のフラグ
	bool idleFlag = false;

	// 移動用のタイマー
	float moveTimer = 4.0f;
	// 待機用のタイマー
	float idleTimer = 3.0f;

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		// プレイヤーのオブジェクトの座標を取得
		PlayerObject = GameObject.Find("Test_Player");
	}

	// Update is called once per frame
	void Update()
	{
		// 時間で乱数のシード値を変えている
		Random.InitState(System.DateTime.Now.Millisecond);

		// HPなくなったら死ぬ。大人しく死にな！
		Death();

		// おぎゃおぎゃしたので実行！
		Babu();

		if (babu_endFlag == true)
		{
			Flying_Act();
		}

		// 4秒間、プレイヤーに向かって移動をする
		if (moveFlag == true)
		{
			Debug.Log("MOVE");

			// プレイヤーに向かって移動
			EM_forPlayer();

			// moveTimerを1秒ずつ減らす
			moveTimer -= 1.0f * Time.deltaTime;

			// moveTimerが0秒以下なら、実行
			if (moveTimer <= 0)
			{
				// moveFlagをfalseにする
				moveFlag = false;
				// duringFlagをfalseにする
				duringFlag = false;
			}

		}

		// 3秒間、その場で待機
		if (idleFlag == true)
		{
			Debug.Log("IDLE");

			// idleTimerを1秒ずつ減らす
			idleTimer -= 1.0f * Time.deltaTime;

			// idleTimerが0秒以下なら、実行
			if (idleTimer <= 0)
			{
				// idleFlagをfalseにする
				idleFlag = false;
				// duringFlagをfalseにする
				duringFlag = false;
			}
		}
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

	// エネミーが、プレイヤーに向かって移動する関数
	private void EM_forPlayer()
	{
		float move = SPEED * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, PlayerObject.transform.position, move);
	}

	// スポーンしたてのときに実行する関数
	private void Babu()
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

	// エネミーの行動関数
	private void Flying_Act()
	{
		// Randomクラスのインスタンスを生成
		var act_rand_instance = new System.Random();
		// 適当な乱数を生成する
		int enemy_act = act_rand_instance.Next();
		// 生成された乱数を4で割った余りを求める
		int flying_act = enemy_act % 4;

		// 割った余りの値によって行動を変える
		if (flying_act == 0)                // 移動
		{
			// duringFlag がfalseなら実行
			if (duringFlag == false)
			{
				// moveFlagとduringFlagをtrueにする
				moveFlag = true;
				duringFlag = true;

				moveTimer = 4.0f;
			}

			// Debug.Log("move");
		}
		else if (flying_act == 1)       // 待機
		{
			// duringFlag がfalseなら実行
			if (duringFlag == false)
			{
				// idleFlagとduringFlagをtrueにする
				idleFlag = true;
				duringFlag = true;

				idleTimer = 3.0f;
			}

			// Debug.Log("stay");
		}
		else if (flying_act == 2)       // 突進攻撃
		{
			// Debug.Log("attack");
		}
		else if (flying_act == 3)       // 真空波攻撃
		{
			// Debug.Log("wave_attack");
		}
	}

	// Babu専用のオブジェクト停止関数
	IEnumerator Stop()
	{
		 EM_forPlayer();

		// 0.5秒待つ
		yield return new WaitForSeconds(StopTime);

		// おぎゃりたてでなくなる
		spawnFlag = false;

		Debug.Log("おぎゃりを不可能状態");

		// 2秒待つ
		yield return new WaitForSeconds(2);

		babu_endFlag = true;

		Debug.Log("babuが終わりました");
		// Debug.Log(spawnFlag);
	}
}