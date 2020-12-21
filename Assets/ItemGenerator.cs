using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject carPrefab;

    public GameObject coinPrefab;

    public GameObject conePrefab;

    //スタート地点を決める変数startPosを定義
    private int startPos = 80;

    //ゴール地点を決める変数goalPosを定義
    private int goalPos = 360;

    //アイテムを出すx（横）方向の範囲を定義
    private float posRange = 3.4f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = startPos; i < goalPos; i += 15) //startPosからgoalPosまで15ごとにiが出てくる
		{
            int num = Random.Range(1, 11); //numは1から11の間でランダム
            if (num <= 2) //numが1,2の場合は、
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f) //変数jが0.4ごとに-1から1
                {
                    GameObject cone = Instantiate(conePrefab);//コーン生成
                    //Instantiate () 」は、()内に指定したPrefabのインスタンスをGameObject型として生成します
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                }
            }
            else //numが3~11の場合は
            {
                //レーンごとにアイテムを生成
                for (int j = -1; j <= 1; j++)//for文で-1,1,1
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11); //item変数は、1から11までの乱数

                    //アイテムを置くZ（前）座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6); //offsetZ変数は、-5から6までの乱数

                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6) //item変数が1以上6以下の場合は、
                    {
                        GameObject coin = Instantiate(coinPrefab);//コインを置く
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                    }
                    else if (7 <= item && item <= 9)//itemが7以上9以下の場合は、
                    {
                        GameObject car = Instantiate(carPrefab);
                        carPrefab.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                    }
                }
            }
			
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

