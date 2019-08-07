
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
using Android.Database.Sqlite;
using Android.Database;
using Java.Util;

namespace BMI
{
    [Activity(Label = "ViewAll")]
    public class ViewAll : Activity
    {
        SQLiteDatabase db;
        Spinner ddMonth;

        Button btnViewAll;
        Button btnGo;

        ListView lvwProgress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_view_all);

            // Create your application here

            ddMonth = (Spinner)FindViewById(Resource.Id.ddMonth);
            ArrayAdapter<string> adbMonth = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleDropDownItem1Line, months());
            ddMonth.Adapter = adbMonth;

            lvwProgress = (ListView)FindViewById(Resource.Id.lvwProgress);


            btnViewAll = (Button)FindViewById(Resource.Id.btnViewAll);
            btnGo = (Button)FindViewById(Resource.Id.btnGo);

            btnViewAll.Click += (object sender, System.EventArgs e) =>
            {
                createDB();
                FindAll("");
            };

            btnGo.Click += (object sender, System.EventArgs e) =>
            {
                createDB();
                FindAll(ddMonth.SelectedItem.ToString());
            };


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

        private void createDB()
        {
            db = Application.Context.OpenOrCreateDatabase("BmiApp", FileCreationMode.Private, null);
            db.ExecSQL("Create TABLE IF NOT EXISTS bmiInfo(id INTEGER PRIMARY KEY, month VARCHAR(20),week INTEGER, weight Double(18,2))");
        }

        protected void FindAll(String Month)
        {
            try
            {
                List<string> data = new List<string>();
                String query;
                double PreviousWeight = 0;
                if (Month=="")
                {
                    query = "Select month, week, weight from bmiInfo";
                }
                else
                {
                    query = "Select month, week, weight from bmiInfo where month = '" + Month + "'";
                }

               ICursor cursor = db.RawQuery(query, null);

                if (cursor.MoveToFirst())
                {
                    int count = 0;

                    do
                    {
                        String month = cursor.GetString(0).Substring(0, 3);
                        String Week = cursor.GetString(1);
                        String weight = cursor.GetString(2).Substring(0, 4);
                        double Difference;

                        if (count == 0)
                            Difference = 0;
                        else
                            Difference = Double.Parse(weight) - PreviousWeight;

                        String comment = "";

                        if (Difference > 0)
                        {
                            comment = "BMI Increased";
                        }
                        else if (Difference == 0)
                        {
                            comment = "No difference";
                        }
                        else if (Difference < 0)
                        {
                            comment = "BMI decreased";
                        }

                        data.Add(month + "\t\t\t\t" + Week + "\t\t\t\t" + weight + "\t\t\t" + Difference.ToString().Substring(0, 1)
                                + "\t\t" + comment);
                        PreviousWeight = Double.Parse(weight);
                        count++;
                    }
                    while (cursor.MoveToNext());

                    ArrayAdapter<string> adapter = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleExpandableListItem1,
                        data);
                    lvwProgress.Adapter = adapter;
                }
                else
                {
                    Toast.MakeText(Application.Context, "No Data Found", ToastLength.Long).Show();
                }



            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.Message,ToastLength.Long).Show();
            }
        }

    }
}
