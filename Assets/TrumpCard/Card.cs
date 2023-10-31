using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Transform TFM;
    private MeshRenderer Num_L;//左上の番号
    private MeshRenderer Num_R;//右上の番号
    private MeshRenderer Suit_L;//左上のスート
    private MeshRenderer Suit_R;//右上のスート

    private Vector2[] Offsets = new Vector2[16]
    {
        new Vector2(0f,0f),
        new Vector2(0.25f,0f),
        new Vector2(0.5f,0f),
        new Vector2(0.75f,0f),
        new Vector2(0f,0.25f),
        new Vector2(0.25f,0.25f),
        new Vector2(0.5f,0.25f),
        new Vector2(0.75f,0.25f),
        new Vector2(0f,0.5f),
        new Vector2(0.25f,0.5f),
        new Vector2(0.5f,0.5f),
        new Vector2(0.75f,0.5f),
        new Vector2(0f,0.75f),
        new Vector2(0.25f,0.75f),
        new Vector2(0.5f,0.75f),
        new Vector2(0.75f,0.75f)

    };
    //四つのスートのオフセット
    private Vector2[] SuiteOffsets = new Vector2[4]
    {
        new Vector2(0f,0.25f),
        new Vector2(0.25f,0.25f),
        new Vector2(0.5f,0.25f),
        new Vector2(0.75f,0.25f)
    };
    //十一個のスートのリスト
    public List<MeshRenderer> SuitRender = new List<MeshRenderer>();

    public enum SuiteMenu
    {
        SPADE, HEART, CLUB, DAIYA
    };
    public SuiteMenu SuiteOrder = SuiteMenu.SPADE;
    public enum NumMenu
    {
        Joker, Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Qween, King
    };
    public NumMenu NumOrder = NumMenu.Ace;
    //直前のスートと番号を記憶する変数
    private int OldSuit = 0;
    private int OldNum = 0;
    //
    private int[][] SuitePos = new int[14][]
    {
        new int[]{9},//Joker
        new int[]{9},//Ace
        new int[]{8,10},//Two
        new int[]{8,9,10},//Three
        new int[]{0,3,4,7},//Four
        new int[]{0,3,4,7,9},//Five
        new int[]{0,1,3,4,5,7},//Six
        new int[]{0,3,4,7,8,9,10},//Seven
        new int[]{0,1,2,3,4,5,6,7},//Eight
        new int[]{0,1,2,3,4,5,6,7,9},//Nine
        new int[]{0,1,2,3,4,5,6,7,8,10},//Ten
        new int[]{8,10},//Jack
        new int[]{8,10},//Qween
        new int[]{8,10}//King
    };

    void Awake()
    {
        TFM = GetComponent<Transform>();
        foreach (MeshRenderer mrd in TFM.GetComponentsInChildren<MeshRenderer>())
        {
            if (mrd.name == "Num_L")
            {
                //左上の番号
                Num_L = mrd;
            }
            if (mrd.name == "Num_R")
            {
                //右上の番号
                Num_R = mrd;
            }
            if (mrd.name == "Suit_L")
            {
                //左上のスート
                Suit_L = mrd;
            }
            if (mrd.name == "Suit_R")
            {
                //右上のスート
                Suit_R = mrd;
            }
            //Suit00～10はまとめてリストに格納
            if (mrd.name.Contains("Suit") && !mrd.name.Contains("_L") && !mrd.name.Contains("_R"))
            {
                SuitRender.Add(mrd);

                //一旦スートを非表示に
                mrd.enabled = false;
            }
        }

        //OldNum = (int)NumOrder;
        //OldSuit = (int)SuiteOrder;
        //SuitChange((int)SuiteOrder, (int)NumOrder);
    }

    void Update()
    {
        /*
        if (OldNum != (int)NumOrder || OldSuit != (int)SuiteOrder)
        {
            SuitChange((int)SuiteOrder, (int)NumOrder);
            OldNum = (int)NumOrder;
            OldSuit = (int)SuiteOrder;
        }
        */
    }
    public void SuitChange(int st, int num)
    {
        st = Mathf.Clamp(st, 0, 3);//スートは四つ分
        num = Mathf.Clamp(num, 0, 13);//番号は0(Joker)～13まで
        //11個のスートの表示
        int i = 0;
        foreach (MeshRenderer mrd in SuitRender)
        {
            if (num > 0 && num <= 10)
            {
                //数字が1～10のとき、スート用のオフセット引数st
                mrd.material.SetTextureOffset("_MainTex", SuiteOffsets[st]);
            }
            else
            {
                //数字が0(Joker)か11～13のとき、番号用の引数num
                mrd.material.SetTextureOffset("_MainTex", Offsets[num]);
            }
            //スートがひとつのときは大きく表示   //論理演算子（条件　？　true時の処理　：　false時の処理）
            mrd.transform.localScale = num <= 1 ? new Vector3(1.0f, 1.0f, 1.0f) : num >= 11 ? new Vector3(0.75f, 0.75f, 1.0f) : new Vector3(0.25f, 0.25f, 1.0f);
            foreach (int pos in SuitePos[num])
            {
                if (i == pos)
                {
                    //現在の番号で表示するべきスートなら
                    mrd.enabled |= true;
                    break;
                }
            }
            i++;
        }
        //左上・右下の番号の表示
        if (num == 0)
        {
            //Jokerのとき
            Num_L.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.75f));
            Num_R.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.75f));
            Num_L.material.color = Color.red;
            Num_R.material.color = Color.red;
        }
        else
        {
            //1～13のとき
            Num_L.material.SetTextureOffset("_MainTex", Offsets[num]);
            Num_R.material.SetTextureOffset("_MainTex", Offsets[num]);

            //論理演算子（条件　？　true時の処理　：　false時の処理）
            Num_L.material.color = st % 2 == 0 ? Color.black : Color.red;
            Num_R.material.color = st % 2 == 0 ? Color.black : Color.red;
        }
        //右上・左上のスートの表示
        if (num == 0)
        {
            //Jokerのとき
            Suit_L.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.75f));
            Suit_R.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.75f));
        }
        else
        {
            //1～13
            Suit_L.material.SetTextureOffset("_MainTex", SuiteOffsets[st]);
            Suit_R.material.SetTextureOffset("_MainTex", SuiteOffsets[st]);
        }
    }
}
