using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/UIGradient")]
public class UIGradient : BaseMeshEffect
{
    public enum GradientMode { Vertical, Horizontal }
    public GradientMode mode = GradientMode.Vertical;
    public bool invert = false;
    public Color topColor = Color.white;
    public Color bottomColor = Color.black;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive()) return;
        UIVertex v = new UIVertex();
        int count = vh.currentVertCount;
        if (count == 0) return;

        float min = float.MaxValue;
        float max = float.MinValue;

        for (int i = 0; i < count; i++)
        {
            vh.PopulateUIVertex(ref v, i);
            float value = mode == GradientMode.Vertical ? v.position.y : v.position.x;
            if (value > max) max = value;
            if (value < min) min = value;
        }

        float size = max - min;
        for (int i = 0; i < count; i++)
        {
            vh.PopulateUIVertex(ref v, i);
            float pos = mode == GradientMode.Vertical ? v.position.y : v.position.x;
            float t = size > 0 ? (pos - min) / size : 0f;
            if (invert) t = 1f - t;
            v.color *= Color.Lerp(bottomColor, topColor, t);
            vh.SetUIVertex(v, i);
        }
    }
}
