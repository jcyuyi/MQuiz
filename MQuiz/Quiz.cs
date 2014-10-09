using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQuiz
{
    class Quiz
    {
        public enum QuizType
        {
            Addition,Substruction,Multiplication,Division
        }
        public string question { set; get; }
        public string answer { set; get; }
        public int score { set; get; }
    }

    class QuizFactory
    {
        public static Quiz getQuiz(Quiz.QuizType type,float timeInterval,int rangeFrom,int rangeTo)
        {
            Quiz quiz = null;
            
            //Judge Difficuty
            //if range lenght = 0 then range diffcuty is 1
            Double rangeDiffcuty = 1 + Math.Log10(rangeTo - rangeFrom + 1);
            //get max abs num
            int maxNum = Math.Abs(rangeFrom) > Math.Abs(rangeTo) ?
                Math.Abs(rangeFrom) : Math.Abs(rangeTo);
            //if maxNum is 0, then num diffcuty is 1;
            Double numDiffcuty = 1 + Math.Log10(maxNum + 1);
            //if time interval is 1 then time diffcuty is 1.5
            Double timeDiffcuty = 1 / timeInterval + 0.5;
            Double diffcuty = rangeDiffcuty * numDiffcuty * timeDiffcuty;

            switch (type)
            {
                case Quiz.QuizType.Addition:
                    quiz = getAddition(rangeFrom, rangeTo);
                    quiz.score = Convert.ToInt32(Math.Round(diffcuty * 2));
                    break;
                case Quiz.QuizType.Substruction:
                    quiz = getSubstruction(rangeFrom, rangeTo);
                    quiz.score = Convert.ToInt32(Math.Round(diffcuty * 2.5));
          
                    break;
                case Quiz.QuizType.Multiplication:
                    quiz = getMultiplication(rangeFrom, rangeTo);
                    quiz.score = Convert.ToInt32(Math.Round(diffcuty * 4));
          
                    break;
                case Quiz.QuizType.Division:
                    quiz = getDivision(rangeFrom, rangeTo);
                    quiz.score = Convert.ToInt32(Math.Round(diffcuty * 5));
          
                    break;
                default:
                    break;
            }
            return quiz;
        }


        private static Quiz getAddition(int rangeFrom, int rangeTo)
        {
            Quiz quiz = new Quiz();
            int num1 = MRandom.getRandomNumber(rangeFrom, rangeTo);
            int num2 = MRandom.getRandomNumber(rangeFrom, rangeTo);
            int ans = num1 + num2;
            quiz.question = num1.ToString() + " + " + num2.ToString ();
            quiz.answer = ans.ToString();
            return quiz;
        }

        private static Quiz getSubstruction(int rangeFrom, int rangeTo)
        {
            Quiz quiz = new Quiz();
            int num1 = MRandom.getRandomNumber(rangeFrom, rangeTo);
            int num2 = MRandom.getRandomNumber(rangeFrom, rangeTo);
            int ans = num1 - num2;
            quiz.question = num1.ToString() + " - " + num2.ToString();
            quiz.answer = ans.ToString();
            return quiz;
        }

        private static Quiz getMultiplication(int rangeFrom, int rangeTo)
        {
            Quiz quiz = new Quiz();
            int num1 = MRandom.getRandomNumber(rangeFrom, rangeTo);
            int num2 = MRandom.getRandomNumber(rangeFrom, rangeTo);
            int ans = num1 * num2;
            quiz.question = num1.ToString() + " X " + num2.ToString();
            quiz.answer = ans.ToString();
            return quiz;
        }

        private static Quiz getDivision(int rangeFrom, int rangeTo)
        {
            Quiz quiz = new Quiz();
            int div1 = 1;
            //div1 should not be 0
            while (div1 == 0)
            {
                div1 = MRandom.getRandomNumber(rangeFrom, rangeTo);
            }
            int div2 = MRandom.getRandomNumber(rangeFrom, rangeTo);
            int mult = div1 * div2;
            quiz.question = mult.ToString() + " ÷ " + div1.ToString();
            quiz.answer = div2.ToString();
            return quiz;
        }
    }
}
