using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android.Content;

namespace BMI
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText txtWeight;
        Spinner ddFeet;
        Spinner ddInches;
        Button btnCalculate;
        Button btnViewDietPlan;
        TextView lblResult;
        TextView lblCategory;
        Button btnSaveBMI;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            txtWeight = FindViewById<EditText>(Resource.Id.txtWeight);
            ddFeet = FindViewById<Spinner>(Resource.Id.ddFeet);
            ddInches = FindViewById<Spinner>(Resource.Id.ddInches);
            btnCalculate = FindViewById<Button>(Resource.Id.btCalculate);
            btnViewDietPlan = FindViewById<Button>(Resource.Id.btnViewDietPlan);
            btnSaveBMI = FindViewById<Button>(Resource.Id.btnSave);
            lblResult = FindViewById<TextView>(Resource.Id.lblResult);
            if (savedInstanceState != null)
            {
                lblResult.Text = savedInstanceState.GetCharSequence("bmi");
            }
            lblCategory = FindViewById<TextView>(Resource.Id.lblCategory);
            if (savedInstanceState != null)
            {
                lblCategory.Text = savedInstanceState.GetCharSequence("category");
            }

            ArrayAdapter<String> adapterInches = new ArrayAdapter<String>(Android.App.Application.Context, Android.Resource.Layout.SimpleDropDownItem1Line, SpinnerInch());
            ArrayAdapter<String> adapterFeet = new ArrayAdapter<String>(Android.App.Application.Context, Android.Resource.Layout.SimpleDropDownItem1Line, SpinnerFeet());

            ddInches.Adapter = adapterInches;
            ddFeet.Adapter = adapterFeet;

            btnCalculate.Click += (object sender, System.EventArgs e) =>
            {
                Calculate();
            };

            btnViewDietPlan.Click += (object sender, System.EventArgs e) =>
            {
                GoToDietPlan();
            };

            btnSaveBMI.Click += (object sender, System.EventArgs e) =>
            {
                GoToSave();
            };
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            //outState.PutCharSequence("weight",txtWeight.Text);
            //outState.PutInt("feet", int.Parse(ddFeet.SelectedItem.ToString()));
            //outState.PutInt("inches", int.Parse(ddInches.SelectedItem.ToString()));

            outState.PutCharSequence("bmi", lblResult.Text);
            outState.PutCharSequence("category", lblCategory.Text);
        }

        public String[] SpinnerInch()
        {
            String[] inches = new String[12];
            inches[0] = "0";
            inches[1] = "1";
            inches[2] = "2";
            inches[3] = "3";
            inches[4] = "4";
            inches[5] = "5";
            inches[6] = "6";
            inches[7] = "7";
            inches[8] = "8";
            inches[9] = "9";
            inches[10] = "10";
            inches[11] = "11";

            return inches;
        }

        public String[] SpinnerFeet()
        {
            String[] inches = new String[8];
            inches[0] = "0";
            inches[1] = "1";
            inches[2] = "2";
            inches[3] = "3";
            inches[4] = "4";
            inches[5] = "5";
            inches[6] = "6";
            inches[7] = "7";

            return inches;
        }


        public void Calculate()
        {
            int feet = int.Parse(ddFeet.SelectedItem.ToString());
            int inches = int.Parse(ddInches.SelectedItem.ToString()) + (feet * 12);
            double meters = inches * 0.0254;

            int weight = int.Parse(txtWeight.Text.ToString());
            double Bmi = weight / (meters * meters);

            lblResult.Text = "Your BMI is " + Bmi.ToString().Substring(0, 4);
            String text = "You are ";

            if (Bmi < 18.5)
            {
                lblCategory.Text = text + "Underweight";
            }

            else if (Bmi < 25)
            {
                lblCategory.Text = text + "Normal Weight";
            }

            else if (Bmi < 30)
            {
                lblCategory.Text = text + "Overweight";
            }

            else
            {
                lblCategory.Text = text + "Obese";
            }
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        private void CalculateOnClick(object sender, EventArgs eventArgs)
        {
            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected void GoToDietPlan()
        {
            Intent dietPlan = new Intent(Android.App.Application.Context, typeof(ViewDietPlan));
            dietPlan.PutExtra("Category",lblCategory.Text.ToString().Substring(7));
            StartActivity(dietPlan);
        }

        protected void GoToSave()
        {
            Intent saveBMI = new Intent(Android.App.Application.Context, typeof(SaveBMI));
            //dietPlan.PutExtra("Category", lblCategory.Text.ToString().Substring(7));
            StartActivity(saveBMI);
        }

    }
}

