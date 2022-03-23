using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials; //this library allows us to use the dialer

namespace Phoneword
{
    public class MainPage : ContentPage
    {
        Entry phoneNumberText;
        Button translateButton;
        Button callButton;
        string translatedNumber;

        public MainPage()
        {
            this.Padding = new Thickness(20, 20, 20, 20); //sets padding for the page
            this.BackgroundColor = Color.DeepPink; //color change

            StackLayout panel = new StackLayout // this defaults to vertical, since nothing is selected.  could make horizontal by choosing Orentation = StackOrientation.Horizontal on l 21
            {
                Spacing = 15 //establishes layout
            };

            panel.Children.Add(new Label //because this is stack layout, each subsequent panel.Children.Add is stacked top to bottom
            {
                Text = "Enter a Phoneword:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(phoneNumberText = new Entry //entry field
            {
                Text = "1-855-XAMARIN", //prefilled text
            });

            panel.Children.Add(translateButton = new Button 
            {
                Text = "Translate",
                TextColor = Color.White,
                BackgroundColor = Color.Purple,
                
            });

            translateButton.Clicked += OnTranslate; //click handling for translateButton calls OnTranslate func, l 53
            

            panel.Children.Add(callButton = new Button()
            {
                Text = "Call",
                IsEnabled = false,
                BackgroundColor = Color.BlueViolet,
                TextColor = Color.White,
            });

            callButton.Clicked += OnCall; //click handling for Call button calls OnCall function l.72

            this.Content = panel;

        }
        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = phoneNumberText.Text;
            translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber); //calls the ToNumber method from the PhonewordTranslator module

            if (!string.IsNullOrEmpty(translatedNumber)) //checks for content
            {
                callButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNumber;
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }

        async void OnCall(object sender, System.EventArgs e) //async keyword goes with await
        {
            if (await this.DisplayAlert(
                "Dial a Number",
                "Would you like to call " + translatedNumber + "?",
                "Yes",
                "No"))
            {
                //dialphone
                try
                {
                    PhoneDialer.Open(translatedNumber);
                }
                catch (ArgumentNullException) //error handling for not valid or empty
                {
                    await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
                 }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Unable to dial", "Phone dialing not supported.", "OK");
                }
                catch (Exception)
                {
                    //another error
                    await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
                }
            }
        }
    }
}
