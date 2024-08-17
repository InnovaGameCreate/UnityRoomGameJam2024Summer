using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ToppingMaker : MonoBehaviour
{
    public GameObject blueberryPrefab;
    public GameObject strawberryPrefab;
    public GameObject bananaPrefab;
    public GameObject chocolatePrefab;
    public GameObject nutsPrefab;
    public GameObject butterPrefab;
    ReactiveProperty<ToppingList> nextTopping = new ReactiveProperty<ToppingList>();
    public IReactiveProperty<ToppingList> OnChangeNextTopping => nextTopping;

    [SerializeField][Tooltip("�t���C�p������̑��Έʒu")] private Vector3 _dropPosition;
    [SerializeField] private GameObject _FlyingPan;

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
            case ToppingList.banana:
                toppingPrefab = bananaPrefab;
                break;
            case ToppingList.chocolate:
                toppingPrefab = chocolatePrefab;
                break;
            case ToppingList.nuts:
                toppingPrefab = nutsPrefab;
                break;
            case ToppingList.butter:
                toppingPrefab = butterPrefab;
                break;
        }

        //�v���n�u���ݒ肳��Ă���ꍇ�A�Q�[����ɐ���
        if (toppingPrefab != null){
            Instantiate(toppingPrefab, _FlyingPan.transform.position + _dropPosition, Quaternion.identity);
            nextTopping.Value = SelectTopping();    //���̃g�b�s���O�𒊑I
        }
    }

    //�g�b�s���O���w��͈͓��Œ��I
    private ToppingList SelectTopping() 
    {
        var SelectedTopping = (ToppingList)Random.Range(1, 6);
        return SelectedTopping;
    }
}
