using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPan : MonoBehaviour
{
    [SerializeField][Tooltip("�t���C�p����U��Ƃ��̍ő�̗�")] private float _maxPower;
    [SerializeField][Tooltip("���E�ɓ����ō����x�ł�")] private float _chargePowerSpeed;
    /// <summary>
    /// ���݂ǂꂾ���͂����ߍ���ł��邩�̒l
    /// </summary>
    private float _chargePower;
    [SerializeField][Tooltip("���E�ɓ����ō����x�ł�")] private Rigidbody2D _myRB2D;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && _maxPower > _chargePower)
        {
            _chargePower += _chargePowerSpeed * Time.deltaTime;
            if (_maxPower < _chargePower) { _chargePower = _maxPower; }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _myRB2D.AddForce(new Vector2(0, _chargePower), ForceMode2D.Impulse);
            _chargePower = 0;
        }
       
    }

    /// <summary>
    /// �ǂꂾ���͂����߂Ă��邩��Ԃ�
    /// </summary>
    /// <returns>1��100%�Ƃ��ă`���[�W���Ă��闦��Ԃ�</returns>
    public float GetPowerCharagePercent() {
        float powerPercent = _chargePower / _maxPower;
        if (powerPercent > 1) { powerPercent = 1; }
        return powerPercent;
    }
}
