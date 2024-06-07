using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class skillup_UserProfile
    {
        dbServices ds = new dbServices();
        public async Task<responseData> UserProfile(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                MySqlParameter[] insertParams = new MySqlParameter[]
              {
                        new MySqlParameter("@profile_picture", req.addInfo["profile_picture"].ToString()),
                        new MySqlParameter("@first_name", req.addInfo["first_name"].ToString()),
                        new MySqlParameter("@last_name", req.addInfo["last_name"].ToString()),
                        new MySqlParameter("@date_of_birth", req.addInfo["date_of_birth"].ToString())  ,
                        new MySqlParameter("@gender", req.addInfo["gender"].ToString()),
                        new MySqlParameter("@bio", req.addInfo["bio"].ToString()),
              };
                var sq = @"insert into pc_student.Skillup_UserProfile(profile_picture,first_name,last_name,date_of_birth,gender,bio) values(@profile_picture,@first_name,@last_name,@date_of_birth,@gender,@bio)";

                var insertResult = ds.executeSQL(sq, insertParams);
                if (insertResult[0].Count() == null)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "UnSuccessful";
                }
                else
                {
                    resData.rData["rCode"] = 0;
                    resData.rData["rMessage"] = "UserProfile Successful";

                }


            }
            catch (Exception ex)
            {

                throw;
            }
            return resData;
        }


    }
}

