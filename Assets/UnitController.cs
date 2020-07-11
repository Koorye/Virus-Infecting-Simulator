using UnityEngine;

public class UnitController : MonoBehaviour
{
    private GameObject Unit;
    // Start is called before the first frame update
    void Start()
    {
        Unit = GameObject.Find("UnitController/Unit");
        for (int x = -40; x <= 40; x += 2)
        {
            for (int z = -40; z <= 40; z += 2)
            {
                Instantiate(Unit, new Vector3(x, 0, z),
                    new Quaternion(0, 0, 0, 0));
            }
        }
    }
    
}
