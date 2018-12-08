using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {



    public int            GenerateCount    { get; set; }
    public float          GenerateStep     { get; set; }
    public GameObject     GenerateItem     { get; set; }
    public GameController GameController   { get; set; }


    public Vector3 Generate(int panelID,Vector3 prev)
    {
        //for(int i = 0; i< GenerateCount; i++)
        {
           GameController.AddPanel(Instantiate(GenerateItem, prev + transform.forward * GenerateStep, Quaternion.identity));
           return prev + transform.forward * GenerateStep;
        }
    }


	// Use this for initialization
	void Start ()
    {
	}
	
	
}
