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
    /// 내가 구현한 스킬로직을 사용하면 포톤에도 호출을 해줌 -> 그래야 상대방도 받을 수 있음
    /// RpcTarget => ALL : 내가 Send하면 나도 받음/ Others : 나 빼고 다른 사람들 / Master : 방장만 받는것
    /// </summary>
    public void SendSKill()
    {
        PV.RPC("OnSKill", RpcTarget.All, true, 1); // 함수 이름 값, 타겟, 함수의 매개변수
    }

}
