//묵찌빠 GameManager
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
    //public Image _imgYou; //내 가위바위보 이미지
    [SerializeField] private RawImage _imgYou;
    [SerializeField] private RawImage _imgCom; //컴퓨터 가위바위보 이미지

    [SerializeField] private TMP_Text _txtYou; //내 승리 횟수 표시용
    [SerializeField] private TMP_Text _txtCom; //컴퓨터 승리 횟수
    [SerializeField] private TMP_Text _txtResult; //판정 결과
    [SerializeField] private TMP_Text _winPer; //승률 표시용
    [SerializeField] private TMP_Text _drawCount; //무승부 횟수 표시용

    int _totalGame = 0; //플레이 횟수
    int _cntYou = 0; //내 승리 횟수
    int _cntCom = 0; //컴퓨터 승리 횟수
    int _cntDraw = 0; //무승부 횟수

    public int _attackKey = 0; //공격권을 가지면 1로 줘.


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
            //나중에 게임들 모아서 하나의 프로젝트로 묶을때 수정 필요
            //Application.Quit();
        }

        //CalculateWinRate();
    }

    public void OnButtonClick(GameObject button)
    {
        _totalGame++;

        //Debug.Log(button.name);
        int you = int.Parse(button.name.Substring(7, 1)); //나는 - 전체 7문자에서 한 문자만 잘라낸 것을 정수로 바꿔주기
        int com = UnityEngine.Random.Range(1, 4); //컴퓨터는 - 1이상 4미만의 숫자 중 랜덤 숫자를 뽑는다.

        int res = CheckResult(you, com); //첫 가위바위보 하고 난 결과값 불러옴
        //승:1, 패:-1, 무:0
        string str = "";

        if (_attackKey == 0) //비겼으면 다시 선택하게
        {
            _attackKey = 0;
            _txtResult.text = "DRAW~";
            str = "Draw. Choose again!";
        }
        else if (_attackKey == 1) //내가 이기면 공격권이 나에게
        { 
            if(you == com)
            {
                Debug.Log("you win");
                _cntYou++;
                _txtResult.text = "YOU WIN !!!";
                str = "WIN";
                _cntDraw++;
                CalculateWinRate(); //내가 이긴 경우에 한해 승률을 계산해서 출력하는 함수 호출
            }
        }
        else if (_attackKey == -1) //내가 졌으면 공격권이 컴퓨터에게
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

    //초기화 함수
    void InitGame()
    {
        //(1) 교재처럼 Gameobject.Find()를 써서 변수에 값 할당
        //(2) 변수를 public으로 선언해서 인스펙터에서 직접 오브젝트 할당

        _txtResult.text = "Choose you Choice";
        _winPer.text = "Your Win Percentage is  " + 0;
    }


    //첫 가위바위보 승부 판정 함수
    int CheckResult(int you, int com)
    {
        int res = 0;

        //공식을 만들어서 더 간단하게 작성하는 승부 판정
        int k = you - com;
        switch (k)
        {
            //내가 짐
            case -1: case 2:
                res = -1;
                _attackKey = -1;
                Debug.Log("공격권이 컴퓨터에게 갔습니다.");
                break;
            //내가 이김
            case -2: case 1:
                res = 1;
                _attackKey = 1;
                Debug.Log("공격권이 내게 주어졌습니다.");
                break;
            //비김
            case 0:
                res = 0;
                //_cntDraw++;
                //_attackKey = 0;
                Debug.Log("공격권이 아무에게도 가지 않았습니다.");
                break;
        }

        return res;
    }

    //게임 결과를 UI에 표시하는 함수
    void SetResult(int you, int com, string str)
    {
        _imgYou.texture = Resources.Load("img_" + you) as Texture; //이미지타입 RawImage 타입
        _imgCom.texture = Resources.Load("img_" + com) as Texture;

        _imgCom.transform.localScale = new Vector3(-1, (int)2.5, 2);

        _txtYou.text = _cntYou.ToString();
        _txtCom.text = _cntCom.ToString();

        //결과 표시
        _txtResult.text = str;
        _drawCount.text = "Win Count:  " + _cntDraw.ToString();
    }

    //승률계산 함수
    public float CalculateWinRate()
    {
        if (_totalGame == 0)
        {
            return 0f;
        }
        
        //float _resultPer = (float)_cntYou / _totalGame;
        _winPer.text = "Your Win Percentage is  " + (float)_cntYou /_totalGame;

        Debug.Log("승률 계산 결과값: " + (float)_cntYou /_totalGame);

        return (float)_cntYou /_totalGame;
    }
}
