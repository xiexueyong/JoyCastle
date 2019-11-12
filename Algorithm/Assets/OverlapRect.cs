using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Position
{
    public Position(float x,float y)
    {
        this.x = x;
        this.y = y;
    }
    public float x;
    public float y;
}

public class OverlapRect
{
    //矩形的宽和高
    private int _rectWidth = 2;
    private int _rectHeight = 2;

    //矩形的数量
    private int _rectCount = 10000;
    //矩形的中心
    public Position[] rectCenters;

    //关系矩阵
    private int[,] overlapMatrix;

    public OverlapRect(int rectWidth, int rectHeight, int rectCount)
    {
        this._rectWidth = rectWidth;
        this._rectHeight = rectHeight;
        this._rectCount = rectCount;

        rectCenters = new Position[_rectCount];
        overlapMatrix = new int[_rectCount,_rectCount];

        //矩形容器的宽高
        int containerWidth = _rectWidth * (int)Math.Sqrt(_rectCount)*2;
        int containerHeight = _rectHeight * (int)Math.Sqrt(_rectCount)*2;
        //随机矩形的位置
        for (int i = 0;i<_rectCount;i++)
        {
            float x = UnityEngine.Random.Range(_rectWidth / 2, containerWidth - _rectWidth / 2);
            float y = UnityEngine.Random.Range(_rectHeight / 2, containerHeight - _rectHeight / 2);
            rectCenters[i] = new Position(x,y);
        }
        //用于测试的位置
        //rectCenters = new Position[] { new Position(5, 6), new Position(2,2), new Position(1,3), new Position(3,4), new Position(6, 6) };

        //根据覆盖关系填充矩阵
        for (int i = 0; i < _rectCount; i++)
        {
            for (int k = i + 1; k < _rectCount; k++)
            {
                int overlapValue = overlap(rectCenters[i], rectCenters[k]) ? 1 : 0;
                overlapMatrix[i, k] = overlapMatrix[k, i] = overlapValue;
            }
        }
    }

    //获取重叠矩形的数量
    public int OverlapCount()
    {
        int c = 0;
        string matrixStr = "";
        for (int i = 0; i < _rectCount; i++)
        {
           
            bool isOverlap = false;
            for (int k = 0; k < _rectCount; k++)
            {
                matrixStr += overlapMatrix[i, k].ToString();
                if (overlapMatrix[i, k] > 0)
                    isOverlap = true;
            }
            matrixStr += "\n";

            if (isOverlap)
                c++;
        }
        Debug.Log(matrixStr);
        return c;
    }


    //判断矩形是否重叠，未考虑矩形点旋转情况
    private bool overlap(Position rectA, Position rectB)
    {
        Position rectATopLeft = new Position(rectA.x - this._rectWidth / 2, rectA.y + this._rectHeight / 2);
        Position rectABottomRight = new Position(rectA.x + this._rectWidth / 2, rectA.y - this._rectHeight / 2);

        Position rectBTopLeft = new Position(rectB.x - this._rectWidth / 2, rectB.y + this._rectHeight / 2);
        Position rectBBottomRight = new Position(rectB.x + this._rectWidth / 2, rectB.y - this._rectHeight / 2);

        if(rectBBottomRight.y >= rectATopLeft.y || rectBTopLeft.y <= rectABottomRight.y || rectBBottomRight.x <= rectATopLeft.x || rectBTopLeft.x >= rectABottomRight.x)
        {
            return false;
        }

        return true;
    }
}