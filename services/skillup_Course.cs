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

        // public async Task<responseData> getCourse(requestData req)
        // {
        //     responseData resData = new responseData();
        //     try
        //     {
        //         var list = new Dictionary<string, object>();
        //         MySqlParameter[] insertParams = new MySqlParameter[]
        //       {
        //                 new MySqlParameter("@title", req.addInfo["title"].ToString()),
        //                 new MySqlParameter("@description", req.addInfo["description"].ToString()),
        //                 new MySqlParameter("@details", req.addInfo["details"].ToString()),
        //                 new MySqlParameter("@popularity", req.addInfo["popularity"].ToString())  ,
        //                 new MySqlParameter("@enrolled", req.addInfo["enrolled"].ToString()),
        //       };
        //         var sq = @"";

        //         var insertResult = ds.executeSQL(sq, insertParams);
        //         if (insertResult[0].Count() == null)
        //         {
        //             resData.rData["rCode"] = 1;
        //             resData.rData["rMessage"] = "UnSuccessful";
        //         }
        //         else
        //         {

        //             for (var i = 0; i < insertResult[0].Count(); i++)
        //             {
        //                 var myDict = new Dictionary<string, object>();
        //                 myDict.Add("id", insertResult[0][i][0].ToString());
        //                 myDict.Add("name", insertResult[0][i][1].ToString());
        //                 myDict.Add(list);
        //             }
        //             resData.rData["rData"] = list;
        //             resData.rData["rCode"] = 0;
        //             resData.rData["rMessage"] = "Course insert Successful";

        //         }



        //     }
        //     catch (Exception ex)
        //     {

        //         throw;
        //     }
        //     return resData;
        // }


    }
}