﻿using UnityEngine;
using System.Collections;

public class PoisonPotion : Item
{
    protected override void Consume(Opponent opponent)
    {
    }

    protected override bool ShouldDestroyAfterConsumed
    {
        get { return true; }
    }
}
