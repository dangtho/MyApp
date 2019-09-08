using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MyMp3.MyMp3App.Modle;
using MyMp3.MyMp3App.service;
using MyMp3.MyMp3App.Ui.adapter;
using MyMp3.MyMp3App.Ui.Base;

namespace MyMp3.MyMp3App.Ui.fragment
{
    class MainMp3 : BaseFrament
    {
        public static int REQUEST_CODE_MP3 = 99;
        private View view;
        private Context context;
        public static SongManager songManager;
        private SongAdater songAdater;
        private RecyclerView rcvSongs;
        public static List<Song> songList;
        public static ImageView imgPrivious, imgPlay, imgNext;
        private static TextView tvNameSinger, tvNameSong;
        public static bool isPause = true;
        public static int currentSong;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.activity_mainmp3, container, false);
            CheckPermistion();
            SetNameCurrentSongPlay(songList[currentSong]);
            return view;
        }
        public void ini()
        {
            imgNext = view.FindViewById<ImageView>(Resource.Id.imgNext);
            imgPlay = view.FindViewById<ImageView>(Resource.Id.imgPlay);
            imgPrivious = view.FindViewById<ImageView>(Resource.Id.imgPrivious);
            tvNameSinger = view.FindViewById<TextView>(Resource.Id.nameSinger);
            tvNameSong = view.FindViewById<TextView>(Resource.Id.nameSong);
            songManager = new SongManager();
            songList = new List<Song>();
            songList.AddRange(songManager.getListSong(Activity));
            rcvSongs = view.FindViewById<RecyclerView>(Resource.Id.rcvListSong);
            rcvSongs.SetLayoutManager(new LinearLayoutManager(Context));
            songAdater = new SongAdater(songList, Context);
            rcvSongs.SetAdapter(songAdater);
            songAdater.play += (position) =>
            {
                SetNameCurrentSongPlay(songList[position]);
                currentSong = position;
                songManager.Stop();
                songManager.Play(songList[position].nameSong);
                isPause = false;
                imgPlay.SetImageResource(Resource.Drawable.ic_paused);
                Intent intent = new Intent(Context, typeof(SongService));
                Context.StartService(intent);
            };
            imgNext.Click += (sender, args) =>
            {
                NextSong();
            };
            imgPlay.Click += (sender, args) =>
             {
                 PauseSong();
             };
            imgPrivious.Click += (sender, args) =>
             {
                 PriSong();
             };
        }
        public static void NextSong()
        {
            if (currentSong < songList.Count - 1)
            {
                SetNameCurrentSongPlay(songList[currentSong + 1]);
                songManager.Stop();
                songManager.Play(songList[currentSong + 1].nameSong);
                currentSong++;
            }
        }
        public static void PriSong()
        {
            if (currentSong > 0)
            {
                SetNameCurrentSongPlay(songList[currentSong - 1]);
                songManager.Stop();
                songManager.Play(songList[currentSong - 1].nameSong);
                currentSong--;
            }
        }
        public static void PauseSong()
        {
            if (!isPause)
            {
                songManager.OnStop();
                isPause = true;
                imgPlay.SetImageResource(Resource.Drawable.ic_play);
            }
            else
            {
                if (songManager.state == SongManager.IDLE)
                {
                    songManager.Play(songList[currentSong].nameSong);
                    imgPlay.SetImageResource(Resource.Drawable.ic_paused);
                }
                else
                {
                    songManager.OnResume();
                    isPause = false;
                    imgPlay.SetImageResource(Resource.Drawable.ic_paused);
                }
            }
        }
        public static void SetNameCurrentSongPlay(Song song)
        {
            tvNameSinger.Text = song.nameSinger;
            tvNameSong.Text = song.nameSong.Substring(song.nameSong.LastIndexOf("/") + 1);
        }
        public bool HasPermistion(Context context, String[] permissions)
        {
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.M && context != null && permissions != null)
            {
                foreach (var per in permissions)
                {
                    if (ActivityCompat.CheckSelfPermission(context, per) != Permission.Granted)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void CheckPermistion()
        {
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.M)
            {
                String[] permissions = { Android.Manifest.Permission.ReadExternalStorage };

                if (!HasPermistion(Context, permissions))
                {
                    ActivityCompat.RequestPermissions(Activity, permissions, REQUEST_CODE_MP3);
                }
                else
                {
                    ini();
                }
            }
            else
            {
                ini();
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (requestCode == REQUEST_CODE_MP3)
            {
                if (grantResults.Length == 1 && grantResults[0] == Permission.Granted)
                {
                    ini();
                }
                else
                {
                    CheckPermistion();
                }
            }
        }
    }
}