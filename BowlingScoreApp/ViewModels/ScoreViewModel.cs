// Ajer Lodhi
// Purpose: Connect collection of frames held by ScoreCard to MainWindow.xaml 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MicroMvvm;
using BowlingScoreApp;

namespace BowlingScoreApp.ViewModels
{
    public class ScoreViewModel : ObservableObject
    {
        public ScoreViewModel() { }

        #region Private Variables
        private int score;
        private char charScore;
        private ScoreCard sc = new ScoreCard();
        #endregion Private Variables

        #region Variables
        // The Score for each frame represented by integers.
        public int Score
        {
            get { return score; }
            set { score = value; RaisePropertyChanged(() => Score); }
        }

        // The Score for each frame represented by 'X','x' or '/'.
        public char CharScore
        {
            get { return charScore; }
            set { charScore = value; RaisePropertyChanged(() => CharScore); }
        }

        // The ScoreCard holding a colection of Frames.
        public ScoreCard ScoreCard
        {
            get { return sc; }
            set { sc = value; RaisePropertyChanged(() => ScoreCard); }
        }
        #endregion Variables

        /// <summary>
        /// Relays to event handler on any changes made it to the Score and if inputs are valid or not
        /// </summary>
        #region ICommands
        public ICommand UpdateScore
        {
            get
            {
                return new RelayCommand(AddScore, IsValidScoreInput);
            }
        }

        public ICommand UpdateCharScore
        {
            get
            {
                return new RelayCommand(AddCharScore, IsValidCharScoreInput);
            }
        }

        public ICommand ResetTable
        {
            get
            {
                return new RelayCommand(() => ScoreCard = new ScoreCard(), () => true);
            }
        }
        #endregion ICommands

        /// <summary>
        /// Adding both character and Integer score inputs to each frame to be porcessed in Score Card.
        /// </summary>
        #region Adding Scores
        public void AddScore()
        {
            sc.AddScore(Score);

            RaisePropertyChanged(() => ScoreCard);
        }

        public void AddCharScore()
        {
            sc.AddScore(CharScore);

            RaisePropertyChanged(() => ScoreCard);
        }
        #endregion Adding Scores

        /// <summary>
        ///  Check validity of both integer and character score inputs.
        /// </summary>
        /// <returns></returns>
        #region Checking Input Validity
        public bool IsValidScoreInput()
        {
            return sc.IsValidScoreInput(Score);
        }

        public bool IsValidCharScoreInput()
        {
            return sc.IsValidScoreInput(CharScore);
        }
        #endregion Checking Input Validity

    }
}