using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour {

    private GameObject[] stack;
    private TextMesh dispscore;
    private int score=0,Flag=1,j=0, k,top;
    private float X=0,Z=0,scx=5,scz=5,i,speed = 2f,tileTransition=0.0f;
    public int topIndex;
    private bool gameFlag = true;
	void Start () {
        stack = new GameObject[transform.childCount];
        dispscore = new TextMesh();
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

        if (gameFlag)
        {
            if (score != 0 && Flag == 1 && score % 10 == 0)
            {
                speed += 0.75f;
                Flag = 0;
            }
            else if (score % 10 != 0)
                Flag = 1;
            moveTile();
            if (Input.GetKeyDown("space"))
            {
                splitTile();
                spawnTile();
                score++;
                gameFlag = endCheck();
                
                transform.position = new Vector3(transform.position.x, transform.position.y -1, transform.position.z);
                dispscore.text = string.Empty + score;
            }
        }
        else
            return;
       
	}
    private void splitTile()
    {
        float xdiff=0,zdiff=0;


            xdiff = stack[top - 1].transform.localPosition.x - X;
            zdiff = stack[top - 1].transform.localPosition.z - Z;

        scx = stack[top - 1].transform.localScale.x - Mathf.Abs(xdiff);
        scz = stack[top - 1].transform.localScale.z - Mathf.Abs(zdiff);
        stack[top - 1].transform.localScale = new Vector3(scx, 1, scz);
        X = stack[top - 1].transform.localPosition.x-(xdiff / 2f);
        Z = stack[top - 1].transform.localPosition.z - (zdiff / 2f);
        stack[top - 1].transform.localPosition = new Vector3(X, stack[top - 1].transform.localPosition.y, Z);
                     
    }

    private void spawnTile() {
        
        k++;
        stack[topIndex].transform.localPosition = new Vector3(X, k, Z);
        stack[topIndex].transform.localScale = new Vector3(scx, 1, scz);

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
            stack[top - 1].transform.localPosition = new Vector3(X+Mathf.Sin(tileTransition)*(scx+0.5f), k, Z);
     else
            stack[top - 1].transform.localPosition = new Vector3(X, k, Z+Mathf.Sin(tileTransition)*(scz+0.5f));

    }

    private bool endCheck()
    {
        if (scx <= 0.1 || scx <= 0.1)
            return false;
        if (stack[top - 1].transform.localPosition.x >= (X+scx) || stack[top - 1].transform.localPosition.x <= (X - scx)|| stack[top - 1].transform.localPosition.z >= (X+scz)|| stack[top - 1].transform.localPosition.z <= (X - scz))
            return false;
        return true;
    }
}
