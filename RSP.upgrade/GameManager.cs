using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Image, text legacy
using TMPro; //text Mesh pro
using UnityEngine.AI;
using System;


public class GameManager : MonoBehaviour
{
    //---------------- Variable Declare 
    //public Image _imgYou; //�� ���������� �̹���
    [SerializeField] private RawImage _imgYou;
    [SerializeField] private RawImage _imgCom; //��ǻ�� ���������� �̹���

    [SerializeField] private TMP_Text _txtYou; //�� �¸� Ƚ�� ǥ�ÿ�
    [SerializeField] private TMP_Text _txtCom; //��ǻ�� �¸� Ƚ��
    [SerializeField] private TMP_Text _txtResult; //���� ���
    [SerializeField] private TMP_Text _winPer; //�·� ǥ�ÿ�
    [SerializeField] private TMP_Text _drawCount; //���º� Ƚ�� ǥ�ÿ�

    int _totalGame = 0; //�÷��� Ƚ��
    int _cntYou = 0; //�� �¸� Ƚ��
    int _cntCom = 0; //��ǻ�� �¸� Ƚ��
    int _cntDraw = 0; //���º� Ƚ��

    public int _attackKey = 0; //���ݱ��� ������ 1�� ��.


    //----------------------------------------
    //---------------------------------- Method 
    void Start()
    {
        
    }

    private void Awake()
    {
        InitGame();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //���߿� ���ӵ� ��Ƽ� �ϳ��� ������Ʈ�� ������ ���� �ʿ�
            //Application.Quit();
        }

        //CalculateWinRate();
    }

    public void OnButtonClick(GameObject button)
    {
        _totalGame++;

        //Debug.Log(button.name);
        int you = int.Parse(button.name.Substring(7, 1)); //���� - ��ü 7���ڿ��� �� ���ڸ� �߶� ���� ������ �ٲ��ֱ�
        int com = UnityEngine.Random.Range(1, 4); //��ǻ�ʹ� - 1�̻� 4�̸��� ���� �� ���� ���ڸ� �̴´�.

        int res = CheckResult(you, com); //ù ���������� �ϰ� �� ����� �ҷ���
        //��:1, ��:-1, ��:0
        string str = "";

        if (_attackKey == 0) //������� �ٽ� �����ϰ�
        {
            _attackKey = 0;
            _txtResult.text = "DRAW~";
            str = "Draw. Choose again!";
        }
        else if (_attackKey == 1) //���� �̱�� ���ݱ��� ������
        { 
            if(you == com)
            {
                Debug.Log("you win");
                _cntYou++;
                _txtResult.text = "YOU WIN !!!";
                str = "WIN";
                _cntDraw++;
                CalculateWinRate(); //���� �̱� ��쿡 ���� �·��� ����ؼ� ����ϴ� �Լ� ȣ��
            }
        }
        else if (_attackKey == -1) //���� ������ ���ݱ��� ��ǻ�Ϳ���
        {
            if(you == com)
            {
                Debug.Log("you lose");
                _cntCom++;
                _txtResult.text = "YOU LOSE....OTL";
                str = "Lose";
            }
        }

        //SetResult(you, com);
        SetResult(you, com, str);
    }

    public void OnMouseExit(GameObject button)
    {
        Animator anim = button.GetComponent<Animator>();
        anim.Play("Normal");

    }

    //--------------------------------------
    //---------------- User-Defined Method

    //�ʱ�ȭ �Լ�
    void InitGame()
    {
        //(1) ����ó�� Gameobject.Find()�� �Ἥ ������ �� �Ҵ�
        //(2) ������ public���� �����ؼ� �ν����Ϳ��� ���� ������Ʈ �Ҵ�

        _txtResult.text = "Choose you Choice";
        _winPer.text = "Your Win Percentage is  " + 0;
    }


    //ù ���������� �º� ���� �Լ�
    int CheckResult(int you, int com)
    {
        int res = 0;

        //������ ���� �� �����ϰ� �ۼ��ϴ� �º� ����
        int k = you - com;
        switch (k)
        {
            //���� ��
            case -1: case 2:
                res = -1;
                _attackKey = -1;
                Debug.Log("���ݱ��� ��ǻ�Ϳ��� �����ϴ�.");
                break;
            //���� �̱�
            case -2: case 1:
                res = 1;
                _attackKey = 1;
                Debug.Log("���ݱ��� ���� �־������ϴ�.");
                break;
            //���
            case 0:
                res = 0;
                //_cntDraw++;
                //_attackKey = 0;
                Debug.Log("���ݱ��� �ƹ����Ե� ���� �ʾҽ��ϴ�.");
                break;
        }

        return res;
    }

    //���� ����� UI�� ǥ���ϴ� �Լ�
    void SetResult(int you, int com, string str)
    {
        _imgYou.texture = Resources.Load("img_" + you) as Texture; //�̹���Ÿ�� RawImage Ÿ��
        _imgCom.texture = Resources.Load("img_" + com) as Texture;

        _imgCom.transform.localScale = new Vector3(-1, (int)2.5, 2);

        _txtYou.text = _cntYou.ToString();
        _txtCom.text = _cntCom.ToString();

        //��� ǥ��
        _txtResult.text = str;
        _drawCount.text = "Win Count:  " + _cntDraw.ToString();
    }

    //�·���� �Լ�
    public float CalculateWinRate()
    {
        if (_totalGame == 0)
        {
            return 0f;
        }
        
        //float _resultPer = (float)_cntYou / _totalGame;
        _winPer.text = "Your Win Percentage is  " + (float)_cntYou /_totalGame;

        Debug.Log("�·� ��� �����: " + (float)_cntYou /_totalGame);

        return (float)_cntYou /_totalGame;
    }
}
