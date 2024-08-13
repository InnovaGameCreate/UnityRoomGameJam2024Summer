using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField][Tooltip("���E�ɓ����ō����x�ł�")] private float _maxSpeed;
    [SerializeField][Tooltip("�������߂ɉ����o�����͂ł�")] private float _pushPower;
    [SerializeField][Tooltip("x���W�����ɋ������ő�̈ړ��ʒu�ł�")] private float _maxX;
    [SerializeField][Tooltip("x���W�����ɋ������ŏ��̈ړ��ʒu�ł�")] private float _minX;
    [SerializeField][Tooltip("���M��Rigidbody2D�ł�")] private Rigidbody2D _myRB2;

    [SerializeField][Tooltip("�ړ��{�^����b������������������Ĉړ��������邩�ɂ��Ăł�")] private bool inertia;


    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.D) && _myRB2.velocity.x < _maxSpeed)
        {
            _myRB2.AddForce(new Vector2(_pushPower, 0));
        }
        if (Input.GetKey(KeyCode.A) && _myRB2.velocity.x > -_maxSpeed)
        {
            _myRB2.AddForce(new Vector2(-_pushPower, 0));
        }

        if (this.transform.position.x > _maxX && _myRB2.velocity.x > 0)
        { //�E�s���߂��\�h
            _myRB2.velocity = new Vector2(0, 0);
        }
        if (this.transform.position.x < _minX && _myRB2.velocity.x < 0)
        { //�E�s���߂��\�h
            _myRB2.velocity = new Vector2(0, 0);
        }

        if (!inertia && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            _myRB2.velocity = Vector2.zero;
        }

    }

}
