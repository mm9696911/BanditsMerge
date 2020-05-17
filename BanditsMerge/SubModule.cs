using System;
using ModLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace BanditsMerge
{
    public class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleFolderName = "BanditsMerge";

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            try
            {
                FileDatabase.Initialise(ModuleFolderName);
            }
            catch (Exception ex)
            {
                InformationManager.DisplayMessage(new InformationMessage(
                    "Could not Initialise ModLib for BanditsMerge: " + ex.Message, Color.FromUint(4282569842U)));
            }
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            try
            {
                var listener = new BattleEndEventListener();
                CampaignEvents.OnPlayerBattleEndEvent.AddNonSerializedListener(listener,
                    listener.MergeNearbyBanditsAfterBanditFight);
            }
            catch (Exception ex)
            {
                InformationManager.DisplayMessage(new InformationMessage(
                    "An error has occurred during OnGameLoaded for BanditsMerge: " + ex.Message,
                    Color.FromUint(4282569842U)));
            }
        }
    }
}