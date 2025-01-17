﻿namespace AtomicTorch.CBND.CoreMod.Technologies.Tier3.Defense
{
  using AtomicTorch.CBND.CoreMod.CraftRecipes;

  public class TechNodeBackpackMilitary : TechNode<TechGroupDefenseT3>
  {
    protected override void PrepareTechNode(Config config)
    {
      config.Effects
              .AddRecipe<RecipeBackpackMilitary>();

      config.SetRequiredNode<TechNodeMilitaryArmor>();
    }
  }
}