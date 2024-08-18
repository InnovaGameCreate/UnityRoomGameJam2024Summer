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

        /*
        //hp�̐��������̃X���C�_�[��1�ɂ���B
        for (int i = 1; i < onePlaceHP; i++)
        {
            Debug.Log(i - 1);
            lifeSliders[i - 1].padding = new Vector4(0, 0, 0, 0);
        }
        */
        //���S�ɍ��Ă��鐯�Ɗ��S�Ɏc���Ă��鐯�̌����ڂ�ύX����
        for (int i = 0; i < lifeSliders.Count; i++) {

            if (i <= onePlaceHP) {
                lifeSliders[i].padding = new Vector4(0, 0, 0, 0);
            } else if (i > onePlaceHP) {
                lifeSliders[i].padding = new Vector4(0, 0, 0, 100);
            }
        }

        float decimalHP = currentHP - onePlaceHP;
        //HP��3�̎��ɏ��������̌v�Z���s��Ȃ��̂�3�ȏ�̎���return����B
        if (hp >= 3)
        {
            return;
        }
        if (onePlaceHP >= 0) {
            //���������̃X���C�_�[�𒲐�����
            lifeSliders[onePlaceHP].padding = new Vector4(0, 0, 0, 36 - (36 * decimalHP));
        }       
    }
}