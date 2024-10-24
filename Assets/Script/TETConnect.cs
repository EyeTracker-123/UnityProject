using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text;
using System.IO;
using UnityEngine;
using System;

public class TETConnect : MonoBehaviour
{
    //データ格納用クラス
    [System.Serializable]
    public class raw
    {
        public float x = 0;
        public float y = 0;
    }

    [System.Serializable]
    public class pcenter
    {
        public float x = 0;
        public float y = 0;
    }

    [System.Serializable]
    public class avg
    {
        public float x = 0;
        public float y = 0;
    }

    [System.Serializable]
    public class lefteye
    {
        public avg avg = null;
        public pcenter pcenter = null;
        public float psize = 0;
        public raw raw = null;
    }

    [System.Serializable]
    public class righteye
    {
        public avg avg = null;
        public pcenter pcenter = null;
        public float psize = 0;
        public raw raw = null;
    }

    [System.Serializable]
    public class frame
    {
        public avg avg = null;
        public bool fix = false;
        public lefteye lefteye = null;
        public raw raw = null;
        public righteye righteye = null;
        public int state = 0;
        public int time = 0;
        public string timestamp = null;
    }

    [System.Serializable]
    public class values
    {
        public frame frame = null;
    }

    [System.Serializable]
    public class TETData
    {
        public string category = null;
        public string request = null;
        public int statuscode = 0;
        public values values = null;
    }
    private Thread thread;
    private ThreadStart ts;
    private bool keepOnRunning = true;
    private TcpClient client;
    private Stream stream;
    private int bufferSize = 4096;
    private int ThreadSleepTime = 5;
    private byte[] buffer;


    public TETData TET;
    



    public TETConnect() {
      
    }

    public void Connect()
    {
        client = new TcpClient("127.0.0.1", 6555);// -> Thee EyeTribe server
        stream = client.GetStream();
        buffer = new byte[bufferSize];
        byte[] writeBuffer = Encoding.ASCII.GetBytes(@"{""enableRawOutput"": true, ""format"": ""Json""}");
        stream.Write(writeBuffer, 0, writeBuffer.Length);
        while (keepOnRunning)
        {
            /////main loop
            ParseData();
            Thread.Sleep(ThreadSleepTime);
        }
        Disconnect();
    }

    private void ParseData()
    {
        if (stream.CanRead)
        {
            try
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string packet = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                StringReader Rdr = new StringReader(packet);

                /////////////
                //Debug.Log(packet);
                /////////////

                while (true)
                {
                    string dataLine = Rdr.ReadLine();
                    if (dataLine != null)
                    {
                        ///do something here 
                        if( dataLine.Contains("frame")){
                            TET = JsonUtility.FromJson<TETData>(dataLine);
                            //Debug.Log( TET.values.frame.lefteye.avg.x );
                            //Debug.Log( TET.values.Frame.avrg.x );
                        }

                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (IOException e)
            {
                Debug.Log("IOException: " + e);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ts = new ThreadStart(Connect);
        thread = new Thread(ts);
        thread.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        keepOnRunning = false;
    }
    private void OnDisable()
    {
        keepOnRunning = false;
    }
    private void OnApplicationQuit()
    {
        keepOnRunning = false;
    }
    private void Disconnect()
    {
        Task.Delay(ThreadSleepTime * 2);//just in case 
        stream.Close();
        thread.Abort();//just in case 
    }

}