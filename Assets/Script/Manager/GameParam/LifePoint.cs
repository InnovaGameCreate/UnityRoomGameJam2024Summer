using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;

public class LifePoint : MonoBehaviour
{
    [SerializeField]
    ReactiveProperty<float> life = new FloatReactiveProperty();
    public IReactiveProperty<float> lifeProperty => life;

    [SerializeField]
    float DecreaseValuePerSec = 0.005f;
    float DecreaseRate = 1;

    void  Start()
    {
        StartLifeDecrease(this.GetCancellationTokenOnDestroy()).Forget();
    }

    //�w�莞�Ԃ��Ƃɑ̗͌���
    public async UniTask StartLifeDecrease(CancellationToken cancellationToken)
    {
        while (true)
        {
            var ms_DecreaseTime = TranslateSecondToMs(DecreaseRate);
            await UniTask.Delay(ms_DecreaseTime, cancellationToken: cancellationToken);
            SubtractLife(DecreaseValuePerSec);
            Debug.Log(life.Value);
        }
    }

    int TranslateSecondToMs(float second)   //�b���~���b�ɕϊ�
    {
        var milliSecond = second * 1000;
        return (int)milliSecond;
    }

    public void SubtractLife(float minus = 1)
    {
        if (minus <= 0)
            throw new ArgumentOutOfRangeException("�����͐��̐����łȂ��Ă͂����܂���.");
        life.Value -= minus;
        if (life.Value < 0)
            life.Value = 0;
    }

    public void AddLife(int plus = 1)
    {
        if (plus <= 0)
            throw new ArgumentOutOfRangeException("�����͐��̐����łȂ��Ă͂����܂���.");
        life.Value += plus;
    }
}
