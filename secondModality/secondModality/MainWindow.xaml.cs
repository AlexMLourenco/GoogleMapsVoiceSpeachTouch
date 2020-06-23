using System.Windows;

namespace secondModality
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private SecondMod _sm;
        public MainWindow()
        {
            //InitializeComponent();

            _sm = new SecondMod();
        
        }

        private void B1_OnClick(object sender, RoutedEventArgs e)
        {
            _sm.sendToFusionTransport("DRIVING");

        }

        private void B2_OnClick(object sender, RoutedEventArgs e)
        {
            _sm.sendToFusionTransport("BICYCLING");

        }
        private void B3_OnClick(object sender, RoutedEventArgs e)
        {
            _sm.sendToFusionTransport("TRANSIT");

        }
        private void B5_OnClick(object sender, RoutedEventArgs e)
        {
            _sm.sendToFusionTransport("WALKING");
        }

        private void B6_OnClick(object sender, RoutedEventArgs e)
        {
            _sm.sendToFusion("ZOOM", "MORE");
        }

        private void B7_OnClick(object sender, RoutedEventArgs e)
        {
            _sm.sendToFusion("ZOOM", "LESS");
        }

       



        /*
        private void _sm_Recognized(object sender, SpeechEventArg e)
        {
            result.Text = e.Text;
            confidence.Text = e.Confidence+"";
            if (e.Final) result.FontWeight = FontWeights.Bold;
            else result.FontWeight = FontWeights.Normal;
        }

    */

    }
}
