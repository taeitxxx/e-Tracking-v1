using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Tracking.Bussiness
{
    public class FollowAppServ
    {

        public List<tbfollowing> Add(tbfollowing follow)
        {
            try
            {
                using (var db = new ETrackingContext())
                {
                    follow.lastmodifieddate = DateTime.Now;
                    db.tbfollowings.Add(follow);
                    db.SaveChanges();

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<tbfollowing> UpdateFlag_follow(string prNumber, string userid, string flag_follow)
        {
            try
            {
                using (var db = new ETrackingContext())
                {
                    db.tbfollowings.Where(i => i.userid == userid && i.pr_number == prNumber).ToList()
                        .ForEach(x => x.flag_follow = flag_follow);
                    db.SaveChanges();

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<tbfollowing> FindUserFollowByPRNumber(string prNumber, string userid)
        {
            try
            {
                using (var db = new ETrackingContext())
                {
                    var items = db.tbfollowings.Where(i => i.userid == userid && i.pr_number == prNumber).ToList();

                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }

}