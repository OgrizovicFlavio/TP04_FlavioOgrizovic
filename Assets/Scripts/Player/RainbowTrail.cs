using UnityEngine;

public class RainbowTrail : MonoBehaviour
{
    private TrailRenderer trailRenderer;
    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    public void ApplayRainbowEffect()
    {
        Gradient rainbowGradient = new Gradient();
        rainbowGradient.SetKeys(
            new GradientColorKey[]
            {
                new GradientColorKey(new Color(1f, 0f, 0f), 0.0f),
                new GradientColorKey(new Color(1f, 0.65f, 0f), 0.17f),
                new GradientColorKey(new Color(1f, 1f, 0f), 0.33f),
                new GradientColorKey(new Color(0f, 1f, 0f), 0.50f),
                new GradientColorKey(new Color(0f, 0f, 1f), 0.67f),
                new GradientColorKey(new Color(0.29f, 0f, 0.51f), 0.83f),
                new GradientColorKey(new Color(0.93f, 0.51f, 0.93f), 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.5f, 0.0f),
                new GradientAlphaKey(0.5f, 1.0f)
            });

        trailRenderer.colorGradient = rainbowGradient;
    }
}
