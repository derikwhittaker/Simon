using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace Dimesoft.Simon.Domain.Managers
{
    public interface IAudioManager
    {
        Task Play(string fileName);
        //Task PlayItemSelected();
        //Task PlaySelectedItemMatch();
        //Task PlayWonGame();
    }

    public class AudioManager : IAudioManager
    {
        private MediaElement _mediaElement;

        public async Task Play(string fileName)
        {
            var packageLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var assetsFolder = await packageLocation.GetFolderAsync("Assets");
            var soundsFolder = await assetsFolder.GetFolderAsync("Audio");
            StorageFile myAudio = await soundsFolder.GetFileAsync(fileName);

            _mediaElement = new MediaElement();

            var stream = await myAudio.OpenAsync(FileAccessMode.Read);
            _mediaElement.SetSource(stream, myAudio.ContentType);

            _mediaElement.Play();
        }

        //public async Task PlayItemSelected()
        //{
        //    if (!_storageManager.Preferences().Result.PlayAudio) { return; }

        //    await Play("SelectItemAudio.mp3");
        //}

        //public async Task PlaySelectedItemMatch()
        //{
        //    if (!_storageManager.Preferences().Result.PlayAudio) { return; }

        //    await Play("SelectedItemMatchAudio.mp3");
        //}

        //public async Task PlayWonGame()
        //{
        //    if (!_storageManager.Preferences().Result.PlayAudio) { return; }

        //    await Play("WonGameAudio.mp3");
        //}
    }
}
