using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Event_Draw_Winner_Picker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        Random rnd = new Random((int)(DateTime.Now.Ticks & 0x0000FFFF));
		
		private List<Control> _layoutAwareControls;

        public MainPage()
        {
            this.InitializeComponent();
			Loaded += MainPage_Loaded;
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
		
		void MainPage_Loaded(object sender, RoutedEventArgs e)
 		{

			var control = sender as Control;
 
       		if (control == null) return;
 
       		// Set the initial visual state of the control
 
        	VisualStateManager.GoToState(control, ApplicationView.Value.ToString(), false);
 
      		if (this._layoutAwareControls == null)
 
      		{
 
           		this._layoutAwareControls = new List<Control>();
 
      		}
 
       		this._layoutAwareControls.Add(control);
 
      		Window.Current.SizeChanged += Current_SizeChanged;
		}
		
		
		void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
  		{
 			string visualState = ApplicationView.Value.ToString();
 
 		    if (this._layoutAwareControls != null)
  		    {
          		foreach (var layoutAwareControl in this._layoutAwareControls)
         		{
					VisualStateManager.GoToState(layoutAwareControl, visualState, false);
          		}
     		}
 		 }

        private void drawButton_Click(object sender, RoutedEventArgs e)
        {
			drumroll.Play();
            output.Text = "";

            int[] attendies = new int[Convert.ToInt32(attendiesNumBox.Text) +1];
            for (int i = 0; i < Convert.ToInt32(attendiesNumBox.Text); i++)
            {
                attendies[i] = 0;
            }

            

            for (int i = 0; i < (Convert.ToInt32(rollNumBox.Text) + Convert.ToInt32(attendiesNumBox.Text)); i++)
            {
                int randomNum = rnd.Next(0, Convert.ToInt32(attendiesNumBox.Text));
                attendies[randomNum] = attendies[randomNum] + 1;
            }

            for (int i = 0; i < Convert.ToInt32(winnersNumBox.Text); i++)
            {
                int winner = 0;
                int highest = attendies[0];
                for (int j = 0; j < Convert.ToInt32(attendiesNumBox.Text); j++)
                {
                    if (attendies[j] > highest)
                    {
                        highest = attendies[j];
                        winner = j;
                    }
                }
                attendies[winner] = 0;
                output.Text += "\n" + (i+1) + "  /  " + (winner + 1);
            }
        }
    }
}
