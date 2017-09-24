namespace UniTube.ViewModels
{
    public class VMLocator
    {
        public WelcomeViewModel Welcome { get; set; }

        public VMLocator()
        {
            Welcome = new WelcomeViewModel();
        }
    }
}
