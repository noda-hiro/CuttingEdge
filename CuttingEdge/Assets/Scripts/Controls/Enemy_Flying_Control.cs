using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Flying_Control : MonoBehaviour
{
	private HPControl playerHP;
	
	// エネミー・飛翔の体力
	[SerializeField]
	int HP = 35;
	// エネミー・飛翔の攻撃力
	[SerializeField]
	int ATK = 8;
	// エネミー・飛翔の移動速度
	[SerializeField]
	float SPEED = 0.0f;
	// エネミー・飛翔の突進速度
	[SerializeField]
	float RushSpeed = 0.0f;

	// ATKのプロパティー
	public int atk
	{
		get { return ATK; }
	}

	// GameObjectを入れておくやつ
	private GameObject PlayerObject;

	// 座標を入れておくやつ
	private Vector3 BackPosition;
	private Vector3 PlayerPosition;

	// 可視状態かどうか
	private bool isVisible = false;
	// ↑のプロパティー
	public bool IsVisible
	{
		get { return isVisible; }
	}

	// おぎゃりたてかどうか
	bool spawnFlag = true;
	// Babuが終わったかどうかのフラグ
	bool babu_endFlag = false;
	// Flying_Act_Flagのifが実行中かどうかのフラグ
	bool duringFlag = false;
	// 座標の取得をしたかどうかのフラグ
	bool coordinate_getFlag = false;
	// 真空波攻撃をもう放ったかどうかのフラグ
	bool fireFlag = false;

	// Flying_Act -> 移動用のフラグ
	bool moveFlag = false;
	// Flying_Act -> 待機用のフラグ
	bool idleFlag = false;
	// Flying_Act -> 突進攻撃用のフラグ
	bool rushFlag = false;
	// Flying_Act -> 真空波攻撃用のフラグ
	bool aerial_slashFlag = false;

	// 移動用のタイマー
	float moveTimer = 4.0f;
	// 待機用のタイマー
	float idleTimer = 3.0f;
	// 突進攻撃用のタイマー
	float rushTimer = 4.0f;
	// 真空波攻撃用のタイマー
	float aerial_slashTimer = 3.0f;

	// IEnumeratorで停止する秒数
	float StopTime = 0.5f;

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		playerHP = GameObject.Find("Player").GetComponent<HPControl>();
		// プレイヤーのオブジェクトを取得
		PlayerObject = GameObject.Find("Player");
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

		// 各種行動のフラグ行動管理
		if (babu_endFlag == true)
		{
			Flying_Act_Flag();
		}

		// MOVE
		// 4秒間、プレイヤーに向かって移動をする
		if (moveFlag == true)
		{
			Debug.Log("MOVE");

			// moveTimerを1秒ずつ減らす
			moveTimer -= 1.0f * Time.deltaTime;

			// moveTimerが4.0未満で3.0以上のときに実行
			if (3.0 <= moveTimer && moveTimer < 4.0)
			{
				// 座標を1回だけ取得
				Player_Position();

				// 取得した座標に向かって移動
				EM_forPlayer();

				// coordinate_getFlagをfalseにする
				coordinate_getFlag = false;
			}
			// moveTimerが3.0未満で2.0以上のときに実行
			else if (2.0 <= moveTimer && moveTimer < 3.0)
			{
				// 座標を1回だけ取得
				Player_Position();

				// 取得した座標に向かって移動
				EM_forPlayer();

				// coordinate_getFlagをfalseにする
				coordinate_getFlag = false;
			}
			// moveTimerが2.0未満で1.0以上のときに実行
			else if (1.0 <= moveTimer && moveTimer < 2.0)
			{
				// 座標を1回だけ取得
				Player_Position();

				// 取得した座標に向かって移動
				EM_forPlayer();

				// coordinate_getFlagをfalseにする
				coordinate_getFlag = false;
			}
			// moveTimerが1.0未満で0.0以上のときに実行
			else if (0.0 <= moveTimer && moveTimer < 1.0)
			{
				// 座標を1回だけ取得
				Player_Position();

				// 取得した座標に向かって移動
				EM_forPlayer();

				// coordinate_getFlagをfalseにする
				coordinate_getFlag = false;
			}
			// moveTimerが0秒以下なら実行
			else if (moveTimer <= 0)
			{
				// moveFlagをfalseにする
				moveFlag = false;
				// duringFlagをfalseにする
				duringFlag = false;
			}
		}

		// IDLE
		// 3秒間、その場で待機
		if (idleFlag == true)
		{
			Debug.Log("IDLE");

			// idleTimerを1秒ずつ減らす
			idleTimer -= 1.0f * Time.deltaTime;

			// idleTimerが0秒以下なら実行
			if (idleTimer <= 0)
			{
				// idleFlagをfalseにする
				idleFlag = false;
				// duringFlagをfalseにする
				duringFlag = false;
			}
		}

		// RUSH_ATTACK
		// 2秒かけてプレイヤーの座標まで移動、後に2秒かけて元のyに戻る
		if (rushFlag == true)
		{
			Debug.Log("RUSH");

			// rushTimerを1秒ずつ減らす
			rushTimer -= 1.0f * Time.deltaTime;

			// coordinate_getFlagがfalseなら実行
			if (coordinate_getFlag == false)
			{
				// 自身の座標を取得
				BackPosition = transform.position;
				// プレイヤーの座標を取得
				PlayerPosition = PlayerObject.transform.position;
				// 取得したのでtrueにする
				coordinate_getFlag = true;
			}

			// rushTimerが2秒以上、3秒未満なら実行
			if (2.0 <= rushTimer && rushTimer < 3.0)
			{
				// プレイヤーに向かって突進
				Attack_Rush();
			}
			// rushTimerが0秒より上、2.0秒未満なら実行
			else if (0.0 < rushTimer && rushTimer < 2.0)
			{
				// 戻る
				Rush_Back();
			}
			//rushTimerが0秒以下なら実行
			else if (rushTimer <= 0)
			{
				// coordinate_getFlagをfalseにする
				coordinate_getFlag = false;
				// rushFlagをfalseにする
				rushFlag = false;
				// duringFlagをfalseにする
				duringFlag = false;
			}
		}

		// AERIAL-SLASH_ATTACK
		// ゆっくり気味にプレイヤーへ向かって飛ぶ
		if (aerial_slashFlag == true)
		{
			Debug.Log("AERIAL-SLASH");

			// aerial_slashTimerを1秒ずつ減らす
			aerial_slashTimer -= 1.0f * Time.deltaTime;

			// aerial_slashTimerが2秒以上、3秒未満なら実行
			if (2.0 <= aerial_slashTimer && aerial_slashTimer < 3.0)
			{
				// Debug.Log("攻撃準備");
				// 攻撃準備
			}
			// aerial_slashTimerが0秒より上、2秒未満なら実行
			else if (0.0 < aerial_slashTimer && aerial_slashTimer < 2.0)
			{
				// Debug.Log("攻撃");

				// すでに真空波を放っていたら実行
				if (fireFlag == true)
				{
					// Debug.Log("発射済みだよ〜ん");
				}
				// まだ真空波を放っていなかったら実行
				else if (fireFlag == false)
				{
					// Debug.Log("発射！");

					// 真空波プレハブをGameObject型で取得
					//GameObject aerialSlash = (GameObject)Resources.Load("Prefabs/Flying_AerialSlash");
					// 真空波プレハブを元に、インスタンスを生成
					//Instantiate(aerialSlash, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

					// fireFlagをtrueにする
					fireFlag = true;
				}
			}
			// aerial_slashTimerが0秒以下なら、実行
			else if (aerial_slashTimer <= 0)
			{
				// fireFlagをfalseにする
				fireFlag = false;
				// aerial_slashFlagをfalseにする
				aerial_slashFlag = false;
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

	// プレイヤーの座標を1回だけ取得する関数
	private void Player_Position()
	{
		// coordinateFlagがfalseのとき実行
		if (coordinate_getFlag == false)
		{
			// プレイヤーの座標を取得
			PlayerPosition = PlayerObject.transform.position;

			// coordinateFlagをtrueにする
			coordinate_getFlag = true;
		}
	}

	// エネミーが、プレイヤーに向かって移動する関数
	private void EM_forPlayer()
	{
		float move = SPEED * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, PlayerPosition, move);
	}

	// 突進用関数
	private void Attack_Rush()
	{
		float rush = RushSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, PlayerPosition * 2, rush);
	}

	// 突進後に戻る用関数
	private void Rush_Back()
	{
		float back = SPEED * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, BackPosition, back);
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
				// 座標を1回だけ取得
				Player_Position();
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
	private void Flying_Act_Flag()
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

				// Timerを元に戻す
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

				// Timerを元に戻す
				idleTimer = 3.0f;
			}

			// Debug.Log("stay");
		}
		else if (flying_act == 2)       // 突進攻撃
		{
			// duringFlag がfalseなら実行
			if (duringFlag == false)
			{
				// rushFlagとduringFlagをtrueにする
				rushFlag = true;
				duringFlag = true;

				// Timerを元に戻す
				rushTimer = 4.0f;
			}

			// Debug.Log("attack");
		}
		else if (flying_act == 3)       // 真空波攻撃
		{
			// duringFlag がfalseなら実行
			if (duringFlag == false)
			{
				// aerial_slashFlagとduringFlagをtrueにする
				aerial_slashFlag = true;
				duringFlag = true;

				// Timerを元に戻す
				aerial_slashTimer = 3.0f;
			}

			// Debug.Log("wave_attack");
		}
	}

	// Babu専用のオブジェクト停止コルーチン
	IEnumerator Stop()
	{
		// 1回だけプレイヤーの座標を取得
		Player_Position();
		// 取得した座標に向かって動く
		EM_forPlayer();

		// 0.5秒待つ
		yield return new WaitForSeconds(StopTime);

		// おぎゃりたてでなくなる
		spawnFlag = false;

		// Debug.Log("おぎゃりを不可能状態");

		// 2秒待つ
		yield return new WaitForSeconds(2);

		babu_endFlag = true;

		// Debug.Log("babuが終わりました");
		// Debug.Log(spawnFlag);
	}
	//野田追加点2
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{

			var p = collision.gameObject.GetComponent<HPControl>();
			if (null != p) p.Damege(ATK);
			// this.gameObject.SetActive(false);

		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Attack"|| collision.gameObject.tag == "Dumpling")
		{
			this.gameObject.SetActive(false);
		//	nowStatus = playerHP.psType;

		//	//CDebug.Log("aaaaaaa");
		//	if (nowStatus == PlayerStatus.Dryness)
		//	{
		//		playerHP.playerHp -= 5;
		//	}
		}
	}
}