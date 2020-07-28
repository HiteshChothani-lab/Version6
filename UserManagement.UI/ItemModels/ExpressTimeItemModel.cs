using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
