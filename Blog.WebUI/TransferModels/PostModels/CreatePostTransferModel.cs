using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebUI.TransferModels.PostModels
{
    public class CreatePostTransferModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageFileName { get; set; }
        public string AuthorId { get; set; }
    }
}
