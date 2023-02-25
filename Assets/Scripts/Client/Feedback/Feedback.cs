using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    public abstract void StartFeedback();

    public abstract void CompletePreviousFeedback();

    protected virtual void OnDestroy()
    {
        CompletePreviousFeedback();
    }
}
