using AspNetCoreAddin.Data.Enums;
using System.Collections.Generic;

namespace AspNetCoreAddin.Data.ViewModels
{
    public class FunctionViewModel
    {
        public string Id { set; get; }

        public string Name { set; get; }

        public string URL { set; get; }

        public string ParentId { set; get; }

        public string IconCss { get; set; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }

        public List<FunctionViewModel> ChildFunctions { get; set; }
    }
}