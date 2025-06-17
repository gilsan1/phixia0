using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public partial class PhotonManager : MonoBehaviourPunCallbacks
{
    [PunRPC]
    void OnSkill(bool _Owner, int _SKill)
    { }
    /// <summary>
    /// ���� ������ ��ų������ ����ϸ� ���濡�� ȣ���� ���� -> �׷��� ���浵 ���� �� ����
    /// RpcTarget => ALL : ���� Send�ϸ� ���� ����/ Others : �� ���� �ٸ� ����� / Master : ���常 �޴°�
    /// </summary>
    public void SendSKill()
    {
        PV.RPC("OnSKill", RpcTarget.All, true, 1); // �Լ� �̸� ��, Ÿ��, �Լ��� �Ű�����
    }

}
