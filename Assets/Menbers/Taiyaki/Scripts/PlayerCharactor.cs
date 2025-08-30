using Cysharp.Threading.Tasks;
using System.Threading;

public class PlayerCharactor : CharactorBase
{
    private bool _doKnockback = false;
    private CancellationTokenSource _cancellationTokenSource;

    public bool DoKnockback { set => _doKnockback = value; }

    private void Start()
    {
        _ = Moveing(_cancellationTokenSource.Token);
    }

    private async UniTask Moveing(CancellationToken token)
    {
        while (true)
        {
            if (_doKnockback) await Knockback(token);
            await UniTask.Yield(token);
        }
    }
    public async UniTask Knockback(CancellationToken token)
    {
        await UniTask.Yield();
        DoKnockback = false;
    }
    public void Deth()
    {
        _cancellationTokenSource.Cancel();
    }
}
