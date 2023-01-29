using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jason;


namespace Kelvin
{
    public class ChangeItemImg : MonoBehaviour
    {
        Inventory inventory;
        bool isKnifeSelected = false;

        [SerializeField]
        private Image myImg;


        ItemPickUp knifeItemPickUp;

        private void Awake()
        {
            Transform parent = AssetAssist.FindObject("Canvas");
            knifeItemPickUp = AssetAssist.FindComponent<ItemPickUp>("Knife", parent);
        }


        public void CheckingSelected(Toggle _toggle)
        {
            //ColorBlock seleckColor = this.GetComponentInParent<ColorBlock>();


            if (_toggle.isOn)
            {
                if (myImg == knifeItemPickUp.item.itemImg)
                {
                    //inventory.ItemClickEvent();
                    isKnifeSelected = true;
                    _toggle.isOn = false;
                    //seleckColor.selectedColor = new Color(1f, 0f, 1f);
                    return;
                }
            }
            else
            {
                isKnifeSelected = false;
            }
            Debug.Log(_toggle);
        }
    }
}