using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kelvin
{
    public class PlayerData4Saving : MonoBehaviour
    {
        //현재 진행된 결과값을 저장

        //마지막 룸 정보
        GameObject currentRoom;

        //진행된 미션


        //슬롯의 아이템
        Image[] currentSlotItems;

        public void currentRoomData(GameObject _room)
        {
            currentRoom = _room;
        }

        public void currentSlotItemData(Image[] _item)
        {
            for (int i = 0; i < currentSlotItems.Length; i++)
            {
                currentSlotItems[i] = _item[i];
            }
        }
    }
}