using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace BanditsMerge
{
    internal class BattleEndEventListener
    {
        public BattleEndEventListener()
        {
            BanditDeathCounter = 0;
        }

        private int BanditDeathCounter { get; set; }

        public void MergeNearbyBanditsAfterBanditFight(MapEvent m)
        {
            if (!m.GetLeaderParty(m.DefeatedSide).MapFaction.IsBanditFaction || !Settings.Instance.MergeEnabled)
                return;

            var banditSide = m.PlayerSide == BattleSideEnum.Attacker ? m.DefenderSide : m.AttackerSide;

            BanditDeathCounter += banditSide.Casualties;

            InformationManager.DisplayMessage(new InformationMessage("Bandit Casualties: " + BanditDeathCounter,
                Color.Black));

            var nearbyBanditParties = MobileParty.All.ToList().FindAll(i =>
                    i.IsBandit
                    && i.Party.NumberOfAllMembers > 0
                    && i.Party.NumberOfAllMembers <= Settings.Instance.BanditNumber
                    && i.Party.Culture.StringId == banditSide.LeaderParty.Culture.StringId
                    && i.MapEvent == null
                    && i.Position2D.DistanceSquared(m.Position) <= Settings.Instance.Radius)
                .OrderBy(o => o.Party.TotalStrength).Take(2).ToList();

            if (nearbyBanditParties.Count < 2)
                return;

            MobileParty temp = null;
            foreach (var mp in nearbyBanditParties)
                if (temp != null)
                {
                    InformationManager.DisplayMessage(new InformationMessage(
                        $"{mp.Party.Culture} has increase {temp.Party.NumberOfAllMembers} mans",
                        Color.FromUint(4282569842U)));
                    MergePartiesAction.Apply(mp.Party, temp.Party);
                    temp = null;
                }
                else
                {
                    temp = mp;
                }

            ResetBanditDeathCounter();
        }


        private void ResetBanditDeathCounter()
        {
            BanditDeathCounter = 0;
        }
    }
}