using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Transform TFM;
    private MeshRenderer Num_L;//����̔ԍ�
    private MeshRenderer Num_R;//�E��̔ԍ�
    private MeshRenderer Suit_L;//����̃X�[�g
    private MeshRenderer Suit_R;//�E��̃X�[�g

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
    //�l�̃X�[�g�̃I�t�Z�b�g
    private Vector2[] SuiteOffsets = new Vector2[4]
    {
        new Vector2(0f,0.25f),
        new Vector2(0.25f,0.25f),
        new Vector2(0.5f,0.25f),
        new Vector2(0.75f,0.25f)
    };
    //�\��̃X�[�g�̃��X�g
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
    //���O�̃X�[�g�Ɣԍ����L������ϐ�
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
                //����̔ԍ�
                Num_L = mrd;
            }
            if (mrd.name == "Num_R")
            {
                //�E��̔ԍ�
                Num_R = mrd;
            }
            if (mrd.name == "Suit_L")
            {
                //����̃X�[�g
                Suit_L = mrd;
            }
            if (mrd.name == "Suit_R")
            {
                //�E��̃X�[�g
                Suit_R = mrd;
            }
            //Suit00�`10�͂܂Ƃ߂ă��X�g�Ɋi�[
            if (mrd.name.Contains("Suit") && !mrd.name.Contains("_L") && !mrd.name.Contains("_R"))
            {
                SuitRender.Add(mrd);

                //��U�X�[�g���\����
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
        st = Mathf.Clamp(st, 0, 3);//�X�[�g�͎l��
        num = Mathf.Clamp(num, 0, 13);//�ԍ���0(Joker)�`13�܂�
        //11�̃X�[�g�̕\��
        int i = 0;
        foreach (MeshRenderer mrd in SuitRender)
        {
            if (num > 0 && num <= 10)
            {
                //������1�`10�̂Ƃ��A�X�[�g�p�̃I�t�Z�b�g����st
                mrd.material.SetTextureOffset("_MainTex", SuiteOffsets[st]);
            }
            else
            {
                //������0(Joker)��11�`13�̂Ƃ��A�ԍ��p�̈���num
                mrd.material.SetTextureOffset("_MainTex", Offsets[num]);
            }
            //�X�[�g���ЂƂ̂Ƃ��͑傫���\��   //�_�����Z�q�i�����@�H�@true���̏����@�F�@false���̏����j
            mrd.transform.localScale = num <= 1 ? new Vector3(1.0f, 1.0f, 1.0f) : num >= 11 ? new Vector3(0.75f, 0.75f, 1.0f) : new Vector3(0.25f, 0.25f, 1.0f);
            foreach (int pos in SuitePos[num])
            {
                if (i == pos)
                {
                    //���݂̔ԍ��ŕ\������ׂ��X�[�g�Ȃ�
                    mrd.enabled |= true;
                    break;
                }
            }
            i++;
        }
        //����E�E���̔ԍ��̕\��
        if (num == 0)
        {
            //Joker�̂Ƃ�
            Num_L.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.75f));
            Num_R.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.75f));
            Num_L.material.color = Color.red;
            Num_R.material.color = Color.red;
        }
        else
        {
            //1�`13�̂Ƃ�
            Num_L.material.SetTextureOffset("_MainTex", Offsets[num]);
            Num_R.material.SetTextureOffset("_MainTex", Offsets[num]);

            //�_�����Z�q�i�����@�H�@true���̏����@�F�@false���̏����j
            Num_L.material.color = st % 2 == 0 ? Color.black : Color.red;
            Num_R.material.color = st % 2 == 0 ? Color.black : Color.red;
        }
        //�E��E����̃X�[�g�̕\��
        if (num == 0)
        {
            //Joker�̂Ƃ�
            Suit_L.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.75f));
            Suit_R.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.75f));
        }
        else
        {
            //1�`13
            Suit_L.material.SetTextureOffset("_MainTex", SuiteOffsets[st]);
            Suit_R.material.SetTextureOffset("_MainTex", SuiteOffsets[st]);
        }
    }
}
