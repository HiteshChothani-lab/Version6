using Prism.Mvvm;

namespace UserManagement.UI.ItemModels
{
    public class ExpressTimeItemModel : BindableBase
    {
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Mode { get; set; }
        public string FinalTime { get; set; }
    }
}
