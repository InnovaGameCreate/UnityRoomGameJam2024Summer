using UnityEngine;
using UniRx;
using System;

public class SuccessCount : MonoBehaviour
{
    ReactiveProperty<int> success = new IntReactiveProperty(0);
    public IReadOnlyReactiveProperty<int> successProperty => success;

    public void AddSuccessCount(int count = 1)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException("成功した回数は正の整数でなければいけません.");

        success.Value += count;
    }
}
