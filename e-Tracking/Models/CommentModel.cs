using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace e_Tracking.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? ParentId { get; set; }
        public string PrNumber { get; set; }
        public string UpdateDate { get; set; }
        public string UserId { get; set; }
        public List<CommentModel> replyComments { get; set; }
    }

    public class FollowModel
    {
        public long id { get; set; }

        public string userid { get; set; }

        public string flag_follow { get; set; }

        public string lastmodifieddate { get; set; }

        public string lastmodifieBy { get; set; }
        public string pr_number { get; set; }

    }

    public class content
    {
        public List<CommentModel> comment_x { get; set; }
        public FollowModel follow_x { get; set; }
    }
}