using UnityEngine;
using UnityEngine.UI;

public class RandomPanelColor : MonoBehaviour
{
    private Image target;
    int lastIndex = -1;

    void OnEnable()
    {
        if (target == null) target = GetComponent<Image>();
        if (NiceColors.Palette.Length == 0 || target == null) return;

        int i = Random.Range(0, NiceColors.Palette.Length);
        if (NiceColors.Palette.Length > 1 && i == lastIndex)
            i = (i + 1) % NiceColors.Palette.Length;

        target.color = NiceColors.Palette[i];
        lastIndex = i;
    }
}
