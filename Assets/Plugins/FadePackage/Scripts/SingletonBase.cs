using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)//インスタンスがなければこっちに入る
            {
                var obj = FindObjectOfType<T>();//Tを探してくる
                if (obj == null)//なければエラー
                    Debug.LogError(typeof(T) + "をアタッチしてあるGameObjectがないよー");
                else//あればインスタンスに
                    _instance = obj;
            }
            return _instance;//基本すぐこれ
        }
    }

    //よくあるシングルトンAwake
    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
