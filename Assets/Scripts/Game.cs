using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;


public class Game : MonoBehaviour {
    struct pair
    {
        private int m_a;
        private int m_b;
        public int a
        {
            get { return m_a; }
            set { m_a = value; }
        }
        public int b
        {
            get { return m_b; }
            set { m_b = value; }
        }
        public pair(int a, int b)
        {
            m_a = a;
            m_b = b;
        }
    }


    public GameObject vertexku;
    public GameObject edgeku;
    public Vector2 spawnvalue;
    public Quaternion spawnrotation;
    public GameObject[] tombolwarna;


    private GameObject[] vertexnya;
    private GameObject[] edgenya;
    private Button theButton;
    private float timeAmt = 10;
    private float time;


    private Color warnanya;
    int diklik;


    public int level;
    public Text timertext;
    public Text leveltext;
    // private bool gameover;


    //utility
    private float xmax = 2.5F;
    private float xmin = -2.5F;
    private float ymax = 1.7F;
    private float ymin = -0.9F;


    // Use this for initialization
    void Start() {
        level = 1;
        time = timeAmt;

        diklik = -1;
        warnanya = Color.white;
        makelevel(1);
        UpdateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics2D.Raycast(ray.origin, ray.direction))
            {
                for (int i = 0; i < vertexnya.Length; i++)
                {
                    if (hit.collider.gameObject == vertexnya[i])
                    {
                        vertexnya[i].SendMessage("SetColor", warnanya);
                    }
                }
            }

        }
    }


    void makelevel(int level)
    {
        vertexnya = new GameObject[level + 2];
        edgenya = new GameObject[3];
        List<pair> mypair = new List<pair>();

        for (int i = 0; i < vertexnya.Length; i++)
        {
            spawnvalue = new Vector2(UnityEngine.Random.Range(xmin, xmax), UnityEngine.Random.Range(ymin, ymax));
            spawnrotation = new Quaternion(0, 0, 0, 0);
            vertexnya[i] = Instantiate(vertexku, spawnvalue, spawnrotation);
            vertexnya[i].SendMessage("SetNumber", i);
        }
        spawnvalue = new Vector2(0, 0);
        spawnrotation = new Quaternion(0, 0, 0, 0);
        int u, v;
        for (int i = 0; i < vertexnya.Length - 1; i++)
        {
            for (int j = i + 1; j < vertexnya.Length; j++)
            {
                u = i;
                v = j;
                pair temp = new pair(u, v);
                mypair.Add(temp);
            }
        }


        for (int i = 0; i < edgenya.Length; i++)
        {
            pair temp2 = new pair();
            int coba;
            while (true)
            {
                coba = UnityEngine.Random.Range(0, mypair.Count);
                temp2 = mypair[coba];

                //kasih if if gajelas
                break;
            }
            edgenya[i] = Instantiate(edgeku, spawnvalue, spawnrotation);
            Vector2 foo = vertexnya[temp2.a].GetComponent<Transform>().position;
            Vector2 bar = vertexnya[temp2.b].GetComponent<Transform>().position;

            edgenya[i].GetComponent<LineRenderer>().SetPosition(0, foo);
            edgenya[i].GetComponent<LineRenderer>().SetPosition(1, bar);
            mypair.Remove(temp2);
        }




    }


    public void UpdateLevel()
    {
        leveltext.text = "Level: " + level;
    }



    public void UpdateTime()
    {
        if (time >= 0)
        {
            time -= Time.deltaTime;
            timertext.text = "Time " + time.ToString("F");
        }
        else SceneManager.LoadScene("Game Over");
    }

    public void Set_Warna(int diklik)
    {
        warnanya = tombolwarna[diklik].GetComponent<Image>().color;
    }

    public void CekBenar()
    {
        //disini cek benar gaknya jawaban player
        for (int i = 0; i < vertexnya.Length; i++)
        {
            Destroy(vertexnya[i]);
        }
        for (int i = 0; i < edgenya.Length; i++)
        {
            Destroy(edgenya[i]);
        }
    }

    private int kuncijawaban()
    {
        return 0;
    }
}
