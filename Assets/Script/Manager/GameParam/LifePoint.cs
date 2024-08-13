using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System;

public class LifePoint : MonoBehaviour
{
    [SerializeField]
    ReactiveProperty<int> life = new IntReactiveProperty();
    public IReactiveProperty<int> lifeProperty => life;

    public void SubtractLife(int minus = 1)
    {
        if (minus <= 0)
            throw new ArgumentOutOfRangeException("�����͐��̐����łȂ��Ă͂����܂���.");
        life.Value -= minus;
        if (life.Value < 0)
            life.Value = 0;

        Debug.Log("Damage:" + minus);
        Debug.Log("���ݑ̗�:" + life.Value);
    }

    public void AddLife(int plus = 1)
    {
        if (plus >= 0)
            throw new ArgumentOutOfRangeException("�����͐��̐����łȂ��Ă͂����܂���.");
        life.Value += plus;
    }
}
