using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyMp3.MyMp3App.Modle
{
    class SongManager
    {
        private MediaController media;
        private MediaPlayer mediaPlayer;
        public static String PLAY = "play";
        public static String STOP = "stop";
        public static String PAUSE = "pause";
        private List<Song> listSong;
        public void OnStop()
        {

        }
        public void OnResume()
        {

        }
        public List<Song> getListSong()
        {
         
        }
    }
}