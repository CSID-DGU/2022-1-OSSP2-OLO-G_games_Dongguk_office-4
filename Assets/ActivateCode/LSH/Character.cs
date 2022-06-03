//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using CodeStage.AntiCheat.ObscuredTypes;
//using Photon.Pun;

//public abstract class Character : MonoBehaviourPun
//{
//    public int nowSelectedWeapone;
//    public GameObject[] nowEquipedWeapones;

//    public GameObject hand;
//    public bool isNeedRotation;
//    public float speed = 3;
//    DataMangaer myData;
//    protected void Awake()
//    {


//    }
//    protected void Start()
//    {
//        Debug.Log("character Instantiated" + photonView.IsMine);
//        if (!PhotonNetwork.InRoom)
//        {
//            DataMangaer.instance.myCharacter = this.gameObject;
//            FollowingCamera.instance.targetCharacterTransform = this.transform;
//            FollowingCamera.instance.gameObject.transform.position = this.transform.TransformPoint(new Vector3(0, 0, -10));
//            return;
//        }
//        if (!photonView.IsMine)
//        {

//            return;
//        }
//        else
//        {
//            DataMangaer.instance.myCharacter = this.gameObject;
//            FollowingCamera.instance.targetCharacterTransform = this.transform;
//        }


//    }
//    protected void Update()
//    {
//        if (!photonView.IsMine) return;
//        //when player input 'k' weapon swap.
//        if (Input.GetKey(KeyCode.T))
//        {
//            Debug.Log("swap weaspone");
//            SwapWeapone();
//        }
//        CharacterMoveController();
//    }
//    protected void FixedUpdate()
//    {
//        if (!photonView.IsMine)
//        {
//            return;
//        }


//    }

//    protected void SwapWeapone()
//    {
//        if (nowSelectedWeapone == 0)
//        {
//            nowSelectedWeapone = 1;
//            if (nowEquipedWeapones[1] != null)
//            {
//                nowEquipedWeapones[1].SetActive(true);
//            }
//            else
//            {
//                return;
//            }
//            if (nowEquipedWeapones[0] != null)
//            {
//                nowEquipedWeapones[0].SetActive(false);
//            }

//        }
//        else
//        {
//            nowSelectedWeapone = 0;
//            if (nowEquipedWeapones[0] != null)
//            {
//                nowEquipedWeapones[0].SetActive(true);
//            }
//            else
//            {
//                return;
//            }
//            if (nowEquipedWeapones[1] != null)
//            {
//                nowEquipedWeapones[1].SetActive(false);
//            }
//        }



//    }

//    [System.Serializable]
//    public class MonsterStatus
//    {
//        HP

//        public int hp;
//        public int damage;   // Damage




//        [HideInInspector]
//        public int maxHp;

//        public int defaultHp;
//        [Range(0f, 1f)]
//        public float hpRandomness;





//        public int defaultDamage;
//        [Range(0f, 1f)]
//        public float damageRandomness;

//        Defense
//        public int physicDefense;
//        public int magicDefense;

//        [Range(0f, 1f)]
//        public float defaultPhysicDefense;

//        [Range(0f, 1f)]
//        public float defaultMagicDefense;


//        [Range(0f, 1f)]
//        public float defenseRandomness;

//        스탯 초기화
//        public void Init()
//        {
//            InitHp();
//            InitDamage();
//            InitDefense();
//        }

//        HP 결정
//        private void InitHp()
//        {
//            (int min, int max) = this.CalculateMinMax(this.defaultHp, this.hpRandomness);

//            this.maxHp = Random.Range(min, max);
//            this.hp = this.maxHp;
//        }

//        Damage 결정
//        private void InitDamage()
//        {
//            (int min, int max) = this.CalculateMinMax(this.defaultDamage, this.damageRandomness);

//            this.damage = Random.Range(min, max);
//        }

//        Defense 결정
//        private void InitDefense()
//        {
//            (float minPhysic, float maxPhysic) = this.CalculateMinMax(this.defaultPhysicDefense, this.defenseRandomness);
//            (float minMagic, float maxMagic) = this.CalculateMinMax(this.defaultMagicDefense, this.defenseRandomness);

//            this.physicDefense = Random.Range(minPhysic, maxPhysic);
//            this.magicDefense = Random.Range(minMagic, maxMagic);
//        }

//        범위 계산(int)
//        private (int, int) CalculateMinMax(int value, float randomness)
//        {
//            return (
//                (int)(value * (1 - randomness)),
//                (int)(value * (1 + randomness))
//            );
//        }

//        범위 계산(float)
//        private (float, float) CalculateMinMax(float value, float randomness)
//        {
//            return (
//                value * (1 - randomness),
//                value * (1 + randomness)
//            );
//        }
//    }

//    public class MonsterSpawner : MonoBehaviourPun
//    {
//        [SerializeField]
//        string monsterNameInResources;
//        private void Awake()
//        {

//        }
//        void Start()
//        {
//            if (PhotonNetwork.IsMasterClient)
//            {
//                GameObject monster = PhotonNetwork.Instantiate(monsterNameInResources, this.transform.position, Quaternion.identity, 0) as GameObject;

//                monster.GetComponent<Monster>().spawnerPosition = this.transform;
//            }

//        }


//        Update is called once per frame
//        void Update()
//        {

//        }
//    }

//    public class Player : MonoBehaviour
//    {
//        float rollTime = 0.1f;
//        float rollAccelerateMagnification = 3.5f;
//        float speed = 2.4f;
//        const float rollCooltime = 2.0f;
//        float rollCooltimeCounter = 2.0f;
//        public GameObject characterSprite;
//        int left, right, up, down;
//        Vector3 characterMovePos;
//        bool isCharacterCanMove;


//        void Start()
//        {
//            for test, character always can move
    
//            isCharacterCanMove = true;
//        }
//        void Update()
//        {
//            rollCooltimeCounter += Time.deltaTime;


//        }
//        private void FixedUpdate()
//        {
//            캐릭터가 움직이다 물리 판정이 생길 수 있으니, fixedupdate에서 이동 처리
//            left = 0; right = 0; up = 0; down = 0;

//            if (Input.GetKey(KeyCode.A))
//            {
//                left = -1;
//            }
//            if (Input.GetKey(KeyCode.D))
//            {
//                right = 1;
//            }
//            if (Input.GetKey(KeyCode.W))
//            {
//                up = 1;
//            }
//            if (Input.GetKey(KeyCode.S))
//            {
//                down = -1;
//            }

//            characterMovePos = new Vector3(left + right, up + down, 0).normalized;
//            if (isCharacterCanMove)
//            {
//                if (Input.GetKey(KeyCode.Space) && rollCooltimeCounter > rollCooltime)
//                {
//                    rollCooltimeCounter = 0;
//                    StartCoroutine(PlayerRollCo());
//                }
//                else
//                {
//                    this.transform.Translate(speed * Time.fixedDeltaTime * characterMovePos);
//                }
//            }


//        }

//        IEnumerator PlayerRollCo()
//        {
//            Debug.Log("캐릭터 구르기");
//            isCharacterCanMove = false;
//            float rolledTime = 0;
//            Vector3 rollStartCharacterDirection = characterMovePos;
//            while (true)
//            {
//                yield return new WaitForFixedUpdate();
//                rolledTime += Time.fixedDeltaTime;
//                if (rolledTime > rollTime)
//                {
//                    break;
//                }
//                else
//                {
//                    transform.Translate(speed * Time.fixedDeltaTime * rollAccelerateMagnification * rollStartCharacterDirection);
//                    characterSprite.transform.Rotate(0, 0, 360 / rollTime * Time.fixedDeltaTime);
//                }
//            }
//            isCharacterCanMove = true;
//            characterSprite.transform.eulerAngles = Vector3.zero;
//        }
//    }

//    public class NetworkManager : MonoBehaviourPunCallbacks
//    {

//        public static NetworkManager instance;
//        public GameObject roomListContent;
//        public GameObject joinRoomButton;

//        private string gameVersion = "1"; //???? ????

//        public GameObject MyCharacter;
//        public Photon.Realtime.Player[] joinedPlayerList;
//        private void Awake()
//        {
//            if (instance == null)
//            {
//                instance = this;

//            }
//            else
//            {
//                Destroy(this.gameObject);
//            }

//        }
//        private void Start()
//        {
//            PhotonNetwork.NickName = DataMangaer.instance.myNickName;
//            PhotonNetwork.GameVersion = gameVersion;
//            PhotonNetwork.AutomaticallySyncScene = true;

//            ServerConnect();

//        }
//        public void ServerConnect()
//        {
//            PhotonNetwork.ConnectUsingSettings();

//            Debug.Log("ConnectUsingSettings");
//        }
//        public override void OnConnectedToMaster()
//        {
//            base.OnConnectedToMaster();


//            Debug.Log("OnConnectedToMaster");
//            PhotonNetwork.JoinLobby();

//        }
//        public override void OnDisconnected(DisconnectCause cause)
//        {
//            base.OnDisconnected(cause);
//        }
//        public void Connect()
//        {

//        }
//        public override void OnJoinedLobby()
//        {
//            base.OnJoinedLobby();



//        }


//        public override void OnJoinRandomFailed(short returnCode, string message)
//        {
//            base.OnJoinRoomFailed(returnCode, message);

//        }

//        public override void OnRoomListUpdate(List<RoomInfo> roomList)
//        {
//            base.OnRoomListUpdate(roomList);

//            foreach (var i in roomList)
//            {
//                if (i.PlayerCount < 1)
//                {
//                    for (int j = roomListContent.transform.childCount - 1; j >= 0; j--)
//                    {
//                        if (roomListContent.transform.GetChild(j).GetComponent<JoinRoomButton>().roomName == i.Name)
//                        {
//                            Destroy(roomListContent.transform.GetChild(j).gameObject);
//                        }
//                    }
//                }
//                else
//                {
//                    bool isAlreadyShow = false;
//                    for (int j = 0; j < roomListContent.transform.childCount; j++)
//                    {
//                        if (roomListContent.transform.GetChild(j).GetComponent<JoinRoomButton>().roomName == i.Name)
//                        {
//                            isAlreadyShow = true;
//                            JoinRoomButton buttonScript = roomListContent.transform.GetChild(j).GetComponent<JoinRoomButton>();
//                            buttonScript.roomName = i.Name;
//                            buttonScript.maxPlayerCount = i.MaxPlayers;
//                            buttonScript.nowPlayerCount = i.PlayerCount;
//                            buttonScript.roomInfoText.text = i.Name + "\n" + i.PlayerCount.ToString() + "/" + i.MaxPlayers.ToString();
//                            if (!i.IsOpen)
//                            {
//                                Debug.Log("playing roomg");
//                                buttonScript.roomInfoText.text += "\n[playing]";
//                            }
//                        }
//                    }
//                    if (!isAlreadyShow)
//                    {
//                        Debug.Log(i.Name + " : " + i.PlayerCount);
//                        GameObject button = Instantiate(joinRoomButton, roomListContent.transform) as GameObject;
//                        JoinRoomButton buttonScript = button.GetComponent<JoinRoomButton>();
//                        buttonScript.roomName = i.Name;
//                        buttonScript.maxPlayerCount = i.MaxPlayers;
//                        buttonScript.nowPlayerCount = i.PlayerCount;
//                        buttonScript.roomInfoText.text = i.Name + "\n" + i.PlayerCount.ToString() + "/" + i.MaxPlayers.ToString();
//                        if (!i.IsOpen)
//                        {
//                            Debug.Log("playing roomg");
//                            buttonScript.roomInfoText.text += "\n[playing]";
//                        }
//                    }

//                }
//            }


//        }
//        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
//        {
//            base.OnPlayerEnteredRoom(newPlayer);

//        }

//        public GameObject NetworkInstantiater(string name)
//        {
//        ???????? ?????? ???? ????
//        GameObject obj = PhotonNetwork.Instantiate(name, Vector3.zero, Quaternion.identity) as GameObject;
//            return obj;
//        }

//        public void makeRoomByName(string roomName)
//        {

//            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4, EmptyRoomTtl = 0, PublishUserId = true });
//        }

//        public void joinRoomByName(string roomName)
//        {
//            join room by name
//            PhotonNetwork.JoinRoom(roomName);
//        }
//        public override void OnJoinedRoom()
//        {
//            base.OnJoinedRoom();
//            joinedPlayerList = PhotonNetwork.PlayerList;
//            LobbyManager.instance.PopUpWaitingRoomPanel();
//            WaitingRoom.instance.SetWaitingRoomStatus();
//            Debug.Log("join room by name success");
//            photonView.RPC("SendMyInfoToMasterWhenJoinedRoom", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.UserId, PhotonNetwork.NickName);


//        }

//        [PunRPC]
//        public void SendMyInfoToMasterWhenJoinedRoom(string uid, string nick)
//        {
//            for (int i = 0; i < WaitingRoom.instance.userInfoList.Length; i++)
//            {
//                if (string.IsNullOrEmpty(WaitingRoom.instance.userInfoList[i].userID))
//                {
//                    WaitingRoom.instance.userInfoList[i].userID = uid;
//                    WaitingRoom.instance.userInfoList[i].userNick = nick;
//                    break;
//                }
//            }
//            if (PhotonNetwork.IsMasterClient)
//            {
//                photonView.RPC("UpdataRoomInfo", RpcTarget.All, JsonConvert.SerializeObject(WaitingRoom.instance.userInfoList));
//            }
//        }
//        [PunRPC]
//        public void UpdataRoomInfo(string infoListJsonData)
//        {
//            Debug.Log("jsonData = " + infoListJsonData);
//            WaitingRoom.instance.userInfoList = JsonConvert.DeserializeObject<WaitingRoomUserInfo[]>(infoListJsonData);
//            WaitingRoom.instance.SetWaitingRoomStatus();
//        }
//        public override void OnJoinRoomFailed(short returnCode, string message)
//        {
//            base.OnJoinRoomFailed(returnCode, message);

//        }

//        public void LeaveRoom()
//        {
//            if (PhotonNetwork.InRoom)
//            {
//                PhotonNetwork.LeaveRoom();
//            }

//        }

//        public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
//        {
//            base.OnRoomPropertiesUpdate(propertiesThatChanged);


//        }

//        [PunRPC]
//        public void UpdateCountOfPlayerInRoomInfo()
//        {

//            joinedPlayerList = PhotonNetwork.PlayerList;
//            foreach (var i in joinedPlayerList)
//            {
//                Debug.Log(i.ActorNumber + "th player name:" + i.NickName);
//            }

//            WaitingRoom.instance.SetWaitingRoomStatus();

//        }


//        public void GameReadyInWaitingRoom()
//        {
//            photonView.RPC("setReadyStatisInWaitingRoom", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.UserId);
//        }

//        [PunRPC]
//        public void setReadyStatisInWaitingRoom(string uid)
//        {
//            foreach (var i in WaitingRoom.instance.userInfoList)
//            {
//                if (i.userID == uid)
//                {
//                    i.isReady = true;
//                }
//            }
//            if (PhotonNetwork.IsMasterClient)
//            {
//                photonView.RPC("UpdataRoomInfo", RpcTarget.All, JsonConvert.SerializeObject(WaitingRoom.instance.userInfoList));
//            }
//        }
//        public void exitRoom()
//        {
//            photonView.RPC("SendLeaveInfoToMaster", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.UserId);


//            PhotonNetwork.LeaveRoom();
//            foreach (var i in WaitingRoom.instance.userInfoList)
//            {
//                i.userID = "";
//                i.userNick = "";
//                i.isReady = false;
//            }
//        }
//        [PunRPC]
//        public void SendLeaveInfoToMaster(string uid)
//        {
//            foreach (var i in WaitingRoom.instance.userInfoList)
//            {
//                if (i.userID == uid)
//                {
//                    i.userID = "";
//                    i.userNick = "";
//                    i.isReady = false;
//                }
//            }
//            if (PhotonNetwork.IsMasterClient)
//            {
//                photonView.RPC("UpdataRoomInfo", RpcTarget.All, JsonConvert.SerializeObject(WaitingRoom.instance.userInfoList));
//            }

//        }
//        [PunRPC]
//        public void exitPlayer(string playerName)
//        {
//            Debug.Log("user has been exited");
//            WaitingRoom.instance.userInfoDic[playerName].transform.GetChild(0).gameObject.SetActive(false);
//            WaitingRoom.instance.userInfoDic[playerName].gameObject.SetActive(false);
//            WaitingRoom.instance.userInfoDic.Remove(playerName);
//        }

//        public void LoadNetworkLevel(string sceneName)
//        {
//            PhotonNetwork.LoadLevel(sceneName);
//        }

//    }

//    public class LoadingSceneController : MonoBehaviour
//    {
//        public Animator animator;
//        static string nextScene;

//        [SerializeField] Image progressBar;

//        public static void LoadScene(string sceneName)
//        {
//            nextScene = sceneName;
//            SceneManager.LoadScene("LoadingScene");
//        }
//        Start is called before the first frame update
//        void Start()
//        {
//            StartCoroutine(LoadSceneProcess());
//        }

//        IEnumerator LoadSceneProcess()
//        {
//            AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
//            op.allowSceneActivation = false;

//            로딩 바
//            float timer = 0f;
//            while (!op.isDone)
//            {
//                yield return null;
//                if (op.progress < 0.9f)
//                {
//                    progressBar.fillAmount = op.progress;
//                }
//                else
//                {
//                    timer += Time.unscaledDeltaTime;
//                    progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
//                    if (progressBar.fillAmount >= 1f)
//                    {
//                        animator.SetTrigger("FadeOut");
//                        op.allowSceneActivation = true;
//                        yield break;
//                    }
//                }
//            }

//        }
//    }

//    public abstract class Enterance : MonoBehaviour
//    {
//        float enterCooltime = 1.0f;//스페이스바 입력간격 설정
//        float timeCounter = 0;
//        private void Update()
//        {
//            timeCounter += Time.deltaTime;
//        }

//        private void OnTriggerStay2D(Collider2D collision)
//        {
//            if (Input.GetKey(KeyCode.Space) && timeCounter > enterCooltime)
//            {
//                if (collision.tag == "Character")
//                {
//                    timeCounter = 0;
//                    EnterAction();
//                }
//            }
//        }
//        protected abstract void EnterAction();

//    }

//    public abstract class DroppedItem : MonoBehaviourPun
//    {
//        public int itemCode;
//        public Sprite itemImage;
//        Start is called before the first frame update
//        void Start()
//        {
//            Debug.Log(itemCode);
//        }

//        Update is called once per frame
//        void Update()
//        {

//        }



//        protected abstract void OnPickUpItem();



//    }
//    public class DataMangaer : MonoBehaviour
//    {
//        const int MaxInventoryItemCount = 16;
//        public GameObject myCharacter;
//        public int inGameIndex;//멀티플레이 몇번째 플레이언지 인덱스
//        public static DataMangaer instance;

//        public UserData userData;
//        public GameObject testObj;

//        public string myNickName;

//        public InGameStat gameStat;//인게임에 적용될 스탯(최종 합산상태)
//        public PlayerEquipData nowEquipData;//현재 착용 장비


//        public bool isInLobby = true;
//        private void Awake()
//        {
//            Application.runInBackground = true;
//            myNickName = PlayerPrefs.GetString("NickName");
//            userData = new UserData();
//            gameStat = new InGameStat();
//            if (instance == null)
//            {
//                instance = this;
//            }
//            else
//            {
//                Destroy(gameObject);
//            }

//            if (!PlayerPrefs.HasKey("playerData"))
//            {
//                userData.selectedCharacterName = "Character1";

//                userData.equipInventory = new List<EquipData>();
//                userData.consumeInventory = new List<ConsumeData>();
//                userData.savedNpcList = new List<int>();
//                userData.stat = new CharacterStatData();

//                userData.stat.initData();
//                userData.haveMoney = 20;
//                saveData();
//            }

//            loadData();

//            UpdateStat();

//        }
//        private void Start()
//        {

//        }
//        public void DeleteAllPrefs()
//        {
//            PlayerPrefs.DeleteAll();
//        }
//        public void UpdateStat()
//        {
//            캐릭터 스탯과 장비 스탯 합산
//            int finalStr = userData.stat.str + nowEquipData.GetAllAddStr();
//            int finalDex = userData.stat.dex + nowEquipData.GetAllAddDex();
//            int finalInt = userData.stat.intelligent + nowEquipData.GetAllAddInt();
//            int finalLuck = userData.stat.luck + nowEquipData.GetAllAddLuck();

//            합산한 스탯 기준으로 인게임 수치 계산
//            가중치로 밸런스 조절
//            gameStat.maxHp = userData.stat.baseHp + nowEquipData.GetAllAddHp() + (finalStr * 5);
//            gameStat.maxMp = userData.stat.baseMp + nowEquipData.GetAllAddMp() + (finalInt * 3);
//            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Lobby")
//            {
//                로비에서만 장비 장착 시 스탯 만땅
//                gameStat.nowHp = gameStat.maxHp;
//                gameStat.nowMp = gameStat.maxMp;
//            }
//            else
//            {
//                그외 스탯 올라가지 않고 최대치까지 낮추기
//                if (gameStat.nowHp > gameStat.maxHp)
//                {
//                    gameStat.nowHp = gameStat.maxHp;
//                }
//                if (gameStat.nowMp > gameStat.maxMp)
//                {
//                    gameStat.nowMp = gameStat.maxMp;
//                }
//            }

//            gameStat.finalPhysicAtk = nowEquipData.GetAllAddAtk() + (finalStr / 2);
//            gameStat.finalMagicAtk = nowEquipData.GetAllAddMagic() + (finalInt * 3);
//            gameStat.finalAccuracyRate = 0.5f + (finalDex / 100); //dex 50이면 무조건 적중
//            gameStat.finalAvoidenceRate = finalLuck / 100; //luck 100이면 무조건 회피
//            InGameUIManager.instance.UpdateStatUI();

//        }


//        public void AddItem(int itemCode, int amount)
//        {
//            saveData();
//        }
//        public void ConsumItem(int itemCode)
//        {

//            saveData();
//        }
//        public void EquipDataItem(int itemCode)
//        {
//            saveData();
//        }


//        public void saveData()
//        {
//            Debug.Log("저장");
//            Debug.Log(ObjectToJson(userData));
//            PlayerPrefs.SetString("playerData", ObjectToJson(userData));
//        }
//        public void loadData()
//        {
//            userData = JsonToOject(PlayerPrefs.GetString("playerData"));
//            nowEquipData.LoadData();
//            InGameUIManager.instance.UpdateGold();
//        }


//        string ObjectToJson(object obj)
//        {

//            return JsonConvert.SerializeObject(obj);
//        }
//        UserData JsonToOject(string jsonData)
//        {
//            return JsonConvert.DeserializeObject<UserData>(jsonData);
//        }
//        public void AddGold(int amount)
//        {
//            userData.haveMoney += amount;
//            saveData();
//            InGameUIManager.instance.UpdateGold();
//        }
//        public void AddEquip(EquipData data)
//        {
//            if (!isEquipInventoryFull())
//            {
//                userData.equipInventory.Add(data);
//            }
//            else
//            {
//                Debug.Log("인벤토리 포화상태");
//            }
//            saveData();
//        }
//        public bool isEquipInventoryFull()
//        {
//            if (userData.equipInventory.Count < MaxInventoryItemCount)
//            {
//                return false;
//            }
//            else
//            {
//                return true;
//            }
//        }
//    }

//    [System.Serializable]
//    public class UserData
//    {
//        public string selectedCharacterName;//나중에 여러 캐릭터 사용을 위함
//        public int haveMoney;//소지 재화

//        public CharacterStatData stat;//캐릭터 기본 스탯


//        public List<EquipData> equipInventory = new List<EquipData>();//장비 인벤토리
//        public List<ConsumeData> consumeInventory = new List<ConsumeData>();//소비 아이템 인벤토리

//        public List<int> savedNpcList;//구한 npc리스트
//    }

//    [System.Serializable]
//    public class CharacterStatData
//    {
//        public int baseHp;
//        public int baseMp;
//        public int str;//물공 관련
//        public int dex;//명중관련
//        public int intelligent;//마공 관련
//        public int luck;//회피 관련

//        public void initData()
//        {
//            baseHp = 100;
//            baseMp = 30;
//            str = 10;
//            dex = 10;
//            intelligent = 10;
//            luck = 10;
//        }


//    }

//    [System.Serializable]
//    public class PlayerEquipData
//    {
//        public EquipData weapon;
//        public EquipData head;//머리
//        public EquipData amor;//갑옷
//        public EquipData ring;//반지

//        public void LoadData()
//        {

//            weapon = DataMangaer.instance.userData.equipInventory.Where(equip => equip.itemType == ItemType.Weapon).Where(equip => equip.getIsNowEquip() == true).FirstOrDefault();
//            head = DataMangaer.instance.userData.equipInventory.Where(equip => equip.itemType == ItemType.Head).Where(equip => equip.getIsNowEquip() == true).FirstOrDefault();
//            amor = DataMangaer.instance.userData.equipInventory.Where(equip => equip.itemType == ItemType.Amor).Where(equip => equip.getIsNowEquip() == true).FirstOrDefault();
//            ring = DataMangaer.instance.userData.equipInventory.Where(equip => equip.itemType == ItemType.Ring).Where(equip => equip.getIsNowEquip() == true).FirstOrDefault();
//            var test = DataMangaer.instance.userData.equipInventory.Where(equip => equip.itemType == ItemType.Head);
//            foreach (var i in test)
//            {
//                Debug.Log(i.itemName);
//                Debug.Log(i.getIsNowEquip());
//                Debug.Log(i.addHp);
//            }

//        }

//        public int GetAllAddHp()
//        {
//            장비 hp옵션 합
//            return (weapon?.addHp ?? 0) + (head?.addHp ?? 0) + (amor?.addHp ?? 0) + (ring?.addHp ?? 0);
//        }
//        public int GetAllAddMp()
//        {
//            장비 mp옵션 합
//            return (weapon?.addMp ?? 0) + (head?.addMp ?? 0) + (amor?.addMp ?? 0) + (ring?.addMp ?? 0);

//        }
//        public int GetAllAddStr()
//        {
//            장비 str옵션 합
//            return (weapon?.addStr ?? 0) + (head?.addStr ?? 0) + (amor?.addStr ?? 0) + (ring?.addStr ?? 0);

//        }
//        public int GetAllAddDex()
//        {
//            장비 dex옵션 합
//            return (weapon?.addDex ?? 0) + (head?.addDex ?? 0) + (amor?.addDex ?? 0) + (ring?.addDex ?? 0);

//        }
//        public int GetAllAddInt()
//        {
//            장비 Int옵션 합
//            return (weapon?.addIntelligent ?? 0) + (head?.addIntelligent ?? 0) + (amor?.addIntelligent ?? 0) + (ring?.addIntelligent ?? 0);

//        }
//        public int GetAllAddLuck()
//        {
//            return (weapon?.addLuck ?? 0) + (head?.addLuck ?? 0) + (amor?.addLuck ?? 0) + (ring?.addLuck ?? 0);

//        }
//        public int GetAllAddAtk()
//        {
//            return (weapon?.addAtk ?? 0) + (head?.addAtk ?? 0) + (amor?.addAtk ?? 0) + (ring?.addAtk ?? 0);

//        }
//        public int GetAllAddMagic()
//        {
//            return (weapon?.addMagic ?? 0) + (head?.addMagic ?? 0) + (amor?.addMagic ?? 0) + (ring?.addMagic ?? 0);

//        }
//    }

//    [System.Serializable]
//    public class InGameStat
//    {
//        public int maxHp;
//        public int maxMp;
//        public int finalPhysicAtk;
//        public int finalMagicAtk;
//        public float finalAvoidenceRate;
//        public float finalAccuracyRate;
//        public int nowHp;
//        public int nowMp;
//    }
//    public class

//    캐릭터 아이템 소유정보, 착용정보, 스탯 분리해서 저장



//public enum AtkType
//    {
//        physic, magic
//    }

//    public class CharacterController : MonoBehaviour
//    {
//        Start is called before the first frame update
//        public GameObject Character;
//        public GameObject hand;
//        public static CharacterController instance;

//        int left, right, up, down;//???? ???? ????
//        Vector3 characterMovePos;

//        private void Awake()
//        {
//            if (instance == null)
//            {
//                instance = this;
//            }
//            else
//            {

//            }

//        }
//        private void Start()
//        {
//            Character = this.gameObject;


//        }

//        private void FixedUpdate()
//        {
//            CharacterMoveController();
//            handRotationController();
//        }
//        void CharacterMoveController()
//        {
//            left = 0; right = 0; up = 0; down = 0;



//            if (Input.GetKey(KeyCode.A))
//            {
//                left = -1;
//            }
//            if (Input.GetKey(KeyCode.D))
//            {
//                right = 1;
//            }
//            if (Input.GetKey(KeyCode.W))
//            {
//                up = 1;
//            }
//            if (Input.GetKey(KeyCode.S))
//            {
//                down = -1;
//            }


//            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > Character.transform.position.x)
//            {
//                offset = 0;
//                Character.transform.localScale = new Vector3(1, 1, 1);
//                if ((left + right) > 0)
//                {
//                    Character.GetComponent<Animator>().SetFloat("moveSpeed", 1.0f);
//                }
//                else if ((left + right) < 0)
//                {
//                    Character.GetComponent<Animator>().SetFloat("moveSpeed", -1.0f);
//                }
//            }
//            else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < Character.transform.position.x)
//            {
//                offset = 180;
//                Character.transform.localScale = new Vector3(-1, 1, 1);
//                if ((left + right) > 0)
//                {
//                    Character.GetComponent<Animator>().SetFloat("moveSpeed", -1.0f);
//                }
//                else if ((left + right) < 0)
//                {
//                    Character.GetComponent<Animator>().SetFloat("moveSpeed", 1.0f);
//                }
//            }



//            characterMovePos = new Vector3(left + right, up + down, 0).normalized;
//            if (characterMovePos != Vector3.zero)
//            {
//                Character.GetComponent<Animator>().SetBool("isMove", true);
//            }
//            else
//            {
//                Character.GetComponent<Animator>().SetBool("isMove", false);
//            }
//            Character.transform.Translate(2.0f * Time.fixedDeltaTime * characterMovePos);
//        }
//        float offset = 0f;
//        void handRotationController()
//        {
//            hand.transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1));
//            RotateTowards(Camera.main.ScreenToWorldPoint(Input.mousePosition));
//        }
//        private void RotateTowards(Vector2 target)
//        {
//            Vector2 direction = (target - (Vector2)hand.transform.position).normalized;
//            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

//            hand.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
//        }

//    }

//    public class FollowingCamera : MonoBehaviour
//    {
//        public static FollowingCamera instance;
//        target object
//        public Transform targetCharacterTransform;
//        smooth following duration
//        public float duration = 0.3f;

//        calculated camera velocity by SmoothDamp
//        private Vector3 velocity = Vector3.zero;
//        camera z position
//        private float zPos;


//        private void Awake()
//        {
//            if (instance != null)
//            {
//                Destroy(instance.gameObject);

//            }
//            instance = this;

//        }
//        private void Start()
//        {


//            init camera z position
//            zPos = transform.position.z;
//        }


//        void FixedUpdate()
//        {

//            if (targetCharacterTransform != null)
//            {
//                Vector3 targetPosition = targetCharacterTransform.TransformPoint(new Vector3(0, 0, -10));

//                Smoothly move the camera towards that target position
//                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, duration);
//                Define a target position above and behind the target transform
//            }


//        }
//    }

//    public class LobbyManager : MonoBehaviour
//    {


//        public static LobbyManager instance;
//        public GameObject dungeonEnteranceAskPanel;//?????????????? ???? ????
//        public Stack<GameObject> panelStack;
//        public GameObject consumptionItemShopPanel;
//        public CharacterController characterController;

//        public GameObject makeRoomPanel;
//        public TMP_InputField makeRoomNameInputField;
//        public GameObject waitingRoomPanel;



//        public TMP_Text serverStatusText;


//        public int connectedRoomUserCounter;



//        private void Awake()
//        {
//            if (FadeInOutManager.instance != null)
//            {
//                FadeInOutManager.instance.FadeIn();
//            }

//            panelStack = new Stack<GameObject>();
//            if (instance == null)
//            {
//                instance = this;
//            }
//            else
//            {
//                Destroy(gameObject);
//            }
//        }
//        void Start()
//        {
//            DataMangaer.instance.isInLobby = true;
//        }

//        Update is called once per frame
//        void Update()
//        {




//        }
//        public void AskEnterDungeon()
//        {
//            InGameUIManager.instance.PopUpPanel(dungeonEnteranceAskPanel);

//        }

//        public void ExitRoomEnterPanel()
//        {
//            dungeonEnteranceAskPanel.SetActive(false);

//        }

//        [PunRPC]
//        public void Ready()
//        {
//            if (PhotonNetwork.IsMasterClient == true)
//            {

//            }
//        }

//        public void PopUpWaitingRoomPanel()
//        {
//            if (waitingRoomPanel != null)
//            {
//                waitingRoomPanel.SetActive(true);
//            }
//            if (makeRoomPanel != null)
//            {
//                makeRoomPanel.SetActive(false);
//            }
//            if (dungeonEnteranceAskPanel != null)
//            {
//                dungeonEnteranceAskPanel.SetActive(false);

//            }

//        }

//        public void PopUpMakeRoomPanelButton()
//        {
//            makeRoomPanel.SetActive(true);
//        }
//        public void CloseMakeRoomPanelButton()
//        {
//            makeRoomPanel.SetActive(false);
//        }
//        public void MakeRoom()
//        {
//            if (!string.IsNullOrEmpty(makeRoomNameInputField.text))
//            {
//                NetworkManager.instance.makeRoomByName(makeRoomNameInputField.text);
//                makeRoomPanel.SetActive(false);
//            }

//        }
//        public void ClosePanel(GameObject panel)
//        {
//            panel.SetActive(false);
//        }
//        public void PopUpPanel(GameObject panel)
//        {
//            panel.SetActive(true);
//        }





//    }