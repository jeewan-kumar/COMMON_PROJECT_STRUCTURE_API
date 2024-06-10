
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class skillup_UserSignIn
    {
        dbServices ds = new dbServices();

        public async Task<responseData> UserSignIn(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                string query = "";
                MySqlParameter[] myParam;

                if (rData.addInfo.ContainsKey("Emaill"))
                {
                    query = @"SELECT * FROM pc_student.Skillup_UserData WHERE Password = @Password AND Emaill = @Emaill";
                    myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@Password", rData.addInfo["Password"]),
                        new MySqlParameter("@Emaill", rData.addInfo["Emaill"])
                    };
                }
                else if (rData.addInfo.ContainsKey("PhoneNumber"))
                {
                    query = @"SELECT * FROM pc_student.Skillup_UserData WHERE Password = @Password AND PhoneNumber = @PhoneNumber";
                    myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@Password", rData.addInfo["Password"]),
                        new MySqlParameter("@PhoneNumber", rData.addInfo["PhoneNumber"])
                    };
                }
                else
                {
                    resData.rData["rMessage"] = "Email or Phone Number is required";
                    return resData;
                }

                var dbData = ds.ExecuteSQLName(query, myParam);
                if (dbData[0].Count() > 0)
                {
                    resData.rData["rCode"] = 0;
                    resData.rData["rMessage"] = "Login Successful";
                    resData.rData["id"]=dbData[0][0]["id"];
                }
                else
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "Invalid Email/Phone Number and Password";
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
