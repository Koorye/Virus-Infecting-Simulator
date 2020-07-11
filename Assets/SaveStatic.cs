using UnityEngine;
using System.IO;

public class SaveStatic : MonoBehaviour
{
    private float time;
    StreamWriter sw;
    static string stringpath ="D:/Static.txt";
    private string tmp;
    FileInfo fi=new FileInfo(stringpath);
    // Start is called before the first frame update
    void Start()
    {
        if (!fi.Exists)
            sw = fi.CreateText();
        else
        {
            fi.Delete();
            sw = fi.CreateText();
        }

        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 1f)
        {
            time = 0f;
            if (UISprite.mild_num <= 0 && UISprite.moderate_num <= 0
                                       && UISprite.severe_num <= 0 
                                       && UISprite.hospital_num <= 0)



            {
                sw.Close();
                sw.Dispose();

            }
            else
            {
                tmp = UISprite.day.ToString() + ","
                                              + UISprite.healthy_num.ToString() + ","
                                              + UISprite.mild_num.ToString() + ","
                                              + UISprite.moderate_num.ToString() + ","
                                              + UISprite.severe_num.ToString() + ","
                                              + UISprite.hospital_num.ToString() + ","
                                              + UISprite.death_num.ToString() + "\n";
                sw.Write(tmp);
            }
        }
    }
}
