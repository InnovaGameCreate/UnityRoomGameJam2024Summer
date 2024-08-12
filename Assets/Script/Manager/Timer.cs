using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField]
    ReactiveProperty<float> time = new FloatReactiveProperty();
    public IReadOnlyReactiveProperty<float> timeProperty => time;

    bool isActive = false;

    void Awake()
    {
        timeProperty    //�J�E���g�_�E����0�ȉ��ɂȂ�����Stop
            .Where(x => x <= 0)
            .Subscribe(_ =>
            {
                OnEnd();
            }).AddTo(this);
    }
    void Update()
    {
        if (!isActive) return;
        time.Value -= Time.deltaTime;
    }

    public void OnStart()
    {
        if (time.Value <= 0)
            throw new ArgumentOutOfRangeException("�������Ԃ�0���傫���Ȃ���΂����܂���.");

        isActive = true;
    }

    public void OnStop()
    {
        isActive = false;
    }

    public void OnEnd()
    {
        Debug.Log("TimeOut");
        time.Value = 0;
        OnStop();
    }
}
