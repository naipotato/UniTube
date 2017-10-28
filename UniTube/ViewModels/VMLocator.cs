namespace UniTube.ViewModels
{
    public class VMLocator
    {

        private MasterViewModel _masterViewModel;
        public MasterViewModel MasterViewModel =>
            _masterViewModel ?? (_masterViewModel = new MasterViewModel());

    }
}
