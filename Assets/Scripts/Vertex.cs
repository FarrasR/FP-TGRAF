using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Vertex : MonoBehaviour {

    private SpriteRenderer mysprite;
    public TextMesh numbering;
    

    Vertex(Color colors)
    {
        mysprite.color= colors;
    }

    
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetNumber(int number)
    {
        numbering.GetComponent<TextMesh>().text = number.ToString();
    }
    
    void SetColor(Color colors)
    {
        this.GetComponent<SpriteRenderer>().color = colors;
    }

}
