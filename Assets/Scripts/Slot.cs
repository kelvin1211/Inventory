using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jason;
using System.IO;

namespace Kelvin
{
    public class Slot : MonoBehaviour
    {
        //ItemPickUp myItemPickup = new ItemPickUp();
        Inventory myInventory;
        SaveNLoad mySaveNLoad;


        public MyItem item; // 획득한 아이템
        public int itemCount; // 획득한 아이템 갯수
        public Image itemSlotImage; // 획득한 아이템 슬롯적용 이미지



        //필요한 컴포넌트
        //[SerializeField]
        //private Text text_Count;
        //[SerializeField]
        //private GameObject go_CountImage;



        private void Awake()
        {
             myInventory = new Inventory();
             mySaveNLoad = new SaveNLoad();
        }

        //이미지 투명도 조절
        private void SetColor(float _alpha)
        {
            if(itemSlotImage.sprite!= null)
            {
                Color color = itemSlotImage.color;
                color.a = _alpha;
                itemSlotImage.color = color;
            }

            

            //ColorBlock colorBlock = ItemButton.colors;
            //colorBlock.normalColor = new Color(10f, 1f, 1f);
            //ItemButton.colors = colorBlock;
        }

        //아이템 획득
        public void AddItem(string _itemName,MyItem _item)
        {
            item = _item;
            //itemCount = _count;
            itemSlotImage.sprite = item.itemImg;

            if (File.Exists(mySaveNLoad.SAVE_DATA_DIRECTORY + mySaveNLoad.SAVE_FILENAME))
            {
                if(mySaveNLoad.GetIsloading())
                {
                    switch (_itemName)
                    {
                        case "Knife":
                            Transform knife = myInventory.knife();
                            itemSlotImage.sprite = knife.gameObject.GetComponent<Image>().sprite;
                            gameObject.GetComponentInChildren <Image>().enabled= true;
                            break;
                        case "Ox":
                            itemSlotImage.sprite = myInventory.ox().gameObject.GetComponent<Image>().sprite;
                            gameObject.GetComponentInChildren<Image>().enabled = true;
                            break;
                        case "Key":
                            itemSlotImage.sprite = myInventory.key().gameObject.GetComponent<Image>().sprite;
                            gameObject.GetComponentInChildren<Image>().enabled = true;
                            break;
                    }
                }
                
            }

            

            
            Debug.Log($"=======additem======" + item.itemName);

            //GameManager.instance.saveDataList._currentSlotItems
            //아이템이 중복으로 획득가능하다면 갯수를 표시해주는 텍스트 활성화
            //if(item.itemtype != MyItem.ItemType.case1)
            //{
            //    text_Count.text = itemCount.ToString();
            //    go_CountImage.SetActive(true);
            //}
            //else
            //{
            //    text_Count.text = "0";
            //    go_CountImage.SetActive(false);
            //}
            SetColor(1);
        }


        //아이템 갯수 조정
        //public void SetSlotCount(int _count)
        //{
        //    itemCount += _count;
        //    text_Count.text = itemCount.ToString();

        //    if(itemCount <= 0 )
        //    {
        //        ClearSlot();
        //    }
        //}

        Toggle myToggle;

        //슬롯 초기화
        public void ClearSlot()
        {
            item = null;
            //itemCount = 0;
            itemSlotImage.sprite = null;
            SetColor(0);
            
            //go_CountImage.SetActive(false);
            //text_Count.text = "0";
        }
    }
}