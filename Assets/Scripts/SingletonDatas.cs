using UnityEngine;
using UsefulSystem.Common;

/// <summary>
/// シーンを超えて各変数を保持するためのスクリプト
/// </summary>
public class SingletonDatas : SingletonBase<SingletonDatas>
{
    public static SingletonDatas Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SingletonDatas");
                SetInstance(go.AddComponent<SingletonDatas>());
                DontDestroyOnLoad(go);
            }
            return GetInstance();
        }
    }
    public bool IsWin{get;set;}
    protected override void Awake()
    {
        base.Awake();
       IsWin = false;
    }
}
