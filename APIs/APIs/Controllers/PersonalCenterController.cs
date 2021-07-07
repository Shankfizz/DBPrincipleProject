using APIs.DBUtility;
using APIs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;



namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class PersonalCenterController : Controller
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetAllInformation(PersonalCenter info)
        {
            DBHelper dBHelper = new DBHelper();
            if (info.UserType == "CUSTOMER")
            {
                UserInfo res = new UserInfo();
                string Sql = @"SELECT ID,USER_NAME,ADDRESS,DATE_OF_REG,PHONE_NUMBER
                                FROM CUSTOMER
                                WHERE ID ="+info.ID;
                DataTable table = dBHelper.ExecuteTable(Sql);
                DataRow row = table.Rows[0];
                res.ID = row["ID"].ToString();
                res.UserName = row["USER_NAME"].ToString();
                res.Address = row["ADDRESS"].ToString();
                res.RegDate = row["DATE_OF_REG"].ToString();
                res.PhoneNumber = row["PHONE_NUMBER"].ToString();
                res.Image = dBHelper.GetCustomerBlob(info.ID);

                //前端或许需要转码，将图片从Base64转成Image
                return Ok(dBHelper.ToJson(res));
            }
            else if (info.UserType == "SELLER")
            {
                SellerInfo res = new SellerInfo();
                string Sql = @"SELECT ID,USER_NAME,ADDRESS,DATE_OF_REG,PHONE_NUMBER
                                FROM SELLER
                                WHERE ID =" + info.ID;
                DataTable table = dBHelper.ExecuteTable(Sql);
                DataRow row = table.Rows[0];
                res.ID = row["ID"].ToString();
                res.SellerName = row["SELLER_NAME"].ToString();
                res.Address = row["ADDRESS"].ToString();
                res.RegDate = row["DATE_OF_REG"].ToString();
                res.Income = int.Parse(row["EARNING"].ToString());
                res.Image = dBHelper.GetCustomerBlob(info.ID);
                
                //前端或许需要转码，将图片从Base64转成Image
                return Ok(dBHelper.ToJson(res));
            }          
            return BadRequest("未知的错误发生了");
        }       
    }
}