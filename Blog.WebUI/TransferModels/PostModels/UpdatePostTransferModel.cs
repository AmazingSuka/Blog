using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebUI.TransferModels.PostModels
{
    public class UpdatePostTransferModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageFileName { get; set; }
        public string EditorId { get; set; }
    }
}
