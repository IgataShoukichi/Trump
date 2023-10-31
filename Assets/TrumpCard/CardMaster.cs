using System.Collections;
using UnityEngine;

public class CardMaster : MonoBehaviour
{

    public Card CardOrg;//オリジナルのカード
    private float CardWidth;//カードの横幅
    private int TOTAL = 0;//複製した数
    //
    //スートを指定する列挙型配列
    public enum SuiteMenu
    {
        SPADE, HEART, CLUB, DAIYA
    };
    public SuiteMenu SuiteOrder = SuiteMenu.SPADE;
    //スートの名前
    private string[] SuiteName = new string[4]
    {
        "SPADE","HEART","CLUB","DAIYA"
    };
    //番号を指定する列挙型配列
    public enum NumMenu
    {
        Joker, Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Qween, King
    };
    public NumMenu NumOrder = NumMenu.Ace;
    private string[] NumName = new string[14]
{
        "Joker", "Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Qween", "King"
};

    void Start()
    {
        if (CardOrg != null)
        {
            foreach (Transform tfm in CardOrg.transform.GetComponentsInChildren<Transform>())
            {
                if (tfm.name == "card")
                {
                    BoxCollider COL = tfm.gameObject.AddComponent<BoxCollider>();
                    CardWidth = COL.size.x * tfm.transform.localScale.x * CardOrg.transform.localScale.x;
                    Destroy(COL);
                    break;
                }
            }
            //オリジナルカードを非表示に
            CardOrg.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(MakeCard((int)SuiteOrder, (int)NumOrder));
        }
    }
    IEnumerator MakeCard(int st, int num)
    {
        GameObject NewCardObj = Instantiate(CardOrg.gameObject);
        NewCardObj.SetActive(true);
        //複製したカードを命名
        NewCardObj.name = "Card_" + (num == 0 ? "" : SuiteName[st]) + NumName[num];
        //複製したカードを横にずらして配置
        NewCardObj.transform.position += new Vector3(CardWidth * 1.2f * TOTAL, 0, 0);
        //複製したカードのスクリプトを取得
        Card NewCard = NewCardObj.GetComponent<Card>();
        //複製したカードのスートと番号を変更
        NewCard.SuitChange(st, num);
        //複製した数を加算
        TOTAL++;
        yield return null;

    }
}
