using UnityEngine;
using System.Collections;

public class Spikes : Item
{
    protected override void Consume(Opponent opponent)
    {
    }

    protected override bool ShouldDestroyAfterConsumed
    {
        get { return false; }
    }
}