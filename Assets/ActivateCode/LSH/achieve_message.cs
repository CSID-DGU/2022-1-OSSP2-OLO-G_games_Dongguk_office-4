//using UnityEngine.UI;
//using UnityEngine;

//public class achieve_message : MonoBehaviour
//{
//    public Text message;
//    public GameObject character;
//    int kill = 0;
//    int respawn = 0;
//    int playtime = 0;
//    int enhancement = 0;
//    int gold = 0;
//    int hit = 0;

//    // Start is called before the first frame update
//    void Start()
//    {
//        SetText();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        SetText();
//    }
    
//    void SetText()
//    {
//        if (playtime == 0)
//            this.message.text = "Welcome to the new world!!";

//        else if (playtime == 1)
//            this.message.text = "Escape from beginner!";

//        else if (playtime == 6)
//            this.message.text = "Where is the Half Day?";

//        else if (playtime == 12)
//            this.message.text = "One's jet lag being changed.";

//        else if (playtime == 24)
//            this.message.text = "Why does Tomorrow come?";

//        else if (playtime == 24 * 30)
//            this.message.text = "Good Bye, 1 month";

//        if (kill == 10)
//            this.message.text = "Beginner Slayer!";

//        if (kill == 50)
//            this.message.text = "Where is monsters?";

//        if (kill == 100)
//            this.message.text = "I want to Stronger Monster!";

//        if (kill == 200)
//            this.message.text = "No where are monsters hunted...";

//        if (respawn == 1)
//            this.message.text = "New life!";

//        if (respawn == 10)
//            this.message.text = "I hate death";

//        if (respawn == 50)
//            this.message.text = "Too many respawned?";

//        if (respawn == 100)
//            this.message.text = "There is no bleeding";

//        if (enhancement == 1)
//            this.message.text = "A novice blacksmith";

//        if (enhancement == 5)
//            this.message.text = "A four-leaf clover";

//        if (enhancement == 50)
//            this.message.text = "A strong heart";

//        if (enhancement == 100)
//            this.message.text = "Lucky guy";

//        if (0 < gold && gold <= 50)
//            this.message.text = "Beggar";

//        if (50 < gold && gold <= 100)
//            this.message.text = "The middle class";

//        if (100 < gold && gold <= 1000)
//            this.message.text = "The rich";

//        if (1000 < gold)
//            this.message.text = "billionaire";

//        if (hit == 10)
//            this.message.text = "앗 따가워!";

//        if (hit == 50)
//            this.message.text = "멍든 세상";

//        if (hit == 100)
//            this.message.text = "더 맞을 곳이 있나?";

//        if (hit == 500)
//            this.message.text = "이젠 아무렇지도 않다...";


//    }

//}