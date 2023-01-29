using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jason;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Data;
using Unity.VisualScripting;
using TMPro;
using System;
using System.IO;
using Kelvin;
using UnityEngine.SceneManagement;

//public class SlotData
//{
//    bool isEmpty;
//    GameObject slotObj;
//}

namespace Kelvin
{
    public class Inventory : MonoBehaviour
    {
        SaveDataList mySaveData;

        //오디오 클립
        [SerializeField]
        AudioClip LockSound;
        [SerializeField]
        AudioClip UnLockSound;
        [SerializeField]
        AudioClip ItemResponSound;
        [SerializeField]
        AudioClip ItemClickSound;
        [SerializeField]
        AudioClip SurpriseSound;

        Transform parent;
        ToggleGroup toggleGroupInstance;


        [SerializeField]
        public Toggle[] toggles;

        //씬 위의 아이템
        private Transform J_knife;
        private Transform J_ox;
        private Transform J_key;

        public Transform knife() { return J_knife; }
        public Transform ox() { return J_ox; }
        public Transform key() { return J_key; }


        ////씬 위의 클릭area (인벤아이템 선택 후)
        Transform J_areaA;
        Transform J_areaB;

        //씬 위의 아이템 버튼
        Button Knife;
        Button Ox;
        Button Ox1;
        Button Key;

        public MyItem item;

        //씬 위의 특정 area버튼 (인벤에서 아이템 선택 후 특정 위치를 클릭하기 위함)
        Button knifeArea;
        Button OxArea;
        public Button ZoomArea;
        //public Button zoomArea() { return ZoomArea; }


        //인벤슬롯에 아이템있는지 여부
        string toggleSpriteName;

        //슬롯 인덱스
        int index;

        //아이템의 선택 유무
        bool isKnifeSelected = false;
        bool isOxSelected = false;
        
        bool isKeySelected = false;

        bool isToggleEmpty;

        //토글 선택,비선택 색상
        [SerializeField]
        Color selectedColor;
        [SerializeField]
        Color UnselectedColor;


        //public List<SlotData> slots = new List<SlotData>();

        //인벤위의 슬롯과 아이템버튼
        [SerializeField]
        Image[] InvenItemImg;


        public Transform Room00;
        Transform Room01;
        Transform Room02;
        Transform Room03;
        Transform Room04;


        [SerializeField]
        public Slot[] slots;

        public Slot[] GetSlots() { return slots; }

        [SerializeField]
        private MyItem[] items;


        public bool isGetKnife;

        public bool isGetOx;

        public bool isGetKey;



        //현재 씬
        string CurrentScene;
        public string CurrentSceneName() { return CurrentScene; }
        
        private void Awake()
        {
            mySaveData = new SaveDataList();
            isGetKnife = mySaveData._isGetKnifeItem;
            isGetOx = mySaveData._isGetOxItem;
            isGetKey = mySaveData._isGetKeyItem;


            Debug.Log("===isKnife===" + isGetKnife);
            Debug.Log("===isox===" + isGetOx);
            Debug.Log("===isKey===" + isGetKey);

            Debug.Log("===mySaveData._isGetKnifeItem===" + mySaveData._isGetKnifeItem);
            Debug.Log("===mySaveData._isGetOxItem===" + mySaveData._isGetOxItem);
            Debug.Log("===mySaveData._isGetKeyItem===" + mySaveData._isGetKeyItem);

            /*같은 신에서는 싱글톤으로 접근해서 변수를 얻어오고, 다른 신에서는 상속으로 얻어온다.
            Debug.Log("invent=====" + GameManager.instance.myName);*/
            
            LoadComponent();
            UnSelectToggle();
        }

        private void Start()
        {
            GameManager.instance.LoadComponents();
            ItemClickEvent();
            areaClickEvent();

             //= mySaveData._currentStage;


            Debug.Log("invent CurrentScene==" + CurrentScene);
        }

        /// <summary>
        /// 아이템 추가 순서
        /// 1.메모리할당
        /// 2.아이템 scriptable object 설정
        /// 3.아이템 선택 유무 설정   >>>>isKeySelected
        /// 4.조건문 추가             >>>>CheckingSelected
        /// 5.해당 area 함수 추가     >>>>  areaClickEvent
        /// 6.아이템 클릭 이벤트 함수 추가   >>>> ItemClickEvent
        /// 6.
        /// </summary>
       
        public void LoadComponent()
        {
            // 현재 씬이름
            CurrentScene = SceneManager.GetActiveScene().name;
            Debug.Log("invenscene==" + CurrentScene);

            //CurrentScene = GameManager.instance._Scene();

            parent = AssetAssist.FindObject("UICanvas");
            //토글그룹과 자식의 토글 컴포넌트 메모리할당
            toggleGroupInstance = AssetAssist.FindComponent<ToggleGroup>("Content", parent);
            toggles = toggleGroupInstance.GetComponentsInChildren<Toggle>();


            //Room Transform
            Room00 = AssetAssist.FindComponent<Transform>("Room0", parent);
            Room01 = AssetAssist.FindComponent<Transform>("Room1", parent);
            Room02 = AssetAssist.FindComponent<Transform>("Room2", parent);
            Room03 = AssetAssist.FindComponent<Transform>("Room3", parent);
            Room04 = AssetAssist.FindComponent<Transform>("Room4", parent);


            //프리펩 불러오기
            J_knife = AssetAssist.Instantiate("Prefebs/Items/Knife", Room00);
            J_ox = AssetAssist.Instantiate("Prefebs/Items/Ox", Room01);
            J_key = AssetAssist.Instantiate("Prefebs/Items/Key", Room01);
            J_areaA = AssetAssist.Instantiate("Prefebs/Area/KnifeArea", Room03);
            J_areaB = AssetAssist.Instantiate("Prefebs/Area/OxArea", Room04);

            ////KnifePrefeb = Resources.Load("Prefebs/Items/Knife")as GameObject;


            RemoveText(J_knife.gameObject);
            RemoveText(J_ox.gameObject);
            RemoveText(J_key.gameObject);
            RemoveText(J_areaA.gameObject);
            RemoveText(J_areaB.gameObject);


            //불러온 프리펩 초기위치 설정
            J_key.localPosition = new Vector3(1, 1, 0);





            //인벤의 아이템슬롯 메모리할당
            for (int i = 0; i < InvenItemImg.Length; i++)
            {
                InvenItemImg[i] = AssetAssist.FindComponent<Image>($"item" + i, parent);

                if (InvenItemImg[i] == null)
                {
                    InvenItemImg[i].enabled = false;
                    toggles[i].enabled = false;
                }
            }


            ////씬위 특정area 메모리 할당
            knifeArea = AssetAssist.FindComponent<Button>("KnifeArea", parent);
            OxArea = AssetAssist.FindComponent<Button>("OxArea", parent);
            ZoomArea = AssetAssist.FindComponent<Button>("ZoomIn", parent);
        }


        public void LoadToInvent(int _arrayNum, string _itemName, int _itemCount)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].itemName == _itemName)
                {
                    slots[_arrayNum].AddItem(items[i].itemName, items[i]);
                }
            }
        }


        //클론객체 텍스트지우기
        public void RemoveText(GameObject preFab)
        {
            int index = preFab.name.IndexOf("(Clone)");
            if (index > 0)
                preFab.name = preFab.name.Substring(0, index);
        }

        //모든 토글키 비활성화
        public void UnSelectToggle()
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].isOn = false;
            }
        }


        //받은 매개변수로 해당 아이템의 정보를 additem함수로 전달 >>  전달받은 additem은 해당 아이템의 sprite로 변경
        public void AccquireItem(MyItem _item)
        {

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    toggles[i].enabled = true;
                    InvenItemImg[i].enabled = true;
                    slots[i].AddItem(_item.itemName, _item);
                    isToggleEmpty = true;
                    return;
                }
            }
        }


        /*슬롯의 아이템 sprite 변경

        //void PutItemIntoInven(string Name)
        //{
        //    for (int i = 0; i < InvenItemImg.Length; i++)
        //    {
        //        if (InvenItemImg[i].sprite == null)
        //        {
        //            //Resources 폴더에 있는 sprite 이미지 호출
        //            Sprite ChangeSprite = Resources.Load<Sprite>($"Items/Img/" + Name);
        //            InvenItemImg[i].sprite = ChangeSprite;
        //            break;
        //        }


        //    }
        //}

        */

        //씬에 배치된 아이템 클릭시 해당 아이템비활성화 & 슬롯에 추가하는 함수 실행 & 특정 area 클릭시 아이템 활성화
        // 필드 아이템 클릭 이벤트



        // 다시생성된 프리펩 제어안되는 문제!!!!!!!!!!!!!!
        private void ItemClickEvent()
        {
            if (CurrentScene== "Stage01")
            {
                if (J_knife.gameObject != null)
                {
                    J_knife.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        GameManager.instance.PlaySound(ItemClickSound);
                        AccquireItem(J_knife.GetComponent<ItemPickUp>().item);
                        isGetKnife = true;
                        Destroy(J_knife.gameObject, 0.1f);
                    });
                }


                if (J_ox.gameObject != null)
                {
                    J_ox.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        GameManager.instance.PlaySound(ItemClickSound);
                        AccquireItem(J_ox.GetComponent<ItemPickUp>().item);

                        isGetOx = true;
                        Destroy(J_ox.gameObject, 0.1f);
                        //Ox.gameObject.SetActive(false);
                    });
                }


                if (J_key.gameObject != null)
                {
                    J_key.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        GameManager.instance.PlaySound(ItemClickSound);
                        AccquireItem(J_key.GetComponent<ItemPickUp>().item);
                        isGetKey = true;
                        Destroy(J_key.gameObject, 0.1f);
                        //Key.gameObject.SetActive(false);
                    });
                }
            }
        }

        //onclick 버튼 이벤트는 destory 할 때 파괴 된다. Instantiate 재 생성 해서 쓸 때 다시, onClick 이벤트를 걸어줘야한다.

        // 필드 area 클릭 이벤트
        private void areaClickEvent()
        {
            if (CurrentScene == "Stage01")
            {
                if (J_knife.gameObject.activeSelf || J_key.gameObject.activeSelf)
                {
                    knifeArea.onClick.AddListener(() =>
                    {
                        GameManager.instance.PlaySound(LockSound);
                        if (isKnifeSelected || isKeySelected)
                        {
                            UnSelectToggle();
                            isKnifeSelected = false;
                            isKeySelected = false;

                            if (toggleSpriteName == "Knife")
                            {
                                slots[index].ClearSlot();

                                //아이템 생성
                                J_knife = AssetAssist.Instantiate("Prefebs/Items/Knife", Room03);
                                RemoveText(J_knife.gameObject);
                                J_knife.transform.localPosition = new Vector3(10, 111, 0);
                                J_knife.GetComponent<Button>().onClick.AddListener(() =>
                                {
                                    GameManager.instance.PlaySound(ItemClickSound);
                                    AccquireItem(J_knife.GetComponent<ItemPickUp>().item);
                                    isGetKnife = true;
                                    Destroy(J_knife.gameObject, 0.1f);
                                });
                                //Debug.Log("==J_knife. button===" + J_knife.GetComponent<Button>().ToString());

                                isGetKnife = true;
                                //칼로 자르는 애니메이션

                                GameManager.instance.PlaySound(ItemClickSound);

                                return;
                            }
                            else if (toggleSpriteName == "key_inven")
                            {
                                slots[index].ClearSlot();
                                GameManager.instance.PlaySound(UnLockSound);
                                isGetKey = true;
                                //Key.gameObject.SetActive(true);

                                // 문 또는 상자열리는 애니메이션

                                //return;
                            }
                        }


                    });
                }

                if (J_ox.gameObject.activeSelf)
                {
                    OxArea.onClick.AddListener(() =>
                    {
                        if (isOxSelected == true)
                        {
                            UnSelectToggle();
                            isOxSelected = false;

                            if (toggleSpriteName == "Ox")
                            {
                                slots[index].ClearSlot();
                                //J_ox.localPosition = new Vector3(50, 10, 0);
                                isGetOx = true;
                                return;
                            }
                        }

                    });
                }


                if (ZoomArea.gameObject.activeSelf)
                {
                    ZoomArea.onClick.AddListener(() =>
                    {
                        GameManager.instance.PlaySound(SurpriseSound);
                        GameManager.instance.Fade();
                        GameManager.instance.DownArrow.gameObject.SetActive(true);
                        GameManager.instance.LeftArrow.gameObject.SetActive(false);
                        Debug.Log("00invent====roomTypeobj===" + GameManager.instance.roomTypeObj + "==localscale:" + GameManager.instance.roomTypeObj.transform.localScale.x);
                        StartCoroutine(zoomin());
                        Debug.Log("11invent====roomType===" + GameManager.instance.roomType + "==localscale:" + GameManager.instance.roomTypeObj.transform.localScale.x);
                        Debug.Log("GMroomType===" + GameManager.instance.Room00.name + "==localscale==" + GameManager.instance.Room00.transform.localScale.x);
                    });
                }

            }

        }
        IEnumerator zoomin()
        {
            yield return new WaitForSeconds(0.3f);
            GameManager.instance.Room00.localScale = new Vector3(5, 5, 1);
            GameManager.instance.Room00.localPosition = (new Vector3(110, 2000, 1));
            //.localPosition = new Vector3(0, 0, 1);
            ZoomArea.gameObject.SetActive(false);
            Room00.GetComponent<DragSystem>().enabled = false;
            yield return null;
            //Debug.Log("GMroomType===" + GameManager.instance.Room00.name + "==localscale==" + GameManager.instance.Room00.transform.localScale.x);
        }
        

        void OpenTheBox()
        {
            Debug.Log("=======openBox======");
        }
        /// <summary>
        /// 토글그룹 안의 토글중 하나라도 켜져 있으면, if문을 돈다.
        /// 문제는 토글에 해당하는 아이템을 검사하여 각 토글별 다르게 적용해야하는데...........
        /// 각 토글의 
        /// </summary>


        //각 슬롯버튼에 이벤트로 걸어줄 함수
        public void ColorChange(Toggle _toggle)
        {
            ColorBlock cb = _toggle.colors;

            if (_toggle.isOn)
            {
                cb.normalColor = selectedColor;
                cb.selectedColor = selectedColor;
                _toggle.colors = cb;
            }
            else
            {
                cb.normalColor = UnselectedColor;
                cb.selectedColor = UnselectedColor;
                _toggle.colors = cb;
                GameManager.instance.myText_item.text = "";
            }
        }
        public void CheckingSelected(int _slotNum)
        {
            if (InvenItemImg[_slotNum].sprite != null)
            {
                toggleSpriteName = InvenItemImg[_slotNum].sprite.name;
                Debug.Log("toggleSpriteName ==" + toggleSpriteName);

                

                //GameManager.instance.saveDataList.getImg(1, InvenItemImg[_slotNum]);
                //Debug.Log("saveDataList._currentSlotItemName===" + GameManager.instance.saveDataList._currentSlotItems[0]);

                index = _slotNum;
                Debug.Log("Index======" + index);
                if (toggleSpriteName == "Knife")
                {
                    GameManager.instance.myText_item.text = "칼";
                    Debug.Log("toggleSpriteName====" + toggleSpriteName);
                    isKnifeSelected = true;
                    
                    Debug.Log($"knifeis True====" + isKnifeSelected);
                }
                else if (toggleSpriteName == "Ox")
                {
                    isOxSelected = true;
                    GameManager.instance.myText_item.text = "도끼";
                }
                else if (toggleSpriteName == "key_inven")
                {
                    isKeySelected = true;
                    GameManager.instance.myText_item.text = "열쇠";
                }
                else
                {
                    isKnifeSelected = false;
                    isOxSelected = false;
                    isKeySelected = false;

                }
            }
        }
    }
}