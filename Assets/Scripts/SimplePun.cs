using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class SimplePun : MonoBehaviourPunCallbacks {

    [SerializeField] public TimeCounter timeCounter;

    // Use this for initialization
    void Start () {
        //旧バージョンでは引数必須でしたが、PUN2では不要です。
        PhotonNetwork.ConnectUsingSettings();
    }

    void OnGUI()
    {
        //ログインの状態を画面上に出力
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }


    //ルームに入室前に呼び出される
    public override void OnConnectedToMaster() {
        // PhotonNetwork.CreateRoom("Room", new RoomOptions(), TypedLobby.Default);
        // ルームの参加人数を1人に設定する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 1;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    // ランダムで参加できるルームが存在しないなら、新規でルームを作成する
    public override void OnJoinRandomFailed(short returnCode, string message) {
        // ルームの参加人数を1人に設定する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 1;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    //ルームに入室後に呼び出される
    public override void OnJoinedRoom(){

        // ルームが満員になったら、以降そのルームへの参加を不許可にする
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers) {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        
            //キャラクターを生成
            GameObject player = PhotonNetwork.Instantiate("player", new Vector3(9, 2, -9), Quaternion.identity, 0);
            player.name = player.name.Replace("(Clone)", "");

            GameObject.Find("Main Camera").GetComponent<CameraController>().target = player;
            GameObject.Find("PlayerName").GetComponent<UIFollowTarget>().target = player;
            //自分だけが操作できるようにスクリプトを有効にする
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.enabled = true;

            timeCounter.isMove = true;

            // UnityEngine.Debug.Log("OnJoinedRoom:" + player.name);
    }
}
