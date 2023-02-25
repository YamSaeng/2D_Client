using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    [SerializeField]
    private List<Feedback> _FeedbackPlayList = null;

    public void PlayFeedBack()
    {
        FinishFeedback();

        foreach (Feedback feedback in _FeedbackPlayList)
        {
            feedback.StartFeedback();
        }
    }

    private void FinishFeedback()
    {
        foreach (Feedback feedback in _FeedbackPlayList)
        {
            feedback.CompletePreviousFeedback();
        }
    }
}
