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
//using Mutagen.Bethesda.Plugins;

//var env = GameEnvironment.Typical.Skyrim(SkyrimRelease.SkyrimSE);

var mod = SkyrimMod.CreateFromBinary(
    ModPath.FromPath("HelloWorldMod.esp"),
    release: SkyrimRelease.SkyrimSE
);

ILinkCache scriptLinkCache = mod.ToMutableLinkCache();

var questKey = FormKey.Factory("000D61:HelloWorldMod.esp");

if (scriptLinkCache.TryResolve<Quest>(questKey, out var quest)) {
    Console.WriteLine($"We found our quest via FormKey: {quest.EditorID}");
}

foreach (var helloQuest in mod.EnumerateMajorRecords<IQuest>()) {
    var vmad = helloQuest.VirtualMachineAdapter;
    if (vmad?.Scripts != null 
    && vmad.Scripts.Find(x => x.Name == "HelloModQuestScript") != null) {
        Console.WriteLine($"We found this quest by searching for the script: {quest.EditorID}");
    }
}

foreach (var anyQuest in mod.EnumerateMajorRecords<IQuest>()) {
    anyQuest.EditorID += "PatchedAndScripted";
    Console.WriteLine($"{anyQuest.EditorID}");
}

mod.WriteToBinary(
    "HelloWorldModPatch.esp",
    param: new BinaryWriteParameters() {
        ModKey = ModKeyOption.CorrectToPath
    }
);

// var formLink = new FormLink<IFormListGetter>(FormKey.Factory("000D61:HelloWorldMod.esp"));

// if (formLink.TryResolve(scriptLinkCache, out var foundRecord)) {
//     foreach (var item in foundRecord.Items) {
//         item.
//     }
// }



//Console.WriteLine("Hello world!");
