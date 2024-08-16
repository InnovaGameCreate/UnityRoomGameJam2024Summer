using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingMaker : MonoBehaviour
{
    public GameObject blueberryPrefab;
    public GameObject strawberryPrefab;

    public void ToppingMake(ToppingList topping){
        GameObject toppingPrefab = null;

        switch (topping){
            case ToppingList.blueberry:
                toppingPrefab = blueberryPrefab;
                break;
            case ToppingList.strawberry:
                toppingPrefab = strawberryPrefab;
                break;
        }

        //�v���n�u���ݒ肳��Ă���ꍇ�A�Q�[����ɐ���
        if (toppingPrefab != null){
            Instantiate(toppingPrefab, transform.position, Quaternion.identity);
        }
    }
}
