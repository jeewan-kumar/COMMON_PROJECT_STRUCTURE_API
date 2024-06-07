
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class skillup_Video
    {
        dbServices ds = new dbServices();
        public async Task<responseData> Video(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                MySqlParameter[] insertParams = new MySqlParameter[]
              {
                        new MySqlParameter("@lesson_id", req.addInfo["lesson_id"].ToString()),
                        new MySqlParameter("@title", req.addInfo["title"].ToString()),
                        new MySqlParameter("@url", req.addInfo["url"].ToString()),
                        new MySqlParameter("@duration", req.addInfo["duration"].ToString())  ,
              };
                var sq = @"insert into pc_student.Skillup_Video(lesson_id,title,url,duration) values(@lesson_id,@title,@url,@duration)";

                var insertResult = ds.executeSQL(sq, insertParams);
                
                if (insertResult[0].Count() == null)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "UnSuccessful";
                }
                else
                {
                    resData.rData["rCode"] = 0;
                    resData.rData["rMessage"] = "Video insert Successful";

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