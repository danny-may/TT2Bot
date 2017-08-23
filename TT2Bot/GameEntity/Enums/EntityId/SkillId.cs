using TitanBot.Formatting;
using static TT2Bot.TT2Localisation.Enums.BonusTypeText;

namespace TT2Bot.GameEntity.Enums.EntityId
{
    public enum SkillId
    {
        Default,
        None,
        PetQTE,
        BossDmgQTE,
        PetGoldQTE,
        HelperDmgQTE,
        HelperCountQTE,
        Fairy,
        BossTimer,
        ClanQTE,
        OfflineGold,
        MeleeHelperDmg,
        SpellHelperDmg,
        RangedHelperDmg,
        PetDmg,
        LessMonsters,
        SplashDmg,
        AutoAdvance,
        MultiMonsters,
        PetOfflineDmg,
        BurstSkillBoost,
        FireTapSkillBoost,
        MPRegenBoost,
        MPCapacityBoost,
        HelperDmgSkillBoost,
        MidasSkillBoost,
        ManaStealSkillBoost,
        CritSkillBoost,
        CloneSkillBoost,
        ManaMonster
    }

    public static class SkillIdMethods
    {
        public static LocalisedString AsLocalisableValue(this SkillId skill, double value)
        {
            switch (skill)
            {
                case SkillId.None:
                case SkillId.Default:
                    return new LocalisedString(FORMATNONE);
                case SkillId.Fairy:
                case SkillId.SplashDmg:
                case SkillId.MultiMonsters:
                case SkillId.CritSkillBoost:
                case SkillId.ManaStealSkillBoost:
                    return new LocalisedString(FORMATCHANCE, value);
                case SkillId.BossTimer:
                case SkillId.HelperCountQTE:
                case SkillId.CloneSkillBoost:
                case SkillId.MPCapacityBoost:
                case SkillId.MPRegenBoost:
                    return new LocalisedString(FORMATADD, value);
                case SkillId.LessMonsters:
                    return new LocalisedString(FORMATMINUS, value);
                case SkillId.ManaMonster:
                case SkillId.AutoAdvance:
                    return new RawString("{0}", value);
                default:
                    return new LocalisedString(FORMATMULTIPLY, value);
            }
        }
    }
}
