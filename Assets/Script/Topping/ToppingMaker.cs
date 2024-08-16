using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ToppingMaker : MonoBehaviour
{
    public GameObject blueberryPrefab;
    public GameObject strawberryPrefab;

    ReactiveProperty<ToppingList> nextTopping = new ReactiveProperty<ToppingList>();
    public IReactiveProperty<ToppingList> OnChangeNextTopping => nextTopping;


    void Start()
    {
        nextTopping.Value = SelectTopping();    //�J�n���Ɏ��̃g�b�s���O�𒊑I    
    }

    public void ToppingMake(){
        GameObject toppingPrefab = null;

        switch (nextTopping.Value){
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
            nextTopping.Value = SelectTopping();    //���̃g�b�s���O�𒊑I
        }
    }

    //�g�b�s���O���w��͈͓��Œ��I
    private ToppingList SelectTopping() 
    {
        var SelectedTopping = (ToppingList)Random.Range(1, 2);
        return SelectedTopping;
    }
}
