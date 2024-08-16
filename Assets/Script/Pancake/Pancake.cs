using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Pancake : MonoBehaviour
{
    private GameMaster _gameMaster;

    [SerializeField] private List<PancakeParts> _pancakeParts;
    //[SerializeField] protected bool _debugMode;
    private int _bakedNum;
    private int _burntNum;

    private bool InjectComplete = false;

    /*private void Start()
    {
        foreach (var part in _pancakeParts)
        {
            part.SetgameMaster(_gameMaster);
        }

        InjectComplete = true;
    }*/

    [Inject]
    private void InjectGameMaster(GameMaster master)
    {
        _gameMaster = master;
        foreach (var part in _pancakeParts)
        {
            part.SetgameMaster(_gameMaster);
        }

        InjectComplete = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!InjectComplete) return;

        if (_burntNum/_pancakeParts.Count >= 0.5) {
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
    }

    public void BakedCount() 
    { 
        _bakedNum += 1;
    }

    public void BurntCount()
    {
        _burntNum += 1;
    }
}
