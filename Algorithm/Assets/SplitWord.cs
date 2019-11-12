using System.Collections.Generic;
using UnityEngine;

//正向最大分词法
public class SplitWord
{
    //词集
    private List<string> _wordSet;
    //句子
    private string _sentence;
    //词集中最长单词的长度
    private int _maxSize;

    public SplitWord(string sentence,List<string> wordSet)
    {
        this._wordSet = wordSet;
        this._sentence = sentence;

        for (int i = 0; i < wordSet.Count; i++)
        {
            if (this._maxSize < wordSet[i].Length)
            {
                this._maxSize = wordSet[i].Length;
            }
        }
    }

   
    //判断是否可以分词
    public bool canSplit()
    {
        string debugStr = "";
        string word = string.Empty;
        for (int i = 0;i< _sentence.Length;i++)
        {
            word = getNextWord(i);
            debugStr += word + ",";
            if (string.IsNullOrEmpty(word))
            {
                Debug.Log("False: "+debugStr);
                return false;
            }
            i = i + word.Length-1;
        }
        Debug.Log("True: "+debugStr);
        return true;
    }

    //获取下一个分词
    private string getNextWord(int startIndex)
    {
        string nw = string.Empty;
        int wordSize = startIndex + this._maxSize > this._sentence.Length ? this._sentence.Length -startIndex: this._maxSize;
        for (int i= wordSize; i>0;i--)
        {
            nw = this._sentence.Substring(startIndex, i);
            if (this._wordSet.Contains(nw))
            {
                return nw;
            }
        }
        return null;
    }



}

