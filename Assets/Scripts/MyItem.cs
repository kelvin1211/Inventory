using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName ="New Item/item")]
public class MyItem : ScriptableObject
{
    public string itemName ;// 아이템 이름
    public ItemType itemtype;  //아이템유형
    public Sprite itemImg; // 아이템 이미지
    public GameObject itemPrefab; //아이템 프리팹
    public int itemCount; // 획득한 아이템 갯수

    public enum ItemType
    {
        Item, Area, 
    }
}
