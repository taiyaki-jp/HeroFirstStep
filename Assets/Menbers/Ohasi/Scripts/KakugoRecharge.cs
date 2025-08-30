using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KakugoRecharge : MonoBehaviour
{
    [SerializeField] private float RechargeTime;    //覚悟回復間隔
    [SerializeField] private int Maxcharge;         //覚悟最大値
    [SerializeField] private int ChargeAmount;      //覚悟回復量
    [SerializeField] private Text TextKakugo;

    private int CurrentKakugoValue = 0;     //現在の覚悟量

    private float RechargeIntervalTime; //回復までの時間

    // Update is called once per frame
    void Update()
    {

        if (CurrentKakugoValue < Maxcharge)  //最大値より少なければ回復
        {
            RechargeIntervalTime += Time.deltaTime;

            if (RechargeIntervalTime >= RechargeTime)
            {
                RechargeIntervalTime = 0f;
                CurrentKakugoValue += ChargeAmount;
            }
        }

        TextKakugo.text = CurrentKakugoValue + "/" + Maxcharge;
    }

    public void KakugoConsumption(int ConsumptionValue)
    {
        if (ConsumptionValue < CurrentKakugoValue)  //覚悟の値が消費量よりも多いとき
        {
            CurrentKakugoValue -= ConsumptionValue;
        }
    }
}
