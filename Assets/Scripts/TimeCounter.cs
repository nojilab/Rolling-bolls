using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    //カウントダウン
    public float countdown = 60.0f;

    //時間を表示するUI
    [SerializeField] public GameObject TimePanel;

    // 時間を表示するUIのテキスト
    [SerializeField] public TextMeshProUGUI TimeText;

    //ゲーム時にスコアを表示するUI
    [SerializeField] public GameObject ScorePanel;

    // クリア時に表示するUI
    [SerializeField] public GameObject ClearPanel;

    // クリア時に表示するUI
    [SerializeField] public TextMeshProUGUI ClearText;

    //クリア時にスコアを表示するUI
    [SerializeField] public GameObject ScorePanelFin;

    // クリア時にスコアを表示するUIのテキスト
    [SerializeField] public TextMeshProUGUI ScoreTextFin;

    [SerializeField] public DataManager dataManager;

    //勝敗を表す文字
    private string WL;
    private int number;
    public int Score;                                      // スコア
    public int GetPickupNumber;                            //取得したPickUpの個数
    public int FallNumber;                                 //ステージから落ちた回数
    public int JumpNumber;                                 //ジャンプ台に乗った回数
    public bool isMove;


    void Awake()
    {
        isMove = false;
        ClearPanel.SetActive(false);                                    // クリア時に表示するUIを非表示にする
        ScorePanelFin.SetActive(false);    
        number = 0;
        Score = 0;
        GetPickupNumber = 0;
        FallNumber = 0;
        JumpNumber = 0;
        WL = "Congratulation";
        ClearText.text = "Congratulation !!";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isMove)
        {
            //時間をカウントダウンする
            countdown -= Time.deltaTime;

            //時間を表示する
            TimeText.text = countdown.ToString("f1") + "秒";

            //countdownが0以下になったとき
            if (countdown <= 0)
            {
                ClearPanel.SetActive(true);                                  // クリア時に表示するUIを表示する
                ScorePanelFin.SetActive(true);                               // クリア時にスコアを表示するUIを表示する
                TimePanel.SetActive(false);                                  //制限時間を表示していたパネルを非表示にする
                ScorePanel.SetActive(false);                                 //ゲーム時にスコアを表示していたパネルを非表示にする

                if(Score <= 500)
                {
                    WL = "Game Over";
                    ClearText.text = "Game Over...";
                }
                dataManager.UpdateResult(WL + "," + Score.ToString() + "," + GetPickupNumber.ToString() + "," + FallNumber.ToString() + "," + JumpNumber.ToString()); // csvファイルに書き込むデータを追加
                ScoreTextFin.text = "Score:" + Score.ToString();
                isMove = false;
            }
        }
    }
}