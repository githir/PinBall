using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalcurator : MonoBehaviour
{
    // ターゲットのデフォルトスコア
    private int scoreValue = 10;

    // Start is called before the first frame update
    void Start()
    {
        // タグによってスコアを変える
        if (tag == "SmallStarTag")
        {
            this.scoreValue = 10;
        }
        else if (tag == "LargeStarTag")
        {
            this.scoreValue = 20;
        }
        else if (tag == "SmallCloudTag" || tag == "LargeCloudTag")
        {
            this.scoreValue = 50;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //なにもしない
    }

    //衝突時に呼ばれる関数
    void OnCollisionEnter(Collision other)
    {
        //ScoreTextControllerオブジェクトのAddScoreを呼び出して得点を追加する
        FindObjectOfType<ScoreTextController>().AddScore(this.scoreValue);
    }
}