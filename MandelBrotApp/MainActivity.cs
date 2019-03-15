using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace MandelBrotApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            TextView debugOutput = FindViewById<TextView>(Resource.Id.debugView);
            Core.PrecisionDecimal p1 = new Core.PrecisionDecimal(1,200000);
            Core.PrecisionDecimal p2 = new Core.PrecisionDecimal(1753131, 198731);
            debugOutput.Text = (p1 * p2).ToString();

        }
    }
}