using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextController : MonoBehaviour
{
    //スコア
    public int score;

    //スコアを表示するテキスト
    private GameObject scoreText;

    // Start is called before the first frame update
    void Start()
    {
        this.score = 0;
        //シーン中のScoreTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        this.scoreText.GetComponent<Text>().text = this.score.ToString("D6");
    }

    //スコアを加算する関数
    public void AddScore(int val)
    {
        this.score += val;
    }
}
