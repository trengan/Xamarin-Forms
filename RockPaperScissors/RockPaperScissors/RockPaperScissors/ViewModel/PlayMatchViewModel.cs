using RockPaperScissors.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RockPaperScissors.ViewModel
{
    public class PlayMatchViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        int _counter;
        bool _timerResult;
        MatchType _matchType;
        bool _isTimeElasped;
        ImageSource _imageSourceSelectedByPlayer1;
        ImageSource _imageSourceSelectedByPlayer2;
        Color _textHighLightedColor = Color.White;
        bool _IsChoiceSelected;
        double _rockOpacity = 1;
        double _paperOpacity = 1;
        double _scissorsOpacity = 1;
        bool _isHumanVSComputerSelected;
        string _result;
        string _player1Name;
        string _player2Name;

        public PlayMatchViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            // Todo : Get the GameType from  user selection in UI.
            Game = new Game() {GameType = GameType.RPS };
        }

        #region public property

        public Game Game { get; set; }

        public int Counter
        {
            get
            {
                return _counter;
            }
            set
            {
                _counter = value;
                OnPropertyChanged();
            }
        }

        public bool IsTimeElasped
        {
            get
            {
                return _isTimeElasped;
            }
            set
            {
                _isTimeElasped = value;
                OnPropertyChanged();
            }
        }

        public ImageSource ImageSourceSelectedByPlayer1
        {
            get { return _imageSourceSelectedByPlayer1; }
            set
            {
                _imageSourceSelectedByPlayer1 = value;
                OnPropertyChanged();
            }
        }

        public ImageSource ImageSourceSelectedByPlayer2
        {
            get { return _imageSourceSelectedByPlayer2; }
            set
            {
                _imageSourceSelectedByPlayer2 = value;
                OnPropertyChanged();
            }
        }

        public Color HighLightedColor
        {
            get
            {
                return _textHighLightedColor;
            }
            set
            {
                _textHighLightedColor = value;
                OnPropertyChanged();
            }
        }

        public bool IsChoiceSelected
        {
            get
            {
                return _IsChoiceSelected;
            }
            set
            {
                _IsChoiceSelected = value;
                OnPropertyChanged();
            }
        }

        public double RockOpacity
        {
            get
            {
                return _rockOpacity;
            }
            set
            {
                _rockOpacity = value;
                OnPropertyChanged();
            }
        }

        public double PaperOpacity
        {
            get
            {
                return _paperOpacity;
            }
            set
            {
                _paperOpacity = value;
                OnPropertyChanged();
            }
        }

        public double ScissorsOpacity
        {
            get
            {
                return _scissorsOpacity;
            }
            set
            {
                _scissorsOpacity = value;
                OnPropertyChanged();
            }
        }
  
        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        public bool IsHumanVSComputerSelected
        {
            get
            {
                return _isHumanVSComputerSelected;
            }
            set  
            {
                _isHumanVSComputerSelected = value;
                OnPropertyChanged();
            }
        }

        public string Player1Name
        {
            get
            {
                return _player1Name;
            }
            set
            {
                _player1Name = value;
                OnPropertyChanged();
            }
        }

        public string Player2Name
        {
            get
            {
                return _player2Name;
            }
            set
            {
                _player2Name = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region public methods
        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                _matchType=  (MatchType)Enum.Parse(typeof(MatchType), navigationData.ToString());
            }
            IsHumanVSComputerSelected = _matchType == MatchType.HumanVSComputer;

            if(IsHumanVSComputerSelected)
            {
                Player1Name = "Person";
                Player2Name = "Computer";
            }
            else
            {
                Player1Name = "Computer";
                Player2Name = "Computer";
            }
            Play();
            MessagingCenter.Send(this, MessageKeys.PLAYMATCHLANDEDKEY);
            return base.InitializeAsync(navigationData);
        }

        public void Play()
        {
            Counter = 0;
            _timerResult = false;
            IsTimeElasped = false;
            IsChoiceSelected = false;
            ImageSourceSelectedByPlayer1 = ImageSource.FromFile("");
            ImageSourceSelectedByPlayer2 = ImageSource.FromFile("");
            Result = string.Empty;

            RockOpacity = 1;
            PaperOpacity = 1;
            ScissorsOpacity = 1;
            Game.Play(_matchType);
            Device.StartTimer(TimeSpan.FromSeconds(1.0), TimerElapsed);
        }

       

        public ICommand SelectChoiceCommand => new Command((object sender) =>
        {
            Choice choice = Choice.Rock;
            if (Game.Match.IsPlayer1Human)
            {
                if (sender != null)
                {
                    string strChoice = sender.ToString();
                    if (!string.IsNullOrEmpty(strChoice))
                    {
                        short result;
                        if (Int16.TryParse(strChoice, out result))
                        {
                            choice = (Choice)result;
                        }
                    }
                }

                switch (choice)
                {
                    case Choice.Rock:
                        {
                            RockOpacity = 1.0;
                            PaperOpacity = ScissorsOpacity = 0.5;
                            break;
                        }
                    case Choice.Paper:
                        {
                            PaperOpacity = 1.0;
                            RockOpacity = ScissorsOpacity = 0.5;
                            break;
                        }
                    case Choice.Scissors:
                        {
                            ScissorsOpacity = 1.0;
                            RockOpacity = PaperOpacity = 0.5;
                            break;
                        }
                }

                Game.Match.SetChoiceForPlayer1(choice);
                IsChoiceSelected = true;
                Counter = 10;
                TimerElapsed();
            }
        });

        public ICommand PlayCommand => new Command(() =>
        {
            Play();
        });

        public ICommand ChangeModeCommand => new Command(async() =>
        {
           await _navigationService.NavigateBackAsync();
        });

        #endregion


        #region private methods
        private void GetImageSource()
        {
            Choice choice;
            switch (_matchType)
            {
                case MatchType.HumanVSComputer:
                    {
                        choice = Game.Match.GetChoiceFromPalyer2();
                        ImageSourceSelectedByPlayer2 = ImageSource.FromFile(choice.ToString() + "_Rotate.png");
                        break;
                    }
                case MatchType.ComputerVSComputer:
                    {
                         choice = Game.Match.GetChoiceFromPalyer1();
                        ImageSourceSelectedByPlayer1 = ImageSource.FromFile(choice.ToString() + ".png");
                        choice = Game.Match.GetChoiceFromPalyer2();
                        ImageSourceSelectedByPlayer2 = ImageSource.FromFile(choice.ToString() + "_Rotate.png");
                        break;
                    }

            }
           
        }


        private bool TimerElapsed()
        {
            HighLightedColor = Counter % 2 == 0 ? Color.White : Color.Red;

            if (Counter == 10)
            {
                _timerResult = false;

                if (_matchType == MatchType.ComputerVSComputer || (IsChoiceSelected && _matchType == MatchType.HumanVSComputer))
                {
                    GetImageSource();
                    IsTimeElasped = true;
                    Result = Game.GetResult();
                }
            }
            else
            {
                _timerResult = true;
                Counter++;
            }
            return _timerResult;
        }

        #endregion 
    }
}
