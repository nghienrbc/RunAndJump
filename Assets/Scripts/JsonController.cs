using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Lidar
{
    public string TimeStamp
    {
        get;
        set;
    }
    public string LidarPoint
    {
        get;
        set;
    }
}
public class JsonController : MonoBehaviour
{
    List<Lidar> lidarList;
    List<Lidar> lidarList1;
    // Start is called before the first frame update
    void Start()
    {
        lidarList = new List<Lidar>();
        lidarList1 = new List<Lidar>();
        for (int i = 0; i < 10; i++)
        { 
            Lidar lidarData = new Lidar()
            {
                // Roll number
                TimeStamp = "Alex",
                LidarPoint = "12346",
            };
            lidarList.Add(lidarData);
        }
        // convert to Json string by seralization of the instance of class.
        string stringjson = JsonConvert.SerializeObject(lidarList);
        lidarList1 = JsonConvert.DeserializeObject<List<Lidar>>(stringjson);
        //lidarList1 = (List<Lidar>)JsonConvert.DeserializeObject(stringjson);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
