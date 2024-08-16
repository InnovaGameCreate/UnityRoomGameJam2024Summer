using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LifeViewer : MonoBehaviour
{
    [SerializeField] private List<RectMask2D> lifeSliders;

    private float currentHP;

    public void SetLife(float hp)
    {
        //List��0����HP��0
        currentHP = hp;
        //currentHP�̏���������؂�̂Ă�B
        int onePlaceHP = Mathf.FloorToInt(currentHP);

        //hp�̐��������̃X���C�_�[��1�ɂ���B
        for (int i = 1; i < onePlaceHP; i++)
        {
            Debug.Log(i - 1);
            lifeSliders[i - 1].padding = new Vector4(0, 0, 0, 0);
        }

        float decimalHP = currentHP - onePlaceHP;
        //HP��3�̎��ɏ��������̌v�Z���s��Ȃ��̂�3�ȏ�̎���return����B
        if (hp >= 3)
        {
            return;
        }
        //���������̃X���C�_�[�𒲐�����
        lifeSliders[onePlaceHP].padding = new Vector4( 0, 0, 0,100-(100 * decimalHP));
    }
}