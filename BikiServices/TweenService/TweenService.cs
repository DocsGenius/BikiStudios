using UnityEngine;
using System.Collections;
using System;

public class TweenService : MonoBehaviour
{
    public static TweenService Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // A generic Move tween
    public void MoveTo(Transform target, Vector3 endPos, float duration, AnimationCurve curve = null)
        {
            Vector3 startPos = target.position; // Capture the start point!
            StartCoroutine(TweenRoutine(duration, (t) => {
                float progress = (curve != null) ? curve.Evaluate(t) : t;
                target.position = Vector3.Lerp(startPos, endPos, progress);
            }));
    }

    private IEnumerator TweenRoutine(float duration, Action<float> onUpdate)
    {
        float progress = 0f;

        // While we haven't reached 100% completion
        while (progress < 1f)
        {
            // 1. Get the raw frame time
            // 2. Multiply by your custom scale (e.g., 0.1 for slow mo)
            // 3. Divide by duration to see what % of the total trip this frame represents
            float scaledDeltaProgress = (Time.unscaledDeltaTime * TimeManager.Instance.CustomTimeScale) / duration;
            
            progress += scaledDeltaProgress;
            
            // Clamp to 1.0 so we don't overset the position
            onUpdate(Mathf.Clamp01(progress));
            
            yield return null;
        }
    }
}