using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace PerfectCurse;

[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    public const string ModId = "PerfectCurse";
    public const string ResPath = "res://PerfectCurse";
    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } = new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Initialize()
    {
        new Harmony(ModId).PatchAll();
        Logger.Info("Perfect Curse v0.1.0-alpha.3 initialized. Design: 龙娘无限好.");
    }
}
