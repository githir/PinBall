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
        int touchResult = getTouchResult();         //タッチ操作に対応する
        Debug.Log("touch right" + (touchResult & 0x01));
        Debug.Log("touch left" + (touchResult & 0x10));

        //左矢印キーまたはAキーを押した時左フリッパーを動かす
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (touchResult & 0x10) == 0x10) && tag == "LeftFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //右矢印キーまたはAキーを押した時右フリッパーを動かす
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (touchResult & 0x01) == 0x01) && tag == "RightFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //下矢印キーまたはSキーを押した時左右フリッパーを動かす
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (touchResult & 0x11) == 0x11) && (tag == "LeftFripperTag" || tag == "RightFripperTag"))
        {
            SetAngle(this.flickAngle);
        }

        //各キーが離された時フリッパーを元に戻す
        if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || (touchResult & 0x10) == 0x00) && tag == "LeftFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || (touchResult & 0x01) == 0x00) && tag == "RightFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if ((Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S) || (touchResult & 0x11) == 0x00) && (tag == "LeftFripperTag" || tag == "RightFripperTag"))
        {
            SetAngle(this.defaultAngle);
        }

    }

    //タッチ操作に対応する
    int getTouchResult()
    {
        int result = 0;

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
                    Debug.Log("Right");
                    result |= 0x01;
                }
                else
                {
                    Debug.Log("Left");
                    result |= 0x10;
                }
            }
        }
        Debug.Log("Touch Reult" + result);
        return result;
    }

    //フリッパーの傾きを設定
    public void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }
}