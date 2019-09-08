using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Media.App;
using Android.Views;
using Android.Widget;
using MyMp3.MyMp3App.Modle;
using MyMp3.MyMp3App.Ui.fragment;

namespace MyMp3.MyMp3App.service
{
    class SongService : Service
    {
        private SongManager songManager;
        private Song song;
        private Notification notification;
        private static string nextAction = "com.music.next";
        private static string priAction = "com.music.pri";
        private static string playAction = "com.music.play";

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
             if (intent == null)
            {
                StopSelf();
                return StartCommandResult.NotSticky;
            }
            else
            {
                StarForceGroundService();
                if (intent.Action == playAction)
                {
                    MainMp3.PauseSong();
                }
                else if (intent.Action == priAction)
                {
                    MainMp3.PriSong();
                }
                else
                {
                    MainMp3.NextSong();
                }
            }
            return StartCommandResult.NotSticky;
        }
        public void StarForceGroundService()
        {
            Intent nextIntent = new Intent(this, typeof(SongService));
            Intent priIntent = new Intent(this, typeof(SongService));
            Intent playIntent = new Intent(this, typeof(SongService));

            nextIntent.SetAction(nextAction);
            priIntent.SetAction(priAction);
            playIntent.SetAction(playAction);

            PendingIntent nextPendingIntent = PendingIntent.GetService(this, 0, nextIntent, 0);
            PendingIntent priPendingIntent = PendingIntent.GetService(this, 0, priIntent, 0);
            PendingIntent playtPendingIntent = PendingIntent.GetService(this, 0, playIntent, 0);
            int[] action;
            notification = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.default_album_art)
                .SetContentText(song.nameSong.Substring(song.nameSong.LastIndexOf("/") + 1))
                .SetContentTitle(song.nameSinger)
                .AddAction(Resource.Drawable.ic_privous, "", priPendingIntent)
                .AddAction(Resource.Drawable.ic_play, "", playtPendingIntent)
                .AddAction(Resource.Drawable.ic_next, "", nextPendingIntent)
                  .Build();
           StartForeground(100, notification);
        }
    }
}