using System.Collections;
using UnityEngine;

public class CardMaster : MonoBehaviour
{

    public Card CardOrg;//�I���W�i���̃J�[�h
    private float CardWidth;//�J�[�h�̉���
    private int TOTAL = 0;//����������
    //
    //�X�[�g���w�肷��񋓌^�z��
    public enum SuiteMenu
    {
        SPADE, HEART, CLUB, DAIYA
    };
    public SuiteMenu SuiteOrder = SuiteMenu.SPADE;
    //�X�[�g�̖��O
    private string[] SuiteName = new string[4]
    {
        "SPADE","HEART","CLUB","DAIYA"
    };
    //�ԍ����w�肷��񋓌^�z��
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
            //�I���W�i���J�[�h���\����
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
        //���������J�[�h�𖽖�
        NewCardObj.name = "Card_" + (num == 0 ? "" : SuiteName[st]) + NumName[num];
        //���������J�[�h�����ɂ��炵�Ĕz�u
        NewCardObj.transform.position += new Vector3(CardWidth * 1.2f * TOTAL, 0, 0);
        //���������J�[�h�̃X�N���v�g���擾
        Card NewCard = NewCardObj.GetComponent<Card>();
        //���������J�[�h�̃X�[�g�Ɣԍ���ύX
        NewCard.SuitChange(st, num);
        //���������������Z
        TOTAL++;
        yield return null;

    }
}
