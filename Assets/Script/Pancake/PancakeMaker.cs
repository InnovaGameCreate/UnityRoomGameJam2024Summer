using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeMaker : MonoBehaviour
{
    public GameObject pancakePrefab; // �p���P�[�L�I�u�W�F�N�g�̃v���n�u
    //private PancakeMaker _pancakeMaker;
    [SerializeField]private GameMaster _gameMaster; // �Q�[���}�X�^�[�̎Q��

    void Start(){
        //PancakeMake();
    }

    public void PancakeMake(){
        // �p���P�[�L�I�u�W�F�N�g�𐶐��A��ʂɕ\��
        GameObject pancake = Instantiate(pancakePrefab, transform.position, Quaternion.identity);

        pancake.GetComponent<Pancake>().SetGameMaster(_gameMaster);
    }
}
