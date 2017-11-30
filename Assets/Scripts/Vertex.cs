using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Vertex : MonoBehaviour {

    private SpriteRenderer mysprite;

    Vertex(Color colors)
    {
        mysprite.color= colors;
    }


	// Use this for initialization
	void Start () {
        //mysprite = GetComponent<SpriteRenderer>();
        //mysprite.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void SetColor(Color colors)
    {
        //mysprite.color = Color.yellow;
        this.GetComponent<SpriteRenderer>().color = colors;
    }

}
