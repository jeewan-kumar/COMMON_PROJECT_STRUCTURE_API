using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class skillup_Lesson
    {
        dbServices ds = new dbServices();
        public async Task<responseData> Lesson(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                MySqlParameter[] insertParams = new MySqlParameter[]
              {
                        new MySqlParameter("@course_id", req.addInfo["course_id"].ToString()),
                        new MySqlParameter("@title", req.addInfo["title"].ToString()),
                        new MySqlParameter("@description", req.addInfo["description"].ToString()),
              };
                var sq = @"insert into pc_student.Skillup_Lesson(course_id,title,description) values(@course_id,@title,@description)";

                var insertResult = ds.executeSQL(sq, insertParams);
                if (insertResult[0].Count() == null)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "UnSuccessful";
                }
                else
                {
                    resData.rData["rCode"] = 0;
                    resData.rData["rMessage"] = "Lesson insert Successful";

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