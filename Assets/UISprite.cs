using UnityEngine;
using UnityEngine.UI;

public class UISprite : MonoBehaviour
{
    private GameObject Unit;
    private Text Static;
    private Button Building, Restrict, TempratureTest;

    public static bool temprature_test;
    
    public static int healthy_num, mild_num, 
        moderate_num, severe_num, death_num, 
        hospital_num, hospital_max_num, day;

    public static float dynamic_degree, virus_incubation_time,
        virus_vulnerability, virus_infectivity,
        cure_rate, dynamic_rate, temprature_test_rate;

    private float time;

    private string temprature;
    // Start is called before the first frame update
    void Start()
    {
        dynamic_degree = 1f;//人群活跃程度，数值越大移动越快
        dynamic_rate = 0.8f;//人群活动概率，数值越大人群每天会移动的概率越大（单位：%）
        virus_incubation_time = 5f;//病毒潜伏期，数值越大病毒潜伏越久(单位：天）
        virus_vulnerability = 0.1f;//病毒致命性，数值越大每天死亡率越高（单位：%）
        virus_infectivity = 0.1f;//病毒传染性，数值越大接触传染概率越大（单位：%）
        cure_rate = 0.2f;//治愈率，数值越大每天越有可能治愈（单位：%）
        hospital_max_num = 200;//医院病床数量
        temprature_test = false;//是否进行体温检测
        
        healthy_num = -1;//健康人数（绿色）
        mild_num = 0;//轻度感染人数（蓝色）
        moderate_num = 0;//中度感染人数（黄色）
        severe_num = 0;//重度感染人数（红色）
        hospital_num = 0;//治疗中人数
        death_num = 0;
        temprature_test_rate = 0.5f;//体温检测发现概率（单位：%）
        day = 0;
        time = 0;

        Static = GameObject.Find("Canvas/Static").transform.GetComponent<Text>();
        Unit = GameObject.Find("UnitController/Unit");
        Building = GameObject.Find("Canvas/BuildingButton").transform.GetComponent<Button>();
        Restrict = GameObject.Find("Canvas/RestrictButton").transform.GetComponent<Button>();
        TempratureTest = GameObject.Find("Canvas/TempratureTestButton").transform.GetComponent<Button>();
        Building.onClick.AddListener(delegate { OnBuilding(); });
        Restrict.onClick.AddListener(delegate { OnRestrict(); });
        TempratureTest.onClick.AddListener(delegate { OnTempratureTest(); });
    }

    void OnBuilding()
    {
        hospital_max_num += 100;
    }

    void OnRestrict()
    {
        if (dynamic_rate > 0f)
            dynamic_rate -= 0.1f;
    }

    void OnTempratureTest()
    {
        if (temprature_test == false)
            temprature_test = true;
        else
            temprature_test = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            ++day;
            time = 0;
            
            for(int i=0;i<hospital_num;++i)
                if (Random.Range(0f, 1f) <= cure_rate)
                {
                    --hospital_num;
                    Instantiate(Unit, 
                        new Vector3(Random.Range(-40f,40f),0,Random.Range(-40f,40f)),
                        new Quaternion(0, 0, 0, 0));
                }
        }

        if (temprature_test)
            temprature = "yes";
        else
            temprature = "no";

        Static.text = "Day: " + day + "\nHealthy: " + healthy_num
                      + "\nMild: " + mild_num + "\nModerate: " + moderate_num
                      + "\nSevere: " + severe_num + "\nDeath: " + death_num
                      + "\n\nHospital: " + hospital_num
                      + "\nHospital Max Number: " + hospital_max_num
                      + "\n\nGoing Out Rate: " + string.Format("{0:F0}", dynamic_rate * 100) + "%"
                      + "\nTemprature Test: " + temprature;
    }
}
