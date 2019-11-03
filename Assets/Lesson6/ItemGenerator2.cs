using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lesson6 発展課題の処理。
/// これを使う時は既存の ItemGenerator を使わないこと。
/// </summary>
public class ItemGenerator2 : MonoBehaviour
{
    [SerializeField] GameObject carPrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject conePrefab;

    /// <summary>アイテムを置く間隔</summary>
    [SerializeField] int m_interval = 15;

    /// <summary>どれくらい先まで前もってアイテムを生成しておくかを設定する</summary>
    [SerializeField] int m_prepareDistance = 40;

    /// <summary>ゴールの Z 座標</summary>
    private int goalPos = 120;

    /// <summary>アイテムを出すx方向の範囲</summary>
    private float posRange = 3.4f;

    /// <summary>プレイヤー</summary>
    [SerializeField] GameObject m_player;

    /// <summary>最後にアイテムを生成した場所の Z 座標</summary>
    private float m_latestGeneratedPosZ;

    private void Start()
    {
        // メンバ変数の初期化
        m_latestGeneratedPosZ = m_player.transform.position.z;
    }

    void Update()
    {
        // どの Z 座標にアイテムを生成するか決める
        float posZ = m_player.transform.position.z + m_prepareDistance;

        // 既にアイテムを生成した場所には生成しない。goalPos より手前に、m_interval ごとにアイテムを生成する
        if (posZ > m_latestGeneratedPosZ + m_interval && posZ < goalPos)
        {
            int dice = Random.Range(0, 10); // ２割の確率でコーンの列を生成する。８割はランダムアイテムを生成する。
            if (dice <= 1)
                GenerateConeLine(posZ);
            else
                GenerateRandomItem(posZ);
            m_latestGeneratedPosZ = posZ;   // 既にアイテムを生成した場所は保存しておく。
        }
    }

    /// <summary>
    /// posZ で指定した Z 座標の位置に、ランダムなアイテムを生成する。
    /// </summary>
    /// <param name="posZ">アイテムを生成する Z 座標</param>
    void GenerateRandomItem(float posZ)
    {
        for (int i = -1; i < 2; i++)
        {
            int itemDice = Random.Range(1, 11);
            int offsetZ = Random.Range(-5, 6);
            if (1 <= itemDice && itemDice <= 6)
            {
                GameObject coin = Instantiate(coinPrefab) as GameObject;
                coin.transform.position = new Vector3(posRange * i, coin.transform.position.y, posZ + offsetZ);
            }
            else if (7 <= itemDice && itemDice <= 9)
            {
                GameObject car = Instantiate(carPrefab) as GameObject;
                car.transform.position = new Vector3(posRange * i, car.transform.position.y, posZ + offsetZ);
            }
        }
    }

    /// <summary>
    /// posZ で指定した Z 座標の位置に、コーンの列を生成する。
    /// </summary>
    /// <param name="posZ">コーンの列を生成する Z 座標</param>
    void GenerateConeLine(float posZ)
    {
        for (float f = -1; f <= 1; f += 0.4f)
        {
            GameObject go = Instantiate(conePrefab) as GameObject;
            go.transform.position = new Vector3(4 * f, 1, posZ);
        }
    }
}