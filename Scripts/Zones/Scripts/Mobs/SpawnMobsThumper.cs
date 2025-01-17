﻿namespace AtomicTorch.CBND.CoreMod.Zones
{
  using AtomicTorch.CBND.CoreMod.Characters.Mobs;
  using AtomicTorch.CBND.CoreMod.Triggers;
  using System;

  /// <summary>
  /// Very rare spawn of Thumper in Barren biome.
  /// (They appear mostly during an event - see EventMigrationThumper)
  /// </summary>
  public class SpawnMobsThumper : ProtoZoneSpawnScript
  {
    protected override void PrepareZoneSpawnScript(Triggers triggers, SpawnList spawnList)
    {
      triggers
          .Add(GetTrigger<TriggerWorldInit>())
          .Add(GetTrigger<TriggerTimeInterval>().ConfigureForSpawn(TimeSpan.FromMinutes(30)));

      spawnList.CreatePreset(interval: 140, padding: 1.5, useSectorDensity: false)
               .AddExact<MobThumper>()
               .SetCustomPaddingWithSelf(70);
    }
  }
}