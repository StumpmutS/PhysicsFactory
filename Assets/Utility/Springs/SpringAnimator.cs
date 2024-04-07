using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringAnimator : MonoBehaviour
{
    [SerializeField] private SpringController spring;
    [SerializeField] private bool playOnEnable;
    [SerializeField] private List<SpringAnimationData> animationData;

    private float _timer;

    private void OnEnable()
    {
        if (playOnEnable) PlayAnimation();
    }

    public void PlayAnimation() => StartCoroutine(PlayComponent(0));

    private IEnumerator PlayComponent(int animationIndex)
    {
        spring.SetTarget(animationData[animationIndex].springTarget);
        yield return new WaitForSeconds(animationData[animationIndex].time);
        if (animationData.Count > animationIndex + 1)
        {
            StartCoroutine(PlayComponent(animationIndex + 1));
        }
    }
    
    [Serializable]
    private class SpringAnimationData
    {
        public float springTarget;
        public float time;
    }
}