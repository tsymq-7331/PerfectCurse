using Godot;

namespace PerfectCurse.Extensions;

public static class AssetPaths
{
    public static string CardPortrait(this string file, bool big = false) =>
        ExistingOrFallback(Path.Join(MainFile.ResPath, "images", "card_portraits", big ? "big" : "", file),
            Path.Join(MainFile.ResPath, "images", "card_portraits", big ? "big" : "", "card.png"));

    public static string RelicIcon(this string file, bool big = false) =>
        ExistingOrFallback(Path.Join(MainFile.ResPath, "images", "relics", big ? "big" : "", file),
            Path.Join(MainFile.ResPath, "images", "relics", big ? "big" : "", "relic.png"));

    public static string PowerIcon(this string file, bool big = false) =>
        ExistingOrFallback(Path.Join(MainFile.ResPath, "images", "powers", big ? "big" : "", file),
            Path.Join(MainFile.ResPath, "images", "powers", big ? "big" : "", "power.png"));

    private static string ExistingOrFallback(string requested, string fallback) =>
        ResourceLoader.Exists(requested) ? requested : fallback;
}
