using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using MyMp3.MyMp3App.Ui.Base;

namespace MyMp3.MyMp3App.Ui.fragment
{
    class MainMp3 : BaseFrament
    {
        public static int REQUEST_CODE_MP3 = 99;
        private View view;
        private Context context;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.activity_main, container, false);
            ini();
            return view;
        }
        public void ini() { }
        public void CheckPermistion()
        {
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.M)
            {
                String[] permissions = { Android.Manifest.Permission.ReadExternalStorage, Android.Manifest.Permission.WriteExternalStorage };

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
    }
}