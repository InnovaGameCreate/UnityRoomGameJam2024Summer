using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;

public class FirePower : MonoBehaviour
{
    ReactiveProperty<float> fire = new FloatReactiveProperty();
    public IReadOnlyReactiveProperty<float> fireProperty => fire;

    [SerializeField]
    float s_firePowerUpInterval = 20;
    [SerializeField]
    float fireIncreaseValue = 10;

    //���Ԋu�ŉἨA�b�v
    public async UniTask StartPowerUp(CancellationToken cancellationToken)
    {
        
        Debug.Log("�ἨA�b�v�J�n");
        while(true)
        {
            var ms_Interval = TranslateSecondToMs(s_firePowerUpInterval);
            await UniTask.Delay(ms_Interval, cancellationToken: cancellationToken);
            IncreaseFirePower(fireIncreaseValue);
        }
    }

    int TranslateSecondToMs(float second)   //�b���~���b�ɕϊ�
    {
        var milliSecond = second * 1000;
        return (int)milliSecond;
    }

    void IncreaseFirePower(float increaseValue)
    {
        Debug.Log("�ἨA�b�v");
        if (increaseValue <= 0)
            throw new ArgumentOutOfRangeException("�����͐��̐����łȂ��Ă͂����܂���.");
        fire.Value += increaseValue;
        Debug.Log("���݂̉Η�:" + fire.Value);
    }

    void DecreaseFirePower(float decreaseValue)
    {
        if (decreaseValue <= 0)
            throw new ArgumentOutOfRangeException("�����͐��̐����łȂ��Ă͂����܂���.");
        fire.Value -= decreaseValue;
    }
}
