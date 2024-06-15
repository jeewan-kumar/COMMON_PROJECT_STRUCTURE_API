using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class skillup_Course
    {
        dbServices ds = new dbServices();
        public async Task<responseData> Course(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                MySqlParameter[] insertParams = new MySqlParameter[]
              {
                        new MySqlParameter("@title", req.addInfo["title"].ToString()),
                        new MySqlParameter("@description", req.addInfo["description"].ToString()),
                        new MySqlParameter("@details", req.addInfo["details"].ToString()),
                        new MySqlParameter("@popularity", req.addInfo["popularity"].ToString())  ,
                        new MySqlParameter("@enrolled", req.addInfo["enrolled"].ToString()),
              };
                var sq = @"insert into pc_student.Skillup_Course(title,description,details,popularity,enrolled) values(@title,@description,@details,@popularity,@enrolled)";

                var insertResult = ds.executeSQL(sq, insertParams);
                if (insertResult[0].Count() == null)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "UnSuccessful";
                }
                else
                {
                    resData.rData["rCode"] = 0;
                    resData.rData["rMessage"] = "Course insert Successful";

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return resData;
        }

        public async Task<responseData> getCourse(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                MySqlParameter[] Params = new MySqlParameter[]
              {
                        new MySqlParameter("@id", req.addInfo["id"]),
                       
              };
                var selectQuery = @"SELECT * FROM pc_student.Skillup_Course where id=@id";

                var selectResult = ds.executeSQL(selectQuery, Params);
                if (selectResult[0].Count() == 0)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "Course Not found";
                }
                else
                {
                    resData.rData["rCode"] = 0;
                    resData.rData["rMessage"] = "Course retrieved Successfully";
                    resData.rData["lessons"] = selectResult;
                }
            }
            catch (Exception ex)
            {
                resData.rData["rCode"] = 1;
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }
            return resData;
        }
        public async Task<responseData> UpdateCourse(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                MySqlParameter[] updateParams = new MySqlParameter[]
                {
                    new MySqlParameter("@id", req.addInfo["id"].ToString()),
                    new MySqlParameter("@title", req.addInfo["title"].ToString()),
                    new MySqlParameter("@description", req.addInfo["description"].ToString()),
                    new MySqlParameter("@details", req.addInfo["details"].ToString()),
                    new MySqlParameter("@popularity", req.addInfo["popularity"].ToString())  ,
                    new MySqlParameter("@enrolled", req.addInfo["enrolled"].ToString()),
                  
                };

                var updateQuery = @"UPDATE pc_student.Skillup_Course SET title = @title, description = @description, details = @details, popularity = @popularity, enrolled = @enrolled WHERE id = @id";

                var updateResult = ds.executeSQL(updateQuery, updateParams);
                if (updateResult[0].Count() == 0 && updateResult==null)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "UnSuccessful update Course";
                }
                else
                {
                    resData.rData["rCode"] = 0;
                    resData.rData["rMessage"] = "Course updated Successfully";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rCode"] = 1;
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> DeleteCourse(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                // Create MySQL parameters for the delete query
                MySqlParameter[] deleteParams = new MySqlParameter[]
                {
            new MySqlParameter("@id", req.addInfo["id"].ToString()),
              new MySqlParameter("@status",0)
                };

                // Define the delete query
                var query = @"DELETE FROM pc_student.Skillup_Course WHERE id = @id";
                //var query = @"UPDATE pc_student.Skillup_UserProfile SET status = @status WHERE id = @id";

                // Execute the delete query
                var deleteResult = ds.executeSQL(query, deleteParams);

                // Check the result of the delete operation
                if (deleteResult[0].Count() == 0 && deleteResult==null)
                {
                    resData.rData["rCode"] = 1; // Unsuccessful
                    resData.rData["rMessage"] = "Course Unsuccessful delete";
                }
                else
                {
                    resData.rData["rCode"] = 0; // Successful
                    resData.rData["rMessage"] = "Course delete Successful";
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the operation
                resData.rData["rCode"] = 1; // Indicate an error
                resData.rData["rMessage"] = "Error: " + ex.Message;
            }

            // Return the response data
            return resData;
        }

    }
}

