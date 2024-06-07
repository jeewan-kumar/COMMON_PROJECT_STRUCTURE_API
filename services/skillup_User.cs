using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class skillup_User
    {
        dbServices ds = new dbServices();
        public async Task<responseData> User(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                MySqlParameter[] myParam = new MySqlParameter[]
              {
                new MySqlParameter("@Emaill",req.addInfo["Emaill"].ToString()),
                new MySqlParameter("@PhoneNumber",req.addInfo["PhoneNumber"].ToString()),
               
              };
                var query = @"select * from pc_student.Skillup_User where Emaill=@Emaill or PhoneNumber=@PhoneNumber ";

                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > 0)
                {
                    resData.rData["rMessage"] = "Duplicate Credentials";
                }
                else
                {
                    MySqlParameter[] insertParams = new MySqlParameter[]
                  {
                        //  new MySqlParameter("@Name", req.addInfo["Name"].ToString()),
                        new MySqlParameter("@PhoneNumber", req.addInfo["PhoneNumber"].ToString()),
                        new MySqlParameter("@Password", req.addInfo["Password"].ToString())  ,
                           new MySqlParameter("@Emaill", req.addInfo["Emaill"].ToString())
                  };
                    var sq = @"insert into pc_student.Skillup_User(Emaill,PhoneNumber,Password ) values(@Emaill,@PhoneNumber,@Password)";

                    var insertResult = ds.executeSQL(sq, insertParams);
                    if (insertResult[0].Count() == null)
                    {
                        resData.rData["rCode"] = 1;
                        resData.rData["rMessage"] = "UnSuccessful";
                    }
                    else
                    {
                        resData.rData["rCode"] = 0;
                        resData.rData["rMessage"] = "Registration Successful";

                    }

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