﻿namespace AtomicTorch.CBND.CoreMod.Technologies.Tier1.Defense
{
  using AtomicTorch.CBND.CoreMod.CraftRecipes;

  public class TechNodeHipbag : TechNode<TechGroupDefenseT1>
  {
    protected override void PrepareTechNode(Config config)
    {
      config.Effects
              .AddRecipe<RecipeHipbag>();

      config.SetRequiredNode<TechNodeClothArmor>();
    }
  }
}