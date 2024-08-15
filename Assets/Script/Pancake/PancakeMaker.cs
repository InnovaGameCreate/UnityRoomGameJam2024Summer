using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeMaker : MonoBehaviour
{
    public GameObject pancakePrefab; // �p���P�[�L�I�u�W�F�N�g�̃v���n�u
    private PancakeMaker _pancakeMaker;
    private GameMaster _gameMaster; // �Q�[���}�X�^�[�̎Q��

    void Start(){
        PancakeMake();
    }

    public void PancakeMake(){
        // �p���P�[�L�I�u�W�F�N�g�𐶐��A��ʂɕ\��
        GameObject pancake = Instantiate(pancakePrefab, transform.position, Quaternion.identity);
        
        // ���������p���P�[�L�I�u�W�F�N�g��GameMaster���Z�b�g
        if (_gameMaster != null){
            pancake.SendMessage("SetGameMaster", _gameMaster, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void SetGameMaster(GameMaster master){
        _gameMaster = master;
    }
}
