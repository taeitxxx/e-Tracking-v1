using System;
using System.Web;
using System.Web.Mvc;
using e_Tracking.Models;
using System.Collections.Generic;
using e_Tracking.Bussiness;
using System.Linq;
using System.Globalization;

namespace e_Tracking.Controllers
{
    public class HomeController : Controller
    {
        CultureInfo EngCI = new System.Globalization.CultureInfo("en-US");

        public ActionResult Index()
        {
            HttpContext.Session["userid"] = "0001";

            CommentAppService commentAppService = new CommentAppService();
            var items = commentAppService.FindByPrNumber("1100069792");

            FollowAppServ followAppService = new FollowAppServ();
            var user_follow = followAppService.FindUserFollowByPRNumber("1100069792", HttpContext.Session["userid"].ToString());

            var commentItems = items.Where(x => x.ParentId == null);
            var replyItems = items.Where(x => x.ParentId != null);
            var commentModels = new List<CommentModel>();

            var followModel = new FollowModel();

            foreach (var j in user_follow)
            {
                var follow = ConvertToViewModel_follow(j);
                followModel = follow;
            }

            foreach (var i in commentItems)
            {
                var comment = ConvertToViewModel(i);
                comment.replyComments = new List<CommentModel>();
                var replies = replyItems.Where(x => x.ParentId == i.Id).ToList();
                foreach (var j in replies)
                {
                    var reply = ConvertToViewModel(j);
                    comment.replyComments.Add(reply);
                }
                commentModels.Add(comment);
            }

            var content = new content();

            content.comment_x = new List<CommentModel>();
            content.follow_x = new FollowModel();

            content.comment_x = commentModels;
            content.follow_x = followModel;



            return View(content);

        }

        public ActionResult AnotherLink()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult AddComment(CommentModel item)
        {
            var comment = new Comment();
            comment.Text = item.Text;
            comment.ParentId = item.ParentId;
            comment.PrNumber = item.PrNumber;
            comment.UserId = "123-456-789";

            CommentAppService commentAppService = new CommentAppService();
            var a = commentAppService.Add(comment);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddFollow()
        {
            var follow = new tbfollowing();
            follow.flag_follow = "y";
            follow.pr_number = "1100069792";
            follow.userid = HttpContext.Session["userid"].ToString();

            FollowAppServ followAppServ = new FollowAppServ();
            var a = followAppServ.Add(follow);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UnFollow()
        {
            //var follow = new tbfollowing();
            //follow.flag_follow = "y";
            //follow.pr_number = "1100069792";
            //follow.userid = HttpContext.Session["userid"].ToString();

            FollowAppServ followAppServ = new FollowAppServ();
            var a = followAppServ.UpdateFlag_follow("1100069792", HttpContext.Session["userid"].ToString(), "n");

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Follow()
        {
            //var follow = new tbfollowing();
            //follow.flag_follow = "y";
            //follow.pr_number = "1100069792";
            //follow.userid = HttpContext.Session["userid"].ToString();

            FollowAppServ followAppServ = new FollowAppServ();
            var a = followAppServ.UpdateFlag_follow("1100069792", HttpContext.Session["userid"].ToString(), "y");

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public CommentModel ConvertToViewModel(Comment model)
        {
            var vm = new CommentModel();
            vm.Id = model.Id;
            vm.Text = model.Text;
            vm.ParentId = model.ParentId;
            vm.PrNumber = model.PrNumber;
            vm.UserId = model.UserId;
            vm.UpdateDate = model.UpdateDate.ToString("dd-MMM-yyyy HH:mm");
            return vm;
        }

        public FollowModel ConvertToViewModel_follow(tbfollowing model)
        {
            var vm = new FollowModel();
            vm.id = model.id;
            vm.flag_follow = model.flag_follow;
            vm.userid = model.userid;
            return vm;
        }
    }
}
