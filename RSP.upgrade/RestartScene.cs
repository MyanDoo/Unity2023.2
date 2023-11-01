using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScene : MonoBehaviour
{
    [SerializeField] Button button;
    GameObject _gameObject;

    [SerializeField] private RawImage _imgYou;
    [SerializeField] private RawImage _imgCom; //��ǻ�� ���������� �̹���
    [SerializeField] private TMP_Text _txtResult; //���� ���

    GameObject _game;
    int _attackScore;

    // Start is called before the first frame update
    void Start()
    {
        _gameObject = new GameObject("GameManager");
        DontDestroyOnLoad(_gameObject);

        _game = GameObject.Find("GameManager");
        _attackScore = _game.GetComponent<GameManager>()._attackKey;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick(GameObject button)
    {
        Debug.Log("���¹�ư �ҷ���");

        _txtResult.text = "";
        //_imgYou.SetActive(true);
        _imgYou.texture = Resources.Load("img_4") as Texture;
        _imgCom.transform.localScale = new Vector3(1, (float)2.6, 1);
        _imgCom.texture = Resources.Load("img_4") as Texture;

        _attackScore = 0;

        //onbuttonclick 함수 수정
    }
}
