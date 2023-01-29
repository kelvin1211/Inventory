using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room", menuName = "New Room/Room")]
public class MyRoom : ScriptableObject
{
    public string RoomName;// 아이템 이름
    public Sprite RoomImg; // 아이템 이미지
    public RoomType myRoomType;
    public int myRoomCount;
    public GameObject RoomObject;
    public enum RoomType
    {
        Room00, Room01, Room02,Room03,Room04,Room05,Room06,
    }
}
