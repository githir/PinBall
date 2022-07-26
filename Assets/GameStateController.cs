using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{

    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // ゲームオーバーになった場合）
        if (this.isGameOver == true)
        {
            // クリックされたらシーンをロードする（追加）
            if (Input.GetMouseButtonDown(0))
            {
                //SampleSceneを読み込む
                SceneManager.LoadScene("PinBall");
            }
        }

    }
}
