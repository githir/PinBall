using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour
{
    //HingeJointコンポーネントを入れる
    private HingeJoint myHingeJoint;

    //初期の傾き
    private float defaultAngle = 20;
    //弾いた時の傾き
    private float flickAngle = -20;

    private enum TOUCH_STATE
    {
        NONE,   //押していない
        LEFT,
        RIGHT,
        BOTH   //両方押している
    }

    // Use this for initialization
    void Start()
    {
        //HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);

    }

    // Update is called once per frame
    void Update()
    {
        TOUCH_STATE touchResult = GetTouchResult();         //タッチ操作の結果を受け取る

        //左矢印キーまたはAキーを押した時左フリッパーを動かす
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || touchResult == TOUCH_STATE.LEFT) && tag == "LeftFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //右矢印キーまたはAキーを押した時右フリッパーを動かす
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || touchResult == TOUCH_STATE.RIGHT) && tag == "RightFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //下矢印キーまたはSキーを押した時左右フリッパーを動かす
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || touchResult == TOUCH_STATE.BOTH) && (tag == "LeftFripperTag" || tag == "RightFripperTag"))
        {
            SetAngle(this.flickAngle);
        }

        if (Input.anyKey) { touchResult = TOUCH_STATE.BOTH; }

        //各キーが離された時フリッパーを元に戻す
        if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || touchResult == TOUCH_STATE.NONE || touchResult == TOUCH_STATE.RIGHT ) && tag == "LeftFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || touchResult == TOUCH_STATE.NONE || touchResult == TOUCH_STATE.LEFT) && tag == "RightFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if ((Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S) || touchResult == TOUCH_STATE.NONE) && (tag == "LeftFripperTag" || tag == "RightFripperTag"))
        {
            SetAngle(this.defaultAngle);
        }

    }

    //タッチ操作の結果を返す。0b00 bit0=right,bit1=left
    TOUCH_STATE GetTouchResult()
    {
        //タッチ操作に対応する
        //資料 https://docs.unity3d.com/ja/current/ScriptReference/Touch.html , https://torikasyu.com/?p=668
        //   Debug.Log("touchCount=" + Input.touchCount);

        bool isLeft = false;
        bool isRight = false;
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.position.x > Screen.width / 2)
                {
                    isRight = true;
                }
                else
                {
                    isLeft = true;
                }
            }
        }

        TOUCH_STATE result = TOUCH_STATE.NONE;
        if (isLeft && isRight) { result = TOUCH_STATE.BOTH; }
        if (!isLeft && isRight) { result = TOUCH_STATE.RIGHT; }
        if (isLeft && !isRight) { result = TOUCH_STATE.LEFT; }
        return result;

    }


    //フリッパーの傾きを設定
    void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }
}