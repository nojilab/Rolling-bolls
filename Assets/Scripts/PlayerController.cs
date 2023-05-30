using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;                    // プレイヤーの速さ
    private TextMeshProUGUI scoreText;                      // スコアのテキスト
    private DataManager dataManager;                        // データ管理のクラス
    private Rigidbody rb;                                   // rigidbody
    private Vector3 movement;                               // 進行方向
    public int score;                                      // スコア
    public int getPickupNumber;                            //取得したPickUpの個数
    public int fallNumber;                                 //ステージから落ちた回数
    public int jumpNumber;                                 //ジャンプ台に乗った回数

    //-------落とし穴関係の変数---------------------------

    //通常状態のプレイヤーのレイヤー。
	LayerMask defaultPlayerLayer;

    //落下状態のプレイヤーのレイヤー。
	LayerMask fallenPlayerLayer;

    //落下状態のフラグ。
	public bool isFalling;

    //落下してから落下状態がリセットされるまでの距離。
	float endFallDistance = -10.0f;

    //落下コルーチン管理用変数。
	Coroutine fall;

    //落下開始位置。
	float startFallPositionY;

    // -----------------------------------------------


    // ---------ジャンプ台関係の変数--------------------
    private float jumpForce = 5.0f;
    // ------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();                     // rigidbodyの取得
        movement = new Vector3(0.0f, 0.0f, 0.0f);           // 進行方向の初期化
        score = 0;                                          // scoreの初期化
        getPickupNumber = 0;                                // getPickupNumberの初期化
        fallNumber = 0;                                     // fallNumberの初期化
        jumpNumber = 0;                                     // jumpNuberの初期化
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score:" + score.ToString();       // 表示するスコアの更新
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
    }

    // 落下のためのレイヤを設定
    void Awake()
	{
		defaultPlayerLayer = LayerMask.NameToLayer("Player");
		fallenPlayerLayer = LayerMask.NameToLayer("FallenPlayer");
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(movement*speed);                                        // プレイヤーに力を加える(物理演算)
        
        if(this.transform.position.y < -10.0) {
            this.transform.position = new Vector3(9, 2, -9);
            score -= 50;
            fallNumber++;                                                   // 表示する「落下回数」の更新
            GameObject.Find("TimeCounter").GetComponent<TimeCounter>().FallNumber = fallNumber;
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 moveDirection = value.Get<Vector2>();                       // キー入力の方向を取得
        movement = new Vector3(moveDirection.x, 0.0f, moveDirection.y);     // 3次元に変換
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))                           // PickUpタグに接触したとき
        {
            Destroy(other.gameObject);                                      // オブジェクトを消去する
            score += 100;                                                   // スコアに100ポイント加点する
            scoreText.text = "Score:" + score.ToString();
            GameObject.Find("TimeCounter").GetComponent<TimeCounter>().Score = score;
            getPickupNumber++;                                              // 表示する「取得したPickUpの個数」の更新
            GameObject.Find("TimeCounter").GetComponent<TimeCounter>().GetPickupNumber = getPickupNumber;
            dataManager.UpdateData(score.ToString() + "," + GameObject.Find("TimeCounter").GetComponent<TimeCounter>().countdown.ToString()); // csvファイルに書き込むデータを追加
        }

        if(other.gameObject.CompareTag("Pit"))                              // Pitタグに接触したとき
        {
            StartFall();
        }

        if(other.gameObject.CompareTag("JumpCube"))                           // Pitタグに接触したとき
        {
            // 当たった相手のRigidbodyコンポーネントを取得して、上向きの力を加える
			rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            jumpNumber++;                                                     // 表示する「ジャンプ台に乗った回数」の更新
            GameObject.Find("TimeCounter").GetComponent<TimeCounter>().JumpNumber = jumpNumber;                                      
        }
    }

    // ---------落とし穴関係の関数---------------------------------
    void StartFall()
	{        
		fall = StartCoroutine("Fall");
	}

	IEnumerator Fall()
	{
		isFalling = true;
		gameObject.layer = fallenPlayerLayer;
		startFallPositionY = transform.position.y;

		while (true) {

			if (transform.position.y - startFallPositionY <= endFallDistance) {
				isFalling = false;
				gameObject.layer = defaultPlayerLayer;
                // UnityEngine.Debug.Log(gameObject.layer);

				yield break;
			}

			yield return null;
		}
	}


    //落下コルーチンをリセット(オブジェクトプールで使い回す＆死亡等で強制終了させる場合用)。
	void ResetFall()
	{
		if (fall != null) {
			StopCoroutine(fall);
			fall = null;
		}
	}
    // -----------------------------------------------------------
}