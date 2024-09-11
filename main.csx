#r "nuget: Mutagen.Bethesda, 0.45.1"
#r "nuget: Mutagen.Bethesda.Core, 0.45.1"
#r "nuget: Mutagen.Bethesda.Skyrim, 0.45.1"

using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Binary.Parameters;
using Mutagen.Bethesda.Plugins.Order;

// General process for creating a mod
var mod = SkyrimMod.CreateFromBinary(
    ModPath.FromPath("HelloWorldMod.esp"),
    release: SkyrimRelease.SkyrimSE
);


// We must turn the mod to a linkCache before we can do anything
ILinkCache scriptLinkCache = mod.ToMutableLinkCache();

// Get quest by FormKey (you can use this format for any record)
var questKey = FormKey.Factory("000D61:HelloWorldMod.esp");

// Search for this FormKey in our linkCache
if (scriptLinkCache.TryResolve<Quest>(questKey, out var quest)) {
    Console.WriteLine($"We found our quest via FormKey: {quest.EditorID}");
}

// Search for our HelloModQuest by checking scirpts
foreach (var helloQuest in mod.EnumerateMajorRecords<IQuest>()) {
    var vmad = helloQuest.VirtualMachineAdapter;
    if (vmad?.Scripts != null 
    && vmad.Scripts.Find(x => x.Name == "HelloModQuestScript") != null) {
        Console.WriteLine($"We found this quest by searching for the script: {quest.EditorID}");
    }
}

// Process all quests at once
foreach (var anyQuest in mod.EnumerateMajorRecords<IQuest>()) {
    anyQuest.EditorID += "PatchedAndScripted";
    Console.WriteLine($"{anyQuest.EditorID}");
}

// Demonstartion of how to work with multiple esps
var patchMod = SkyrimMod.CreateFromBinary(
    ModPath.FromPath("HelloWorldModPatch.esp"),
    // You can use importMask to selectively grab parts of mods
    importMask: new GroupMask() {
        Quests = true,
    },
    release: SkyrimRelease.SkyrimSE
);

// Create load order
var loadOrder = new LoadOrder<ISkyrimMod>();
loadOrder.Add(mod);
loadOrder.Add(patchMod);

// Iterate through the combined mods
foreach (var quest in loadOrder.PriorityOrder.Quest().WinningOverrides()) {
    Console.WriteLine($"Here's our quest from the combined mods: {quest.EditorID}");
}

// Write to patch
mod.WriteToBinary(
    "HelloWorldModPatch.esp",
    param: new BinaryWriteParameters() {
        ModKey = ModKeyOption.CorrectToPath
    }
);
