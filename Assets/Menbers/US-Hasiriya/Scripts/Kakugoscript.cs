using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kakugoscript : MonoBehaviour
{
    [SerializeField] Text _textKakugo;
    public int maxKakugo;
    public int Kakugo = 0;

    private bool kakugoCount;

    private float countTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        kakugoCount = true;
        maxKakugo = 2000;
        _textKakugo.text = Kakugo + "/" + maxKakugo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Kakugo < maxKakugo)
        {
            kakugoCount = true;
        }
        else
        {
            kakugoCount = false;
        }

        if (kakugoCount)
        {
            countTime += Time.deltaTime;

            if (countTime >= 0.1f)
            {
                Kakugo++;
                countTime = 0;
            }
        }

        _textKakugo.text = Kakugo + "/" + maxKakugo;
    }
}
