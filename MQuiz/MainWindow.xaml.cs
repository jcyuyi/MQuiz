using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MQuiz
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //current quiz
        Quiz quiz;
        int score;
        float time; //current time
        int timeInterval;

        string statusMsg;
        bool isStarted = false;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();
            sliderTimeInterval.ValueChanged += sliderTimeInterval_ValueChanged;
            cbLimitedTime.Checked += cbLimitedTime_Checked;
            cbLimitedTime.Unchecked += cbLimitedTime_Unchecked;

            //init dispatcherTimer
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,0,0,0,100); //call Tick every 0.1s
            
            statusMsg = "Ready. ";

            //btnStart_Click(null, null);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            time += 0.1f; //curreent sec 
            pbTime.Value = (float)time / timeInterval * 100;
            if (pbTime.Value > 99.9) //finished
            {
                nextQuiz();
            }
        }



        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            isStarted = true;
            score = 0;
            lbScore.Content = score.ToString();
            statusMsg = "New game started. ";
            timeInterval = Convert.ToInt32(tbTimeInterval.Text);
            btnEnter.IsEnabled = true;
            lbStatues.Background = null;
            nextQuiz();
            tbAns.SelectAll();
        }

        private void nextQuiz()
        {     
            try
            {
                int rangeFrom = Convert.ToInt32(tbRangeFrom.Text);
                int rangeTo = Convert.ToInt32(tbRangeTo.Text);
                if (rangeFrom >= rangeTo)
                {
                    throw new Exception("Range must contain at least two numbers");
                }
                int timeInterval = Convert.ToInt32(tbTimeInterval.Text);
                Quiz.QuizType quizType;
                if (cbQuiz.SelectedValue.ToString() == "Random!")
                {
                    int n = MRandom.getRandomNumber(0,4);
                    quizType = (Quiz.QuizType)Enum.Parse(typeof(Quiz.QuizType), n.ToString());
                }
                else
                    quizType = (Quiz.QuizType)Enum.Parse(typeof(Quiz.QuizType),cbQuiz.SelectedValue.ToString());
                quiz = QuizFactory.getQuiz(quizType, timeInterval, rangeFrom, rangeTo);
                
                //set quiz
                lbQuiz.Content = quiz.question;
                lbStatues.Content = statusMsg +
                    "The next question is worth "  +
                    quiz.score                +
                    "points!";

                //Set timer
                if (Convert.ToBoolean( cbLimitedTime.IsChecked))
                {
                    time = 0; //reset current time
                    dispatcherTimer.Start();
                    //Set timer visiblity
                    if (pbTime.Visibility == System.Windows.Visibility.Hidden)
                    {
                        pbTime.Visibility = System.Windows.Visibility.Visible; ;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sliderTimeInterval_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbTimeInterval.Text = sliderTimeInterval.Value.ToString();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (!isStarted)
            {
                return;
            }
            if (quiz.answer == tbAns.Text) //answer is right
            {
                score += quiz.score;
                lbScore.Content = score.ToString();
                lbStatues.Background = Brushes.Green;
                statusMsg = "Congradulations! ";
            }
            else //answer if wrong
            {
                statusMsg = "Wrong answer. ";
                lbStatues.Background = Brushes.Red;
            }
            //next 
            nextQuiz();

            tbAns.SelectAll();
        }

        private void tbAns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnEnter_Click(null, null);
            }
        }

        private void cbLimitedTime_Checked(object sender, RoutedEventArgs e)
        {
            //pbTime.Visibility = System.Windows.Visibility.Visible;
            //nextQuiz(); //start a new quiz
        }

        private void cbLimitedTime_Unchecked(object sender, RoutedEventArgs e)
        {
            pbTime.Visibility = System.Windows.Visibility.Hidden;
            //stop the timer
            dispatcherTimer.Stop();
        }

    }
}
