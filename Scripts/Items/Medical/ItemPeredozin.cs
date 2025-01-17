﻿namespace AtomicTorch.CBND.CoreMod.Items.Medical
{
  using AtomicTorch.CBND.CoreMod.Characters;
  using AtomicTorch.CBND.CoreMod.CharacterStatusEffects;
  using AtomicTorch.CBND.CoreMod.CharacterStatusEffects.Invisible;
  using AtomicTorch.CBND.CoreMod.CharacterStatusEffects.Neutral;
  using AtomicTorch.CBND.CoreMod.ItemContainers;
  using AtomicTorch.CBND.CoreMod.SoundPresets;
  using AtomicTorch.CBND.CoreMod.Systems.Notifications;
  using AtomicTorch.CBND.GameApi.Data.Characters;

  public class ItemPeredozin : ProtoItemMedical
  {
    public const string NotificationNoMedicineOveruse_Message =
        "You don't have any symptoms of medicine overuse. There's no point in using Peredozin now.";

    public const string NotificationNoMedicineOveruse_Title = "No medicine overuse";

    public const string NotificationRemovePants_Message = "This is a rectal medicine! Remove your pants first!";

    public const string NotificationRemovePants_Title = "Pants are in the way!";

    public const string NotificationTooMuch_Message =
        "You've already used Peredozin recently, and you feel the next one will not fit...";

    public const string NotificationTooMuch_Title = "Too much!";

    public static double MedicalCooldownDuration => MedicineCooldownDuration.Medium;

    public override double CooldownDuration => MedicalCooldownDuration;

    public override string Description =>
        "Peredozin helps to alleviate the negative effects of frequently using strong medical items. Rectal application only.";

    public override double MedicalToxicity => 0;

    public override string Name => "Peredozin";

    protected override void PrepareEffects(EffectActionsList effects)
    {
      effects
          // removes any overdose instantly
          .WillRemoveEffect<StatusEffectMedicineOveruse>()
          // this effect prevents player from quickly putting armor back
          .WillAddEffect<StatusEffectPeredozinApplication>(isHidden: true)
          // this effect will prevent usage more than once in 10 minutes
          .WillAddEffect<StatusEffectAnalBlockage>();
    }

    protected override ReadOnlySoundPreset<ItemSound> PrepareSoundPresetItem()
    {
      return ItemsSoundPresets.ItemMedicalTablets;
    }

    protected override bool SharedCanUse(ICharacter character, PlayerCharacterCurrentStats currentStats)
    {
      // does the player even have meds overuse?
      if (!character.SharedHasStatusEffect<StatusEffectMedicineOveruse>())
      {
        if (IsClient)
        {
          NotificationSystem.ClientShowNotification(
              NotificationNoMedicineOveruse_Title,
              NotificationNoMedicineOveruse_Message,
              NotificationColor.Bad,
              icon: this.Icon);
        }

        return false;
      }

      // does the player have anal blockage?
      if (character.SharedHasStatusEffect<StatusEffectAnalBlockage>())
      {
        if (IsClient)
        {
          NotificationSystem.ClientShowNotification(
              NotificationTooMuch_Title,
              NotificationTooMuch_Message,
              NotificationColor.Bad,
              icon: this.Icon);
        }

        return false;
      }

      // does the player have pants equipped?
      if (ItemsContainerCharacterEquipment.HasArmorOrFullBodyEquipment(character))
      {
        if (IsClient)
        {
          NotificationSystem.ClientShowNotification(
              NotificationRemovePants_Title,
              NotificationRemovePants_Message,
              NotificationColor.Bad,
              icon: this.Icon);
        }

        return false;
      }

      return true;
    }
  }
}