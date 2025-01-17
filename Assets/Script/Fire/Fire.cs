using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Diagnostics;

public class Fire : MonoBehaviour
{
    public float rotationAmount = 5f; // 揺れの角度
    public float speed = 5f; // 揺れの速さ
    public float sizeChangeSpeed = 2f;
    public float firePower = 1f;
    [SerializeField]
    private GameMaster _gameMaster;
    [SerializeField]
    float FirePowerLimit = 8;
    private float _randomOffset;
    private Vector3 _initialScale;
    private Vector3 _targetScale;

    void Start(){
        _gameMaster = FindObjectOfType<GameMaster>();
        _randomOffset = Random.Range(0f, 2f * Mathf.PI);
        _initialScale = transform.localScale;
        _targetScale = _initialScale;
    }

    void Update(){
        float angle = rotationAmount * Mathf.Sin(Time.time * speed + _randomOffset);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (_gameMaster != null)
        { 
            firePower = _gameMaster.GetFire();
        }

        // デバッグ用
        /*if (Input.GetKey(KeyCode.A)){
            firePower = 0.8f;
        }
        if (Input.GetKey(KeyCode.S)){
            firePower = 1.5f;
        }*/

        // サイズが変更された場合にのみChangeFireを呼び出す
        if (transform.localScale.x != firePower * _initialScale.x) {
            ChangeFire(firePower);
        }

    }

    public void ChangeFire(float firePower){
        
        _targetScale = _initialScale * firePower / FirePowerLimit;
        transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, Time.deltaTime * sizeChangeSpeed);
    }
}
