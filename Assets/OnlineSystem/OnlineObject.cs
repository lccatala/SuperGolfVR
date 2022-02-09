using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;

public class OnlineObject : MonoBehaviour
{

    public static string username = "Luis";// Oculus.Platform.Users.GetLoggedInUser().ToString();
    public static int priority = 0;

    public int refreshRate = 5000;

    private static readonly Dictionary<string, GameObject> onlineObject = new Dictionary<string, GameObject>();
    private double posx, posy, posz;
    private double speedx, speedy, speedz;

    private Rigidbody rb;

    public GameObject ballPrefab;
    public static GameObject ballPrefabStatic;

    async void Start()
    {
        ballPrefabStatic = ballPrefab;
        rb = GetComponent<Rigidbody>();

        UpdateStatus();
    }


    void Update()
    {
        posx = transform.position.x;
        posy = transform.position.y;
        posz = transform.position.z;

        speedx = rb.velocity.x;
        speedy = rb.velocity.y;
        speedz = rb.velocity.z;

        WebsocketClient.SendData(EncondeData());
        DecodeData(WebsocketClient.GetData());
        WebsocketClient.GetData().Clear();
    }


    void UpdateStatus()
    {
        /*
        while (true)
        {
            //await Task.Run(() =>
            {
                
            });

            //await Task.Delay(refreshRate);
        }

        //return Task.Delay(refreshRate);
        */
    }

    static void DecodeData(List<string> data)
    {
        
        float px, py, pz;
        float sx, sy, sz;
        string n;
        int prior;
        
        foreach (string d in data)
        {
            string[] s = d.Split(';');

            prior = StringToInteger(s[0]);
            n = s[1];
            px = StringToFloat(s[2]);
            py = StringToFloat(s[3]);
            pz = StringToFloat(s[4]);
            sx = StringToFloat(s[5]);
            sy = StringToFloat(s[6]);
            sz = StringToFloat(s[7]);


            // If friend already registered
            if (!onlineObject.ContainsKey(n))
            {
                GameObject newBall = Instantiate(ballPrefabStatic, new Vector3(0, 0, 0), Quaternion.identity);
                newBall.layer = 7; // Multiplayer layer so it doesn't collide with the stick
                newBall.GetComponentInChildren<TextMesh>().text = n;
                onlineObject.Add(n, newBall);
            }
            Debug.Log(n);
            Debug.Log(onlineObject[n]);

            onlineObject[n].transform.position = new Vector3(px, py, pz);
            onlineObject[n].GetComponent<Rigidbody>().velocity = new Vector3(sx, sy, sz);

        }
    }


    string EncondeData()
    {
        return NumberToString(priority++) + ";" + username + ";"
            + NumberToString(posx) + ";" + NumberToString(posy) + ";" + NumberToString(posz) + ";"
            + NumberToString(speedx) + ";" + NumberToString(speedy) + ";" + NumberToString(speedz);
    }

    public static string NumberToString(int n)
    {
        return n.ToString(CultureInfo.InvariantCulture);
    }

    public static string NumberToString(double n)
    {
        return n.ToString(CultureInfo.InvariantCulture);
    }

    public static int StringToInteger(string s)
    {
        int res = 0;
        int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out res);
        return res;
    }

    public static float StringToFloat(string s)
    {
        float res = 0f;
        float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out res);
        return res;
    }
}
