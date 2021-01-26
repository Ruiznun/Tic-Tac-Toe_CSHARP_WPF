using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        /// <summary>
        /// Holds the marks of the grid
        /// </summary>
        private MarkType[] mResults;
        /// <summary>
        /// if true player one is playing
        /// </summary>
        private bool mPlayer1Turn;
        /// <summary>
        /// did the game end?
        /// </summary>
        private bool mGameEnd;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion

        /// <summary>
        /// Starts a new game and clers all values back to start
        /// </summary>
        private void NewGame()
        {
            // new blank type
            mResults = new MarkType[9];
            for (int i = 0; i < mResults.Length; i++) mResults[i] = MarkType.Free;
            //player one is current player
            mPlayer1Turn = true;
            //Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.GhostWhite;
                button.Foreground = Brushes.Blue;
            });

            //Make sure the game hasn't ended
            mGameEnd = false;
        }

        /// <summary>
        /// Handles a click event
        /// </summary>
        /// <param name="sender">The button was clicked</param>
        /// <param name="e">The event of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //check if game has ended
            if (mGameEnd)
            {
                NewGame();
                return;
            }
            //cast sender to a button
            var button = (Button)sender;
            //find the button in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = (row * 3) + column;
            //if taken, ignore
            if(mResults[index] != MarkType.Free)return;
            //set cell value based on player turn
            mResults[index] = mPlayer1Turn ? MarkType.X : MarkType.O;
            //set button text
            button.Content = mPlayer1Turn ? "X" : "O";
            // change O to red
            if (!mPlayer1Turn) button.Foreground = Brushes.Red;
            //flip turn
            mPlayer1Turn ^= true;

            //check for a winner
            CheckForWinner();
        }

        /// <summary>
        /// Checks for a winner
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal Wins
            //check for horizontal wins
            //
            //row 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnd = true;
                //highlight winning cells
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnd = true;
                //highlight winning cells
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnd = true;
                //highlight winning cells
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical Wins
            //check for vertical wins
            //
            //col 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnd = true;
                //highlight winning cells
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //col 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnd = true;
                //highlight winning cells
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //col 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnd = true;
                //highlight winning cells
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal Wins
            //check for Diagonal wins
            //
            //top left to bottom right
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnd = true;
                //highlight winning cells
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            //
            //top right to bottom left
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnd = true;
                //highlight winning cells
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion

            #region No Wins
            //
            //check for no wins
            //
            if (!mResults.Any(results => results == MarkType.Free) && !mGameEnd){
                mGameEnd = true;
                //turn game cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button => button.Background = Brushes.Orange);
            }
            #endregion
        }
    }
}
