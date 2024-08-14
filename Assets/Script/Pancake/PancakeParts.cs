using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeParts : MonoBehaviour
{
    private GameMaster _gameMaster;
    [SerializeField] private Pancake _pancake;
    [SerializeField] private float _bakedDegree;
    private bool _baked;
    private bool _burnt;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_bakedDegree < 50)
        {//�Ă���O

        }
        else if (_bakedDegree < 100)
        {//�ł���O
            if (!_baked)
            {
                Debug.Log("�Ă���");
                _baked = true;
                _pancake.BakedCount();
            }
        }
        else {
            //�ł�����
            if (!_burnt) {
                Debug.Log("�ł���");
                _burnt = true;
                _pancake.BurntCount();
            }
        }
    }

    public void SetgameMaster(GameMaster gameMaster) { 
        _gameMaster = gameMaster;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FlyingPan")
        {
            _bakedDegree += _gameMaster.GetFire() * Time.deltaTime;//FixedUpdate�Ȃ̂�Time/Deltatime�͕s�v�����ǂ��ꂪ����ƃt���[�����Z���Ȃ��čςނ̂Œǉ�
        }
    }


}