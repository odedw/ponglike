using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour
{
    public int Price;

    protected abstract void Consume(Opponent opponent);
    protected abstract bool ShouldDestroyAfterConsumed { get; }

    public void ActivateItem(Opponent opponent)
    {
        Consume(opponent);

        if (ShouldDestroyAfterConsumed)
        {
            DestroyObject(gameObject);
        }
    }
}
