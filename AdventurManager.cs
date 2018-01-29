using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Insol.JungleHunter.Data;
using System.Text;
using System;
using System.Linq;
using UnityEngine.UI;
using Insol.JungleHunter.Helper;
using Vuforia;
using UnityEngine.SceneManagement;
using Insol.JungleHunter.Manager;

namespace Insol.Adventur.Manager
{
    public class AdventurManager : Sigleton<AdventurManager>
    {


        #region
        public List<Safari> Land { get; set; }

        //public GameObject GameGuideUI;
        public GameObject MainUI;
        public GameObject InventoryUI;

        public Text AnimalRunName;
        public GameObject InvenBu;
        public GameObject InvenBu1;
        public GameObject InvenBu2;
        public GameObject InvenBu3;

        public GameObject CatchButto;
        private bool isDataInited = false; // 헌팅 타임 유무
        /// <summary>
        /// 메인 화면 부분
        /// </summary>
        private DetectBall detectBall;
        
        public Animator anim;
        public GameObject anim1;
        public GameObject AnimalNameText1;
        public Text CatchAnimalName;
        
        public GameObject AnimalNameRunText1;
        public GameObject RunBu;
        public GameObject InventoryBu;
        public GameObject DogamBackBu;
        /// <summary>
        /// 동물 설명 부분
        /// </summary>
        public GameObject AnimalBackBu;
        public GameObject AnimalName;
        public GameObject AnimalPicture;

        public GameObject AnimalExplan;
        public string msg = "";
        public Text CatchSu;
        public GameObject CatchSu1;
        #endregion
        public ParticleSystem NomalEffect = null;
        #region
        public GameObject AnimalDogam0;
        public GameObject AnimalDogam1;
        public GameObject AnimalDogam2;
        public GameObject AnimalDogam3;
        public GameObject AnimalDogam4;
        public GameObject AnimalDogam5;
        public GameObject AnimalDogam6;
        public GameObject AnimalDogam7;
        public GameObject AnimalDogam8;
        public GameObject AnimalDogam9;
        public GameObject AnimalDogam10;
        public GameObject AnimalDogam11;

        private bool bStartEndinit = false;
        #endregion

        //public Text AnimalNameText;
        //public Text AnimalNameRunText;
        /// <summary>
        /// 인벤토리
        /// </summary>
        [SerializeField]
        HuntingArea LastHuntingArea;
        HuntingTime LastHuntingTime;
        Transform LastHuntingCard;
        public GameObject CurrentAnimal;

        public Transform slotPanel;
        private GameObject[] slotArray;
        public string catchAnimalName;
        

        private bool isThrowBall = false;

        [SerializeField]
        // Use this for initialization
        
        void Start()
        {
            Debug.Log("Start시작");
            StartCoroutine("ShowGuid");
            VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
            VuforiaARController.Instance.RegisterOnPauseCallback(OnPaused);
            InitHuntingArea();
            Screen.orientation = ScreenOrientation.Portrait;

            slotArray = new GameObject[slotPanel.childCount];
            for (int i = 0; i < slotArray.Length; i++)
            {
                slotArray[i] = slotPanel.GetChild(i).GetChild(0).gameObject;
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (CurrentAnimal != null)
                {
                    deactivateCurrentAnimal(false); //deactivatecurrentanimal은 true가 되야지 타겟이 인식됨 .

                }
            }
            if(!bStartEndinit)
            {
                bStartEndinit = true;
                AutoFocusOn();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(1);
                VuforiaBehaviour.Instance.enabled = false;
            }
        }

        IEnumerator CatchAnimal()
        {
            AnimalNameText1.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            deactivateCurrentAnimal(true);
            Debug.Log("CatchAnimal입니다.");
            Debug.Log(catchAnimalName);
            switch (catchAnimalName)
            {
               ///동물들 인벤토리 배열에 넣어줌
                #region

                case "180":
                    slotArray[0].gameObject.SetActive(true);
                    break;
                case "181":
                    slotArray[1].gameObject.SetActive(true);
                    break;
                case "182":
                    slotArray[2].gameObject.SetActive(true);
                    break;
                case "183":
                    slotArray[3].gameObject.SetActive(true);
                    break;
                case "184":
                    slotArray[4].gameObject.SetActive(true);
                    break;
                case "185":
                    slotArray[5].gameObject.SetActive(true);
                    break;
                case "186":
                    slotArray[6].gameObject.SetActive(true);
                    break;
                case "187":
                    slotArray[7].gameObject.SetActive(true);
                    break;
                case "188":
                    slotArray[8].gameObject.SetActive(true);
                    break;
                case "189":
                    slotArray[9].gameObject.SetActive(true);
                    break;
                case "190":
                    slotArray[10].gameObject.SetActive(true);
                    break;
                case "191":
                    slotArray[11].gameObject.SetActive(true);
                    break;

                    #endregion

            }
            catchAnimalName = string.Empty;
            isThrowBall = false;
        }

        void OnPaused(bool paused)
        {
            if (!paused) // resumed
            {
                CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
            }
        }
        public void deactivateCurrentAnimal(bool checkChance) 
        {
            if (CurrentAnimal != null)
            {
                var handler = CurrentAnimal.GetComponentInParent<AdventureTrackableEventHandler>();
                if (handler != null)
                {
                    handler.OnTrackingLost(checkChance);
                }
               
            }

        }
        /// <summary>
        /// 잡기버튼
        /// </summary>
        public void CatchButton()
        {
            if (catchAnimalName != string.Empty)
            {
                StartCoroutine(CatchAnimal());
                Debug.Log("업데이트 캐치애니몰 스타트 코루틴.");
                Destroy(CurrentAnimal);
                CurrentAnimal = null;

            }
            //동물 잡으면 잡았다고 나와요
            #region
            switch (catchAnimalName)
            {
                case "180":
                    msg = "고릴라를 잡았다 !";
                    break;
                case "181":
                    msg = "기린을 잡았다!";
                    break;
                case "182":
                    msg = "낙타를 잡았다!";
                    break;
                case "183":
                    msg = "불곰을 잡았다!";
                    break;
                case "184":
                    msg = "숫사자를 잡았다!";
                    break;
                case "185":
                    msg = "암사자를 잡았다!";
                    break;
                case "186":
                    msg = "얼룩말을 잡았다!";
                    break;
                case "187":
                    msg = "코끼리를 잡았다!";
                    break;
                case "188":
                    msg = "코뿔소를 잡았다!";
                    break;
                case "189":
                    msg = "토끼를 잡았다!";
                    break;
                case "190":
                    msg = "하마를 잡았다!";
                    break;
                case "191":
                    msg = "호랑이를 잡았다!";
                    break;
            }
            CatchSu.text = msg;
            CatchSu1.SetActive(true);
            #endregion
        }
        private void OnVuforiaStarted()
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO); 

        }
        void AutoFocusOn() //카메라 오토포커싱
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO); //씬 로두된후 오토포커싱
        }
        IEnumerator ShowGuid() //여긴 메인화면 부분인데 타겟이 인식되서 동물이 나타나면 메세지가 없어지고 동물이 없으면 메세지가 다시 떠요
        {
            while (true)
            {
                yield return new WaitForSeconds(2.5f);
                if (CurrentAnimal != null)
                {
                    MainUI.SetActive(false);
                    CatchButto.SetActive(true);
                }
                else
                {
                    CatchSu1.SetActive(false);
                    if(CurrentAnimal == null)
                    {
                        MainUI.SetActive(true);
                        CatchButto.SetActive(false);
                        
                    }
                }
            }
        }
        private void InitHuntingArea()
        {
           Land = new List<Safari>() { Safari.고릴라, Safari.기린, Safari.낙타, Safari.불곰, Safari.숫사자, Safari.암사자, Safari.얼룩말, Safari.코끼리, Safari.코뿔소, Safari.토끼, Safari.하마, Safari.호랑이 };
        }

        public void Back() //도감 화면에서 인벤토리 화면으로  돌아가기
        {
            #region
            InventoryUI.SetActive(true);

            if (slotArray[0].gameObject.activeSelf == true)
            {
                AnimalDogam0.SetActive(false);
            }

            else if (slotArray[1].gameObject.activeSelf == true)
            {
                AnimalDogam1.SetActive(false);
            }
            else if (slotArray[2].gameObject.activeSelf == true)
            {
                AnimalDogam2.SetActive(false);
            }

            else if (slotArray[3].gameObject.activeSelf == true)
            {
                AnimalDogam3.SetActive(false);
            }

            else if (slotArray[4].gameObject.activeSelf == true)
            {
                AnimalDogam4.SetActive(false);
            }

            else if (slotArray[5].gameObject.activeSelf == true)
            {
                AnimalDogam5.SetActive(false);
            }

            else if (slotArray[6].gameObject.activeSelf == true)
            {
                AnimalDogam6.SetActive(false);
            }

            else if (slotArray[7].gameObject.activeSelf == true)
            {
                AnimalDogam7.SetActive(false);
            }

            else if (slotArray[8].gameObject.activeSelf == true)
            {
                AnimalDogam8.SetActive(false);
            }

            else if (slotArray[9].gameObject.activeSelf == true)
            {
                AnimalDogam9.SetActive(false);
            }

            else if (slotArray[10].gameObject.activeSelf == true)
            {
                AnimalDogam10.SetActive(false);
            }

            else if (slotArray[11].gameObject.activeSelf == true)
            {
                AnimalDogam11.SetActive(false);
            }
            if (InventoryUI.activeSelf != true)
            {
                InventoryUI.SetActive(true);
            }

            #endregion
        }
        public void InvenNagagi()
        {
            MainUI.SetActive(true);
            InventoryBu.SetActive(true);
            InventoryUI.SetActive(false);
            VuforiaBehaviour.Instance.enabled = true;

        }
        public void InventoryButton() //인벤토리 들어가기 버튼
        {
            
            InventoryUI.SetActive(true);
            //카메라 끄기 
            CatchButto.SetActive(false);
            InventoryUI.SetActive(true);
            MainUI.SetActive(false);
            InventoryBu.SetActive(false);
            VuforiaBehaviour.Instance.enabled = false;

        }


        public void AnimalDogam_UI()
        {
            //카메라
            #region
                ///동물들 인벤토리 배열에 넣어줌
                if (slotArray[0].gameObject.activeSelf ==true)
            {
                AnimalDogam0.SetActive(true);
            }
               else if(slotArray[1].gameObject.activeSelf == true)
            {
                AnimalDogam1.SetActive(true);
            }

        }
        public void Dogamgo()//인벤토리에서 잡은 동물 클릭하면 동물 도감 띄어줌
        {
            //InventoryUI.SetActive(false);
            Debug.Log(catchAnimalName);


#region 
            ///동물들 인벤토리 배열에 넣어줌

            if (slotArray[0].gameObject.activeSelf == true)
            {
                AnimalDogam0.SetActive(true);
            }

            if (slotArray[1].gameObject.activeSelf == true)
            {
                AnimalDogam1.SetActive(true);
            }

            if (slotArray[2].gameObject.activeSelf == true)
            {
                AnimalDogam2.SetActive(true);
            }

            if (slotArray[3].gameObject.activeSelf == true)
            {
                AnimalDogam3.SetActive(true);
            }

            if (slotArray[4].gameObject.activeSelf == true)
            {
                AnimalDogam4.SetActive(true);
            }

            if (slotArray[5].gameObject.activeSelf == true)
            {
                AnimalDogam5.SetActive(true);
            }

            if (slotArray[6].gameObject.activeSelf == true)
            {
                AnimalDogam6.SetActive(true);
            }

            if (slotArray[7].gameObject.activeSelf == true)
            {
                AnimalDogam7.SetActive(true);
            }

            if (slotArray[8].gameObject.activeSelf == true)
            {
                AnimalDogam8.SetActive(true);
            }

            if (slotArray[9].gameObject.activeSelf == true)
            {
                AnimalDogam9.SetActive(true);
            }

            if (slotArray[10].gameObject.activeSelf == true)
            {
                AnimalDogam10.SetActive(true);
            }

            if (slotArray[11].gameObject.activeSelf == true)
            {
                AnimalDogam11.SetActive(true);
            }


#endregion
        }

        IEnumerator deactive()
        {
            yield return new WaitForSeconds(2f);
            deactivateCurrentAnimal(true);
        }
        IEnumerator Runmag()
        {
            yield return new WaitForSeconds(2f);
            AnimalNameRunText1.SetActive(false);
        }
        
        public void OnTrackingFound(AnimalData animalData, Transform cardTransform) //동물 찾기
        {
            Debug.Log("온트래킹파운드입니다.");
            Renderer[] rendererComponents = cardTransform.GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = cardTransform.GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            // DustEffect.gameObject.SetActive(true);
            // DustEffect.Play();
            

            StartCoroutine(AssetBundleManager.Instance.LoadAnimalAssetBundle(animalData.AnimalType, animalData.AnimalName, (assetGameObject) =>
             {
                 //동물 타입 이름 .
                 Debug.Log(animalData.AnimalType);
                 Debug.Log(animalData.AnimalName);
                 NomalEffect.Play();
                 catchAnimalName = animalData.AnimalName.ToString();
                 //동물 종류;
          
                 #region
                 switch (catchAnimalName)
                 {
                     case "180":
                         msg = "고릴라가 나타났다!! 잡아보자!!!";
                         break;
                     case "181":
                         msg = "기린이 나타났다!! 잡아보자!!!";
                         break;
                     case "182":
                         msg = "낙타가 나타났다!! 잡아보자!!!";
                         break;
                     case "183":
                         msg = "불곰이 나타났다!! 잡아보자!!!";
                         break;
                     case "184":
                         msg = "숫사자가 나타났다!! 잡아보자!!!";
                         break;
                     case "185":
                         msg = "암사자가 나타났다!! 잡아보자!!!";
                         break;
                     case "186":
                         msg = "얼묵말이 나타났다!! 잡아보자!!!";
                         break;
                     case "187":
                         msg = "코끼리가 나타났다!! 잡아보자!!!";
                         break;
                     case "188":
                         msg = "코불쏘 나타났다!! 잡아보자!!!";
                         break;
                     case "189":
                         msg = "토끼가 나타났다!! 잡아보자!!!";
                         break;
                     case "190":
                         msg = "하마가 나타났다!! 잡아보자!!!";
                         break;
                     case "191":
                         msg = "호랑이가 나타났다!! 잡아보자!!!";
                         break;
                 }
                 CatchAnimalName.text = msg;
                 #endregion

                 //DustEffect.Pause();
                 //DustEffect.gameObject.SetActive(false);
                 if (assetGameObject == null)
                    return;

                CurrentAnimal = Instantiate(assetGameObject);
                CurrentAnimal.transform.SetParent(cardTransform);
                animalData.AnimalGameObject = CurrentAnimal;
                float scaleRatio = 0;

                switch (animalData.AnimalType)
                {
                    case AnimalType.Safari:
                        scaleRatio = 0.2f;
                        break;
                    default:
                        scaleRatio = 1f;
                        break;
                }

                 CurrentAnimal.transform.SetParent(cardTransform);
                 CurrentAnimal.transform.localScale = Vector3.one * scaleRatio;
                 CurrentAnimal.transform.localPosition = new Vector3(0, 0, 0);
                 CurrentAnimal.SetActive(true);
                 



                Renderer[] Render = CurrentAnimal.GetComponentsInChildren<Renderer>(true);
                for (int index = 0; index < Render.Length; index++)
                {
                    // Shader 이름 정보는 가지고 있는데 지정되어 있지 않아서
                    // Shader를 다시 넣어준다
                    if (Render[index].sharedMaterials.Count() > 1)
                    {
                        foreach (var material in Render[index].sharedMaterials)
                        {
                            if (material != null && material.shader != null)
                            {
                                var shader = Shader.Find(material.shader.name);//여기 부분
                                if (shader != null)
                                    material.shader = shader;
                            }
                        }
                    }
                    else if (Render[index].sharedMaterial != null)
                    {
                        var shader = Shader.Find(Render[index].sharedMaterial.shader.name);
                        if (shader != null)
                            Render[index].sharedMaterial.shader = shader;
                    }
                }
                foreach (Collider component in colliderComponents)
                    {
                        component.enabled = true;
                    }

                    ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
                    foreach (ParticleSystem particleSystem in particleSystems)
                    {
                        particleSystem.Play();
                    }

            }));
            AnimalNameText1.SetActive(false);
        }
    }
    [Serializable]
    public class AnimalData
    {
        public GameObject AnimalGameObject;
        public AnimalType AnimalType;
        public string AnimalName;
    }
}
