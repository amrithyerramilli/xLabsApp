using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xLabsApp
{
    public class ProfilePage : BaseContentPage
    {
        public ProfilePage()
        {
            Content = new Label()
            {
                Text = "Profile Page - " + App.user.id,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
        }
    }
}
