using UnityEngine;

public static class NiceColors
{
    public static readonly Color[] Palette = new Color[]
    {
        Hex("#FF6B6B"), // Vibrant Red
        Hex("#F06595"), // Pink Rose
        Hex("#CC5DE8"), // Purple
        Hex("#845EF7"), // Violet
        Hex("#5C7CFA"), // Indigo
        Hex("#339AF0"), // Blue Sky
        Hex("#22B8CF"), // Cyan
        Hex("#20C997"), // Teal Green
        Hex("#51CF66"), // Fresh Green
        Hex("#94D82D"), // Lime
        Hex("#FFD43B"), // Yellow Sun
        Hex("#FCC419"), // Golden
        Hex("#FFA94D"), // Orange Soft
        Hex("#FF922B"), // Deep Orange
        Hex("#FAB005"), // Amber
        Hex("#A9E34B"), // Apple Green
        Hex("#63E6BE"), // Mint
        Hex("#4DABF7"), // Light Blue
        Hex("#B197FC"), // Lavender
        Hex("#FFD8A8")  // Peach Pastel
    };

    static Color Hex(string hex)
    {
        Color c;
        ColorUtility.TryParseHtmlString(hex, out c);
        return c;
    }
}
