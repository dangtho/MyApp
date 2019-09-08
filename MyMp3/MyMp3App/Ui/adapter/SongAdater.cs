using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace MyMp3.MyMp3App.Ui.adapter
{
    class SongAdater : RecyclerView.Adapter
    {
        private List<Song> songList;
        private LayoutInflater inflater;
        private Context context;
        public Action<int> play;
        public SongAdater(List<Song> songList, Context context)
        {
            this.songList = songList;
            this.context = context;
   
            inflater = LayoutInflater.From(context);
        }
        public override int ItemCount
        {
            get
            {
                return songList.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Song song = songList[position];
            var songHoldler = holder as SongHoldler;
            songHoldler.tvArtisrt.Text = song.nameSinger;
            songHoldler.tvName.Text = song.nameSong.Substring(song.nameSong.LastIndexOf("/") + 1);
            songHoldler.tvName.Click += (sender, e) =>
            {
                play.Invoke(position);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return new SongHoldler(inflater.Inflate(Resource.Layout.item_song, parent, false));
        }

        class SongHoldler : RecyclerView.ViewHolder
        {
            public TextView tvName { get; set; }
            public TextView tvArtisrt { get; set; }
            public TextView tvAbulm { get; set; }
            public TextView tvTime { get; set; }
       
            public SongHoldler(View itemView) : base(itemView)
            {
                tvName = itemView.FindViewById<TextView>(Resource.Id.nameSong);
                tvArtisrt = itemView.FindViewById<TextView>(Resource.Id.nameSinger);
                
            }
        }
    }
}