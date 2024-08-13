using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingMaker : MonoBehaviour
{
    public GameObject Topping1Prefab;
    public GameObject Topping2Prefab;
    public Transform parentTransform;
    private Dictionary<ToppingType, GameObject> toppingPrefabs;
    void Start(){
        toppingPrefabs = new Dictionary<ToppingList, GameObject>()
        {
            { ToppingList.topping1, Topping1 },
            { ToppingList.topping2, Topping2 }
        };
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            ToppingMake(ToppingList.topping1);
        }
    }

    void ToppingMake(ToppingList toppingType){
        Instantiate(toppingPrefabs[toppingType], parentTransform);
    }
}
