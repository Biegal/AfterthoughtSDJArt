using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Gentleman
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }

        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                RaisePropertyChanged("Result");
            }
        }

        private ICommand _getResultCommand;
        public ICommand GetResultCommand
        {
            get { return _getResultCommand ?? (_getResultCommand = new RelayCommand(o => GetResult())); }
        }

        public void GetResult()
        {
            Result = String.Empty;
            if (!String.IsNullOrEmpty(Name))
            {
                Result = String.Format("Cześć, {0} !", Name);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
