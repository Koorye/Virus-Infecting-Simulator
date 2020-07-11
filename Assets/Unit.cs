using UnityEngine;

public class Unit : MonoBehaviour
{
    private bool dynamic_flag;
    private Rigidbody rid;
    private int status;
    private float speed, dynamic_time, virus_time, death_time;
    private Vector3 dir;
    
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < 1000)
        {
            speed = UISprite.dynamic_degree;
            dir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));

            dynamic_time = 0f;
            virus_time = 0f;
            death_time = 0f;

            if (Random.Range(0, 100) == 0)
            {
                status = 1;
                ++UISprite.mild_num;
            }
            else
            {
                status = 0;
                ++UISprite.healthy_num;
            }
        }
        else
            status = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 1000)
        {
            if (dynamic_flag)
                GetComponent<Rigidbody>().velocity= speed * dir;
            else
                GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

            if (status != 0)
                virus_time += Time.deltaTime;

            if (virus_time > UISprite.virus_incubation_time && status < 3)
            {
                ++status;
                virus_time = 0f;
                if (status == 2)
                {
                    --UISprite.mild_num;
                    ++UISprite.moderate_num;
                }
                else if (status == 3)
                {
                    --UISprite.moderate_num;
                    ++UISprite.severe_num;
                }
            }

            if (status == 0)
                GetComponent<MeshRenderer>().material.color = Color.green;
            else if (status == 1)
                GetComponent<MeshRenderer>().material.color = Color.blue;
            else if (status == 2)
                GetComponent<MeshRenderer>().material.color = Color.yellow;
            else if (status == 3)
                GetComponent<MeshRenderer>().material.color = Color.red;

            dynamic_time += Time.deltaTime;
            if (dynamic_time >= 1f)
            {
                if (Random.Range(0f, 1f) <= UISprite.dynamic_rate)
                    dynamic_flag = true;
                else
                    dynamic_flag = false;
                if (status == 2 && UISprite.temprature_test &&
                    Random.Range(0f, 1f) <= UISprite.temprature_test_rate)
                {
                    if (UISprite.hospital_num < UISprite.hospital_max_num)
                    {
                        Destroy(gameObject);
                        --UISprite.moderate_num;
                        ++UISprite.hospital_num;
                    }
                }
                
                else if (status == 3)
                {
                    if (UISprite.hospital_num < UISprite.hospital_max_num)
                    {
                        Destroy(gameObject);
                        --UISprite.severe_num;
                        ++UISprite.hospital_num;
                    }
                    else if (Random.Range(0f, 1f) <= UISprite.virus_vulnerability)
                    {
                        Destroy(gameObject);
                        ++UISprite.death_num;
                        --UISprite.severe_num;
                    }
                }
            }   
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.transform.CompareTag("Player") && Random.Range(0f,1f)<UISprite.virus_infectivity)
        {
            dir=new Vector3(Random.Range(-1f,1f),0f,Random.Range(-1f,1f));
            
            int other_status = other.transform.GetComponent<Unit>().status;
            if (status == 0 && other_status != 0)
            {
                ++status;
                ++UISprite.mild_num;
                --UISprite.healthy_num;
            }
        }
    }
    
}
