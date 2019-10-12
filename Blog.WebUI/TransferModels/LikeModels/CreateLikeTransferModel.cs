using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebUI.TransferModels.LikeModels
{
    public class CreateLikeTransferModel
    {
        public string TargetElementId { get; set; }
        public string OwnerId { get; set; }
    }
}
