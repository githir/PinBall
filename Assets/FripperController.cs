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
        byte touchResult = GetTouchResult();         //タッチ操作の結果を受け取る。0b00 bit0=right,bit1=left
        //Debug.Log("touch right" + (touchResult & 0b01));
        //Debug.Log("touch left" + (touchResult & 0b10));

        //左矢印キーまたはAキーを押した時左フリッパーを動かす
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (touchResult & 0b10) == 0b10) && tag == "LeftFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //右矢印キーまたはAキーを押した時右フリッパーを動かす
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (touchResult & 0b01) == 0b01) && tag == "RightFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //下矢印キーまたはSキーを押した時左右フリッパーを動かす
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (touchResult & 0b11) == 0b11) && (tag == "LeftFripperTag" || tag == "RightFripperTag"))
        {
            SetAngle(this.flickAngle);
        }

        //★全く素敵ではないがKeyとTouchを両立させるための処理
        //なにかキーが押されていたときは以下if文の中のtouchResultに引っかからないようにする。これがないとTouch無効のときにFripperが戻ってしまう
        if (Input.anyKey)
        {
            touchResult = 0b11; //Debug.Log("some key is down. ignore touchResult");
        }

        //各キーが離された時フリッパーを元に戻す
        if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || (touchResult & 0b10) == 0b00) && tag == "LeftFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || (touchResult & 0b01) == 0b00) && tag == "RightFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if ((Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S) || (touchResult & 0b11) == 0b00) && (tag == "LeftFripperTag" || tag == "RightFripperTag"))
        {
            SetAngle(this.defaultAngle);
        }

    }

    //タッチ操作の結果を返す。0b00 bit0=right,bit1=left
    byte GetTouchResult()
    {
        byte result = 0b00;

        //タッチ操作に対応する
        //資料 https://docs.unity3d.com/ja/current/ScriptReference/Touch.html , https://torikasyu.com/?p=668
        //   Debug.Log("touchCount=" + Input.touchCount);
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                //Debug.Log("x=" + t.position.x + " y=" + t.position.y);
                if (t.position.x > 540)
                {
                    result = 0b01;  // Debug.Log("Right");
                }
                else
                {
                    result = 0b10;  // Debug.Log("Left");
                }
            }
        }
        // Debug.Log("Touch Reult" + result);
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