using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


namespace Kelvin
{
    public class IntroManager : MonoBehaviour
    {
        public static IntroManager Instance;

        private SaveNLoad thehSaveNLoad;

        private SaveDataList thehSaveDataList;

        Inventory theInvent;

        string sceneName = "Stage01";

        public Image fadePanel;

        public string scene;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }

            thehSaveNLoad = new SaveNLoad();
            thehSaveDataList = new SaveDataList();
            theInvent = new Inventory();

            theInvent.isGetKnife = thehSaveDataList._isGetKnifeItem;
            theInvent.isGetKey = thehSaveDataList._isGetKeyItem;
            theInvent.isGetOx = thehSaveDataList._isGetOxItem;



            Debug.Log("intro loadscene===" + scene);
            Debug.Log("===theinventknife==" + thehSaveDataList._isGetKnifeItem);
            Debug.Log("===theinventox==" + thehSaveDataList._isGetKeyItem);
            Debug.Log("===theinventkey==" + thehSaveDataList._isGetOxItem);
        }

        public void testStage02()
        {
            SceneManager.LoadScene(2);
        }


        //시작하기
        public void ClickStart(string _gameSceneName)
        {
            Debug.Log("===로딩중===");

            StartCoroutine(NewGame(_gameSceneName));
            //SceneManager.LoadScene(_gameSceneName);
        }
        //이어하기
        
        public void ClickLoad()
        {
            StartCoroutine(LoadCoroutine());
            Debug.Log("===로드===");
        }


        IEnumerator NewGame(string _scene)
        {
            fadePanel.gameObject.SetActive(true);
            float time = 0f;
            float fadeTime = 2f;
            Color alpha = fadePanel.color;
            while (alpha.a < 1f)
            {
                time += Time.deltaTime / fadeTime;
                alpha.a = Mathf.SmoothStep(0, 1, time);
                fadePanel.color = alpha;
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            AsyncOperation operation = SceneManager.LoadSceneAsync(_scene);
            while (!operation.isDone)
            {
                yield return null;
            }
        }

        //씬이 로드된 이후 저장된 데이터 로드
        IEnumerator LoadCoroutine()
        {
            fadePanel.gameObject.SetActive(true);
            float time = 0f;
            float fadeTime = 2f;
            Color alpha = fadePanel.color;
            while (alpha.a < 1f)
            {
                time += Time.deltaTime / fadeTime;
                alpha.a = Mathf.SmoothStep(0, 1, time);
                fadePanel.color = alpha;
                yield return null;
            }
            
            yield return new WaitForSeconds(1f);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
            {
                yield return null;
            }

            thehSaveNLoad = FindObjectOfType<SaveNLoad>();
            thehSaveNLoad.LoadData();
            Destroy(gameObject);

        }

        //종료
        public void ClickExit()
        {
            Debug.Log("===종료===");
            Application.Quit();
        }



        #region fade in /out 로직: 로드버튼에 out, 로드되는 씬에 in
        public void Fade()
        {
            StartCoroutine(FadeFlow());
        }


        IEnumerator FadeFlow()
        {
            fadePanel.gameObject.SetActive(true);
            float time = 0f;
            float fadeTime = 0.3f;
            Color alpha = fadePanel.color;
            while (alpha.a < 1f)
            {
                time += Time.deltaTime / fadeTime;
                alpha.a = Mathf.Lerp(0, 1, time);
                fadePanel.color = alpha;
                yield return null;
            }

            time = 0f;
            yield return new WaitForSeconds(fadeTime);

            while (alpha.a > 0f)
            {
                time += Time.deltaTime / fadeTime;
                alpha.a = Mathf.Lerp(1, 0, time);
                fadePanel.color = alpha;
                yield return null;
            }
            fadePanel.gameObject.SetActive(false);
            yield return null;
        }
        #endregion


    }

}