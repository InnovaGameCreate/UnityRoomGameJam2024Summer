using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Zenject;

public class Pancake : MonoBehaviour
{
    private GameMaster _gameMaster;

    [SerializeField] private List<PancakeParts> _pancakeParts;
    //[SerializeField] protected bool _debugMode;
    private int _bakedNum;
    private int _burntNum;

    private bool InjectComplete = false;

    private bool _droped;

    /*private void Start()
    {
        foreach (var part in _pancakeParts)
        {
            part.SetgameMaster(_gameMaster);
        }

        InjectComplete = true;
    }*/



    // Update is called once per frame
    void Update()
    {
        if (!InjectComplete) return;

        if ((float)_burntNum/(float)_pancakeParts.Count >= 0.5) {
            Debug.Log("�ł��Ă��܂���");
            _gameMaster.PancakeComplete(PancakeFlag.BURNT);
            Destroy(this.gameObject);
        }
        if (_bakedNum == _pancakeParts.Count) {

            if (_burntNum == 0)
            {
                Debug.Log("�����ɏĂ���������");
                _gameMaster.PancakeComplete(PancakeFlag.PERFECT);
                Destroy(this.gameObject);
            }
            else {
                Debug.Log("�Ă���������");
                _gameMaster.PancakeComplete(PancakeFlag.BAKED);
                Destroy(this.gameObject);
            }

        }
    }

    public void SetGameMaster(GameMaster gameMaster)
    {
        _gameMaster = gameMaster;
        foreach (var part in _pancakeParts)
        {
            part.SetgameMaster(_gameMaster);
        }

        InjectComplete = true;
    }

    public void BakedCount() 
    { 
        _bakedNum += 1;
    }

    public void BurntCount()
    {
        _burntNum += 1;
        Debug.Log("�ł�����" + _burntNum);
        Debug.Log("�ł�������" + _burntNum / _pancakeParts.Count);
    }

    /// <summary>
    /// ���������ƔF��
    /// </summary>
    public void Drop() {
        
        if (!_droped) {
            _gameMaster.PancakeComplete(PancakeFlag.DROPED);
            Destroy(this.gameObject);
            _droped = true;
        }
    }
}
