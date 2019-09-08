using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyMp3.MyMp3App
{
    class Song
    {
        public string nameSong { get; set; }
        public string nameSinger { get; set; }
        public string nameAbulm { get; set; }
        public long time { get; set; }
        public int state { get; set; }
        public Song(string nameSong,string nameSinger,string nameAbum,long time)
        {
            this.nameAbulm = nameAbulm;
            this.nameSinger = nameSinger;
            this.nameSong = nameSong;
            this.time = time;
        } 
    }
}