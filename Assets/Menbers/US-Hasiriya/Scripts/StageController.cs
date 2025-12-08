using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class StageController
{
    public static string stageName;

    // 「このメソッドが実行された時に開いているシーンの名前」を取得する。
    // 今回の場合は、ゲームオーバーの条件が揃った時に、このメソッドを呼び出す。
    public static void CurrentStageNumber()
    {
        stageName = SceneManager.GetActiveScene().name;
        Debug.Log(stageName);
    }

    // 上記のメソッドで取得されたシーンに戻る。
    // 今回の場合は、コンティニューボタンを押した時にこのメソッドを実行する。
    public static void BackToBeforeScene()
    {
        SceneManager.LoadScene(stageName);
    }

    // StageController.CurrentStageNumber();

}