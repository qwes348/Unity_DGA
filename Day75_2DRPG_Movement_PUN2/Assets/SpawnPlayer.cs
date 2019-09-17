﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        PhotonNetwork.Instantiate(playerPrefab.name,
                                   Vector3.zero,
                                   Quaternion.identity);    // 모든 클라이언트에서 오브젝트생성
    }
}
