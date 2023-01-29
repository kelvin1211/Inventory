using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Kelvin;
using Jason;
using UnityEngine.UI;


[System.Serializable]
public class SaveDataList
{
    //Stage
    public string _currentStage;

    //room
    //public GameObject _currentRoomData;
    public string _currentRoomType;

    //items
    public List<string> inventItemName = new List<string>();
    public List<int> inventArrayNum = new List<int>();
    public List<int> inventItemCount = new List<int>();

    public List<MyItem> inventItem = new List<MyItem>();

    //아이템 획득여부
    public bool _isGetKnifeItem = false;
    public bool _isGetOxItem = false;
    public bool _isGetKeyItem = false;

    //미션별 성공여부 
    public bool _iskeyMission = false;




    //public string Type, Name, Explain, Number;
    //public bool isUsing;

    //public Item(string _Type, string _Name, string _Explain, string _Number, bool _isUsing)
    //{
    //    Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; isUsing = _isUsing;
    //}

}
public class SaveNLoad : MonoBehaviour
{
    SaveDataList saveDataList = new SaveDataList();
    IntroManager myIntroManager;

    public string SAVE_DATA_DIRECTORY;
    public string SAVE_FILENAME = "/SaveFile.txt";

    private Inventory theInvent;

    private bool isLoading = false;
    public bool GetIsloading() { return isLoading; }

    string scene;
    public string _Scene() { return scene; }


    private void Awake()
    {
        CreatDirectory();
        myIntroManager = new IntroManager();
    }


    float SaveTime = 10f;
    private void Update()
    {
        
    }

    

    //디렉토리 검사해서 없으면 디렉토리생성
    void CreatDirectory()
    {
        SAVE_DATA_DIRECTORY = Application.persistentDataPath + "/Saves/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);

        Debug.Log("SAVE_DATA_DIRECTORY ==" + SAVE_DATA_DIRECTORY);
    }


    public void save()
    {
        StartCoroutine(saveData());
    }

    // 데이터 초기화후 저장
    IEnumerator saveData()
    {
        theInvent = FindObjectOfType<Inventory>();

        // 저장 전 데이터 초기화
        saveDataList.inventArrayNum.Clear();
        saveDataList.inventItemName.Clear();
        saveDataList.inventItemCount.Clear();
        saveDataList._iskeyMission = false;
        saveDataList._isGetKnifeItem= false;
        saveDataList._isGetKeyItem= false;
        saveDataList._isGetOxItem= false;
        saveDataList._currentRoomType = "";
        saveDataList._currentStage = "";

        File.Delete(Application.persistentDataPath + "/Saves/SaveFile.txt");
        Debug.Log("===데이터 초기화 완료===");


        yield return new WaitForSeconds(0.1f);

        // Room data
        saveDataList._currentRoomType = GameManager.instance.roomType;

        // Item data
        Slot[] slots = theInvent.GetSlots();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                saveDataList.inventArrayNum.Add(i);
                saveDataList.inventItemName.Add(slots[i].item.itemName);
                saveDataList.inventItemCount.Add(slots[i].item.itemCount);
            }
        }

        //아이템 획득여부
        saveDataList._isGetKeyItem = theInvent.isGetKey;
        saveDataList._isGetOxItem = theInvent.isGetOx;
        saveDataList._isGetKnifeItem = theInvent.isGetKnife;


        //현재 Scene
        saveDataList._currentStage = GameManager.instance._Scene();
        Debug.Log("saveDataList._currentStage ====" + saveDataList._currentStage);


        //json
        string json = JsonUtility.ToJson(saveDataList);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);
        Debug.Log("저장완료===" + SAVE_DATA_DIRECTORY.ToString());
    }

    

    public void LoadData()
    {
        Debug.Log("====로드 시작===" );
        theInvent = FindObjectOfType<Inventory>();
        isLoading = true;


        myIntroManager.scene = saveDataList._currentStage;
        //IntroManager.Instance.scene = saveDataList._currentStage;
        // Item data
        //Slot[] slots = theInvent.GetSlots();

        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            //json
            string Loadjson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveDataList = JsonUtility.FromJson<SaveDataList>(Loadjson);

            
            // Room data
            GameManager.instance.RoomActivateOnLoad(saveDataList._currentRoomType);


            // Item data
            theInvent = FindObjectOfType<Inventory>();

            for (int i = 0; i < saveDataList.inventItemName.Count; i++)
            {
                theInvent.LoadToInvent(saveDataList.inventArrayNum[i], saveDataList.inventItemName[i], saveDataList.inventItemCount[i]);
            }

            //아이템 획득여부
            theInvent.isGetKey = saveDataList._isGetKeyItem;
            theInvent.isGetOx = saveDataList._isGetOxItem;
            theInvent.isGetKnife = saveDataList._isGetKnifeItem;

            //획득여부에 따라 아이템세팅
            if (theInvent.isGetKnife)
                Destroy(theInvent.knife().gameObject);

            if (theInvent.isGetOx)
                Destroy(theInvent.ox().gameObject);

            if(theInvent.isGetKey)
                Destroy(theInvent.key().gameObject);

            //theInvent.CurrentScene = saveDataList._currentStage;
            //Debug.Log("===SaveNLoad scene===" + theInvent.CurrentScene);

            Debug.Log("===로드완료===");
        }
        else
        {
            Debug.Log("=SAVEFILE IS NOT EXIST ^.^=");
        }

        isLoading= false;
    }
    
    

    //저장된 파일 삭제 (파일은 삭제되지만 저장된 데이터들은 메모리화되어 삭제되지 않음. 초기화 해줘야함)
    public void ClickDelete()
    {
        File.Delete(Application.persistentDataPath + "/Saves/SaveFile.txt");
        Debug.Log("===삭제===");
    }
    // Start is called before the first frame update



}
