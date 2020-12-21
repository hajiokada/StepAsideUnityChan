using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    //myAnimator定義
    private Animator myAnimator;

    private Rigidbody myRigidbody;




    //タイマー的なメンバ変数に
    //UnityのTimeクラス



    //前方向の速度
    private float velocityZ = 16f;
    

    //上方向の速度
    private float velocityY = 10f;

    //横方向の速度
    private float velocityX = 10f;
    //横方向への動く範囲
    private float movableRange = 3.4f;

    //動きを減速させる係数
    private float coefficient = 0.99f;

    //ゲーム終了の判定
    private bool isEnd = false;

    //GameObject型のstateTextを定義
    private GameObject stateText;

    private GameObject scoreText;

    private int score = 0;

    //ボタン系三連発
    private bool isLButtonDown = false;
    private bool isRButtonDown = false;
    private bool isJButtonDown = false;




    void Start()
    {



        //stateTextを取得
        this.stateText = GameObject.Find("GameResultText");

        //scoreText取得
        this.scoreText = GameObject.Find("ScoreText");

        //走らせる
        this.myAnimator = GetComponent<Animator>();
        this.myAnimator.SetFloat("Speed", 1.0f);
        this.myRigidbody = GetComponent<Rigidbody>();


    }






    void Update()
    {

        

        //ゲーム終了の時に減速させる
        if (this.isEnd)
		{
            this.velocityZ *= this.coefficient;
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
		}


        float inputVelocityX = 0; //横方向のRigidBodyに代入する値を定義

        if ((Input.GetKey (KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
		{
            inputVelocityX = -this.velocityX;
		}
        else if ((Input.GetKey (KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
		{
            inputVelocityX = this.velocityX;
		}

        float inputVelocityY = 0; //上方向のRigidBodyに代入する値を定義

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            inputVelocityY = this.velocityY;
        }
        else
		{
            inputVelocityY = this.myRigidbody.velocity.y;
        }

        //ジャンプ時false
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, this.velocityZ); //横には代入、上、前はvelocityをそのまま代入
    }

    //トリガーモードで他のオブジェクトと接触した場合の処理
    void OnTriggerEnter (Collider other)
	{
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
		{
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "GAME OVER";
		}
        if (other.gameObject.tag == "GoalTag")
		{
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
		}
        //コインに衝突した場合
        if (other.gameObject.tag == "CoinTag")
        {
            //スコア加算
            this.score += 10;
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";

            GetComponent<ParticleSystem>().Play();
            //ParticleSystemクラスの「Play」関数を呼ぶとパーティクルが再生される。

            //接触したコインのオブジェクトを破棄
            Destroy(other.gameObject);
        }
    }

    //上ボタン
    public void GetMyJumpButtonDown()
	{
        this.isJButtonDown = true;
	}
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }


    //左ボタン
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }


    //右ボタン
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }


}
