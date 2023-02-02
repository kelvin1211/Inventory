using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jason;
using Kelvin;
using System;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

//������ �޸������� ������ ����Ƽ�� ������ ��� ������
//[System.Serializable]
//public class SaveDataList
//{
//    //room
//    public GameObject _currentRoomData;

//    //items
//    public List _currentSlotItems;

//    public void getImg(int _slotnum, Image _img)
//    {
//        //PlayerPrefs.SetInt()

//        for (int i = 0; i < 16; i++)
//        {
//            //_currentSlotItems[_slotnum].ad= _img;
//        }

//    }
//    //public string[] _currentSlotItemName = {"1","2","3","4" };


//    //�̼Ǻ� �������� 
//    public bool _iskeyMission = false;




//    //public string Type, Name, Explain, Number;
//    //public bool isUsing;

//    //public Item(string _Type, string _Name, string _Explain, string _Number, bool _isUsing)
//    //{
//    //    Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; isUsing = _isUsing;
//    //}

//}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    string CurrentScene;
    public string _Scene() { return CurrentScene; }

    #region SAVEnLOAD

    //string SAVE_DATA_DIRECTORY;
    //string SAVE_FILENAME = "/SaveFile.txt";


    ////start�� �������� �Լ�
    //void CreatDirectory()
    //{
    //    SAVE_DATA_DIRECTORY = Application.dataPath+ "/Saves/";

    //    if(!Directory.Exists(SAVE_DATA_DIRECTORY)) 
    //        Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    //}


    //public void SaveData()
    //{
    //    // Room data
    //    saveDataList._currentRoomData = roomTypeObj;



    //    //json
    //    string json = JsonUtility.ToJson(saveDataList);
    //    File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);
    //    Debug.Log("����Ϸ�===" + json);
    //}

    //public void LoadData()
    //{
    //    //json
    //    if(File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
    //    {
    //        string Loadjson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
    //        saveDataList = JsonUtility.FromJson<SaveDataList>(Loadjson);

    //        roomTypeObj = saveDataList._currentRoomData;
    //        Debug.Log("�ε�Ϸ�");
    //    }
    //    else
    //    {
    //        Debug.Log("���̺����� ����");
    //    }
        

    //}
    #endregion

    //�� ��ũ��Ʈ ����
    Inventory myInven;
    CheckRoomRay myRay;
    Slot mySlot;
    PlayerData4Saving myPlayerData;
    SaveNLoad mySaveNLoad;

    //������ �޸������� ������ ����Ƽ�� ������ ��� ������
    //public TextAsset ItemDatabase;
    //public List<Item> AllItemList;

    AudioSource TheAudio;


    //RayCast �ؼ� ������ Room ����
    public string roomType;
    public GameObject roomTypeObj;

    //Fade �������
    Image panel;
    float time = 0f;
    float fadeTime = 0.3f;


    //Room �̵� ��ư
    public Button LeftArrow;
    public Button RightArrow;
    public Button DownArrow;
    public Button UpArrow;


    //Room 
    public Transform RoomImgGroup;
    public Transform Room00;
    public Transform Room01;
    public Transform Room02;
    public Transform Room03;
    public Transform Room04;



    //Area
    Button ZoomArea_A;

    public TextMeshProUGUI myText_item;
    public TextMeshProUGUI myText_discription;


    //save
    public string path;



    //fade panel
    Image fadePanel;


    private void Awake()
    {

        //// ���� ���̸�
        //CurrentScene = SceneManager.GetActiveScene().name;
        //Debug.Log("scene==" + CurrentScene);

        myInven = new Inventory();
        myRay = new CheckRoomRay();
        mySlot = new Slot();
        myPlayerData = new PlayerData4Saving();
        mySaveNLoad = new SaveNLoad();

        #region �̱���
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            // �ΰ��� ���� �޴����� �������� �ʵ��� ����
            Destroy(gameObject);
            Destroy(this);
        }
        #endregion

        LoadComponents();
        Debug.Log("gm loadcomponent===");
        ClickArrowEvent();
        Debug.Log("gm Clickevent===");
        StartCoroutine(FadeIn());


        //Debug.Log("roomName_Raycast====" + myRay.roomName_Raycast);
        //Debug.Log(" myInven.Room00.name==" + myInven);
    }

    

    void Start()
    {
        /*������ �޸������� ������ ����Ƽ�� ������ ��� ������
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');

        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4] == "TRUE"));

        }*/
        myText_discription.text = "���� �ϼ��� �ʱ�ȭ���ּ���";

    }


    private void Update()
    {
        
    }

    
    public void LoadComponents()
    {
        Transform parent = AssetAssist.FindObject("UICanvas");
        RoomImgGroup = AssetAssist.FindComponent<Transform>("RoomImgGroup");
        panel = AssetAssist.FindComponent<Image>("FadeInOut", parent);


       

        //Room �̵� ��ư
        LeftArrow = AssetAssist.FindComponent<Button>("LeftArrow", parent);
        RightArrow = AssetAssist.FindComponent<Button>("RightArrow", parent);
        DownArrow = AssetAssist.FindComponent<Button>("DownArrow", parent);
        UpArrow = AssetAssist.FindComponent<Button>("UpArrow", parent);
        DownArrow.gameObject.SetActive(false);
        UpArrow.gameObject.SetActive(false);





        //Room Transform
        Room00 = AssetAssist.FindComponent<Transform>("Room0", parent);
        Room01 = AssetAssist.FindComponent<Transform>("Room1", parent);
        Room02 = AssetAssist.FindComponent<Transform>("Room2", parent);
        Room03 = AssetAssist.FindComponent<Transform>("Room3", parent);
        Room04 = AssetAssist.FindComponent<Transform>("Room4", parent);


        //Area
        ZoomArea_A = AssetAssist.FindComponent<Button>("ZoomIn", parent);


        //ù ���������� Room0 �� ��쿡�� �����ʹ�ư ��Ȱ��ȭ
        if (Room00.gameObject.activeSelf)
            RightArrow.gameObject.SetActive(false);

        //Audio
        TheAudio = AssetAssist.FindComponent<AudioSource>("GameManager");

        //TextMesh
        myText_item = AssetAssist.FindComponent<TextMeshProUGUI>("Text_item",parent);
        myText_discription = AssetAssist.FindComponent<TextMeshProUGUI>("Text_discription", parent);
        myText_item.text = "";
        myText_discription.text = "";

        //������
        path = Application.persistentDataPath + "/Save";


        //FadePanel
        fadePanel = AssetAssist.FindComponent<Image>("FadeIn", parent);

        /////////////
        /*
        //������ �ҷ�����
        J_knife = AssetAssist.Instantiate("Prefebs/Items/Knife", Room00);
        J_ox = AssetAssist.Instantiate("Prefebs/Items/Ox", Room01);
        J_key = AssetAssist.Instantiate("Prefebs/Items/Key", Room01);
        J_areaA = AssetAssist.Instantiate("Prefebs/Area/KnifeArea", Room03);
        J_areaB = AssetAssist.Instantiate("Prefebs/Area/OxArea", Room04);


        RemoveText(J_knife.gameObject);
        RemoveText(J_ox.gameObject);
        RemoveText(J_key.gameObject);
        RemoveText(J_areaA.gameObject);
        RemoveText(J_areaB.gameObject);


        //�ҷ��� ������ �ʱ���ġ ����
        J_key.localPosition = new Vector3(1, 1, 0);


        Knife = J_knife.GetComponent<Button>();

        //���� ������ �޸� �Ҵ�
        Knife = AssetAssist.FindComponent<Button>("Knife", parent);
        Ox = AssetAssist.FindComponent<Button>("Ox", parent);
        Ox1 = AssetAssist.FindComponent<Button>("Ox1", parent);
        Key = AssetAssist.FindComponent<Button>("Key", parent);


        //Discrpition
        Discription = AssetAssist.FindComponent<TextMeshProUGUI>("Text", parent);
        */
    }


    

    // ���� �ε�� �� Fade in
    public IEnumerator FadeIn()
    {
        fadePanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        float time = 0f;
        float fadeTime = 3f;
        Color alpha = fadePanel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.SmoothStep(1, 0, time);
            fadePanel.color = alpha;
            yield return null;
        }
        fadePanel.gameObject.SetActive(false);
    }

   

    //Json ����ȭ,������ȭ�� ����Ϸ��� �⺻json���� �ٸ�newtonsoft���� ��ųʸ��� ���.


    IEnumerator Sound()
    {
        yield return new WaitForSeconds(2f);
        //TheAudio.volume -= 0.1f;
    }

    public void PlaySound(AudioClip _clip)
    {
        TheAudio.clip = _clip;
        TheAudio.Play();
        StartCoroutine(Sound());
    }




    // discription Fade npc�� Ŭ���ϸ� ������ ���忡 �ɾ��� �ڷ�ƾ
    IEnumerator LerpColor()
    {
        yield return new WaitForSeconds(5f);

        float smoothness = 0.01f;
        float duration = 5f;
        float progress = 0;

        //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {
            myText_discription.color = Color.Lerp(myText_discription.color, Color.clear, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        myText_discription.text = "";
        yield return true;
    }



    // Fade �� �ƿ� ����
    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(0, 1, time);
            panel.color = alpha;
            yield return null;
        }

        time = 0f;
        yield return new WaitForSeconds(fadeTime);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(1, 0, time);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }

    //Raycast�� ���� ������ �ش� Room�̸��� roomType�� ����
    public void CheckRayToGM(string _Room, GameObject _RoomObj)
    {
        roomTypeObj = _RoomObj;
        roomType = _Room;
    }


    #region Room �̵� �ڷ�ƾ
    void RoomActivate()
    {
        switch (roomType)
        {
            case "Room0":
                Room00.gameObject.SetActive(true);
                Room01.gameObject.SetActive(false);
                Room02.gameObject.SetActive(false);
                Room03.gameObject.SetActive(false);
                Room04.gameObject.SetActive(false);
                break;
            case "Room1":
                Room00.gameObject.SetActive(false);
                Room01.gameObject.SetActive(true);
                Room02.gameObject.SetActive(false);
                Room03.gameObject.SetActive(false);
                Room04.gameObject.SetActive(false);

                break;
            case "Room2":
                Room00.gameObject.SetActive(false);
                Room01.gameObject.SetActive(false);
                Room02.gameObject.SetActive(true);
                Room03.gameObject.SetActive(false);
                Room04.gameObject.SetActive(false);

                break;
            case "Room3":
                Room00.gameObject.SetActive(false);
                Room01.gameObject.SetActive(false);
                Room02.gameObject.SetActive(false);
                Room03.gameObject.SetActive(true);
                Room04.gameObject.SetActive(false);

                break;
            case "Room4":
                Room00.gameObject.SetActive(false);
                Room01.gameObject.SetActive(false);
                Room02.gameObject.SetActive(false);
                Room03.gameObject.SetActive(false);
                Room04.gameObject.SetActive(true);

                break;
        }
    }

    // �ε� �� �� Room, ȭ��ǥ ����  
    public void RoomActivateOnLoad(string _room)
    {
        switch (_room)
        {
            case "Room0":
                Room00.gameObject.SetActive(true);
                Room01.gameObject.SetActive(false);
                Room02.gameObject.SetActive(false);
                Room03.gameObject.SetActive(false);
                Room04.gameObject.SetActive(false);
                break;
            case "Room1":
                Room00.gameObject.SetActive(false);
                Room01.gameObject.SetActive(true);
                Room02.gameObject.SetActive(false);
                Room03.gameObject.SetActive(false);
                Room04.gameObject.SetActive(false);

                break;
            case "Room2":
                Room00.gameObject.SetActive(false);
                Room01.gameObject.SetActive(false);
                Room02.gameObject.SetActive(true);
                Room03.gameObject.SetActive(false);
                Room04.gameObject.SetActive(false);

                break;
            case "Room3":
                Room00.gameObject.SetActive(false);
                Room01.gameObject.SetActive(false);
                Room02.gameObject.SetActive(false);
                Room03.gameObject.SetActive(true);
                Room04.gameObject.SetActive(false);

                break;
            case "Room4":
                Room00.gameObject.SetActive(false);
                Room01.gameObject.SetActive(false);
                Room02.gameObject.SetActive(false);
                Room03.gameObject.SetActive(false);
                Room04.gameObject.SetActive(true);

                break;
        }
        if (_room == "Room0")
        {
            RightArrow.gameObject.SetActive(false);
        }
        else
        {
            RightArrow.gameObject.SetActive(true);
        }

        if(_room == "Room4")
        {
            LeftArrow.gameObject.SetActive(false);
        }
        else
        {
            LeftArrow.gameObject.SetActive(true);
        }

    }

    


    // �⺻ �̵� ��ƾ
    IEnumerator ChangeRoomLeft()
    {

        yield return new WaitForSeconds(0.3f);


        switch (roomType)
        {
            case "Room0":
                roomType = "Room1";
                RightArrow.gameObject.SetActive(true);
                break;
            case "Room1":
                roomType = "Room2";
                RightArrow.gameObject.SetActive(true);
                LeftArrow.gameObject.SetActive(true);
                break;
            case "Room2":
                roomType = "Room3";
                RightArrow.gameObject.SetActive(true);
                LeftArrow.gameObject.SetActive(true);
                break;
            case "Room3":
                roomType = "Room4";
                LeftArrow.gameObject.SetActive(false);
                break;
        }
        RoomActivate();

    }



    IEnumerator ChangeRoomRight()
    {
        yield return new WaitForSeconds(0.3f);


        switch (roomType)
        {
            case "Room4":
                roomType = "Room3";
                LeftArrow.gameObject.SetActive(true);
                break;
            case "Room3":
                roomType = "Room2";
                RightArrow.gameObject.SetActive(true);
                LeftArrow.gameObject.SetActive(true);
                break;
            case "Room2":
                roomType = "Room1";
                LeftArrow.gameObject.SetActive(true);
                RightArrow.gameObject.SetActive(true);
                break;
            case "Room1":
                roomType = "Room0";
                RightArrow.gameObject.SetActive(false);
                break;
        }
        RoomActivate();
    }

    IEnumerator ChangeRoomDown()
    {
        yield return new WaitForSeconds(0.3f);


        RoomActivate();
    }


    IEnumerator ChangeRoomUp()
    {
        yield return new WaitForSeconds(0.3f);

        RoomActivate();

    }

    
    // Room �̵� �����ư �̺�Ʈ
    public void ClickArrowEvent()
    {
        LeftArrow.onClick.AddListener(() =>
        {
            Debug.Log("leftArrow===" + LeftArrow.gameObject.activeSelf);
            Fade();
            StartCoroutine(ChangeRoomLeft());
        });



        RightArrow.onClick.AddListener(() =>
        {
            Fade();
            StartCoroutine(ChangeRoomRight());
        });

        DownArrow.onClick.AddListener(() =>
        {
            Fade();
            ZoomArea_A.gameObject.SetActive(true);
            Room00.GetComponent<DragSystem>().enabled = true;

            if (roomTypeObj.transform.localScale.x == 1f)
            {
                DownArrow.gameObject.SetActive(false);
                UpArrow.gameObject.SetActive(true);
                RightArrow.gameObject.SetActive(false);
                LeftArrow.gameObject.SetActive(false);
            }
            else if (roomTypeObj.transform.localScale.x == 5f)
            {
                StartCoroutine(zoomOut());
                LeftArrow.gameObject.SetActive(true);
                return;
            }
        });

        UpArrow.onClick.AddListener(() =>
        {
            Fade();
            UpArrow.gameObject.SetActive(false);
            DownArrow.gameObject.SetActive(true);

            StartCoroutine(ChangeRoomUp());


        });
        //}else if (myInven.CurrentSceneName() == "Stage02")
        //    {
        //        LeftArrow.onClick.AddListener(() =>
        //        {
        //            Debug.Log("leftArrow00===" + LeftArrow.gameObject.activeSelf);
        //            Fade();
        //StartCoroutine(ChangeRoomLeft());
        //});
    }
    #endregion

    public IEnumerator zoomOut()
    {
        yield return new WaitForSeconds(0.3f);
        Room00.transform.localScale = new Vector3(1, 1, 1);
        Room01.transform.localScale = new Vector3(1, 1, 1);
        Room02.transform.localScale = new Vector3(1, 1, 1);
        Room03.transform.localScale = new Vector3(1, 1, 1);
        Room04.transform.localScale = new Vector3(1, 1, 1);

        Room00.transform.localPosition = new Vector3(0, 0, 0);
        Room01.transform.localPosition = new Vector3(0, 0, 0);
        Room02.transform.localPosition = new Vector3(0, 0, 0);
        Room03.transform.localPosition = new Vector3(0, 0, 0);
        Room04.transform.localPosition = new Vector3(0, 0, 0);
        //roomTypeObj.transform.localScale = new Vector3(1, 1, 1);
    }

    
}
