using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LifeViewer : MonoBehaviour
{
    [SerializeField] private List<Image> lifeImages;

    private void Start()
    {
        AllActive();
    }

    public void SetLife(float hp)
    {
        int HP = (int)hp;
        //lifeImages[HP-1].enabled = false; //�`��Ńo�O��̂ňꎞ�R�����g�A�E�g
    }

    void AllActive()
    {
        foreach (var image in lifeImages)
        {
            image.enabled = true;
        }
    }
}
