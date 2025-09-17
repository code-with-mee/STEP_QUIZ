using UnityEngine;

public static class NiceColors
{
    public static readonly Color[] Palette = new Color[]
    {
        Hex("#FF6B6B"),
        Hex("#F06595"),
        Hex("#CC5DE8"),
        Hex("#845EF7"),
        Hex("#5C7CFA"),
        Hex("#339AF0"),
        Hex("#22B8CF"),
        Hex("#20C997"),
        Hex("#51CF66"),
        Hex("#94D82D"),
        Hex("#FCC419"),
        Hex("#FFA94D"),
        Hex("#FF922B"),
        Hex("#FFD43B"),
        Hex("#B197FC"),
        Hex("#74C0FC"),
        Hex("#63E6BE"),
        Hex("#4DABF7"),
        Hex("#A9E34B"),
        Hex("#FAB005")
    };

    static Color Hex(string hex)
    {
        Color c;
        ColorUtility.TryParseHtmlString(hex, out c);
        return c;
    }
}
