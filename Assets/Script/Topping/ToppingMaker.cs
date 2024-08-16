using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingMaker : MonoBehaviour
{
    public GameObject blueberryPrefab;
    public GameObject strawberryPrefab;

    public void ToppingMake(){
        GameObject toppingPrefab = null;
        var topping = SelectTopping();

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

    //�g�b�s���O���w��͈͓��Œ��I
    private ToppingList SelectTopping() 
    {
        var SelectedTopping = (ToppingList)Random.Range(1, 2);
        return SelectedTopping;
    }
}
