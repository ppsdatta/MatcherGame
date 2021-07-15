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


        public MainWindow()
        {
            InitializeComponent();
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
                        MessageBox.Show("Game over");
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
