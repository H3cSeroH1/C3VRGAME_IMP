using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HitEvent : MonoBehaviour
{
    //これが12以上になると強攻撃
    public float MaxHitVelocity = 12f;
    //この間が通常でこれ以下がカス当たり
    public float UnderHitVelocity = 6f;


    //n回殴ると壊れる ここに回数があったらn回殴るとってなる
    public float TakeableHitCount = 0;
    //今殴られてる回数
    public float TakeHitCount = 0;

    //ウケた総速度
    public float TakenVelocity = 0;

    public UnityEvent onTakeHitAbove;
    public UnityEvent onTakeHitModerate;
    public UnityEvent onTakeHitUnder;

    public TextMesh showPower = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (showPower)
        {
            TakenVelocity += collision.relativeVelocity.magnitude;
            showPower.text = collision.relativeVelocity.magnitude.ToString() + "\n" + TakenVelocity.ToString() + "\n" + TakeHitCount.ToString();
        }

        //if (collision.relativeVelocity.magnitude > 2)


        if (MaxHitVelocity < collision.relativeVelocity.magnitude)
        {
            if (TakeableHitCount <= TakeHitCount)
            {
                onTakeHitAbove.Invoke();
            }
            else
            {
                TakeHitCount+=1;
            }




        }
        else if (UnderHitVelocity > collision.relativeVelocity.magnitude)
        {
            onTakeHitModerate.Invoke();

        }

        else
        {



            if (TakeableHitCount <= TakeHitCount)
            {
                onTakeHitUnder.Invoke();

            }
            else
            {
                TakeHitCount += 0.5f;
            }
        }

    }
}
