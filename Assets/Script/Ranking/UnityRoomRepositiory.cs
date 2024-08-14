using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unityroom.Api;

public class UnityRoomRepositiory : MonoBehaviour, IRepositiory
{
    [SerializeField]
    GameObject ranking;
    [SerializeField]
    string HmacKey = "";

    public void SendTimeToDataStore(float time)
    {
        if(UnityroomApiClient.Instance == null) //Instance���������
            InitializeUnityRoomAPI();

        UnityroomApiClient.Instance.SendScore(1, time, ScoreboardWriteMode.Always); //�X�R�A���M
    }

    void InitializeUnityRoomAPI()   //�v�����ꂽ�Ƃ��ɏ��߂�UntiyroomAPI�𐶐�
    {
        var UnityRoomAPI = Instantiate(ranking);
        UnityRoomAPI.GetComponent<UnityroomApiClient>().SetHmacKey(HmacKey);
    }
}
