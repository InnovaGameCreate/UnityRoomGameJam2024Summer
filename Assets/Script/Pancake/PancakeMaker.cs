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
        pancake.GetComponent<Pancake>().SetPancakeMaker(this); // �p���P�[�L��PancakeMaker���Z�b�g
    }

    public void SetGameMaster(GameMaster master){
        _gameMaster = master;
    }

    // �Ă��ڊm�F
    public void Baked(PancakePart part){
        if(part.IsBaked()){
            // �S�p�[�c�ɏĂ��ڂ��t���Ă��邩�m�F�����̏�����
            // �K�v�ɉ����đS�̂̏Ă���Ԃ��Ǘ�����R�[�h��ǉ�
            Debug.Log("baked");
        }
    }

    // �ł��m�F
    public void Burnt(PancakePart part){
        if(part.IsBurnt()){
            Debug.Log("burnt");
            _gameMaster.ReduceLife(); // �Q�[���}�X�^�[�Ƀ��C�t�����炷�w�����o��
        }
    }
}
