
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;

using Android.OS;
using Android.Runtime;
using Android.Content;
using Android.Widget;
using Android.Database.Sqlite;
using Java.Lang;

namespace BMI
{
    [Activity(Label = "SaveBMI")]
    public class SaveBMI : Activity
    {


        SQLiteDatabase db;
        EditText txtWeight;
        Spinner ddFeet;
        Spinner ddInches;
        Spinner ddMonth;
        Spinner ddWeek;
        Button btnSave;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.save_activity);

            // Create your application here


            txtWeight = (EditText)FindViewById(Resource.Id.txtWeight);

            ddFeet = (Spinner)FindViewById(Resource.Id.ddFeet_Save);
            ddInches = (Spinner)FindViewById(Resource.Id.ddInches_Save);

            ddMonth = (Spinner)FindViewById(Resource.Id.ddMonth_Save);
            ddWeek = (Spinner)FindViewById(Resource.Id.ddWeek_Save);

            btnSave = (Button)FindViewById(Resource.Id.btnSave_Save);

            ArrayAdapter<string> adapterInches = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleDropDownItem1Line, spinnerInch());
            ddInches.Adapter = adapterInches;

            ArrayAdapter<string> adapterFeet = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleDropDownItem1Line, spinnerFeet());
            ddFeet.Adapter =adapterFeet;

            ArrayAdapter<string> adbMonth = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleDropDownItem1Line, months());
            ddMonth.Adapter = adbMonth;

            ArrayAdapter<string> adbWeek = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleDropDownItem1Line, weeks());
            ddWeek.Adapter = adbWeek;

            btnSave.Click += (object sender, System.EventArgs e) =>
            {
                createDB();
                Save();
            };
        }





        public void Save()
        {
            int feet = int.Parse(ddFeet.SelectedItem.ToString());
            int inches = int.Parse(ddInches.SelectedItem.ToString()) + (feet * 12);
            double meters = inches * 0.0254;

            int weight = int.Parse(txtWeight.Text.ToString());
            double Bmi = weight / (meters * meters);

            string month = ddMonth.SelectedItem.ToString();
            string week = ddWeek.SelectedItem.ToString();

            string query = "Insert into bmiInfo(month, week, weight) values ('" + month + "', " + week + ", " + Bmi.ToString() + ")";
            db.ExecSQL(query);
            Toast.MakeText(Application.Context, "Saved", ToastLength.Long).Show();
        }


        private string[] months()
        {
            string[] months = new string[12];
            months[0] = "January";
            months[1] = "February";
            months[2] = "March";
            months[3] = "April";
            months[4] = "May";
            months[5] = "June";
            months[6] = "July";
            months[7] = "August";
            months[8] = "September";
            months[9] = "October";
            months[10] = "November";
            months[11] = "December";

            return months;

        }


        private string[] weeks()
        {
            string[] weeks = new string[4];
            weeks[0] = "1";
            weeks[1] = "2";
            weeks[2] = "3";
            weeks[3] = "4";


            return weeks;

        }

        public string[] spinnerInch()
        {
            string[] inches = new string[12];
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

        public string[] spinnerFeet()
        {
            string[] inches = new string[8];
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


        private void createDB()
        {
            db = Application.Context.OpenOrCreateDatabase("BmiApp", FileCreationMode.Private, null);
            db.ExecSQL("Create TABLE IF NOT EXISTS bmiInfo(id INTEGER PRIMARY KEY, month VARCHAR(20),week INTEGER, weight Double(18,2))");
        }


        

    }
}
