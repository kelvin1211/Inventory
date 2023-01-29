using Kelvin;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;

public class test : MonoBehaviour
{

    Inventory myInventory = new Inventory();

    private void Awake()
    {
        GameManager.instance.FadeIn();
        GameManager.instance.LoadComponents();
        GameManager.instance.GetComponent<GameManager>().enabled= true;
    }


    [SerializeField] private MyItem[] myItem;

    //Inventory myinvet = new Inventory();
    public Slot[] slots;

    public void LoadToInven(int _arrayNum, string _itemName, int _itemNum)
    {
        for (int i = 0; i < myItem.Length; i++)
            if (myItem[i].itemName == _itemName)
                slots[_arrayNum].AddItem(_itemName, myItem[i]);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GameManager.instance.Fade();
        //StartCoroutine(GameManager.instance.ChangeRoomLeft());
    }

}
