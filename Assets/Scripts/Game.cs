using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
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

    static public int warnasama1;
    static public int warnasama2;
    static public int warnaplayer;
    static public int warnasistem;



    private Dictionary<int, List<int>> graph;
    private GameObject[] vertexnya;
    private GameObject[] edgenya;
    private Button theButton;
    private float timeAmt = 60;
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

    private float[] koorx = new float[6];
    private float[] koory = new float[6];

    // Use this for initialization
    void Start() {
        level = 1;
        time = timeAmt;

        diklik = -1;
        warnanya = Color.white;


        koorx[0] = -2.5F;
        koorx[1] = -1.5F;
        koorx[2] = -0.5F;
        koorx[3] = 0.5F;
        koorx[4] = 1.5F;
        koorx[5] = 2.5F;

        koory[0] = -0.9F;
        koory[1] = -0.4F;
        koory[2] = 0.1F;
        koory[3] = 0.6F;
        koory[4] = 1.1F;
        koory[5] = 1.7F;

        makelevel(1);
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
        UpdateLevel();
        int jumlahvertex = level + 2;
        if (jumlahvertex >= 25) jumlahvertex = 25;

        vertexnya = new GameObject[jumlahvertex];
        edgenya = new GameObject[3+(2*level-1)];
        
        graph = new Dictionary<int, List<int> >();

        List<pair> mypair = new List<pair>();
        List<int> counting = new List<int>();
        for (int i = 0; i < 25; i++) counting.Add(i);


        for (int i = 0; i < vertexnya.Length; i++)
        {
            int coba,temp2;
            coba = UnityEngine.Random.Range(0, counting.Count);
            temp2 = counting[coba];
            counting.Remove(temp2);

            int linex = temp2 % 5;
            int liney = temp2 / 5;


            spawnvalue = new Vector2(UnityEngine.Random.Range(koorx[linex], (koorx[linex+1]-0.2F)), UnityEngine.Random.Range(koory[liney], (koory[liney + 1] - 0.1F)));
            spawnrotation = new Quaternion(0, 0, 0, 0);
            vertexnya[i] = Instantiate(vertexku, spawnvalue, spawnrotation);
            vertexnya[i].SendMessage("SetNumber", i);
            graph[i] = new List<int>();
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
                if(graph[temp2.a].Count>=7 || graph[temp2.b].Count >= 7)
                {
                    mypair.Remove(temp2);
                }
                else break;
            }
            edgenya[i] = Instantiate(edgeku, spawnvalue, spawnrotation);
            Vector2 foo = vertexnya[temp2.a].GetComponent<Transform>().position;
            Vector2 bar = vertexnya[temp2.b].GetComponent<Transform>().position;

            graph[temp2.a].Add(temp2.b);
            graph[temp2.b].Add(temp2.a);

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
        else SceneManager.LoadScene("Game Over3");
    }

    public void Set_Warna(int diklik)
    {
        warnanya = tombolwarna[diklik].GetComponent<Image>().color;
    }

    public void CekBenar()
    {
        int ans=kuncijawaban();
        List<Color> terpakai = new List<Color>();

        bool flag = true;
        for (int i = 0; i < vertexnya.Length; i++)
        {
            for(int j=0; j<graph[i].Count; j++)
            {
                if(vertexnya[i].GetComponent<SpriteRenderer>().color == vertexnya[graph[i][j]].GetComponent<SpriteRenderer>().color)
                {
                    flag = false;
                    warnasama1 = i;
                    warnasama2 = graph[i][j];
                    break;
                }
            }
        }


        for (int i = 0; i < vertexnya.Length; i++)
        {
            Color temp3 = vertexnya[i].GetComponent<SpriteRenderer>().color;
            if (!terpakai.Contains(temp3))
            {
                terpakai.Add(temp3);
            }
        }

        warnaplayer = terpakai.Count;
        warnasistem = ans;

        Debug.Log("warna yang dipake " + terpakai.Count);

        for (int i = 0; i < vertexnya.Length; i++)
        {
            Destroy(vertexnya[i]);
        }
        for (int i = 0; i < edgenya.Length; i++)
        {
            Destroy(edgenya[i]);
        }

        if (flag == false ) SceneManager.LoadScene("Game Over");
        else if(ans < terpakai.Count) SceneManager.LoadScene("Game Over2");
        else
        {
            time += 20;
            if (time >= 180) time = 180;
            level++;
            makelevel(level);
        }
    }

    private int kuncijawaban()
    {
        pair[] arr=new pair[graph.Count];
        int[] dipake= new int[graph.Count];
        bool[] visit = new bool[graph.Count];
        int current = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = new pair(i, graph[i].Count);
            dipake[i] = 0;
            visit[i] = false;
        }

        Array.Sort<pair>(arr, (x, y) => y.b.CompareTo(x.b));
        for (int i = 0; i < arr.Length; i++)
        {
            Debug.Log(arr[i].a + " " + arr[i].b);
        }

        for(int i=0; i<arr.Length; i++)
        {
            if (visit[arr[i].a] == true) continue;
            else
            {
                current++;
                List<int> pakewarnaini = new List<int>();
                dipake[arr[i].a] = current;
                visit[arr[i].a] = true;
                pakewarnaini.Add(arr[i].a);

                for(int j=0; j<arr.Length; j++)
                {
                    bool hehe;
                    if (visit[arr[j].a] == true) continue;
                    else
                    {
                        hehe = true;
                        for(int k=0; k<pakewarnaini.Count; k++)
                        {
                            if(graph[pakewarnaini[k]].Contains(arr[j].a))
                            {
                                hehe = false;
                            }
                        }

                        if(hehe==true)
                        {
                            pakewarnaini.Add(arr[j].a);
                            dipake[arr[j].a] = current;
                            visit[arr[j].a] = true;
                        }
                    }
                }
            }

        }
        Debug.Log("current"+current);
        return current;
    }
}
