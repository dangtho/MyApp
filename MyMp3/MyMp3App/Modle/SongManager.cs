using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyMp3.MyMp3App.Ui.fragment;
using static Android.Media.MediaPlayer;

namespace MyMp3.MyMp3App.Modle
{
    class SongManager
    {
        private MediaController media;
        private MediaPlayer mediaPlayer;
        public static int PLAYING = 1;
        public static int PAUSE = 2;
        public static int IDLE = 0;
        public int state { get; set; }
        private List<Song> listSong;
        private Action<IOnPreparedListener> startMediaPlayer, stopMediaPlayer;

        public IntPtr Handle => throw new NotImplementedException();

        public SongManager()
        {
            state = IDLE;
        }
        public void OnStop()
        {
            if (state == PLAYING)
            {
                mediaPlayer.Pause();
                state = PAUSE;
            }

        }
        public void OnResume()
        {
            if (state == PAUSE)
            {
                mediaPlayer.Start();
                state = PLAYING;
            }

        }
        public void Stop()
        {
            if (state != IDLE)
            {
                mediaPlayer.Stop();
                mediaPlayer.Release();
                mediaPlayer = null;
                state = IDLE;
            }
        }

        public void Play(string data)
        {
            if (state == IDLE)
            {
                mediaPlayer = new MediaPlayer();
                try
                {
                    mediaPlayer.SetDataSource(data);
                    mediaPlayer.Prepare();
                    mediaPlayer.SetVolume(10, 10);
                    mediaPlayer.Prepared += (sender, args) =>
                    {
                        mediaPlayer.Start();
                        state = PLAYING;
                    };
                    mediaPlayer.Completion += (sender, args) =>
                    {
                        state = IDLE;
                        MainMp3.SetNameCurrentSongPlay(MainMp3.songList[MainMp3.currentSong + 1]);
                        Play(MainMp3.songList[MainMp3.currentSong+1].nameSong);
                        MainMp3.currentSong++;
                     
                    };

                }
                catch (IOException e)
                {

                }
            }

        }

        public List<Song> getListSong(Activity activity)
        {
            List<Song> songList = new List<Song>();
            string[] protection = { MediaStore.Audio.AudioColumns.Data, MediaStore.Audio.AudioColumns.Album,
                MediaStore.Audio.AudioColumns.Artist,MediaStore.Audio.AudioColumns.Duration };
            ICursor cursor = activity.ContentResolver
                .Query(MediaStore.Audio.Media.ExternalContentUri, protection, null, null, null);
            if (cursor != null)
            {
                if (cursor.Count == 0)
                {
                    cursor.Close();
                    return null;
                }
                else
                {
                    cursor.MoveToFirst();
                    while (!cursor.IsAfterLast)
                    {
                        string data = cursor.GetString(cursor.GetColumnIndex(MediaStore.Audio.AudioColumns.Data));
                        long time = cursor.GetLong(cursor.GetColumnIndex(MediaStore.Audio.AudioColumns.Duration));
                        string abulm = cursor.GetString(cursor.GetColumnIndex(MediaStore.Audio.AudioColumns.Album));
                        string artist = cursor.GetString(cursor.GetColumnIndex(MediaStore.Audio.AudioColumns.Artist));
                        songList.Add(new Song(data, artist, abulm, time));
                        cursor.MoveToNext();
                    }
                }
            }
            cursor.Close();
            return songList;
        }

    }

}