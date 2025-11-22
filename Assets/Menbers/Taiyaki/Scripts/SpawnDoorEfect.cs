using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDoorEfect : MonoBehaviour
{
    [SerializeField,Label("ƒhƒA‚ª•Â‚¶‚é‚Ü‚Å‚ÌŽžŠÔ")] private float _doorCloseTimerDef = 1.0f;

    GameObject _home; 
    private float _closeTimer;

    private void Start()
    {
        _home = this.gameObject;
        _closeTimer = _doorCloseTimerDef;
    }

    public void Spawn()
    {
        _closeTimer = _doorCloseTimerDef;
        _ = Door();
    }

    private async UniTask Door()
    {
        //DoorOpen
        await UniTask.WaitUntil(() => _closeTimer <= 0f);
        //DoorClose
    }
}
