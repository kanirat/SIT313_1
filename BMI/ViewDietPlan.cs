
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

namespace BMI
{
    [Activity(Label = "ViewDietPlan")]
    public class ViewDietPlan : Activity
    {
        TextView lblDietView;
        TextView lblCategory;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.diet_plan_activity);

            lblDietView = (TextView)FindViewById(Resource.Id.lblDietTips);
            lblCategory = (TextView)FindViewById(Resource.Id.lblCategory);

            string category = Intent.GetStringExtra("Category").Trim();

            lblCategory.Text = category;




            if (category == "Underweight")
            {
                lblDietView.Text = "•\tAdd healthy calories. Without radically changing your diet, you can increase your calorie intake with each meal by adding nut or seed toppings, cheese and healthy side dishes\n" +
                        "•\tGo nutrient dense. Instead of eating a lot of empty calories and junk food, focus on eating foods that are rich in nutrients. Consider high-protein meats, which can help you to build muscle.\n" +
                        "•\tSnack away. Enjoy snacks that contain plenty of protein and healthy carbohydrates. Consider options like trail mix, protein bars or drinks, and crackers with hummus or peanut butter.\n" +
                        "•\tEat mini-meals. If you’re struggling with a curbed appetite due to medical or emotional issues, taking in large portions of food may not seem appealing. Consider eating smaller meals throughout the day to increase your calorie intake.";
            }
            else if (category == "Normal Weight")
            {
                lblDietView.Text = "•\tFresh, Frozen, or Canned Fruits ― don't think just apples or bananas. " +
                        "All fresh, frozen, or canned fruits are great choices. Be sure to try some \"exotic\" fruits, too. " +
                        "How about a mango? Or a juicy pineapple or kiwi fruit!\n" +
                        "•\tFresh, Frozen, or Canned Vegetables ― try something new. You may find that you love grilled vegetables or" +
                        " steamed vegetables with an herb you haven't tried like rosemary. You can sauté (panfry) vegetables in a " +
                        "non-stick pan with a small amount of cooking spray\n" +
                        "•\tCalcium-rich foods ― you may automatically think of a glass of low-fat or fat-free milk when someone says" +
                        " \"eat more dairy products.\" But what about low-fat and fat-free yogurts without added sugars? These come in a wide " +
                        "variety of flavors and can be a great dessert substitute for those with a sweet tooth.\n";

            }
            else if (category == "Overweight")
            {
                lblDietView.Text = "•\tHave 4 to 6 small meals and snacks everyday rather than 3 large meals. This will help you in controlling your hunger and the nutrients are also utilized effectively.\n" +
                        "•\tPlan your diet chart in advance. Hasty and incomplete planning can make you confused and can increase the temptation towards fatty and junk foods.\n" +
                        "•\tChew your food properly as this can help you in reducing weight\n" +
                        "•\tPlan your food items effectively. Include whole grain cereals, pulses, legumes, vegetables, fruits and water in your everyday diet.\n" +
                        "•\tAvoid fatty foods or high caloric foods. Avoid sugary foods, pastries and sweets.\n";

            }


            // Create your application here
        }
    }
}
