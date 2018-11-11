﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour {

    private GameObject[] stack;
    private int score=0;
    private int j=0, k;
    float X=0,x,Z=0,i;
    public int topIndex;
    private int speed = 3;
    private int top;
    private float tileTransition=0.0f;
   

	void Start () {
        stack = new GameObject[transform.childCount];
        
        for (j = 0; j < transform.childCount; j++)
        {
        stack[j] = transform.GetChild(j).gameObject;
        }

        k = (int)(stack[transform.childCount-1 ].transform.position.y);
        i = (float)(stack[transform.childCount-2].transform.position.x);
        topIndex = 0;
        top = transform.childCount;
    }
	

	void Update () {

        moveTile();

        if (Input.GetKeyDown("space"))
        {
            splitTile();
            spawnTile();

           
            score++;
        }
	}
    private void splitTile()
    {
        float nx = 0,xdiff=0;
        if(top>=2)
        {
            xdiff = stack[top - 1].transform.localPosition.x - stack[top - 2].transform.localPosition.x;
        }
        else if (top >= 1)
        {
            xdiff = stack[top - 1].transform.localPosition.x - stack[transform.childCount - 1].transform.localPosition.x;
        }
        else if (top <= 0)
        {
            xdiff = stack[transform.childCount - 1].transform.localPosition.x - stack[transform.childCount - 2].transform.localPosition.x;
        }
        float sc = stack[transform.childCount - 1].transform.localScale.x - Mathf.Abs(xdiff);
        stack[top - 1].transform.localScale = new Vector3(sc, 1, 1);
        nx = stack[top - 1].transform.localPosition.x-(xdiff / 2f);
        X = nx;
        stack[top - 1].transform.localPosition = new Vector3(nx, stack[top - 1].transform.localPosition.y, stack[top - 1].transform.localPosition.z);
                     
    }

    private void spawnTile() {
        
        k++;
        stack[topIndex].transform.localPosition = new Vector3(0, k, 0);
       
        topIndex++;
        if (top == transform.childCount)
        {
            top = 0;
        }
      
        if(topIndex==transform.childCount)
        {
            topIndex = 0;
        }
        top++;

    }
    private void moveTile()
       
    {
        tileTransition += speed * Time.deltaTime;
     
      if ((top - 1) % 2 != 0 || top == transform.childCount)
            stack[top - 1].transform.localPosition = new Vector3(Mathf.Sin(tileTransition), k, 0);
     else
            stack[top - 1].transform.localPosition = new Vector3(X, k, Mathf.Sin(tileTransition));

    }
}
