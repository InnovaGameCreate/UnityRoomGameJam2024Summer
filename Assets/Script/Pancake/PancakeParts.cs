using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeParts : MonoBehaviour
{
    [SerializeField]
    private GameMaster _gameMaster;
    [SerializeField] private Pancake _pancake;
    [SerializeField] private float _bakedDegree;
    private bool _baked;
    private bool _burnt;

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
        Debug.Log(gameMaster);
        _gameMaster = gameMaster;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("�U��Ă���");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Flyingpan"))
        {
            Debug.Log(Time.deltaTime);
            _bakedDegree += _gameMaster.GetFire() * Time.deltaTime;//FixedUpdate�Ȃ̂�Time/Deltatime�͕s�v�����ǂ��ꂪ����ƃt���[�����Z���Ȃ��čςނ̂Œǉ�
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("�U��Ă���Triger");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Flyingpan"))
        {
            //Debug.Log(Time.deltaTime);
            _bakedDegree += _gameMaster.GetFire() * Time.deltaTime;//FixedUpdate�Ȃ̂�Time/Deltatime�͕s�v�����ǂ��ꂪ����ƃt���[�����Z���Ȃ��čςނ̂Œǉ�
        }

    }
}
