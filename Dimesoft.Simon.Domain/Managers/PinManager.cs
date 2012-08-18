using System;
using System.Threading.Tasks;
using NotificationsExtensions.TileContent;
using Windows.ApplicationModel.Core;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace Dimesoft.Simon.Domain.Managers
{
    public interface IPinManager
    {

        bool IsPinned(string pinnedItemId);

        Task<bool> Pin(string shortName, string description, string tileId, string tileActivationArgs, string tileLogoPath, string smallTileLogoPath);
        Task<bool> UnPin(string pinnedItemId);

        void UpdateMainLiveTile();
        //void UpdateSecondaryTiles();
    }

    public class PinManager : IPinManager
    {

        public bool IsPinned(string pinnedItemId)
        {
            var result = SecondaryTile.Exists(pinnedItemId);

            return result;
        }

        public async Task<bool> Pin(string shortName, string description, string tileId, string tileActivationArgs, string tileLogoPath, string smallTileLogoPath)
        {
            var logo = new Uri(string.Format("ms-appx:///{0}", tileLogoPath));
            var smallLogo = new Uri(string.Format("ms-appx:///{0}", smallTileLogoPath));

            var tile = new SecondaryTile(tileId, shortName, description, tileActivationArgs, TileOptions.ShowNameOnLogo, logo)
                           {
                               ForegroundText = ForegroundText.Light, 
                               SmallLogo = smallLogo
                           };

            var result = await tile.RequestCreateAsync();

            return result;
        }

        public async Task<bool> UnPin(string pinnedItemID)
        {
            if (IsPinned(pinnedItemID))
            {
                var tile = new SecondaryTile(pinnedItemID);
                var result = await tile.RequestDeleteAsync();

                return result;
            }

            return false;
        }

        public async void UpdateMainLiveTile()
        {
            var applicationTile = TileContentFactory.CreateTileWidePeekImageAndText02();
            var squareApplicationTile = TileContentFactory.CreateTileSquareText03();
            var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication(CoreApplication.Id);

            // clear the existing tile info
            tileUpdater.Clear();

            applicationTile.TextBody1.Text = "Leaders:";
            applicationTile.TextBody2.Text = "Larry - 11 Moves";
            applicationTile.TextBody3.Text = "Ryan  - 10 Moves";
            applicationTile.TextBody4.Text = "Ryan  - 09 Moves";
            applicationTile.TextBody5.Text = "Jakob - 09 Moves";

            applicationTile.RequireSquareContent = false;
            applicationTile.Image.Src = "/Assets/LargeTile.png";
            applicationTile.SquareContent = squareApplicationTile;

            var tileNotification = applicationTile.CreateNotification();
            tileUpdater.Update(tileNotification);

            //var applicationTile = TileContentFactory.CreateTileWideText05();
            //var squareApplicationTile = TileContentFactory.CreateTileSquareText03();
            //var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication(CoreApplication.Id);

            //// clear the existing tile info
            //tileUpdater.Clear();

            //applicationTile.TextBody1.Text = "Leaders:";
            //applicationTile.TextBody1.Text = "Larry - 13 Moves";
            //applicationTile.TextBody2.Text = "Larry - 11 Moves";
            //applicationTile.TextBody3.Text = "Ryan  - 10 Moves";
            //applicationTile.TextBody4.Text = "Ryan  - 09 Moves";

            //applicationTile.RequireSquareContent = false;
            ////applicationTile.Image.Src = "/Assets/WidePinTile.png";
            //applicationTile.SquareContent = squareApplicationTile;

            //var tileNotification = applicationTile.CreateNotification();
            //tileUpdater.Update(tileNotification);
        }

        //public async void UpdateSecondaryTiles()
        //{
        //    var allTiles = await SecondaryTile.FindAllAsync();

        //    foreach (var secondaryTile in allTiles)
        //    {
        //        var parts = secondaryTile.TileId.Split('.');
        //        if (parts.Any())
        //        {
        //            var board = parts[1];
        //            var level = parts[2];

        //            var foundItems = await _storageManager.LeaderBoardResultsAsync(board, level);

        //            if (foundItems.Any())
        //            {

        //                var tileImageSrc = _optionsFactory.SecondaryTileForCategory(foundItems.First().GameCategory);

        //                var orderedResults = foundItems.OrderBy(x => x.Attempts).ToArray();
        //                var item1 = TryGetSecondaryTileArrayItem(orderedResults, 0);
        //                var item2 = TryGetSecondaryTileArrayItem(orderedResults, 1);
        //                var item3 = TryGetSecondaryTileArrayItem(orderedResults, 2);

        //                var updatableSecondaryTile = TileContentFactory.CreateTileSquarePeekImageAndText01();
        //                var tileUpdater = TileUpdateManager.CreateTileUpdaterForSecondaryTile(secondaryTile.TileId);
        //                tileUpdater.Clear();

        //                updatableSecondaryTile.Image.Src = tileImageSrc;
        //                updatableSecondaryTile.TextHeading.Text = "Scores";
        //                updatableSecondaryTile.TextBody1.Text = item1;
        //                updatableSecondaryTile.TextBody2.Text = item2;
        //                updatableSecondaryTile.TextBody3.Text = item3;

        //                var tileNotification = updatableSecondaryTile.CreateNotification();
        //                tileUpdater.Update(tileNotification);

        //            }
        //        }
        //    }
        //}

        //private static string TryGetAppTileArrayItem(GameResultDTO[] asArray, int index)
        //{
        //    try
        //    {
        //        var asDto = asArray[index];

        //        return string.Format("{0} [{1}] in {2} attempts by {3}", asDto.GameCategory, asDto.GameLevel, asDto.Attempts, asDto.PlayerName);
        //    }
        //    catch (Exception e)
        //    {
        //        return "";
        //    }
        //}

        //private static string TryGetSecondaryTileArrayItem(GameResultDTO[] asArray, int index)
        //{
        //    try
        //    {
        //        var asDto = asArray[index];

        //        return string.Format("{0} Moves: {1}", asDto.Attempts, asDto.PlayerName);
        //    }
        //    catch (Exception e)
        //    {
        //        return "";
        //    }
        //}

    }
}
