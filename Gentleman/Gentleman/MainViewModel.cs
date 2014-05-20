using System;
using System.Windows.Input;

namespace Gentleman
{
    public class MainViewModel
    {
        public string Name { get; set; }

        public string Result { get; set; }

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
    }
}
