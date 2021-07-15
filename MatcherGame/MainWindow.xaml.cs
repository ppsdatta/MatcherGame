using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatcherGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBlock lastAnimalSelected = null;

        private bool finidingMatch = false;

        private int matchCount = 0;

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private long timeSoFar = 0;

        private const long MAXCOUNT = 200;


        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(.1);
            dispatcherTimer.Tick += Timer_Tick;
            SetupGame();
        }

        private void SetupGame()
        {
            var animals = new List<string>
            {
                "🐘", "🐘",
                "🐅", "🐅",
                "🦓", "🦓",
                "🐇", "🐇",
                "🦁", "🦁",
                "🐭", "🐭",
                "🦏", "🦏",
                "🦜", "🦜",
                "🐍", "🐍",
                "🐵", "🐵"
            };

            var rand = new Random();

            foreach (var textBlock in gameGrid.Children.OfType<TextBlock>())
            {
                int index = rand.Next(0, animals.Count);
                textBlock.Text = animals[index];
                animals.RemoveAt(index);
                textBlock.Visibility = Visibility.Visible;
            }

            lastAnimalSelected = null;
            finidingMatch = false;
            matchCount = 0;

            timeSoFar = 0;
            dispatcherTimer.Stop();
            dispatcherTimer.Start();
    }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeSoFar++;
            timerLabel.Content = timeSoFar.ToString();

            if (timeSoFar >= MAXCOUNT)
            {
                dispatcherTimer.Stop();
                MessageBox.Show("Oops time's up!");
                SetupGame();
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var block = sender as TextBlock;
            
            if (finidingMatch == false)
            {
                finidingMatch = true;
                block.Visibility = Visibility.Hidden;
                lastAnimalSelected = block;
            }
            else
            {
                if (block.Text == lastAnimalSelected.Text)
                {
                    // Matched
                    block.Visibility = Visibility.Hidden;
                    matchCount++;

                    if (matchCount == 10)
                    {
                        dispatcherTimer.Stop();
                        MessageBox.Show("You win!!");
                        SetupGame();
                    }
                }
                else
                {
                    lastAnimalSelected.Visibility = Visibility.Visible;
                }

                finidingMatch = false;
            }

        }
    }
}
