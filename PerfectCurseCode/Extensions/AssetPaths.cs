using Godot;

namespace PerfectCurse.Extensions;

public static class AssetPaths
{
    public static string CardPortrait(this string file, bool big = false) =>
        Path.Join(MainFile.ResPath, "images", "card_portraits", big ? "big" : "", file);
}
