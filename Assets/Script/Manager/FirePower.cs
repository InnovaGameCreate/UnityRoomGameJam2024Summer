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
    float firePowerUpInterval = 20;
    [SerializeField]
    float fireIncreaseValue = 10;

    //���Ԋu�ŉἨA�b�v
    public async UniTask StartPowerUp(CancellationToken cancellationToken)
    {
        while(true)
        {
            await UniTask.Delay((int)firePowerUpInterval, cancellationToken: cancellationToken);
            IncreaseFirePower(fireIncreaseValue);
        }
    }

    void IncreaseFirePower(float increaseValue)
    {
        if (increaseValue <= 0)
            throw new ArgumentOutOfRangeException("�����͐��̐����łȂ��Ă͂����܂���.");
        fire.Value += increaseValue;
    }

    void DecreaseFirePower(float decreaseValue)
    {
        if (decreaseValue <= 0)
            throw new ArgumentOutOfRangeException("�����͐��̐����łȂ��Ă͂����܂���.");
        fire.Value -= decreaseValue;
    }
}
