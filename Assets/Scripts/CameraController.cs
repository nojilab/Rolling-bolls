// using System.Numerics;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;                                              // カメラを向けるターゲット
    private Vector3 offset;                                         // オフセット

    // Start is called before the first frame update
    void Start()
    {
        if(target == null){
            target = GameObject.Find("Stage");                               //ターゲットが存在しな時はステージにカメラを向ける
        }

        // UnityEngine.Debug.Log("camera_Start:" + target.name);
        offset = new Vector3(0, 8, 0);     // オフセットの初期化
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(target == null){
            target = GameObject.Find("Stage");                               //ターゲットが存在しな時はステージにカメラを向ける
        }

        transform.position = target.transform.position + offset;    // オフセットを保持したカメラ移動
        // UnityEngine.Debug.Log("camera_Update:" + target.name);
    }
}
