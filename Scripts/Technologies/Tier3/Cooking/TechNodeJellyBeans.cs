﻿namespace AtomicTorch.CBND.CoreMod.Technologies.Tier3.Cooking
{
    using AtomicTorch.CBND.CoreMod.CraftRecipes;

    public class TechNodeJellyBeans : TechNode<TechGroupCookingT3>
    {
        protected override void PrepareTechNode(Config config)
        {
            config.Effects
                  .AddRecipe<RecipeJellyBeans>();

            config.SetRequiredNode<TechNodeButter>();
        }
    }
}