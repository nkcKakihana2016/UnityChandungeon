using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    // 使用するUIの設定
    public Image tutorialPanel; // チュートリアル画面を催促させるための画像
    public Image tutorial; // チュートリアル本画面

    // 現在チュートリアルスポットライト付近にいるかの判定
    bool panelFlg = false;


    // Use this for initialization
    void Start()
    {
        // ゲーム開始時、画像を非表示に
        tutorialPanel.enabled = false;
        tutorial.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // チュートリアルフィールドに入っているかつHキーでチュートリアル画面表示
        if (Input.GetKey(KeyCode.H)&&panelFlg==true)
        {
            tutorial.enabled = true;
        }
        // Escキーでチュートリアル画面非表示
        if (Input.GetKey(KeyCode.Escape))
        {
            tutorial.enabled = false;
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        // チュートリアルフィールドに入ったらチュートリアル画面を催促するための画像を表示
        // パネルフラグをtrueにしてHキーを押せばチュートリアル画面が出るように
        if (other.gameObject.tag == "Player")
        {
            tutorialPanel.enabled = true;
            panelFlg = true;

        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialPanel.enabled = false;
            panelFlg = false;
        }
    }
}
