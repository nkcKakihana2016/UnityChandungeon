using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameControll : MonoBehaviour {

    public static int Score;

    public int lastTime;

    private int lastKey;
    private int DungeonFloor=1;

    public Text ScoreLabel;
    public Text TimeLabel;
    public Text FloorLabel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        lastTime += (int)Time.deltaTime;

        TimeLabel.text = string.Format("経過時間：{0:00:00}", lastTime);
        ScoreLabel.text = string.Format("スコア：{0:0000}", Score);
        FloorLabel.text = string.Format("{0:0}階", DungeonFloor);
    }
}
