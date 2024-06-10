// using System.Collections.Generic;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class skillup_UserProfile
//     {
//         dbServices ds = new dbServices();
//         public async Task<responseData> CreateProfile(requestData req)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 MySqlParameter[] insertParams = new MySqlParameter[]
//               {
//                         new MySqlParameter("@profile_picture", req.addInfo["profile_picture"].ToString()),
//                         new MySqlParameter("@first_name", req.addInfo["first_name"].ToString()),
//                         new MySqlParameter("@last_name", req.addInfo["last_name"].ToString()),
//                         new MySqlParameter("@date_of_birth", req.addInfo["date_of_birth"].ToString())  ,
//                         new MySqlParameter("@gender", req.addInfo["gender"].ToString()),
//                         new MySqlParameter("@bio", req.addInfo["bio"].ToString()),
//               };
//                 var sq = @"insert into pc_student.Skillup_UserData(profile_picture,first_name,last_name,date_of_birth,gender,bio) values(@profile_picture,@first_name,@last_name,@date_of_birth,@gender,@bio)";

//                 var insertResult = ds.executeSQL(sq, insertParams);
//                 if (insertResult[0].Count() == null)
//                 {
//                     resData.rData["rCode"] = 1;
//                     resData.rData["rMessage"] = "UnSuccessful";
//                 }
//                 else
//                 {
//                     resData.rData["rCode"] = 0;
//                     resData.rData["rMessage"] = "UserProfile Create Successful";

//                 }


//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rCode"] = 1;
//                 resData.rData["rMessage"] = "An error occurred: " + ex.Message;

//             }
//             return resData;
//         }

//         public async Task<responseData> ReadProfile(requestData req)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 MySqlParameter[] Params = new MySqlParameter[]
//               {
//                         new MySqlParameter("@id", req.addInfo["id"]),
                       
//               };
//                 var selectQuery = @"SELECT * FROM pc_student.Skillup_UserData where id=@id";

//                 var selectResult = ds.executeSQL(selectQuery, Params);
//                 if (selectResult[0].Count() == 0)
//                 {
//                     resData.rData["rCode"] = 1;
//                     resData.rData["rMessage"] = "No UserProfile found";
//                 }
//                 else
//                 {
//                     resData.rData["rCode"] = 0;
//                     resData.rData["rMessage"] = "Userprofile retrieved Successfully";
//                     resData.rData["lessons"] = selectResult;
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rCode"] = 1;
//                 resData.rData["rMessage"] = "An error occurred: " + ex.Message;
//             }
//             return resData;
//         }

//         public async Task<responseData> UpdateProfile(requestData req)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 MySqlParameter[] updateParams = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@id", req.addInfo["id"].ToString()),
//                     new MySqlParameter("@profile_picture", req.addInfo["profile_picture"].ToString()),
//                     new MySqlParameter("@first_name", req.addInfo["first_name"].ToString()),
//                     new MySqlParameter("@last_name", req.addInfo["last_name"].ToString()),
//                     new MySqlParameter("@date_of_birth", req.addInfo["date_of_birth"].ToString())  ,
//                     new MySqlParameter("@gender", req.addInfo["gender"].ToString()),
//                     new MySqlParameter("@bio", req.addInfo["bio"].ToString()),
//                 };

//                 var updateQuery = @"UPDATE pc_student.Skillup_UserData SET profile_picture = @profile_picture, first_name = @first_name, last_name = @last_name, date_of_birth = @date_of_birth, gender = @gender, bio = @bio  WHERE id = @id";

//                 var updateResult = ds.executeSQL(updateQuery, updateParams);
//                 if (updateResult[0].Count() == 0)
//                 {
//                     resData.rData["rCode"] = 1;
//                     resData.rData["rMessage"] = "UnSuccessful update profile";
//                 }
//                 else
//                 {
//                     resData.rData["rCode"] = 0;
//                     resData.rData["rMessage"] = "Profile updated Successfully";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rCode"] = 1;
//                 resData.rData["rMessage"] = "An error occurred: " + ex.Message;
//             }
//             return resData;
//         }

//         public async Task<responseData> DeleteProfile(requestData req)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 // Create MySQL parameters for the delete query
//                 MySqlParameter[] deleteParams = new MySqlParameter[]
//                 {
//             new MySqlParameter("@id", req.addInfo["id"].ToString()),
//               new MySqlParameter("@status",0)
//                 };

//                 // Define the delete query
//                 // var deleteQuery = @"DELETE FROM pc_student.Skillup_UserProfile WHERE id = @id";
//                 var query = @"UPDATE pc_student.Skillup_UserData SET status = @status WHERE id = @id";

//                 // Execute the delete query
//                 var deleteResult = ds.executeSQL(query, deleteParams);

//                 // Check the result of the delete operation
//                 if (deleteResult[0].Count() == 0)
//                 {
//                     resData.rData["rCode"] = 1; // Unsuccessful
//                     resData.rData["rMessage"] = "Profile Unsuccessful delete";
//                 }
//                 else
//                 {
//                     resData.rData["rCode"] = 0; // Successful
//                     resData.rData["rMessage"] = "profile delete Successful";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 // Handle any exceptions that occur during the operation
//                 resData.rData["rCode"] = 1; // Indicate an error
//                 resData.rData["rMessage"] = "Error: " + ex.Message;
//             }

//             // Return the response data
//             return resData;
//         }

//     }
// }


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class skillup_UserProfile
    {
        dbServices ds = new dbServices();

        public async Task<responseData> CreateProfile(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                // Check if the ID exists
                MySqlParameter[] checkIdParams = new MySqlParameter[]
                {
                    new MySqlParameter("@id", req.addInfo["id"].ToString())
                };

                var checkIdQuery = @"SELECT COUNT(*) FROM pc_student.Skillup_UserData WHERE id = @id";
                var checkIdResult = ds.executeSQL(checkIdQuery, checkIdParams);

                // if (checkIdResult[0][0]["COUNT(*)"] == null || Convert.ToInt32(checkIdResult[0][0]["COUNT(*)"]) == 0)
                // {
                //     resData.rData["rCode"] = 1;
                //     resData.rData["rMessage"] = "Invalid ID";
                //     return resData;
                // }

                // If ID exists, perform the insert
                MySqlParameter[] insertParams = new MySqlParameter[]
                {
                    new MySqlParameter("@id", req.addInfo["id"].ToString()),
                    new MySqlParameter("@profile_picture", req.addInfo["profile_picture"].ToString()),
                    new MySqlParameter("@first_name", req.addInfo["first_name"].ToString()),
                    new MySqlParameter("@last_name", req.addInfo["last_name"].ToString()),
                    new MySqlParameter("@date_of_birth", req.addInfo["date_of_birth"].ToString()),
                    new MySqlParameter("@gender", req.addInfo["gender"].ToString()),
                    new MySqlParameter("@bio", req.addInfo["bio"].ToString())
                };

                var insertQuery =$"INSERT INTO pc_student.Skillup_UserData (id, profile_picture, first_name, last_name, date_of_birth, gender, bio) VALUES ({id}, @profile_picture, @first_name, @last_name, @date_of_birth, @gender, @bio)ON DUPLICATE KEY UPDATE profile_picture = @profile_picture, first_name = @first_name, last_name = @last_name,  date_of_birth = @date_of_birth, gender = @gender, bio = @bio";

                var insertResult = ds.executeSQL(insertQuery, insertParams);
                if (insertResult[0].Count() == 0)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "Unsuccessful";
                }
                else
                {
                    resData.rData["rCode"] = 0;
                    resData.rData["rMessage"] = "UserProfile Create/Update Successful";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rCode"] = 1;
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }

            return resData;
        }

        public async Task<responseData> ReadProfile(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                MySqlParameter[] Params = new MySqlParameter[]
                {
                    new MySqlParameter("@id", req.addInfo["id"].ToString())
                };

                var selectQuery = @"SELECT * FROM pc_student.Skillup_UserData WHERE id = @id";

                var selectResult = ds.executeSQL(selectQuery, Params);
                if (selectResult[0].Count() == 0)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "No UserProfile found";
                }
                else
                {
                    resData.rData["rCode"] = 0;
                    resData.rData["rMessage"] = "UserProfile retrieved Successfully";
                    resData.rData["profile"] = selectResult[0];
                }
            }
            catch (Exception ex)
            {
                resData.rData["rCode"] = 1;
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }

            return resData;
        }

        public async Task<responseData> UpdateProfile(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                MySqlParameter[] updateParams = new MySqlParameter[]
                {
                    new MySqlParameter("@id", req.addInfo["id"].ToString()),
                    new MySqlParameter("@profile_picture", req.addInfo["profile_picture"].ToString()),
                    new MySqlParameter("@first_name", req.addInfo["first_name"].ToString()),
                    new MySqlParameter("@last_name", req.addInfo["last_name"].ToString()),
                    new MySqlParameter("@date_of_birth", req.addInfo["date_of_birth"].ToString()),
                    new MySqlParameter("@gender", req.addInfo["gender"].ToString()),
                    new MySqlParameter("@bio", req.addInfo["bio"].ToString())
                };

                var updateQuery = @"UPDATE pc_student.Skillup_UserData 
                                    SET profile_picture = @profile_picture, first_name = @first_name, last_name = @last_name, 
                                        date_of_birth = @date_of_birth, gender = @gender, bio = @bio 
                                    WHERE id = @id";

                var updateResult = ds.executeSQL(updateQuery, updateParams);
                if (updateResult[0].Count() == 0)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "Unsuccessful update profile";
                }
                else
                {
                    resData.rData["rCode"] = 0;
                    resData.rData["rMessage"] = "Profile updated Successfully";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rCode"] = 1;
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }

            return resData;
        }

        public async Task<responseData> DeleteProfile(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                MySqlParameter[] deleteParams = new MySqlParameter[]
                {
                    new MySqlParameter("@id", req.addInfo["id"].ToString()),
                    new MySqlParameter("@status", 0)
                };

                var updateQuery = @"UPDATE pc_student.Skillup_UserData SET status = @status WHERE id = @id";

                var deleteResult = ds.executeSQL(updateQuery, deleteParams);
                if (deleteResult[0].Count() == 0)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "Profile Unsuccessful delete";
                }
                else
                {
                    resData.rData["rCode"] = 0;
                    resData.rData["rMessage"] = "Profile delete Successful";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rCode"] = 1;
                resData.rData["rMessage"] = "Error: " + ex.Message;
            }

            return resData;
        }
    }
}
