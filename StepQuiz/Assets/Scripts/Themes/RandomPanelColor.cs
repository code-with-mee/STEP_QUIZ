using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RandomPanelColor : MonoBehaviour
{
    private Image target;
    private int lastIndex = -1;

    void OnEnable()
    {
        if (target == null) target = GetComponent<Image>();
        if (NiceColors.Palette.Length == 0) return;

        int i = Random.Range(0, NiceColors.Palette.Length);
        if (NiceColors.Palette.Length > 1 && i == lastIndex) i = (i + 1) % NiceColors.Palette.Length;

        Color c1 = NiceColors.Palette[i];
        lastIndex = i;

        var gradient = GetComponent<UIGradient>();
        if (gradient != null)
        {
            Color c2 = NiceColors.Palette[Random.Range(0, NiceColors.Palette.Length)];
            while (c2 == c1) c2 = NiceColors.Palette[Random.Range(0, NiceColors.Palette.Length)];

            bool flipColors = Random.value > 0.5f;
            gradient.mode = Random.value > 0.5f ? UIGradient.GradientMode.Vertical : UIGradient.GradientMode.Horizontal;
            gradient.invert = Random.value > 0.5f;

            gradient.topColor = flipColors ? c2 : c1;
            gradient.bottomColor = flipColors ? c1 : c2;

            target.SetVerticesDirty();
        }
        else
        {
            target.color = c1;
        }
    }
}
