using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public partial class PhotonManager : MonoBehaviourPunCallbacks
{
    public PhotonView PV; // 포톤뷰로 포톤 서버와 소통한다

    private void Awake()
    {
        PhotonNetwork.GameVersion = "1.0.0";
        PhotonNetwork.SendRate = 20; // 포톤 서버에 던져주는 패킷의 속도
        PhotonNetwork.SerializationRate = 10; // 암호화 시간?

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnDisconnected(DisconnectCause cause) // 서버가 떨어질때
    {
        base.OnDisconnected(cause);
    }

    public override void OnConnectedToMaster() // 방장이란 개념
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby(); // OnJoinedLobby()가 응답
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
    }

    public void OnLobby() // 로비 들어오면 호출
    {
        PhotonNetwork.IsMessageQueueRunning = true;
    }

    public void LeaveLobby() // 로비 나갔을 때 호출
    {
        PhotonNetwork.LeaveLobby();
    }

}
