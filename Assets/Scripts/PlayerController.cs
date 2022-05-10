using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;                    // プレイヤーの速さ
    [SerializeField] public TextMeshProUGUI scoreText;      // スコアのテキスト
    [SerializeField] public GameObject ClearPanel;          // クリア時に表示するUI
    [SerializeField] public DataManager dataManager;        // データ管理のクラス

    private Rigidbody rb;                                   // rigidbody
    private Vector3 movement;                               // 進行方向
    private int score;                                      // スコア

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();                     // rigidbodyの取得
        movement = new Vector3(0.0f, 0.0f, 0.0f);           // 進行方向の初期化
        score = 0;                                          // scoreの初期化
        scoreText.text = "Score:" + score.ToString();       // 表示するスコアの更新
        ClearPanel.SetActive(false);                        // クリア時に表示するUIを非表示にする


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(movement*speed);                                        // プレイヤーに力を加える(物理演算)
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
            scoreText.text = "Score:" + score.ToString();                   // 表示するスコアの更新
            if(score >= 1000)
            {
                ClearPanel.SetActive(true);                                 // クリア時に表示するUIを表示する
            }
            dataManager.UpdateData(score.ToString()+","+DateTime.Now.ToString("yyyyMMddHHmmss")); // csvファイルに書き込むデータを追加
        }
    }
}