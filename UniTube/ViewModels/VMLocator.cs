namespace UniTube.ViewModels
{
    public class VMLocator
    {
        public MasterViewModel Master { get; set; }

        public WelcomeViewModel Welcome { get; set; }

        public VMLocator()
        {
            Master = new MasterViewModel();
            Welcome = new WelcomeViewModel();
        }
    }
}
