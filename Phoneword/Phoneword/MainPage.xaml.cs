using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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

            StackLayout panel = new StackLayout
            {
                Spacing = 15 //establishes layout
            };

            panel.Children.Add(new Label //because this is stack layout, each subsequent panel.Children.Add is stacked top to bottom
            {
                Text = "Enter a Phoneword:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(phoneNumberText = new Entry
            {
                Text = "1-855-XAMARIN",
            });

            panel.Children.Add(translateButton = new Button
            {
                Text = "Translate",
                
            });

            translateButton.Clicked += OnTranslate;

            panel.Children.Add(callButton = new Button()
            {
                Text = "Call",
                IsEnabled = false,
            });

            this.Content = panel;

        }
        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = phoneNumberText.Text;
            translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber); //calls the ToNumber function from the PhonewordTranslator module

            if (!string.IsNullOrEmpty(translatedNumber))
            {

            }
            else
            {

            }
        }
    }
}
