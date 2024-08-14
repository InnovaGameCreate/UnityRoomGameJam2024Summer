using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeMaker : MonoBehaviour
{
    public GameObject pancakePrefab; // �p���P�[�L�I�u�W�F�N�g�̃v���n�u
    private GameMaster _gameMaster; // �Q�[���}�X�^�[�̎Q��

    void Start(){
        PancakeMake();
    }

    public void PancakeMake(){
        // �p���P�[�L�I�u�W�F�N�g�𐶐��A��ʂɕ\��
        GameObject pancake = Instantiate(pancakePrefab, transform.position, Quaternion.identity);
        
        // �p���P�[�L��PancakeMaker���Z�b�g
        Pancake pancakeScript = pancake.GetComponent<PancakeMake>();
        if (pancakeScript != null){
            pancakeScript.SetPancakeMaker(this);

            // ���������p���P�[�L�I�u�W�F�N�g��GameMaster���Z�b�g
            pancakeScript.SetGameMaster(_gameMaster);
        }
    }

    public void SetGameMaster(GameMaster master){
        _gameMaster = master;
    }
}
