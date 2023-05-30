using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

#if UNITY_WEBGL
    using System.Runtime.InteropServices;
#endif

public class DataManager : MonoBehaviour
{
    #if UNITY_EDITOR
        private StreamWriter sw;
    #elif UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void FileDownLoad(string str, string fileName);
    #endif

    private string dirPath;                                 // 作成するcsvのディレクトリ
    private string fileName;                                // 作成するcsvのファイル名
    private string[] header_result;                         // 作成するcsvファイルのリザルトのヘッダー
    private string[] header_score;                         // 作成するcsvファイルのスコア変移のヘッダー
    private List<string> dataStrs;                          // 作成するcsvファイルの中身（スコア変移）
    private List<string> results;                           // 作成するcsvファイルの中身（リザルト）


    void Start()
    {
        dirPath = Application.dataPath + "/Scripts/CSV/";   // Assets/Scripts/CSVを指定
        fileName = DateTime.Now.ToString("yyyyMMddHHmmss"); // シーンの開始時間をファイル名に指定

        header_result = new string[]{"W&L", "Final_Score", "Get_PickUp_Number", "Falls_Number", "Jumping_Number"};   // リザルトのヘッダーを作成
        header_score = new string[]{"Score", "Time"};    // スコア変移のヘッダーを作成
        dataStrs = new List<String>();                      // csvファイルの中身を初期化
        results = new List<String>();                      // csvファイルの中身を初期化
        if(!Directory.Exists(dirPath)){                     // Assets/Scripts/CSVがなければディレクトリの作成
            Directory.CreateDirectory(dirPath);
        }
        #if UNITY_EDITOR
            sw = new StreamWriter(dirPath + fileName + ".csv", false, Encoding.GetEncoding("UTF-8"));
        #endif
    }
    void Update()
    {
        
    }

    public void OnClickOutputButton()
    {
        #if UNITY_EDITOR
            sw.WriteLine(string.Join(", ", header_result));     // ヘッダーの書き込み
            foreach(string result in results)                   // csvファイルの中身を書き込み
            {
                sw.WriteLine(result);
            }

            sw.WriteLine("");                                   //1行あける

            sw.WriteLine(string.Join(", ", header_score));      // ヘッダーの書き込み
            foreach(string dataStr in dataStrs)                 // csvファイルの中身を書き込み
            {
                sw.WriteLine(dataStr);
            }
            sw.Close();
        #elif UNITY_WEBGL
            FileDownLoad(string.Join(",", header_result) + "\n" + string.Join("\n", results) + "\n" + "\n" + string.Join(",", header_score) + "\n" + string.Join("\n", dataStrs), fileName);
        #endif
        Debug.Log(dirPath + fileName + ".csvを作成しました"); // csvを作成したことをコンソールに表示      
    }

    /*  
    //  UpdateData(string dataStr)
    //  dataStrをdataStrsに追加する関数
    //  PlayerControllerで呼び出すことを想定
    */  
    public void UpdateData(string dataStr)
    {
        dataStrs.Add(dataStr);
    }

    public void UpdateResult(string result)
    {
        results.Add(result);
    }
}