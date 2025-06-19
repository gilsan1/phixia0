using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public partial class PhotonManager : MonoBehaviourPunCallbacks
{
    public PhotonView PV; // ������ ���� ������ �����Ѵ�

    private void Awake()
    {
        PhotonNetwork.GameVersion = "1.0.0";
        PhotonNetwork.SendRate = 20; // ���� ������ �����ִ� ��Ŷ�� �ӵ�
        PhotonNetwork.SerializationRate = 10; // ��ȣȭ �ð�?

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnDisconnected(DisconnectCause cause) // ������ ��������
    {
        base.OnDisconnected(cause);
    }

    public override void OnConnectedToMaster() // �����̶� ����
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby(); // OnJoinedLobby()�� ����
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
    }

    public void OnLobby() // �κ� ������ ȣ��
    {
        PhotonNetwork.IsMessageQueueRunning = true;
    }

    public void LeaveLobby() // �κ� ������ �� ȣ��
    {
        PhotonNetwork.LeaveLobby();
    }

}
