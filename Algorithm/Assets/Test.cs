using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform uiContainer;

    public void Start()
    {
        //分词测试
        bool canSplit1 = new SplitWord("catgood", new List<string> { "good", "cat" }).canSplit();
        bool canSplit2 = new SplitWord("castlejoycastlecatjoy", new List<string> { "castle", "cat", "joy" }).canSplit();
        bool canSplit3 = new SplitWord("castlejoycastlecatjoya", new List<string> { "castle", "cat", "joy" }).canSplit();
        bool canSplit4 = new SplitWord("gogoodgogood", new List<string> { "good", "go" }).canSplit();


        //重叠矩形测试
        OverlapRect overlapRect = new OverlapRect(2, 2, 5);
        int overlapCount = overlapRect.OverlapCount();
        Debug.Log("oc:" + overlapCount);

        //绘制随机矩形，方便调试
        GameObject image = Resources.Load<GameObject>("Image");
        string a = "";
        for(int i = 0;i < overlapRect.rectCenters.Length;i++)
        {
            Position position = overlapRect.rectCenters[i];
            GameObject go = Instantiate(image);
            go.transform.SetParent(uiContainer);
            go.transform.position = new Vector3(position.x, position.y);
            go.name = (i+1).ToString();
            a += string.Format("({0},{1}),", position.x, position.y);
        }
        Debug.Log(a);
    }

}
